using AcademiaNet.Frontend.Repositories;
using AcademiaNet.Shared.Resources;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.Metrics;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.DTOs;

namespace AcademiaNet.Frontend.Pages.AcademicPrograms;

public partial class AcademicProgramForm
{
    private EditContext editContext = null!;

    protected override void OnInitialized()
    {
        editContext = new(AcademicProgramDTO);
    }

    [EditorRequired, Parameter] public AcademicProgramDTO AcademicProgramDTO { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

    public bool FormPostedSuccessfully { get; set; } = false;

    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;

    private List<Institution>? institutions;
    private List<Category>? categories;
    private string? imageUrl;

    protected override async Task OnInitializedAsync()
    {
        await LoadInstitutionsAsync();
        await LoadInstcategoriesAsync();   // Asegúrate de usar await aquí
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        //if (!string.IsNullOrEmpty(AcademicProgramDTO.Image))
        //{
        //    imageUrl = AcademicProgramDTO.Image;
        //    AcademicProgramDTO.Image = null;
        //}
    }

    private async Task LoadInstitutionsAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Institution>>("/api/institutions/combo");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }

        institutions = responseHttp.Response;
    }

    private async Task LoadInstcategoriesAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Category>>("/api/academicPrograms/comboCategories");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            return;
        }

        categories = responseHttp.Response;
    }

    //private void ImageSelected(string imagenBase64)
    //{
    //    AcademicProgramDTO.Image = imagenBase64;
    //    imageUrl = null;
    //}

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
            ShowCancelButton = true,
            CancelButtonText = Localizer["Cancel"],
        });

        var confirm = !string.IsNullOrEmpty(result.Value);
        if (confirm)
        {
            return;
        }

        context.PreventNavigation();
    }
}