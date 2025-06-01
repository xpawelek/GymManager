using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace GymManagerWebApp.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public bool IsMember { get; set; } = false;
        public bool IsTrainer { get; set; } = false;
        public bool IsAdmin { get; set; } = false;

        public ReadSelfMemberDto? MemberProfile { get; set; }
        public ReadTrainerDto? TrainerProfile { get; set; }

        public ProfileModel(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public async Task OnGetAsync()
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                // Jeœli nie zalogowany, przekieruj do logowania
                Response.Redirect("/Account/Login");
                return;
            }

            // Ustal rolê:
            if (User.IsInRole("Member"))
            {
                IsMember = true;
                await LoadMemberProfileAsync();
            }
            else if (User.IsInRole("Trainer"))
            {
                IsTrainer = true;
                await LoadTrainerProfileAsync();
            }
            else if (User.IsInRole("Admin"))
            {
                IsAdmin = true;
            }
            else
            {
                ViewData["ErrorMessage"] = "Nieznana rola u¿ytkownika.";
            }
        }

        private async Task LoadMemberProfileAsync()
        {
            try
            {
                var client = _httpFactory.CreateClient("GymApi");
                // GET /api/members/self
                var response = await client.GetAsync("api/members/self");
                if (response.IsSuccessStatusCode)
                {
                    MemberProfile = await response.Content.ReadFromJsonAsync<ReadSelfMemberDto>();
                }
                else
                {
                    ViewData["ErrorMessage"] = $"B³¹d pobierania profilu cz³onka: {(int)response.StatusCode} {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Wyst¹pi³ b³¹d: " + ex.Message;
            }
        }

        private async Task LoadTrainerProfileAsync()
        {
            try
            {
                var client = _httpFactory.CreateClient("GymApi");
                // GET /api/trainers/profile
                var response = await client.GetAsync("api/trainers/profile");
                if (response.IsSuccessStatusCode)
                {
                    TrainerProfile = await response.Content.ReadFromJsonAsync<ReadTrainerDto>();
                }
                else
                {
                    ViewData["ErrorMessage"] = $"B³¹d pobierania profilu trenera: {(int)response.StatusCode} {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Wyst¹pi³ b³¹d: " + ex.Message;
            }
        }

        // DTO dla profilu cz³onka (zgodnie z ReadSelfMemberDto w backendzie)
        public class ReadSelfMemberDto
        {
            public int Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public DateTime DateOfBirth { get; set; }
            public string MembershipCardNumber { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
        }

        // DTO dla profilu trenera (zgodnie z ReadTrainerDto w backendzie)
        public class ReadTrainerDto
        {
            public int Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string PhotoPath { get; set; } = string.Empty;
        }
    }
}
