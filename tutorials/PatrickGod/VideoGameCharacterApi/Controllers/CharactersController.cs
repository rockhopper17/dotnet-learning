using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.Models;
using VideoGameCharacterApi.Services;

namespace VideoGameCharacterApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CharactersController(ICharacterService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCharacters()
        => Ok(await service.GetAllCharactersAsync());
}