using Blazored.LocalStorage;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;

namespace GymManager.Client.Services;

public class AuthStateService
{
    public bool IsLoggedIn { get; set; }
    public string? UserRole { get; private set; }
    public event Action? OnChange;
    private readonly ILocalStorageService _localStorage;

    public AuthStateService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task InitializeAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        IsLoggedIn = !string.IsNullOrWhiteSpace(token);

        if (IsLoggedIn)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            UserRole = jwtToken.Claims.FirstOrDefault(claim => claim.Type.Contains("role"))?.Value; 
            SetLoginState(IsLoggedIn, UserRole);
        }
        
        OnChange?.Invoke();
    }
    public void SetLoginState(bool isLoggedIn, string? role = null)
    {
        IsLoggedIn = isLoggedIn;
        UserRole = role;
        OnChange?.Invoke();
    }
    
}