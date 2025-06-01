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
            [Required(ErrorMessage = "Podaj imi�.")]
            [StringLength(50)]
            public string FirstName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Podaj nazwisko.")]
            [StringLength(50)]
            public string LastName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Podaj email.")]
            [EmailAddress(ErrorMessage = "Nieprawid�owy format email.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Podaj has�o.")]
            [DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Has�o musi mie� min. 6 znak�w.")]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "Podaj dat� urodzenia.")]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }

            [Required(ErrorMessage = "Podaj numer telefonu.")]
            [Phone(ErrorMessage = "Nieprawid�owy numer telefonu.")]
            public string PhoneNumber { get; set; } = string.Empty;
        }

        public void OnGet()
        {
            // Wy�wietlenie formularza rejestracji
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

                // DTO dla rejestracji � musi odpowiada� typowi CreateServiceRequestDto w Member API:
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
                    // Po poprawnej rejestracji przekieruj na stron� logowania
                    return RedirectToPage("/Account/Login");
                }
                else
                {
                    // Pobierz, je�li jest, tre�� b��du z API
                    var error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Rejestracja nie powiod�a si�: {error}");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Wyst�pi� b��d: " + ex.Message);
                return Page();
            }
        }

        // Model DTO ten sam, kt�ry u�ywa backend:
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
