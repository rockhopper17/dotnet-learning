namespace VideoGameCharacterApi.Dtos;

public class CharacterUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Game { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}