using AcademiaNet.Frontend.Repositories;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Resources;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Diagnostics.Metrics;

namespace AcademiaNet.Frontend.Pages.Institutions;

public partial class InstitutionCreate
{
    private InstitutionForm? institutionForm;
    private InstitutionDTO institutionDTO = new();

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/institutions/full", institutionDTO);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(Localizer[message!], Severity.Error);
            return;
        }

        Return();
        Snackbar.Add(Localizer["RecordCreatedOk"], Severity.Success);
    }

    private void Return()
    {
        institutionForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/institutions");
    }
}