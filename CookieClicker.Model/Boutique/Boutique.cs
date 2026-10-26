namespace CookieClicker.Model.Boutique;
using Batiment;
using Curseur;
using Mami;
using Mine;
using Ferme;


public abstract class Boutique
{
    public required Batiment[] Batiments;

    public static Batiment Buy(int choix)
    {
        switch (choix)
        {
            case 1 :
                return new Curseur();
            case 2 : 
                return new Ferme();
            case 3 :
                return new Mine();
            default : 
                return new Mami();
        }
    }
}