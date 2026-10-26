using CookieClicker.Model.Boutiques;
using CookieClicker.Model.Games;
using CookieClicker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookieClicker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PowerUpsController : ControllerBase
{
    private readonly PowerUpService _powerUpService;
    private readonly GameService _gameService;

    public PowerUpsController(PowerUpService powerUpService, GameService gameService)
    {
        _powerUpService = powerUpService;
        _gameService = gameService;
    }

    [HttpGet]
    public List<ArticlePowerUp> Get()
    {
        return _powerUpService.GetCatalogue();
    }

    [HttpPost("{powerUpId}")]
    public ActionResult<Game> Acheter(string powerUpId)
    {
        switch (_powerUpService.Acheter(powerUpId))
        {
            case ResultatAchat.PowerUpInconnu:
                return NotFound();
            case ResultatAchat.DejaPossede:
                return Conflict("Power-up already bought");
            case ResultatAchat.FondsInsuffisants:
                return BadRequest("Not enough cookies");
        }

        return _gameService.GetEtat();
    }
}
