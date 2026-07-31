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

    public async Task<CharacterReadDto> AddCharacterAsync(CharacterCreateDto character)
    {
        var newCharacter = new Character
        {
            Name = character.Name,
            Game = character.Game,
            Role = character.Role
        };

        dbContext.Characters.Add(newCharacter);
        await dbContext.SaveChangesAsync();

        return new CharacterReadDto
        {
            Id = newCharacter.Id,
            Name = newCharacter.Name,
            Game = newCharacter.Game,
            Role = newCharacter.Role
        };
    }

    public async Task<bool> DeleteCharacterAsync(int id)
    {
        var existingCharacter = await dbContext.Characters.FindAsync(id);
        
        if (existingCharacter is null)
            return false;

        dbContext.Characters.Remove(existingCharacter);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<CharacterReadDto>> GetAllCharactersAsync()
        => await dbContext.Characters.Select(c => new CharacterReadDto
        {
            Id = c.Id,
            Name = c.Name,
            Game = c.Game,
            Role = c.Role
        }).ToListAsync();
        // => await Task.FromResult(characters);  // uses static list

    public async Task<CharacterReadDto?> GetCharacterByIdAsync(int id)
    {
        // var result = characters.FirstOrDefault(c => c.Id == id);  // uses static list
        var result = await dbContext.Characters
            .Where(c => c.Id == id)
            .Select(c => new CharacterReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Game = c.Game,
                Role = c.Role
            })
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<bool> UpdateCharacterAsync(int id, CharacterUpdateDto character)
    {
        var existingCharacter = await dbContext.Characters.FindAsync(id);
        
        if (existingCharacter is null)
            return false;

        existingCharacter.Name = character.Name;
        existingCharacter.Game = character.Game;
        existingCharacter.Role = character.Role;

        await dbContext.SaveChangesAsync();

        return true;
    }
}