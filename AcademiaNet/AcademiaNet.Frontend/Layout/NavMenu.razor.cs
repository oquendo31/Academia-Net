using AcademiaNet.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace AcademiaNet.Frontend.Layout;

public partial class NavMenu
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    private bool collapseNavMenu = true;

    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }
}