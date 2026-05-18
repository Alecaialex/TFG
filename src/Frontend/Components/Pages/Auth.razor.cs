using Frontend.Services;
using Frontend.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;
using Infra.Services;

namespace Frontend.Components.Pages
{
    public partial class Auth
    {
        [Inject] IAuthService AuthService { get; set; } = default!;
        [Inject] NavigationManager Nav { get; set; } = default!;
        [Inject] AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

        bool showLogin = false;
        bool showPassword = false;
        bool showConfirm = false;
        string? errorMessage;

        RegisterModel registerModel = new();
        LoginModel loginModel = new();

        async Task HandleSubmit()
        {
            if (showLogin)
                await HandleLogin();
            else
                await HandleRegister();
        }

        async Task HandleRegister()
        {
            errorMessage = null;
            try 
            {
                var result = await Infra.Services.apiclient
                var result = await AuthService.RegisterAsync(
                    registerModel.Email,
                    registerModel.Password,
                    registerModel.DisplayName,
                    registerModel.IsArtist
                );

                if (result != null)
                {
                    var customAuthStateProvider = (CustomAuthStateProvider)AuthStateProvider;
                    await customAuthStateProvider.MarkUserAsAuthenticated(result.Token);
                    Nav.NavigateTo("/dashboard");
                    return;
                }
            }
            catch 
            {
                // Silently handle API failures for now
            }

            // SIMULAMOS EL REGISTRO MIENTRAS AZURE ESTÁ BLOQUEADO
            var dummyToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiTW9jayBBcnRpc3QiLCJlbWFpbCI6Im1vY2tAbW9jay5jb20iLCJpc0FydGlzdCI6InRydWUifQ.dummy";
            var authProvider = (CustomAuthStateProvider)AuthStateProvider;
            await authProvider.MarkUserAsAuthenticated(dummyToken);
            Nav.NavigateTo("/dashboard");
        }

        async Task HandleLogin()
        {
            errorMessage = null;
            try 
            {
                var result = await AuthService.LoginAsync(loginModel.Email, loginModel.Password);

                if (result != null)
                {
                    var customAuthStateProvider = (CustomAuthStateProvider)AuthStateProvider;
                    await customAuthStateProvider.MarkUserAsAuthenticated(result.Token);
                    Nav.NavigateTo("/dashboard");
                    return;
                }
            }
            catch 
            {
                // Silently handle API failures for now
            }

            // SIMULAMOS EL LOGIN MIENTRAS AZURE ESTÁ BLOQUEADO
            var dummyToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiTW9jayBBcnRpc3QiLCJlbWFpbCI6Im1vY2tAbW9jay5jb20iLCJpc0FydGlzdCI6InRydWUifQ.dummy";
            var authProvider = (CustomAuthStateProvider)AuthStateProvider;
            await authProvider.MarkUserAsAuthenticated(dummyToken);
            Nav.NavigateTo("/dashboard");
        }

        class RegisterModel
        {
            [Required, EmailAddress]
            public string Email { get; set; } = "";

            [Required, MinLength(8)]
            public string Password { get; set; } = "";

            [Required, Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
            public string ConfirmPassword { get; set; } = "";

            [Required]
            public string DisplayName { get; set; } = "";

            public bool IsArtist { get; set; } = false;
        }

        class LoginModel
        {
            [Required, EmailAddress]
            public string Email { get; set; } = "";

            [Required]
            public string Password { get; set; } = "";
        }
    }
}
