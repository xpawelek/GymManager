using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace GymManagerWebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public LoginModel(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; } = new();

        public class LoginInputModel
        {
            [Required(ErrorMessage = "Prosz� poda� adres e-mail.")]
            [EmailAddress(ErrorMessage = "Nieprawid�owy format e-mail.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Prosz� poda� has�o.")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }

        public void OnGet()
        {
            // Wy�wietlenie formularza logowania
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Je�li walidacja nie przesz�a, zosta� na stronie
                return Page();
            }

            try
            {
                var client = _httpFactory.CreateClient("GymApi");
                // Wywo�aj POST /api/auth/login
                var response = await client.PostAsJsonAsync("api/auth/login", new
                {
                    Email = Input.Email,
                    Password = Input.Password
                });

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Nieprawid�owy login lub has�o.");
                    return Page();
                }

                // Je�eli 200 OK, zdeserializuj odpowied�
                var loginResult = await response.Content.ReadFromJsonAsync<LoginResultDto>();
                if (loginResult == null)
                {
                    ModelState.AddModelError(string.Empty, "B��d podczas odczytu odpowiedzi serwera.");
                    return Page();
                }

                // Utw�rz ClaimsPrincipal i zaloguj cookie
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, loginResult.UserId),
                    new Claim(ClaimTypes.Email, loginResult.Email)
                };
                foreach (var role in loginResult.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);

                // Zapisz sesj� cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Przekieruj na stron� g��wn� lub wybran�
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Wyst�pi� b��d serwera: " + ex.Message);
                return Page();
            }
        }

        public class LoginResultDto
        {
            public string Token { get; set; } = string.Empty;
            public string UserId { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public List<string> Roles { get; set; } = new();
        }
    }
}
