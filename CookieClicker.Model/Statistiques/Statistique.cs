namespace CookieClicker.Model.Statistiques;

public class Statistique
{
    public int NbClics { get; private set; }
    public double CookiesProduits { get; private set; }
    public int NbBatimentsAchetes { get; private set; }

    public void AjouterClics(int nbClics, double cookies)
    {
        NbClics += nbClics;
        CookiesProduits += cookies;
    }

    public void AjouterProduction(double cookies)
    {
        CookiesProduits += cookies;
    }

    public void AjouterAchatBatiment()
    {
        NbBatimentsAchetes++;
    }
}
