using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Services;

public class CharacterService : ICharacterService
{
    static List<Character> characters = new List<Character>
    {
        new Character { Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Hero" },
        new Character { Id = 2, Name = "Link", Game = "The Legend of Zelda", Role = "Hero" },
        new Character { Id = 3, Name = "Bowser", Game = "Super Mario Bros.", Role = "Villian" },
        new Character { Id = 7, Name = "Zelda", Game = "The Legend of Zelda", Role = "Princess" }
    };

    public Task<Character> AddCharacterAsync(Character character)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCharacterAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Character>> GetAllCharactersAsync()
        => await Task.FromResult(characters);

    public Task<Character> GetCharacterByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCharacterAsync(int id, Character character)
    {
        throw new NotImplementedException();
    }
}