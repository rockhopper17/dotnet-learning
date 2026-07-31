using VideoGameCharacterApi.Dtos;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Services;

public interface ICharacterService
{
    Task<List<CharacterReadDto>> GetAllCharactersAsync();
    Task<CharacterReadDto?> GetCharacterByIdAsync(int id);
    Task<CharacterReadDto> AddCharacterAsync(CharacterCreateDto character);
    Task<bool> UpdateCharacterAsync(int id, CharacterUpdateDto character);
    Task<bool> DeleteCharacterAsync(int id);
}