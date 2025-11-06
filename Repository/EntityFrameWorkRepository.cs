using ASPNETDemo1.Data;
using ASPNETDemo1.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPNETDemo1.Repository;

public class EntityFrameWorkRepository : IGameRepository
{
    private readonly GameStoreContext dbContext;

    public EntityFrameWorkRepository(GameStoreContext dbContext)
    {
        this.dbContext = dbContext;
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