using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.Dtos;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Services;

public class CharacterService(AppDbContext dbContext) : ICharacterService
{
    // static List<Character> characters = new List<Character>
    // {
    //     new Character { Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Hero" },
    //     new Character { Id = 2, Name = "Link", Game = "The Legend of Zelda", Role = "Hero" },
    //     new Character { Id = 3, Name = "Bowser", Game = "Super Mario Bros.", Role = "Villian" },
    //     new Character { Id = 7, Name = "Zelda", Game = "The Legend of Zelda", Role = "Princess" }
    // };

    public Task<CharacterDto> AddCharacterAsync(Character character)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCharacterAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CharacterDto>> GetAllCharactersAsync()
        => await dbContext.Characters.Select(c => new CharacterDto
        {
            Name = c.Name,
            Game = c.Game,
            Role = c.Role
        }).ToListAsync();
        // => await Task.FromResult(characters);  // uses static list

    public async Task<CharacterDto?> GetCharacterByIdAsync(int id)
    {
        // var result = characters.FirstOrDefault(c => c.Id == id);  // uses static list
        var result = await dbContext.Characters
            .Where(c => c.Id == id)
            .Select(c => new CharacterDto
            {
                Name = c.Name,
                Game = c.Game,
                Role = c.Role
            })
            .FirstOrDefaultAsync();

        return result;
    }

    public Task<bool> UpdateCharacterAsync(int id, Character character)
    {
        throw new NotImplementedException();
    }
}