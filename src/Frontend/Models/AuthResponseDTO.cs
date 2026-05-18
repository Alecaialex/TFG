namespace Frontend.Models
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = "";
        public DateTime Expiration { get; set; }
        public bool IsArtist { get; set; }
    }
}
