using AcademiaNet.Frontend.Repositories;
using AcademiaNet.Frontend.Shared;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Resources;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Diagnostics.Metrics;
using System.Net;

namespace AcademiaNet.Frontend.Pages.Institutions;

[Authorize(Roles = "Admin")]
public partial class InstitutionsIndex
{
    private List<Institution>? Institutions { get; set; }
    private MudTable<Institution> table = new();
    private readonly int[] pageSizeOptions = { 9, 25, 50, int.MaxValue };
    private int totalRecords = 0;
    private bool loading;
    private const string baseUrl = "api/institutions";
    private string infoFormat = "{first_item}-{last_item} => {all_items}";

    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadTotalRecordsAsync();
    }

    private async Task LoadTotalRecordsAsync()
    {
        loading = true;
        var url = $"{baseUrl}/totalRecordsPaginated";

        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"?filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<int>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[message!], Severity.Error);
            return;
        }

        totalRecords = responseHttp.Response;
        loading = false;
    }

    private async Task<TableData<Institution>> LoadListAsync(TableState state, CancellationToken cancellationToken)
    {
        int page = state.Page + 1;
        int pageSize = state.PageSize;
        var url = $"{baseUrl}/paginated/?page={page}&recordsnumber={pageSize}";

        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"&filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<List<Institution>>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[message!], Severity.Error);
            return new TableData<Institution> { Items = [], TotalItems = 0 };
        }
        if (responseHttp.Response == null)
        {
            return new TableData<Institution> { Items = [], TotalItems = 0 };
        }
        return new TableData<Institution>
        {
            Items = responseHttp.Response,
            TotalItems = totalRecords
        };
    }

    private async Task SetFilterValue(string value)
    {
        Filter = value;
        await LoadTotalRecordsAsync();
        await table.ReloadServerData();
    }

    private async Task ShowModalAsync(int id = 0, bool isEdit = false)
    {
        var options = new DialogOptions() { CloseOnEscapeKey = true, CloseButton = true };
        IDialogReference? dialog;
        if (isEdit)
        {
            var parameters = new DialogParameters
                {
                    { "Id", id }
                };
            dialog = DialogService.Show<InstitutionEdit>($"{Localizer["Edit"]} {Localizer["Institution"]}", parameters, options);
        }
        else
        {
            dialog = DialogService.Show<InstitutionCreate>($"{Localizer["New"]} {Localizer["Institution"]}", options);
        }

        var result = await dialog.Result;
        if (result!.Canceled)
        {
            await LoadTotalRecordsAsync();
            await table.ReloadServerData();
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="institution"></param>
    /// <returns></returns>
    private async Task DeleteAsync(Institution institution)
    {
        var parameters = new DialogParameters
            {
                { "Message", string.Format(Localizer["DeleteConfirm"], Localizer["Institution"], institution.Name) }
            };
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, CloseOnEscapeKey = true };
        var dialog = DialogService.Show<ConfirmDialog>(Localizer["Confirmation"], parameters, options);
        var result = await dialog.Result;
        if (result!.Canceled)
        {
            return;
        }

        var responseHttp = await Repository.DeleteAsync($"{baseUrl}/{institution.InstitutionID}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/institutions");
            }
            else
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(Localizer[message!], Severity.Error);
            }
            return;
        }
        await LoadTotalRecordsAsync();
        await table.ReloadServerData();
        Snackbar.Add(Localizer["RecordDeletedOk"], Severity.Success);
    }

    //[Inject] private IRepository Repository { get; set; } = null!;
    //[Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    //[Inject] private NavigationManager NavigationManager { get; set; } = null!;
    //[Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    //private List<Institution>? Institutions { get; set; }

    //protected override async Task OnInitializedAsync()
    //{
    //    await LoadAsync();
    //}

    //private async Task LoadAsync()
    //{
    //    var responseHppt = await Repository.GetAsync<List<Institution>>("api/institutions");
    //    if (responseHppt.Error)
    //    {
    //        var message = await responseHppt.GetErrorMessageAsync();
    //        await SweetAlertService.FireAsync(Localizer["Error"], message, SweetAlertIcon.Error);
    //        return;
    //    }
    //    Institutions = responseHppt.Response!;
    //}

    //private async Task DeleteAsync(Institution institution)
    //{
    //    var result = await SweetAlertService.FireAsync(new SweetAlertOptions
    //    {
    //        Title = Localizer["Confirmation"],
    //        Text = string.Format(Localizer["DeleteConfirm"], Localizer["Institution"], institution.Name),
    //        Icon = SweetAlertIcon.Question,
    //        ShowCancelButton = true,
    //        CancelButtonText = Localizer["Cancel"]
    //    });

    //    var confirm = string.IsNullOrEmpty(result.Value);

    //    if (confirm)
    //    {
    //        return;
    //    }

    //    var responseHttp = await Repository.DeleteAsync($"api/institutions/{institution.InstitutionID}");
    //    if (responseHttp.Error)
    //    {
    //        if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
    //        {
    //            NavigationManager.NavigateTo("/");
    //        }
    //        else
    //        {
    //            var messageError = await responseHttp.GetErrorMessageAsync();
    //            await SweetAlertService.FireAsync(Localizer["Error"], Localizer[messageError!], SweetAlertIcon.Error);
    //        }
    //        return;
    //    }

    //    await LoadAsync();
    //    var toast = SweetAlertService.Mixin(new SweetAlertOptions
    //    {
    //        Toast = true,
    //        Position = SweetAlertPosition.BottomEnd,
    //        ShowConfirmButton = true,
    //        Timer = 3000,
    //        ConfirmButtonText = Localizer["Yes"]
    //    });
    //    toast.FireAsync(icon: SweetAlertIcon.Success, message: Localizer["RecordDeletedOk"]);
    //}
}