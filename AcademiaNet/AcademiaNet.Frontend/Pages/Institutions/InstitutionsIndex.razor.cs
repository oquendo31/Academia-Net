using AcademiaNet.Frontend.Repositories;
using AcademiaNet.Shared.Entites;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Metrics;

namespace AcademiaNet.Frontend.Pages.Institutions;

public partial class InstitutionsIndex
{
    [Inject] private IRepository Repository { get; set; } = null!;
    private List<Institution>? Institutions { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHppt = await Repository.GetAsync<List<Institution>>("api/institutions");
        Institutions = responseHppt.Response!;
    }
}