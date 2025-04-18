using AcademiaNet.Frontend.Repositories;
using AcademiaNet.Frontend.Services;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Diagnostics.Metrics;
using System.Net;

namespace AcademiaNet.Frontend.Pages.Auth;

[Authorize]
public partial class EditUser
{
    private User? user;
    private List<Institution>? institutions;
    private bool loading = true;
    private string? imageUrl;

    private Institution selectedCountry = new();

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ILoginService LoginService { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await LoadUserAsyc();
        await LoadInstitutionsAsync();

        selectedCountry = user!.Institution!;

        if (!string.IsNullOrEmpty(user!.Photo))
        {
            imageUrl = user.Photo;
            user.Photo = null;
        }
    }

    private void ShowModal()
    {
        var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
        DialogService.Show<ChangePassword>(Localizer["ChangePassword"], closeOnEscapeKey);
    }

    private async Task LoadUserAsyc()
    {
        var responseHttp = await Repository.GetAsync<User>($"/api/accounts");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/");
                return;
            }
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError!, Severity.Error);
            return;
        }
        user = responseHttp.Response;
        loading = false;
    }

    private void ImageSelected(string imagenBase64)
    {
        user!.Photo = imagenBase64;
        imageUrl = null;
    }

    private async Task LoadInstitutionsAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Institution>>("/api/institutions/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[message!], Severity.Error);
            return;
        }
        institutions = responseHttp.Response;
    }

    private void CountryChanged(Institution institution)
    {
        selectedCountry = institution;
    }

    private async Task<IEnumerable<Institution>> SearchCountries(string searchText, CancellationToken cancellationToken)
    {
        await Task.Delay(5);
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return institutions!;
        }

        return institutions!
            .Where(c => c.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
            .ToList();
    }

    private async Task SaveUserAsync()
    {
        var responseHttp = await Repository.PutAsync<User, TokenDTO>("/api/accounts", user!);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[message!], Severity.Error);
            return;
        }

        await LoginService.LoginAsync(responseHttp.Response!.Token);
        Snackbar.Add(Localizer["RecordSavedOk"], Severity.Success);
        NavigationManager.NavigateTo("/");
    }

    private void ReturnAction()
    {
        NavigationManager.NavigateTo("/");
    }
}