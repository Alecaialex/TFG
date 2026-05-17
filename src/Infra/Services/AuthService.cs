using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly AppDbContext _context;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService,
        AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _context = context;
    }

    // Registro de nuevo usuario
    public async Task<AuthResponseDto?> RegisterAsync(RegisterDTO model)
    {
        // Creamos el objeto del usuario
        var user = new User
        {
            UserName = model.DisplayName,
            Email = model.Email,
            JoinedAt = DateTime.UtcNow
        };

        // Intentamos crear el usuario con UserManager
        var result = await _userManager.CreateAsync(user, model.Password);

        // Si no funciona se devuelve null para devolver error en el controlador
        if (!result.Succeeded) return null;

        // Asignamos si va a ser artista o cliente
        if (model.IsArtist)
        {
            _context.Artists.Add(new Artist { Id = user.Id });
        }
        else
        {
            _context.Clients.Add(new Client
            {
                Id = user.Id,
                DisplayName = model.DisplayName
            });
        }

        await _context.SaveChangesAsync();

        return _tokenService.CreateToken(user, model.IsArtist);
    }

    // Login de usuario
    public async Task<AuthResponseDto?> LoginAsync(LoginDTO model)
    {
        // Buscamos el usuario con UserManager
        var user = await _userManager.FindByEmailAsync(model.Email);

        // Si no existe devolvemos null para devolver error en controlador
        if (user == null) return null;

        // Se comprueba la contraseña
        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (!result.Succeeded) return null;

        bool isArtist = await _context.Artists.AnyAsync(a => a.Id == user.Id);

        // Creamos el token y lo devolvemos al controlador
        return _tokenService.CreateToken(user, isArtist);
    }

    // Logout de usuario
    public async Task LogoutAsync() => await _signInManager.SignOutAsync();
}