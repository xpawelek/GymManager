using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagerWebApp.Pages.TrainingSessions
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public List<ReadTrainingSessionDto> SessionsList { get; set; } = new();

        public IndexModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _factory.CreateClient("GymApi");
                var response = await client.GetAsync("api/training-sessions/me");
                if (response.IsSuccessStatusCode)
                {
                    SessionsList = await response.Content
                        .ReadFromJsonAsync<List<ReadTrainingSessionDto>>()
                        ?? new List<ReadTrainingSessionDto>();
                }
                else
                {
                    ViewData["ErrorMessage"] = $"B³¹d serwera: {(int)response.StatusCode} {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "B³¹d przy pobieraniu treningów: " + ex.Message;
            }
        }
    }

    public class ReadTrainingSessionDto
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationInMinutes { get; set; }
        public bool IsGroupSession { get; set; }
        public int? MemberId { get; set; }
    }
}
