using Frontend.Models;

namespace Frontend.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> RegisterAsync(string email, string password, string displayName, bool isArtist);
        Task<AuthResponseDTO?> LoginAsync(string email, string password);
    }
}
