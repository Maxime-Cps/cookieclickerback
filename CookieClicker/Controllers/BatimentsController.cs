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

    [HttpGet("{playerId:guid}")]
    public ActionResult<List<BatimentPossede>> Get(Guid playerId)
    {
        var batiments = _batimentService.GetPossedes(playerId);
        if (batiments is null)
        {
            return NotFound();
        }

        return batiments;
    }
}
