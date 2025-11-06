using System.Reflection;
using ASPNETDemo1.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPNETDemo1.Data;

public class GameStoreContext : DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
    {
        
    }

    public DbSet<Game> Games => Set<Game>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    
}