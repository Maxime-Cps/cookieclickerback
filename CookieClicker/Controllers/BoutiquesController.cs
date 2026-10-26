using CookieClicker.Model.Boutiques;
using CookieClicker.Model.Games;
using CookieClicker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookieClicker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoutiquesController : ControllerBase
{
    private readonly BoutiqueService _boutiqueService;
    private readonly GameService _gameService;

    public BoutiquesController(BoutiqueService boutiqueService, GameService gameService)
    {
        _boutiqueService = boutiqueService;
        _gameService = gameService;
    }

    [HttpGet]
    public List<Article> Get([FromQuery] int quantite = 1)
    {
        return _boutiqueService.GetCatalogue(quantite);
    }

    [HttpPost("batiments/{type}")]
    public ActionResult<Game> Acheter(TypeBatiment type, [FromQuery] int quantite = 1)
    {
        return Repondre(type, _boutiqueService.Acheter(type, quantite));
    }

    [HttpPost("batiments/{type}/vente")]
    public ActionResult<Game> Vendre(TypeBatiment type, [FromQuery] int quantite = 1)
    {
        return Repondre(type, _boutiqueService.Vendre(type, quantite));
    }

    private ActionResult<Game> Repondre(TypeBatiment type, ResultatAchat resultat)
    {
        switch (resultat)
        {
            case ResultatAchat.BatimentInconnu:
                return BadRequest($"Unknown building: {type}");
            case ResultatAchat.QuantiteInvalide:
                return BadRequest("Quantity must be 1, 10 or 100");
            case ResultatAchat.FondsInsuffisants:
                return BadRequest("Not enough cookies");
            case ResultatAchat.PasAssezDeBatiments:
                return BadRequest("Not enough buildings to sell");
        }

        return _gameService.GetEtat();
    }
}
