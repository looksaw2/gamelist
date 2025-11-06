namespace ASPNETDemo1.Authorization;

public static class AuthorizationExtension
{
    public static IServiceCollection AddGameStoreAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.ReadAccess, 
                builder => 
                    builder.RequireClaim("scope","games:read"));
            options.AddPolicy(Policies.WriteAccess,
                builder => 
                    builder.RequireClaim("scope","games:write")
                        .RequireRole("Admin")
            );
        });
        return services;
    }
}