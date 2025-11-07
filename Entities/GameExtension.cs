namespace ASPNETDemo1.Entities;

public static class GameExtension
{
    public static GameDtoV1 AsDto(this Game game)
    {
        return new GameDtoV1(
            game.Id,
            game.Name,
            game.Genre,
            game.Price,
            game.ReleaseDate,
            game.ImageUri
        );
    }

    public static GameDtoV2 AsDtoV2(this Game game)
    {
        return new GameDtoV2(
            game.Id,
            game.Name,
            game.Genre,
            game.Price - (game.Price * .3m),
            game.Price,
            game.ReleaseDate,
            game.ImageUri
            );
    } 
}