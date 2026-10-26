using System.Collections.Concurrent;
using CookieClicker.Model.Players;

namespace CookieClicker.Services;

public class PlayerService
{
    private readonly ConcurrentDictionary<Guid, Player> _players = new();

    public IEnumerable<Player> GetAll()
    {
        return _players.Values.Select(Synchroniser).ToList();
    }

    public Player? Get(Guid id)
    {
        var player = _players.GetValueOrDefault(id);
        if (player is null)
        {
            return null;
        }

        return Synchroniser(player);
    }

    public Player Create(string name)
    {
        var player = new Player(name);
        _players[player.Id] = player;

        return player;
    }

    public bool Delete(Guid id)
    {
        return _players.TryRemove(id, out _);
    }

    public Player? Click(Guid id, int nbClics)
    {
        var player = Get(id);
        if (player is null)
        {
            return null;
        }

        lock (player)
        {
            player.Click(nbClics);
        }

        return player;
    }

    private static Player Synchroniser(Player player)
    {
        lock (player)
        {
            player.Synchroniser(DateTime.UtcNow);
        }

        return player;
    }
}
