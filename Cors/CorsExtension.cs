namespace ASPNETDemo1.Cors;

public static class CorsExtension
{
    private const string allowedOriginSetting = "AllowdOrigin";

    public static IServiceCollection AddGameStoreCors(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        return services.AddCors(options =>
        {
            options.AddDefaultPolicy(corsBuilder =>
            {
                var allowedOrigin = configuration[allowedOriginSetting] ?? throw new Exception("No Origin found");
                corsBuilder.WithOrigins(allowedOrigin)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        }); 
    }
    
}