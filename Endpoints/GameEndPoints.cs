using System.Diagnostics;
using ASPNETDemo1.Authorization;
using ASPNETDemo1.Entities;
using ASPNETDemo1.Repository;
using FluentValidation;
using FluentValidation.Results;

namespace ASPNETDemo1.Endpoints;

public static class GameEndPoints
{
    public const string GetGameV1Endpoint = "GetGameV1";
    public const string GetGameV2Endpoint = "GetGameV2";
    public static RouteGroupBuilder MapGameEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.NewVersionedApi()
            .MapGroup("/v{version:apiVersion}/games")
            .HasApiVersion(1.0)
            .HasApiVersion(2.0);
        //V1 Group
        group.MapGet("/",
            async (IGameRepository repository ,LoggerFactory loggerFactory) =>
            {
                try
                {
                    var ans = (await repository.GetAllGamesAsync()).Select(game => game.AsDto());
                    return Results.Ok(ans);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger("Games Endpoints");
                    logger.LogError(ex,"Could not process a request on machine {Machine} , TraceId: {TraceId}",Environment.MachineName,Activity.Current?.TraceId);
                    return Results.Problem(
                        title : "We made a mistake but we're working on it",
                        statusCode: StatusCodes.Status500InternalServerError,
                        extensions: new Dictionary<string, object?>
                        {
                            {"traceID", Activity.Current?.TraceId.ToString()}
                        }
                    );
                }
            }
        ).MapToApiVersion(1.0);
        group.MapGet("/{id}", async (IGameRepository repository, int id) =>
            {
                Game? game = await repository.GetGameByIdAsync(id);
                if (game is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(game.AsDto());
            }).WithName(GetGameV1Endpoint)
            .RequireAuthorization(Policies.ReadAccess
        ).MapToApiVersion(1.0);

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
            return Results.CreatedAtRoute(GetGameV1Endpoint,new { id = game.Id} , game);
        }).RequireAuthorization(Policies.WriteAccess).MapToApiVersion(1.0);


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
        }).RequireAuthorization(Policies.WriteAccess).MapToApiVersion(1.0);


        group.MapDelete("/{id}", async (IGameRepository repository,int id) =>
        {
            Game? game = await repository.GetGameByIdAsync(id);
            if (game is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        }).RequireAuthorization(Policies.WriteAccess).MapToApiVersion(1.0);
        //V2 GROUP
        
        group.MapGet("/",
            async (IGameRepository repository ,LoggerFactory loggerFactory) =>
            {
                try
                {
                    var ans = (await repository.GetAllGamesAsync()).Select(game => game.AsDtoV2());
                    return Results.Ok(ans);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger("Games Endpoints");
                    logger.LogError(ex,"Could not process a request on machine {Machine} , TraceId: {TraceId}",Environment.MachineName,Activity.Current?.TraceId);
                    return Results.Problem(
                        title : "We made a mistake but we're working on it",
                        statusCode: StatusCodes.Status500InternalServerError,
                        extensions: new Dictionary<string, object?>
                        {
                            {"traceID", Activity.Current?.TraceId.ToString()}
                        }
                    );
                }
            }
        ).MapToApiVersion(2.0);
        group.MapGet("/{id}", async (IGameRepository repository, int id) =>
            {
                Game? game = await repository.GetGameByIdAsync(id);
                if (game is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(game.AsDtoV2());
            }).WithName(GetGameV2Endpoint)
            .RequireAuthorization(Policies.ReadAccess
        ).MapToApiVersion(2.0);

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
            return Results.CreatedAtRoute(GetGameV2Endpoint,new { id = game.Id} , game);
        }).RequireAuthorization(Policies.WriteAccess).MapToApiVersion(2.0);


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
        }).RequireAuthorization(Policies.WriteAccess).MapToApiVersion(2.0);


        group.MapDelete("/{id}", async (IGameRepository repository,int id) =>
        {
            Game? game = await repository.GetGameByIdAsync(id);
            if (game is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        }).RequireAuthorization(Policies.WriteAccess).MapToApiVersion(2.0);
        return group;
    }
}