using AcademiaNet.Frontend.Repositories;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Resources;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace AcademiaNet.Frontend.Pages.AcademicPrograms;

public partial class AcademicProgramEdit
{
    private AcademicProgramDTO? academicProgramDTO;
    private AcademicProgramForm? academicProgramForm;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
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
                await SweetAlertService.FireAsync(Localizer["Error"], messageError, SweetAlertIcon.Error);
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
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/academicPrograms/full", academicProgramDTO);

        if (responseHttp.Error)
        {
            var mensajeError = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync(Localizer["Error"], Localizer[mensajeError!], SweetAlertIcon.Error);
            return;
        }

        Return();
        var toast = SweetAlertService.Mixin(new SweetAlertOptions
        {
            Toast = true,
            Position = SweetAlertPosition.BottomEnd,
            ShowConfirmButton = true,
            Timer = 3000
        });
        toast.FireAsync(icon: SweetAlertIcon.Success, message: Localizer["RecordSavedOk"]);
    }

    private void Return()
    {
        academicProgramForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/academicPrograms");
    }
}