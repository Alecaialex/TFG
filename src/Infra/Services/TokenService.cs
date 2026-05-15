using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infra.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public AuthResponseDto CreateToken(User user, bool isArtist)
    {
        // Definimos la expiración del token
        var expiration = DateTime.UtcNow.AddDays(7);

        // Preparamos los Claims
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new("isArtist", isArtist.ToString().ToLower())
        };

        // Creamos la clave de firma
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Configuramos el descriptor del token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiration,
            SigningCredentials = creds
        };

        // Generamos el token
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        // Devolvemos el DTO con los datos que pidió el controlador
        return new AuthResponseDto
        {
            Token = tokenHandler.WriteToken(securityToken),
            Expiration = expiration,
            IsArtist = isArtist
        };
    }
}