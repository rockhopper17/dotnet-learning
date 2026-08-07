using VideoGameCharacterApi.Dtos;
using VideoGameCharacterApi.Entities;

namespace VideoGameCharacterApi.Services;

public interface IAuthService
{
    Task<User?> RegisterAsync(UserDto request);
    Task<TokenResponseDto?> LoginAsync(UserDto request);
    Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
}
