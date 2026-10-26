namespace CookieClicker.Model.Batiments.BatimentEnfants;

public class Usine : Batiment {
    public Usine()
    {
        IsTime = true;
        
        CookiesPerMinutes = 15_600;
        Prix = 130_000; 
    
    }
    public override void Do()
    {
        // add Cookies per minutes

    }
    
    public override  void BuyBonus()
    {
        this.Niveau += 3;
    }
    public override void Upgrade()
    {
        
    }
    public override void AddCookies()
    {
        // Thread
        
    }
}