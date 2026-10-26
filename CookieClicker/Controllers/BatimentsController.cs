using CookieClicker.Model.Batiments;
using CookieClicker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookieClicker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BatimentsController : ControllerBase
{
    private readonly BatimentService _batimentService;

    public BatimentsController(BatimentService batimentService)
    {
        _batimentService = batimentService;
    }

    [HttpGet]
    public List<BatimentPossede> Get()
    {
        return _batimentService.GetPossedes();
    }
}
