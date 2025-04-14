using AcademiaNet.Frontend.Services;
using AcademiaNet.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace AcademiaNet.Frontend.Pages.Auth;

public partial class Logout
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ILoginService LoginService { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    private async Task LogoutActionAsync()
    {
        await LoginService.LogoutAsync();
        NavigationManager.NavigateTo("/");
        CancelAction();
    }

    private void CancelAction()
    {
        MudDialog.Cancel();
    }
}