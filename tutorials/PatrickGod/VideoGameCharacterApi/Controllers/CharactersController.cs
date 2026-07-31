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
    public async Task<ActionResult<List<CharacterDto>>> GetCharacters()
        => Ok(await service.GetAllCharactersAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<CharacterDto>> GetCharacter(int id)
    {
        var character = await service.GetCharacterByIdAsync(id);
        if (character is null)
        {
            return NotFound($"no character with id {id}");
        }
        return Ok(character);
    }
}