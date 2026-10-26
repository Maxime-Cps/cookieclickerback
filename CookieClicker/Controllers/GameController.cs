using CookieClicker.Model.Games;
using CookieClicker.Model.Players;
using CookieClicker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookieClicker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;

    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public Game Get()
    {
        return _gameService.GetEtat();
    }

    [HttpGet("classement")]
    public List<Player> GetClassement()
    {
        return _gameService.GetClassement();
    }

    [HttpPost("reset")]
    public IActionResult Reset()
    {
        _gameService.Reinitialiser();

        return NoContent();
    }
}
