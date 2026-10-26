using CookieClicker.Model.Boutiques;
using CookieClicker.Model.Games;

namespace CookieClicker.Services;

public class BoutiqueService
{
    private readonly Game _game;

    public BoutiqueService(Game game)
    {
        _game = game;
    }

    public List<Article> GetCatalogue(int quantite)
    {
        return _game.Boutique.Catalogue(null, quantite);
    }

    public List<Article>? GetCatalogue(Guid playerId, int quantite)
    {
        var player = _game.GetJoueur(playerId);
        if (player is null)
        {
            return null;
        }

        return _game.Boutique.Catalogue(player, quantite);
    }

    public ResultatAchat Acheter(Guid playerId, TypeBatiment type, int quantite)
    {
        return _game.Acheter(playerId, type, quantite);
    }

    public ResultatAchat Vendre(Guid playerId, TypeBatiment type, int quantite)
    {
        return _game.Vendre(playerId, type, quantite);
    }
}
