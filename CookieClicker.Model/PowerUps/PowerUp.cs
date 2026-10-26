using System.Text.Json.Serialization;
using CookieClicker.Model.Batiments;

namespace CookieClicker.Model.PowerUps;

public class PowerUp
{
    public string Nom { get; }
    public double Prix { get; }
    [JsonIgnore]
    public Type BatimentCible { get; }
    public double Multiplicateur { get; }

    public PowerUp(string nom, double prix, Type batimentCible, double multiplicateur)
    {
        Nom = nom;
        Prix = prix;
        BatimentCible = batimentCible;
        Multiplicateur = multiplicateur;
    }

    public bool Affecte(Batiment batiment)
    {
        return batiment.GetType() == BatimentCible;
    }
}
