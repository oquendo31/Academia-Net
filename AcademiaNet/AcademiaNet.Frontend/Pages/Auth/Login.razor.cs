using AcademiaNet.Frontend.Repositories;
using AcademiaNet.Frontend.Services;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace AcademiaNet.Frontend.Pages.Auth;

public partial class Login
{
    private LoginDTO loginDTO = new();
    private bool wasClose;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ILoginService LoginService { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    private void CloseModal()
    {
        wasClose = true;
        MudDialog.Cancel();
    }

    private async Task LoginAsync()
    {
        if (wasClose)
        {
            NavigationManager.NavigateTo("/");
            return;
        }

        var responseHttp = await Repository.PostAsync<LoginDTO, TokenDTO>("/api/accounts/Login", loginDTO);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[message!], Severity.Error);
            return;
        }

        await LoginService.LoginAsync(responseHttp.Response!.Token);
        NavigationManager.NavigateTo("/");
    }
}