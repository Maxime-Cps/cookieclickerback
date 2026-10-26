using CookieClicker.Model.Games;
using CookieClicker.Model.Players;

namespace CookieClicker.Services;

public class GameService
{
    private readonly Game _game;

    public GameService(Game game)
    {
        _game = game;
    }

    public Game GetEtat()
    {
        return _game;
    }

    public List<Player> GetClassement()
    {
        return _game.Classement();
    }

    public void Reinitialiser()
    {
        _game.Reinitialiser();
    }
}
