using Core.Entities;
using Core.DTOs;

namespace Infra.Services
{
    public interface ITokenService
    {
        // Generar token de usuario
        AuthResponseDto CreateToken(User user, bool isArtist);
    }
}