using System.Text.Json.Serialization;
using CookieClicker.Model.Batiments;

namespace CookieClicker.Model.PowerUps;

public class PowerUp
{
    private const string CibleClic = "Clic";

    public string Id { get; }
    public string Nom { get; }
    public double Prix { get; }
    [JsonIgnore]
    public Type? BatimentCible { get; }
    public double Multiplicateur { get; }

    public string Cible => BatimentCible?.Name ?? CibleClic;
    public bool AffecteClic => BatimentCible is null;

    public PowerUp(string id, string nom, double prix, Type? batimentCible, double multiplicateur)
    {
        Id = id;
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
