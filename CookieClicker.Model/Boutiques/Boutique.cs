using CookieClicker.Model.Batiments;
using CookieClicker.Model.Batiments.BatimentEnfants;
using CookieClicker.Model.Players;
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

    public List<Article> Catalogue(Player? player, int quantite)
    {
        return Fabriques.Keys.Select(type => CreerArticle(type, player, quantite)).ToList();
    }

    public ResultatAchat Acheter(Player player, TypeBatiment type, int quantite)
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
        if (!player.Buy(batiments, Prix(batiments[0], player, quantite)))
        {
            return ResultatAchat.FondsInsuffisants;
        }

        return ResultatAchat.Ok;
    }

    public ResultatAchat Vendre(Player player, TypeBatiment type, int quantite)
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
        if (!player.Vendre(modele, quantite, PrixRevente(modele, player, quantite)))
        {
            return ResultatAchat.PasAssezDeBatiments;
        }

        return ResultatAchat.Ok;
    }

    public List<ArticlePowerUp> CatalogueAmeliorations(Player? player)
    {
        return CataloguePowerUps.Tous
            .Select(p => new ArticlePowerUp(p.Id, p.Nom, p.Prix, p.Cible, p.Multiplicateur, player?.Possede(p) ?? false))
            .ToList();
    }

    public ResultatAchat AcheterPowerUp(Player player, string powerUpId)
    {
        var powerUp = CataloguePowerUps.Trouver(powerUpId);
        if (powerUp is null)
        {
            return ResultatAchat.PowerUpInconnu;
        }

        if (player.Possede(powerUp))
        {
            return ResultatAchat.DejaPossede;
        }

        if (!player.Buy(powerUp))
        {
            return ResultatAchat.FondsInsuffisants;
        }

        return ResultatAchat.Ok;
    }

    private static Article CreerArticle(TypeBatiment type, Player? player, int quantite)
    {
        var batiment = Fabriques[type]();

        return new Article(
            type,
            type.ToString(),
            quantite,
            Prix(batiment, player, quantite),
            PrixRevente(batiment, player, quantite),
            batiment.CookiesPerMinutes,
            player?.Count(batiment) ?? 0);
    }

    private static double Prix(Batiment batiment, Player? player, int quantite)
    {
        int possedes = player?.Count(batiment) ?? 0;

        return CoutCumule(batiment.Prix, possedes, quantite);
    }

    private static double PrixRevente(Batiment batiment, Player? player, int quantite)
    {
        int possedes = player?.Count(batiment) ?? 0;
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
