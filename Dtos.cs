namespace ASPNETDemo1;

public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
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

