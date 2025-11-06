using ASPNETDemo1.Entities;

namespace ASPNETDemo1.Repository;

public interface IGameRepository
{
    public Task<IEnumerable<Game>> GetAllGamesAsync();
    public Task<Game?> GetGameByIdAsync(int id);
    public Task CreateAsync(Game game);
    Task UpdateAsync(Game updateGame);
    public Task DeleteAsync(int id);
}