namespace CookieClicker.Model.Player;

public class Player
{
    public string Name { get; set; }
    public List<Statistique> Statistiques { get; set; }
    public int nbCookies { get; set; }
    public Boutique boutique { get; set; }
    
}