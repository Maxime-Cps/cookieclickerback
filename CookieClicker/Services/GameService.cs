using CookieClicker.Model.Games;

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

    public Game MettreAJourCookies(double cookies, int nbClics)
    {
        _game.MettreAJour(cookies, nbClics);

        return _game;
    }

    public void Reinitialiser()
    {
        _game.Reinitialiser();
    }
}
