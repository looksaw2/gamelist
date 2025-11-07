namespace ASPNETDemo1;

public record GameDtoV1(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string ImageUrl
);

public record GameDtoV2(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    decimal RetailPrice,
    DateTime ReleaseDate,
    string ImageUrl
);

public record CreateGameDto(
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string ImageUrl
);


public record UpdateGameDto(
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string ImageUrl
);

