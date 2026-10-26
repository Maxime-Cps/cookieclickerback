using CookieClicker.Model.Batiments;
using CookieClicker.Model.Batiments.BatimentEnfants;
using CookieClicker.Model.Games;
using CookieClicker.Model.PowerUps;

namespace CookieClicker.Model.Boutiques;

public class Boutique
{
    private const double CroissancePrix = 1.15;
    private const double TauxRevente = 0.25;

    public static readonly IReadOnlyList<int> Quantites = [1, 10, 100];

    private static readonly Dictionary<TypeBatiment, Func<Batiment>> Fabriques = new()
    {
        [TypeBatiment.Curseur] = () => new Curseur(),
        [TypeBatiment.Mami] = () => new Mami(),
        [TypeBatiment.Ferme] = () => new Ferme(),
        [TypeBatiment.Mine] = () => new Mine(),
        [TypeBatiment.Usine] = () => new Usine(),
    };

    public List<Article> Catalogue(Game game, int quantite)
    {
        return Fabriques.Keys.Select(type => CreerArticle(type, game, quantite)).ToList();
    }

    public ResultatAchat Acheter(Game game, TypeBatiment type, int quantite)
    {
        if (!Quantites.Contains(quantite))
        {
            return ResultatAchat.QuantiteInvalide;
        }

        if (!Fabriques.TryGetValue(type, out var fabrique))
        {
            return ResultatAchat.BatimentInconnu;
        }

        var batiments = Enumerable.Range(0, quantite).Select(_ => fabrique()).ToList();
        if (!game.AjouterBatiments(batiments, Prix(batiments[0], game, quantite)))
        {
            return ResultatAchat.FondsInsuffisants;
        }

        return ResultatAchat.Ok;
    }

    public ResultatAchat Vendre(Game game, TypeBatiment type, int quantite)
    {
        if (!Quantites.Contains(quantite))
        {
            return ResultatAchat.QuantiteInvalide;
        }

        if (!Fabriques.TryGetValue(type, out var fabrique))
        {
            return ResultatAchat.BatimentInconnu;
        }

        var modele = fabrique();
        if (!game.RetirerBatiments(modele, quantite, PrixRevente(modele, game, quantite)))
        {
            return ResultatAchat.PasAssezDeBatiments;
        }

        return ResultatAchat.Ok;
    }

    public List<ArticlePowerUp> CatalogueAmeliorations(Game game)
    {
        return CataloguePowerUps.Tous
            .Select(p => new ArticlePowerUp(p.Id, p.Nom, p.Prix, p.Cible, p.Multiplicateur, game.Possede(p)))
            .ToList();
    }

    public ResultatAchat AcheterPowerUp(Game game, string powerUpId)
    {
        var powerUp = CataloguePowerUps.Trouver(powerUpId);
        if (powerUp is null)
        {
            return ResultatAchat.PowerUpInconnu;
        }

        if (game.Possede(powerUp))
        {
            return ResultatAchat.DejaPossede;
        }

        if (!game.AjouterPowerUp(powerUp))
        {
            return ResultatAchat.FondsInsuffisants;
        }

        return ResultatAchat.Ok;
    }

    private static Article CreerArticle(TypeBatiment type, Game game, int quantite)
    {
        var batiment = Fabriques[type]();

        return new Article(
            type,
            type.ToString(),
            quantite,
            Prix(batiment, game, quantite),
            PrixRevente(batiment, game, quantite),
            batiment.CookiesPerMinutes,
            game.Count(batiment));
    }

    private static double Prix(Batiment batiment, Game game, int quantite)
    {
        int possedes = game.Count(batiment);

        return CoutCumule(batiment.Prix, possedes, quantite);
    }

    private static double PrixRevente(Batiment batiment, Game game, int quantite)
    {
        int possedes = game.Count(batiment);
        if (possedes < quantite)
        {
            return 0;
        }

        return TauxRevente * CoutCumule(batiment.Prix, possedes - quantite, quantite);
    }

    private static double CoutCumule(double prixBase, int premier, int quantite)
    {
        return prixBase * Math.Pow(CroissancePrix, premier) * (Math.Pow(CroissancePrix, quantite) - 1) / (CroissancePrix - 1);
    }
}
