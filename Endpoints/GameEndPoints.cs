using ASPNETDemo1.Authorization;
using ASPNETDemo1.Entities;
using ASPNETDemo1.Repository;
using FluentValidation;
using FluentValidation.Results;

namespace ASPNETDemo1.Endpoints;

public static class GameEndPoints
{
    public const string GetGameEndpoint = "GetGame";
    public static RouteGroupBuilder MapGameEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games");
        group.MapGet("/", async (IGameRepository repository) => 
            (await repository.GetAllGamesAsync()).Select(game => game.AsDto())
            );
        group.MapGet("/{id}", async (IGameRepository repository, int id) =>
            {
                Game? game = await repository.GetGameByIdAsync(id);
                if (game is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(game.AsDto());
            }).WithName(GetGameEndpoint)
            .RequireAuthorization(Policies.ReadAccess
        );

        group.MapPost("/", async (IGameRepository repository,CreateGameDto gamedto , IValidator<Game> validator) =>
        {
            Game game = new Game()
            {
                Name = gamedto.Name,
                Genre = gamedto.Genre,
                Price = gamedto.Price,
                ReleaseDate = gamedto.ReleaseDate,
                ImageUri = gamedto.ImageUrl
            };
            ValidationResult result = await validator.ValidateAsync(game);
            if (!result.IsValid)
            {
                return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));
            }
            await repository.CreateAsync(game);
            return Results.CreatedAtRoute(GetGameEndpoint,new { id = game.Id} , game);
        }).RequireAuthorization(Policies.WriteAccess);


        group.MapPut("/{id}", async (IGameRepository repository,int id, UpdateGameDto updatedGameDto, IValidator<Game> validator) =>
        {
            Game updatedGame = new Game()
            {
                Name = updatedGameDto.Name,
                Genre = updatedGameDto.Genre,
                Price = updatedGameDto.Price,
                ReleaseDate = updatedGameDto.ReleaseDate,
                ImageUri = updatedGameDto.ImageUrl
            };
            Game? existingGame = await repository.GetGameByIdAsync(id);
            if (existingGame is null)
            {
                return Results.NoContent();
            }

            ValidationResult result = await validator.ValidateAsync(updatedGame);
            if (!result.IsValid)
            {
                return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));
            }
            existingGame.Name = updatedGame.Name;
            existingGame.Genre = updatedGame.Genre;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;
            existingGame.ImageUri = updatedGame.ImageUri;
            await repository.UpdateAsync(existingGame);
            return Results.NoContent();
        }).RequireAuthorization(Policies.WriteAccess);


        group.MapDelete("/{id}", async (IGameRepository repository,int id) =>
        {
            Game? game = await repository.GetGameByIdAsync(id);
            if (game is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        }).RequireAuthorization(Policies.WriteAccess);
        return group;
    }
}