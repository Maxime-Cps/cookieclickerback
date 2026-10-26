using System.Text.Json.Serialization;
using CookieClicker.Model.Batiments;
using CookieClicker.Model.Boutiques;
using CookieClicker.Model.PowerUps;
using CookieClicker.Model.Statistiques;

namespace CookieClicker.Model.Games;

public class Game
{
    private const double CookiesPerClick = 1;
    private const double SecondesParMinute = 60;

    private readonly object _verrou = new();

    public double Cookies { get; private set; }
    public Statistique Statistiques { get; private set; } = new();
    public List<Batiment> Batiments { get; } = [];
    public List<PowerUp> Upgrades { get; } = [];

    public double CookiesPerSecond => Batiments.Sum(Production);
    public double CookiesParClic => CookiesPerClick * Multiplicateur(Upgrades.Where(u => u.AffecteClic));

    [JsonIgnore]
    public Boutique Boutique { get; } = new();

    public void MettreAJour(double cookies, int nbClics)
    {
        lock (_verrou)
        {
            if (cookies > Cookies)
            {
                Statistiques.AjouterProduction(cookies - Cookies);
            }

            Cookies = cookies;
            Statistiques.AjouterClics(nbClics);
        }
    }

    public ResultatAchat Acheter(TypeBatiment type, int quantite)
    {
        lock (_verrou)
        {
            return Boutique.Acheter(this, type, quantite);
        }
    }

    public ResultatAchat Vendre(TypeBatiment type, int quantite)
    {
        lock (_verrou)
        {
            return Boutique.Vendre(this, type, quantite);
        }
    }

    public ResultatAchat AcheterPowerUp(string powerUpId)
    {
        lock (_verrou)
        {
            return Boutique.AcheterPowerUp(this, powerUpId);
        }
    }

    public void Reinitialiser()
    {
        lock (_verrou)
        {
            Cookies = 0;
            Statistiques = new Statistique();
            Batiments.Clear();
            Upgrades.Clear();
        }
    }

    public int Count(Batiment batiment)
    {
        return Batiments.Count(b => b.GetType() == batiment.GetType());
    }

    public bool AjouterBatiments(List<Batiment> batiments, double prix)
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

    public bool RetirerBatiments(Batiment modele, int quantite, double remboursement)
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

    public bool AjouterPowerUp(PowerUp powerUp)
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
