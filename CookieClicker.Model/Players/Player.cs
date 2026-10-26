using CookieClicker.Model.Batiments;
using CookieClicker.Model.PowerUps;
using CookieClicker.Model.Statistiques;

namespace CookieClicker.Model.Players;

public class Player
{
    private const double CookiesPerClick = 1;
    private const double SecondesParMinute = 60;

    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public double Cookies { get; private set; }
    public Statistique Statistiques { get; } = new();
    public List<Batiment> Batiments { get; } = [];
    public List<PowerUp> Upgrades { get; } = [];

    public double CookiesPerSecond => Batiments.Sum(Production);
    public double CookiesParClic => CookiesPerClick * Multiplicateur(Upgrades.Where(u => u.AffecteClic));

    public Player(string name)
    {
        Name = name;
    }

    public void MettreAJour(double cookies, int nbClics)
    {
        if (cookies > Cookies)
        {
            Statistiques.AjouterProduction(cookies - Cookies);
        }

        Cookies = cookies;
        Statistiques.AjouterClics(nbClics);
    }

    public int Count(Batiment batiment)
    {
        return Batiments.Count(b => b.GetType() == batiment.GetType());
    }

    public bool Buy(List<Batiment> batiments, double prix)
    {
        if (Cookies < prix)
        {
            return false;
        }

        Cookies -= prix;
        Batiments.AddRange(batiments);
        Statistiques.AjouterAchatBatiments(batiments.Count);

        return true;
    }

    public bool Vendre(Batiment modele, int quantite, double remboursement)
    {
        var aVendre = Batiments.Where(b => b.GetType() == modele.GetType()).TakeLast(quantite).ToList();
        if (aVendre.Count < quantite)
        {
            return false;
        }

        foreach (var batiment in aVendre)
        {
            Batiments.Remove(batiment);
        }

        Cookies += remboursement;

        return true;
    }

    public bool Possede(PowerUp powerUp)
    {
        return Upgrades.Contains(powerUp);
    }

    public bool Buy(PowerUp powerUp)
    {
        if (Possede(powerUp) || Cookies < powerUp.Prix)
        {
            return false;
        }

        Cookies -= powerUp.Prix;
        Upgrades.Add(powerUp);

        return true;
    }

    public List<BatimentPossede> BatimentsPossedes()
    {
        return Batiments
            .GroupBy(b => b.GetType().Name)
            .Select(g => new BatimentPossede(g.Key, g.Count(), g.Sum(Production)))
            .ToList();
    }

    public ResumeStatistiques Resume()
    {
        return new ResumeStatistiques(
            Statistiques.NbClics,
            Statistiques.CookiesProduits,
            Batiments.Count,
            Upgrades.Count,
            CookiesPerSecond,
            CookiesParClic);
    }

    public double Production(Batiment batiment)
    {
        double multiplicateur = Multiplicateur(Upgrades.Where(u => u.Affecte(batiment)));

        return batiment.CookiesPerMinutes / SecondesParMinute * multiplicateur;
    }

    private static double Multiplicateur(IEnumerable<PowerUp> powerUps)
    {
        return powerUps.Aggregate(1.0, (total, u) => total * u.Multiplicateur);
    }
}
