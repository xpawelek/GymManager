using Blazored.LocalStorage;
using Microsoft.JSInterop;

namespace GymManager.Client.Services;

public class AuthStateService
{
    public bool IsLoggedIn { get; set; }
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
        
        OnChange?.Invoke();
    }
    public void SetLoginState(bool isLoggedIn)
    {
        IsLoggedIn = isLoggedIn;
        OnChange?.Invoke();
    }
    
}