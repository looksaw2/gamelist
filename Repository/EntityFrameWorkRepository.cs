using ASPNETDemo1.Data;
using ASPNETDemo1.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPNETDemo1.Repository;

public class EntityFrameWorkRepository : IGameRepository
{
    private readonly GameStoreContext dbContext;

    private readonly ILogger<EntityFrameWorkRepository> logger;
    

    public EntityFrameWorkRepository(
        GameStoreContext dbContext,
        ILogger<EntityFrameWorkRepository> logger
        )
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }
    public async Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        return await dbContext.Games.AsNoTracking().ToListAsync();
    }

    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await dbContext.Games.FindAsync(id);
    }

    public async Task CreateAsync(Game game)
    {
        dbContext.Games.Add(game);
        dbContext.SaveChangesAsync();
        logger.LogInformation("Game {Name} with price {Price} has been created",game.Name,game.Price);
    }

    public async Task UpdateAsync(Game updateGame)
    {
        dbContext.Games.Update(updateGame);
        dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        dbContext.Games.Where(game => game.Id == id)
            .ExecuteDeleteAsync();
    }
}