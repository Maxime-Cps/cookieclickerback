using CookieClicker.Model.Boutiques;
using CookieClicker.Model.Players;
using CookieClicker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookieClicker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PowerUpsController : ControllerBase
{
    private readonly PowerUpService _powerUpService;
    private readonly PlayerService _playerService;

    public PowerUpsController(PowerUpService powerUpService, PlayerService playerService)
    {
        _powerUpService = powerUpService;
        _playerService = playerService;
    }

    [HttpGet]
    public List<ArticlePowerUp> GetAll()
    {
        return _powerUpService.GetCatalogue();
    }

    [HttpGet("{playerId:guid}")]
    public ActionResult<List<ArticlePowerUp>> Get(Guid playerId)
    {
        var catalogue = _powerUpService.GetCatalogue(playerId);
        if (catalogue is null)
        {
            return NotFound();
        }

        return catalogue;
    }

    [HttpPost("{playerId:guid}/{powerUpId}")]
    public ActionResult<Player> Acheter(Guid playerId, string powerUpId)
    {
        switch (_powerUpService.Acheter(playerId, powerUpId))
        {
            case ResultatAchat.JoueurInconnu:
            case ResultatAchat.PowerUpInconnu:
                return NotFound();
            case ResultatAchat.DejaPossede:
                return Conflict("Power-up already bought");
            case ResultatAchat.FondsInsuffisants:
                return BadRequest("Not enough cookies");
        }

        return _playerService.Get(playerId)!;
    }
}
