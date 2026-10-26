namespace CookieClicker.Model.Batiment;

public abstract class Batiment
{
    public bool IsTime;
    public int NumberCookies = 0 ;
    public int CookiesPerMinutes;
    public int Niveau;
    public int Prix;
    public abstract void BuyBonus();
    public abstract void Upgrade();
    public abstract void AddCookies();
    public abstract void Do();

}
