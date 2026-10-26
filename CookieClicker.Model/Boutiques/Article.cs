namespace CookieClicker.Model.Boutiques;

public record Article(
    TypeBatiment Type,
    string Nom,
    int Quantite,
    double Prix,
    double PrixRevente,
    int CookiesPerMinutes,
    int Possedes);
