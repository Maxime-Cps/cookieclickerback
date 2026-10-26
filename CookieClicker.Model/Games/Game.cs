using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using CookieClicker.Model.Boutiques;
using CookieClicker.Model.Players;

namespace CookieClicker.Model.Games;

public class Game
{
    private readonly ConcurrentDictionary<Guid, Player> _players = new();

    public DateTime DateDebut { get; private set; } = DateTime.UtcNow;
    public IEnumerable<Player> Players => _players.Values;
    public int NbJoueurs => _players.Count;
    public double CookiesProduits => Players.Sum(p => p.Statistiques.CookiesProduits);

    [JsonIgnore]
    public Boutique Boutique { get; } = new();

    public Player AjouterJoueur(string name)
    {
        var player = new Player(name);
        _players[player.Id] = player;

        return player;
    }

    public Player? GetJoueur(Guid id)
    {
        return _players.GetValueOrDefault(id);
    }

    public bool RetirerJoueur(Guid id)
    {
        return _players.TryRemove(id, out _);
    }

    public Player? MettreAJourCookies(Guid id, double cookies, int nbClics)
    {
        var player = GetJoueur(id);
        if (player is null)
        {
            return null;
        }

        lock (player)
        {
            player.MettreAJour(cookies, nbClics);
        }

        return player;
    }

    public ResultatAchat Acheter(Guid id, TypeBatiment type, int quantite)
    {
        var player = GetJoueur(id);
        if (player is null)
        {
            return ResultatAchat.JoueurInconnu;
        }

        lock (player)
        {
            return Boutique.Acheter(player, type, quantite);
        }
    }

    public ResultatAchat Vendre(Guid id, TypeBatiment type, int quantite)
    {
        var player = GetJoueur(id);
        if (player is null)
        {
            return ResultatAchat.JoueurInconnu;
        }

        lock (player)
        {
            return Boutique.Vendre(player, type, quantite);
        }
    }

    public ResultatAchat AcheterPowerUp(Guid id, string powerUpId)
    {
        var player = GetJoueur(id);
        if (player is null)
        {
            return ResultatAchat.JoueurInconnu;
        }

        lock (player)
        {
            return Boutique.AcheterPowerUp(player, powerUpId);
        }
    }

    public List<Player> Classement()
    {
        return Players.OrderByDescending(p => p.Statistiques.CookiesProduits).ToList();
    }

    public void Reinitialiser()
    {
        _players.Clear();
        DateDebut = DateTime.UtcNow;
    }
}
