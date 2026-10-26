using CookieClicker.Model.Boutiques;
using CookieClicker.Model.Games;

namespace CookieClicker.Services;

public class PowerUpService
{
    private readonly Game _game;

    public PowerUpService(Game game)
    {
        _game = game;
    }

    public List<ArticlePowerUp> GetCatalogue()
    {
        return _game.Boutique.CatalogueAmeliorations(_game);
    }

    public ResultatAchat Acheter(string powerUpId)
    {
        return _game.AcheterPowerUp(powerUpId);
    }
}
