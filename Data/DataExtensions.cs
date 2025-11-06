using ASPNETDemo1.Repository;
using Microsoft.EntityFrameworkCore;

namespace ASPNETDemo1.Data;

public static class DataExtensions
{
    public static async Task InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepository(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connString = configuration.GetConnectionString("GameStoreContext");
        services.AddSqlServer<GameStoreContext>(connString)
            .AddScoped<IGameRepository, EntityFrameWorkRepository>();
        return services;
    }
}