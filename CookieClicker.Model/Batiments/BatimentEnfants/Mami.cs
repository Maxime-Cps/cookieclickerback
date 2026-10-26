namespace CookieClicker.Model.Batiments.BatimentEnfants;

public class Mami : Batiment {
    public Mami()
    {
        IsTime = true;
        
        CookiesPerMinutes = 60;
        Prix = 100; 
    
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