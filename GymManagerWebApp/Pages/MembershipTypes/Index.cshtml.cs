using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagerWebApp.Pages.MembershipTypes
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public List<ReadMembershipTypeDto> MembershipTypesList { get; set; } = new();

        public IndexModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _factory.CreateClient("GymApi");
                var response = await client.GetAsync("api/membership-types");

                if (response.IsSuccessStatusCode)
                {
                    MembershipTypesList = await response.Content
                        .ReadFromJsonAsync<List<ReadMembershipTypeDto>>()
                        ?? new List<ReadMembershipTypeDto>();
                }
                else
                {
                    ViewData["ErrorMessage"] = $"B³¹d: {(int)response.StatusCode} {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "B³¹d podczas pobierania: " + ex.Message;
            }
        }
    }

    public class ReadMembershipTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public bool IncludesPersonalTrainer { get; set; }
        public int? PersonalTrainingsPerMonth { get; set; }
        public bool AllowTrainerSelection { get; set; }
        public bool IncludesProgressTracking { get; set; }
        public bool IsVisible { get; set; }
    }
}
