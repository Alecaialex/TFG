using Core.DTOs;

namespace Core.Interfaces;

public interface IAuthService
{
    // Registro de nuevo usuario
    Task<AuthResponseDto?> RegisterAsync(RegisterDTO model);

    // Login de usuario
    Task<AuthResponseDto?> LoginAsync(LoginDTO model);

    // Logout de usuario
    Task LogoutAsync();
}