using CookieClicker.Model.Boutiques;
using CookieClicker.Model.Players;
using CookieClicker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookieClicker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoutiquesController : ControllerBase
{
    private readonly BoutiqueService _boutiqueService;
    private readonly PlayerService _playerService;

    public BoutiquesController(BoutiqueService boutiqueService, PlayerService playerService)
    {
        _boutiqueService = boutiqueService;
        _playerService = playerService;
    }

    [HttpGet]
    public List<Article> GetAll([FromQuery] int quantite = 1)
    {
        return _boutiqueService.GetCatalogue(quantite);
    }

    [HttpGet("{playerId:guid}")]
    public ActionResult<List<Article>> Get(Guid playerId, [FromQuery] int quantite = 1)
    {
        var catalogue = _boutiqueService.GetCatalogue(playerId, quantite);
        if (catalogue is null)
        {
            return NotFound();
        }

        return catalogue;
    }

    [HttpPost("{playerId:guid}/batiments/{type}")]
    public ActionResult<Player> Acheter(Guid playerId, TypeBatiment type, [FromQuery] int quantite = 1)
    {
        return Repondre(playerId, type, _boutiqueService.Acheter(playerId, type, quantite));
    }

    [HttpPost("{playerId:guid}/batiments/{type}/vente")]
    public ActionResult<Player> Vendre(Guid playerId, TypeBatiment type, [FromQuery] int quantite = 1)
    {
        return Repondre(playerId, type, _boutiqueService.Vendre(playerId, type, quantite));
    }

    private ActionResult<Player> Repondre(Guid playerId, TypeBatiment type, ResultatAchat resultat)
    {
        switch (resultat)
        {
            case ResultatAchat.JoueurInconnu:
                return NotFound();
            case ResultatAchat.BatimentInconnu:
                return BadRequest($"Unknown building: {type}");
            case ResultatAchat.QuantiteInvalide:
                return BadRequest("Quantity must be 1, 10 or 100");
            case ResultatAchat.FondsInsuffisants:
                return BadRequest("Not enough cookies");
            case ResultatAchat.PasAssezDeBatiments:
                return BadRequest("Not enough buildings to sell");
        }

        return _playerService.Get(playerId)!;
    }
}
