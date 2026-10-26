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
        return _game.Boutique.Catalogue(_game, quantite);
    }

    public ResultatAchat Acheter(TypeBatiment type, int quantite)
    {
        return _game.Acheter(type, quantite);
    }

    public ResultatAchat Vendre(TypeBatiment type, int quantite)
    {
        return _game.Vendre(type, quantite);
    }
}
