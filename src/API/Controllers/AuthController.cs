using Core.DTOs;
using Core.Entities;
using Infra.Data;
using Infra.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService) => _authService = authService;

        // Registro de un nuevo usuario
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            // Le pasamos el DTO recibido al servicio de autenticación
            var response = await _authService.RegisterAsync(model);

            // Si la respuesta es correcta, devolvemos los datos que nos da el servicio (Token, expiración, isArtist)
            // Si la respuesta es null, devolvemos el BadRequest
            return response != null
                ? Ok(response)
                : BadRequest(new { message = "Error al registrar el usuario. Comprueba los datos o si el usuario ya existe." });
        }

        // Login de usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            // Le pasamos el DTO recibido al servicio de autenticación
            var response = await _authService.LoginAsync(model);

            // Si la respuesta es correcta, devolvemos los datos que nos da el servicio (Token, expiración, isArtist)
            // Si la respuesta es null, devolvemos el Unauthorized
            return response != null
                ? Ok(response)
                : Unauthorized(new { message = "Credenciales inválidas" });
        }

        // Logout de usuario
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok();
        }
    }
}
