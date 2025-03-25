using AcademiaNet.Frontend.Repositories;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Resources;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using static MudBlazor.Colors;

namespace AcademiaNet.Frontend.Pages.AcademicPrograms;

public partial class AcademicProgramEdit
{
    private AcademicProgramDTO? academicProgramDTO;
    private AcademicProgramForm? academicProgramForm;

    //private Category selectedCategory = new();
    //private Institution selectedInstitution = new();

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<AcademicProgram>($"api/academicPrograms/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("academicPrograms");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError!, Severity.Error);
            }
        }
        else
        {
            var academicProgram = responseHttp.Response;
            academicProgramDTO = new AcademicProgramDTO()
            {
                AcademicProgramID = academicProgram!.AcademicProgramID,
                Name = academicProgram!.Name,
                CategoryID = academicProgram.CategoryID,
                InstitutionID = academicProgram.InstitutionID
            };
            //selectedCategory = academicProgram.Category!;
            //selectedInstitution = academicProgram.Institution!;
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/academicPrograms/full", academicProgramDTO);

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