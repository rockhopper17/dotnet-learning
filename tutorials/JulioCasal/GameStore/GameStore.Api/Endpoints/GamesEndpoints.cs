using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    // app.MapGet("/", () => "Hello World!");

    // private static readonly List<GameSummaryDto> games = [
    //     new (
    //         1,
    //         "Street Fighter II",
    //         "Fighting",
    //         19.99M,
    //         new DateOnly(1992, 7, 15)),
    //     new (
    //         2,
    //         "Final Fantasy XIV",
    //         "Roleplaying",
    //         59.99M,
    //         new DateOnly(2010, 9, 30)),
    //     new (
    //         3,
    //         "FIFA 23",
    //         "Sports",
    //         69.99M,
    //         new DateOnly(2022, 9, 27))
    // ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
            .WithParameterValidation();

        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Games
                .Include(game => game.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync());

        // GET /games/[id]
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => 
        {
            // GameDto? game = games.Find(game => game.Id == id);
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            // if (string.IsNullOrEmpty(newGame.Name))
            // {
            //     return Results.BadRequest("name is required");
            // }

            // GameDto game = new(
            //     games.Count + 1,
            //     newGame.Name,
            //     newGame.Genre,
            //     newGame.Price,
            //     newGame.ReleaseDate);
            // games.Add(game);

            // Game game = new()
            // {
            //     Name = newGame.Name,
            //     Genre = dbContext.Genres.Find(newGame.GenreId),
            //     GenreId = newGame.GenreId,
            //     Price = newGame.Price,
            //     ReleaseDate = newGame.ReleaseDate
            // };

            // GameDto gameDto = new(
            //     game.Id,
            //     game.Name,
            //     game.Genre!.Name,
            //     game.Price,
            //     game.ReleaseDate
            // );

            Game game = newGame.ToEntity();
            // game.Genre = dbContext.Genres.Find(newGame.GenreId);

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game.ToGameDetailsDto());
        });

        // PUT /games/[id]
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updatedGame.ToEntity(id));

            await dbContext.SaveChangesAsync();

            // games[index] = new GameSummaryDto(
            //     id,
            //     updatedGame.Name,
            //     updatedGame.Genre,
            //     updatedGame.Price,
            //     updatedGame.ReleaseDate
            // );

            return Results.NoContent();
        });

        // DELETE /games/[id]
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            // games.RemoveAll(game => game.Id == id); 
            await dbContext.Games
                .Where(game => game.Id == id)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}