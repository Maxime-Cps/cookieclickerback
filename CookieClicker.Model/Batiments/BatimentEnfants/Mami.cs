namespace CookieClicker.Model.Batiments.BatimentEnfants;

public class Mami : Batiment {
    public Mami()
    {
        IsTime = true;
        
        CookiesPerMinutes = 0;
        Prix = 110 ; 
    
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