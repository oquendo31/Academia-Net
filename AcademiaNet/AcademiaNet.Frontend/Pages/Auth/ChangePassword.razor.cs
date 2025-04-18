using AcademiaNet.Frontend.Repositories;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace AcademiaNet.Frontend.Pages.Auth;

public partial class ChangePassword
{
    private ChangePasswordDTO changePasswordDTO = new();
    private bool loading;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    /// <summary>
    /// Change Password Async
    /// </summary>
    /// <returns></returns>
    private async Task ChangePasswordAsync()
    {
        loading = true;
        var responseHttp = await Repository.PostAsync("/api/accounts/changePassword", changePasswordDTO);
        loading = false;
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[message!], Severity.Error);
            return;
        }

        MudDialog.Cancel();
        NavigationManager.NavigateTo("/EditUser");
        Snackbar.Add(Localizer["PasswordChangedSuccessfully"], Severity.Success);
    }

    private void ReturnAction()
    {
        MudDialog.Cancel();
        NavigationManager.NavigateTo("/EditUser");
    }
}