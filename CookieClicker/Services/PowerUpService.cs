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
        return _game.Boutique.CatalogueAmeliorations(null);
    }

    public List<ArticlePowerUp>? GetCatalogue(Guid playerId)
    {
        var player = _game.GetJoueur(playerId);
        if (player is null)
        {
            return null;
        }

        return _game.Boutique.CatalogueAmeliorations(player);
    }

    public ResultatAchat Acheter(Guid playerId, string powerUpId)
    {
        return _game.AcheterPowerUp(playerId, powerUpId);
    }
}
