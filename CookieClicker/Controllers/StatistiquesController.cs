using CookieClicker.Model.Statistiques;
using CookieClicker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookieClicker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatistiquesController : ControllerBase
{
    private readonly StatistiqueService _statistiqueService;

    public StatistiquesController(StatistiqueService statistiqueService)
    {
        _statistiqueService = statistiqueService;
    }

    [HttpGet]
    public ResumeStatistiques Get()
    {
        return _statistiqueService.Get();
    }
}
