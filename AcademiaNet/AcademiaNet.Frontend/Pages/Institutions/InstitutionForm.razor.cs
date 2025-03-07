using AcademiaNet.Shared.Resources;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.Metrics;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Frontend.Repositories;

namespace AcademiaNet.Frontend.Pages.Institutions;

public partial class InstitutionForm
{
    private EditContext editContext = null!;

    private List<Location>? locations;

    protected override void OnInitialized()
    {
        editContext = new(InstitutionDTO);
    }

    //[EditorRequired, Parameter] public Institution Institution { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }

    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
    [EditorRequired, Parameter] public InstitutionDTO InstitutionDTO { get; set; } = null!;

    public bool FormPostedSuccessfully { get; set; } = false;

    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Inject] private IRepository Repository { get; set; } = null!;

    private string? imageUrl;

    protected override async Task OnInitializedAsync()
    {
        await LoadInstitutionsAsync();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (!string.IsNullOrEmpty(InstitutionDTO.Photo))
        {
            imageUrl = InstitutionDTO.Photo;
            InstitutionDTO.Photo = null;
        }
    }

    /// <summary>
    /// Trae la lista de las ubicaciones
    /// </summary>
    /// <returns></returns>
    private async Task LoadInstitutionsAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Location>>("/api/institutions/comboLocations");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }

        locations = responseHttp.Response;
    }

    private void ImageSelected(string imagenBase64)
    {
        InstitutionDTO.Photo = imagenBase64;
        imageUrl = null;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private async Task OnBeforeInternalNavigation(LocationChangingContext context)
    {
        var formWasEdited = editContext.IsModified();

        if (!formWasEdited || FormPostedSuccessfully)
        {
            return;
        }

        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = Localizer["Confirmation"],
            Text = Localizer["LeaveAndLoseChanges"],
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true
        });

        var confirm = !string.IsNullOrEmpty(result.Value);
        if (confirm)
        {
            return;
        }

        context.PreventNavigation();
    }
}