using AcademiaNet.Frontend.Repositories;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Resources;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace AcademiaNet.Frontend.Pages.AcademicPrograms;

public partial class AcademicProgramCreate
{
    private AcademicProgramForm? academicProgramForm;

    private AcademicProgramDTO academicProgramDTO = new();
    //private AcademicProgramDTO? academicProgramDTO;

    //private AcademicProgramForm? academicProgramForm;

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    //private async Task CreateAsync()
    //{
    //    var responseHttp = await Repository.PostAsync("/api/academicPrograms/full", academicProgramDTO);
    //    if (responseHttp.Error)
    //    {
    //        var message = await responseHttp.GetErrorMessageAsync();
    //        Snackbar.Add(message!, Severity.Error);
    //        return;
    //    }

    //    Return();
    //    Snackbar.Add(Localizer["RecordSavedOk"], Severity.Success);
    //}

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("api/academicPrograms/full", academicProgramDTO);

        if (responseHttp.Error)
        {
            var mensajeError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(mensajeError!, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add(Localizer["RecordSavedOk"], Severity.Success);
    }

    private void Return()
    {
        academicProgramForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/academicPrograms");
    }
}