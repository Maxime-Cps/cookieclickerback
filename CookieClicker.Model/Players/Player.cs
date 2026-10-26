using CookieClicker.Model.Batiments;
using CookieClicker.Model.PowerUps;
using CookieClicker.Model.Statistiques;

namespace CookieClicker.Model.Players;

public class Player
{
    private const double CookiesPerClick = 1;

    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public double Cookies { get; private set; }
    public Statistique Statistiques { get; } = new();
    public List<Batiment> Batiments { get; } = [];
    public List<PowerUp> Upgrades { get; } = [];
    public DateTime DerniereSynchro { get; private set; } = DateTime.UtcNow;

    public double CookiesPerSecond => Batiments.Sum(Production);

    public Player(string name)
    {
        Name = name;
    }

    public void Click(int nbClics)
    {
        double cookies = nbClics * CookiesPerClick;

        Cookies += cookies;
        Statistiques.AjouterClics(nbClics, cookies);
    }

    public void Produce(TimeSpan elapsed)
    {
        double produced = CookiesPerSecond * elapsed.TotalSeconds;

        Cookies += produced;
        Statistiques.AjouterProduction(produced);
    }

    public void Synchroniser(DateTime maintenant)
    {
        Produce(maintenant - DerniereSynchro);
        DerniereSynchro = maintenant;
    }

    public int Count(Batiment batiment)
    {
        return Batiments.Count(b => b.GetType() == batiment.GetType());
    }

    public bool Buy(Batiment batiment, double prix)
    {
        if (Cookies < prix)
        {
            return false;
        }

        Cookies -= prix;
        Batiments.Add(batiment);
        Statistiques.AjouterAchatBatiment();

        return true;
    }

    public bool Buy(PowerUp powerUp)
    {
        if (Upgrades.Contains(powerUp) || Cookies < powerUp.Prix)
        {
            return false;
        }

        Cookies -= powerUp.Prix;
        Upgrades.Add(powerUp);

        return true;
    }

    private double Production(Batiment batiment)
    {
        double multiplicateur = Upgrades
            .Where(u => u.Affecte(batiment))
            .Aggregate(1.0, (total, u) => total * u.Multiplicateur);

        return batiment.CookiesPerMinutes * multiplicateur;
    }
}
