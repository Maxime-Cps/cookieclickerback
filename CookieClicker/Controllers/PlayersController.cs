using CookieClicker.Dtos;
using CookieClicker.Model.Players;
using CookieClicker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookieClicker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly PlayerService _playerService;

    public PlayersController(PlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet]
    public IEnumerable<Player> GetAll()
    {
        return _playerService.GetAll();
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Player> Get(Guid id)
    {
        var player = _playerService.Get(id);
        if (player is null)
        {
            return NotFound();
        }

        return player;
    }

    [HttpPost]
    public ActionResult<Player> Create(CreatePlayerRequest request)
    {
        var player = _playerService.Create(request.Name);

        return CreatedAtAction(nameof(Get), new { id = player.Id }, player);
    }

    [HttpPut("{id:guid}/cookies")]
    public ActionResult<Player> MettreAJourCookies(Guid id, CookiesRequest request)
    {
        var player = _playerService.MettreAJourCookies(id, request.Cookies, request.NbClics);
        if (player is null)
        {
            return NotFound();
        }

        return player;
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (!_playerService.Delete(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
