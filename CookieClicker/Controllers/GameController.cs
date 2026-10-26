using CookieClicker.Dtos;
using CookieClicker.Model.Games;
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

    [HttpPut("cookies")]
    public Game MettreAJourCookies(CookiesRequest request)
    {
        return _gameService.MettreAJourCookies(request.Cookies, request.NbClics);
    }

    [HttpPost("reset")]
    public IActionResult Reset()
    {
        _gameService.Reinitialiser();

        return NoContent();
    }
}
