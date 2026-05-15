namespace Core.DTOs;

public class AuthResponseDto
{
    public required string Token { get; set; }
    public DateTime Expiration { get; set; }
    public bool IsArtist { get; set; }
}