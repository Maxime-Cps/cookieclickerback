using CookieClicker.Model.Batiments.BatimentEnfants;

namespace CookieClicker.Model.PowerUps;

public static class CataloguePowerUps
{
    private const double Double = 2;

    public static readonly IReadOnlyList<PowerUp> Tous =
    [
        new("doigt-renforce", "Doigt renforcé", 100, null, Double),
        new("curseur-carbone", "Curseurs en carbone", 500, typeof(Curseur), Double),
        new("rouleau-patisserie", "Rouleaux à pâtisserie", 1_000, typeof(Mami), Double),
        new("engrais", "Engrais", 11_000, typeof(Ferme), Double),
        new("foreuse", "Foreuses", 120_000, typeof(Mine), Double),
        new("tapis-roulant", "Tapis roulants", 1_300_000, typeof(Usine), Double),
    ];

    public static PowerUp? Trouver(string id)
    {
        return Tous.FirstOrDefault(p => p.Id == id);
    }
}
