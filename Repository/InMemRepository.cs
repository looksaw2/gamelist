using ASPNETDemo1.Entities;

namespace ASPNETDemo1.Repository;

public class InMemRepository : IGameRepository
{
    public readonly List<Game> games = new()
    {
        new Game()
        {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99m,
            ReleaseDate = new DateTime(1991, 2, 1),
            ImageUri = "https://placehold.co/100"
        },
    
        new Game()
        {
            Id = 2,
            Name = "Street Fighter III",
            Genre = "Fighting",
            Price = 19.99m,
            ReleaseDate = new DateTime(2018, 2, 1),
            ImageUri = "https://placehold.co/100"
        },
    
        new Game()
        {
            Id = 3,
            Name = "FIFO IX",
            Genre = "Fighting",
            Price = 29.99m,
            ReleaseDate = new DateTime(2022, 2, 1),
            ImageUri = "https://placehold.co/100"
        },
    };

    public async Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        return await Task.FromResult(games);
    }

    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await Task.FromResult(games.Find(g => g.Id == id));
    }

    public async Task CreateAsync(Game game)
    {
        game.Id = games.Max(g => g.Id) + 1;
        games.Add(game);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Game updateGame)
    {
        var index = games.FindIndex(game => game.Id == updateGame.Id);
        games[index] = updateGame;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = games.FindIndex(game => game.Id == id);
        games.RemoveAt(index);
        await Task.CompletedTask;
    }
}