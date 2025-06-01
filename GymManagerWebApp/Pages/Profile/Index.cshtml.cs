using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace GymManagerWebApp.Pages.Trainer
{
    public class ProfileModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public ReadTrainerDto? TrainerProfile { get; set; }

        public ProfileModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _factory.CreateClient("GymApi");
                var response = await client.GetAsync("api/trainers/profile");
                if (response.IsSuccessStatusCode)
                {
                    TrainerProfile = await response.Content.ReadFromJsonAsync<ReadTrainerDto>();
                }
                else
                {
                    ViewData["ErrorMessage"] = $"B³¹d serwera: {(int)response.StatusCode} {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "B³¹d przy pobieraniu profilu trenera: " + ex.Message;
            }
        }
    }

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
