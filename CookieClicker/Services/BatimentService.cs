using CookieClicker.Model.Batiments;
using CookieClicker.Model.Games;

namespace CookieClicker.Services;

public class BatimentService
{
    private readonly Game _game;

    public BatimentService(Game game)
    {
        _game = game;
    }

    public List<BatimentPossede>? GetPossedes(Guid playerId)
    {
        return _game.GetJoueur(playerId)?.BatimentsPossedes();
    }
}
