using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.Dtos;
using VideoGameCharacterApi.Models;
using VideoGameCharacterApi.Services;

namespace VideoGameCharacterApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CharactersController(ICharacterService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CharacterReadDto>>> GetCharacters()
        => Ok(await service.GetAllCharactersAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<CharacterReadDto>> GetCharacter(int id)
    {
        var character = await service.GetCharacterByIdAsync(id);
        if (character is null)
        {
            return NotFound($"no character with id {id}");
        }
        return Ok(character);
    }

    [HttpPost]
    public async Task<ActionResult<CharacterCreateDto>> AddCharacter(CharacterCreateDto character)
    {
        var createdCharacter = await service.AddCharacterAsync(character);
        return CreatedAtAction(nameof(GetCharacter), new { id = createdCharacter.Id }, createdCharacter);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCharacter(int id, CharacterUpdateDto character)
    {
        var updated = await service.UpdateCharacterAsync(id, character);
        return updated ? NoContent() : NotFound($"character with id {id} not found");
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCharacter(int id)
    {
        var deleted = await service.DeleteCharacterAsync(id);
        return deleted ? NoContent() : NotFound($"character with id {id} not found");
    }
}