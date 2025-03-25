using AcademiaNet.Frontend.Repositories;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Resources;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Diagnostics.Metrics;

namespace AcademiaNet.Frontend.Pages.Institutions;

public partial class InstitutionEdit
{
    //private Institution? institution;
    //private InstitutionForm? institutionForm;
    //private InstitutionDTO? institutionDTO;

    //[Inject] private NavigationManager NavigationManager { get; set; } = null!;
    //[Inject] private IRepository Repository { get; set; } = null!;
    //[Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    //[Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    //[Parameter] public int Id { get; set; }

    //protected override async Task OnInitializedAsync()
    //{
    //    var responseHttp = await Repository.GetAsync<Institution>($"api/institutions/{Id}");

    //    if (responseHttp.Error)
    //    {
    //        if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
    //        {
    //            NavigationManager.NavigateTo("institutions");
    //        }
    //        else
    //        {
    //            var messageError = await responseHttp.GetErrorMessageAsync();
    //            await SweetAlertService.FireAsync(Localizer["Error"], Localizer[messageError!], SweetAlertIcon.Error);
    //        }
    //    }
    //    else
    //    {
    //        institution = responseHttp.Response;

    //        institutionDTO = new InstitutionDTO()
    //        {
    //            InstitutionID = institution!.InstitutionID,
    //            Name = institution!.Name,
    //            LocationID = institution!.LocationID,
    //            Description = institution!.Description,
    //            Photo = institution!.Photo
    //        };
    //    }
    //}

    //private async Task EditAsync()
    //{
    //    var responseHttp = await Repository.PutAsync("api/institutions/full", institutionDTO);

    //    if (responseHttp.Error)
    //    {
    //        var mensajeError = await responseHttp.GetErrorMessageAsync();
    //        await SweetAlertService.FireAsync(Localizer["Error"], mensajeError, SweetAlertIcon.Error);
    //        return;
    //    }

    //    Return();
    //    var toast = SweetAlertService.Mixin(new SweetAlertOptions
    //    {
    //        Toast = true,
    //        Position = SweetAlertPosition.BottomEnd,
    //        ShowConfirmButton = true,
    //        Timer = 3000
    //    });
    //    await toast.FireAsync(icon: SweetAlertIcon.Success, message: Localizer["RecordSavedOk"]);
    //}

    //private void Return()
    //{
    //    institutionForm!.FormPostedSuccessfully = true;
    //    NavigationManager.NavigateTo("institutions");
    //}

    private Institution? institution;
    private InstitutionForm? institutionForm;
    private InstitutionDTO? institutionDTO;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<Institution>($"api/institutions/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("institutions");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError!, Severity.Error);
            }
        }
        else
        {
            var institution = responseHttp.Response;
            institutionDTO = new InstitutionDTO()
            {
                InstitutionID = institution!.InstitutionID,
                Name = institution!.Name,
                LocationID = institution!.LocationID,
                Description = institution!.Description,
                Photo = institution!.Photo
            };
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/institutions/full", institutionDTO);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            //Snackbar.Add(messageError!, Severity.Error);
            Snackbar.Add(Localizer[messageError!], Severity.Error);
            return;
        }

        Return();
        Snackbar.Add(Localizer["RecordSavedOk"], Severity.Success);
    }

    private void Return()
    {
        institutionForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("institutions");
    }
}