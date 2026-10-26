namespace CookieClicker.Model.Batiments.BatimentEnfants;
using Batiments;

public class Curseur : Batiment {
    public Curseur()
    {
        IsTime = true;
        
        CookiesPerMinutes = 0;
        Prix = 15 ; 
    
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