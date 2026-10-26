namespace CookieClicker.Model.Player;

using CookieClicker.Model.Batiment;
using CookieClicker.Model.Upgrades;

public class Player
{
    private const double CookiesPerClick = 1;

    public string Name { get; }
    public double Cookies { get; private set; }
    public Statistique Statistiques { get; } = new();
    public List<Batiment> Batiments { get; } = [];
    public List<PowerUps> Upgrades { get; } = [];

    public double CookiesPerSecond => Batiments.Sum(b => b.CookiesParSeconde);

    public Player(string name)
    {
        Name = name;
    }

    public void Click()
    {
        Cookies += CookiesPerClick;
        Statistiques.AjouterClic(CookiesPerClick);
    }

    public void Produce(TimeSpan elapsed)
    {
        double produced = CookiesPerSecond * elapsed.TotalSeconds;

        Cookies += produced;
        Statistiques.AjouterProduction(produced);
    }

    public bool Buy(Batiment batiment)
    {
        if (Cookies < batiment.Prix)
        {
            return false;
        }

        Cookies -= batiment.Prix;
        Batiments.Add(batiment);
        Statistiques.AjouterAchatBatiment();

        return true;
    }
}
