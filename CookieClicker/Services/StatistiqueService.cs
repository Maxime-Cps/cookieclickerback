using CookieClicker.Model.Games;
using CookieClicker.Model.Statistiques;

namespace CookieClicker.Services;

public class StatistiqueService
{
    private readonly Game _game;

    public StatistiqueService(Game game)
    {
        _game = game;
    }

    public ResumeStatistiques Get()
    {
        return _game.Resume();
    }
}
