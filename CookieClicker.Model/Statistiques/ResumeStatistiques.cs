namespace CookieClicker.Model.Statistiques;

public record ResumeStatistiques(
    int NbClics,
    double CookiesProduits,
    int NbBatiments,
    int NbAmeliorations,
    double CookiesParSeconde,
    double CookiesParClic);
