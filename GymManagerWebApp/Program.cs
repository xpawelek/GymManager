using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddHttpClient("GymApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5119/");
});

// Cookie Authentication
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // �cie�ka do strony logowania (je�li niezalogowany zostanie przekierowany tutaj)
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        // Po pomy�lnym zalogowaniu przekieruj na stron� g��wn�:
        options.AccessDeniedPath = "/Account/Login";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Uwaga: kolejno��: Authentication pierwsze, potem Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
