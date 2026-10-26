namespace CookieClicker.Model.Batiments.BatimentEnfants;

public class Mine : Batiment {
    public Mine()
    {
        IsTime = true;
        
        CookiesPerMinutes = 2_820;
        Prix = 12_000; 
    
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