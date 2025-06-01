using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagerWebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public RegisterModel(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; } = new();

        public class RegisterInputModel
        {
            [Required(ErrorMessage = "Podaj imiê.")]
            [StringLength(50)]
            public string FirstName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Podaj nazwisko.")]
            [StringLength(50)]
            public string LastName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Podaj email.")]
            [EmailAddress(ErrorMessage = "Nieprawid³owy format email.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Podaj has³o.")]
            [DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Has³o musi mieæ min. 6 znaków.")]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "Podaj datê urodzenia.")]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }

            [Required(ErrorMessage = "Podaj numer telefonu.")]
            [Phone(ErrorMessage = "Nieprawid³owy numer telefonu.")]
            public string PhoneNumber { get; set; } = string.Empty;
        }

        public void OnGet()
        {
            // Wyœwietlenie formularza rejestracji
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var client = _httpFactory.CreateClient("GymApi");

                // DTO dla rejestracji — musi odpowiadaæ typowi CreateServiceRequestDto w Member API:
                var requestDto = new RegisterMemberDto
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Email = Input.Email,
                    Password = Input.Password,
                    DateOfBirth = Input.DateOfBirth,
                    PhoneNumber = Input.PhoneNumber
                };

                var response = await client.PostAsJsonAsync("api/auth/register-member", requestDto);

                if (response.IsSuccessStatusCode)
                {
                    // Po poprawnej rejestracji przekieruj na stronê logowania
                    return RedirectToPage("/Account/Login");
                }
                else
                {
                    // Pobierz, jeœli jest, treœæ b³êdu z API
                    var error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Rejestracja nie powiod³a siê: {error}");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Wyst¹pi³ b³¹d: " + ex.Message);
                return Page();
            }
        }

        // Model DTO ten sam, który u¿ywa backend:
        public class RegisterMemberDto
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public DateTime DateOfBirth { get; set; }
            public string PhoneNumber { get; set; } = string.Empty;
        }
    }
}
