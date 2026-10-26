using CookieClicker.Model.Games;
using CookieClicker.Model.Players;

namespace CookieClicker.Services;

public class PlayerService
{
    private readonly Game _game;

    public PlayerService(Game game)
    {
        _game = game;
    }

    public IEnumerable<Player> GetAll()
    {
        return _game.Players.ToList();
    }

    public Player? Get(Guid id)
    {
        return _game.GetJoueur(id);
    }

    public Player Create(string name)
    {
        return _game.AjouterJoueur(name);
    }

    public bool Delete(Guid id)
    {
        return _game.RetirerJoueur(id);
    }

    public Player? MettreAJourCookies(Guid id, double cookies, int nbClics)
    {
        return _game.MettreAJourCookies(id, cookies, nbClics);
    }
}
