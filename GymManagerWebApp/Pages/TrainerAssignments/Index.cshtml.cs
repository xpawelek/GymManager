using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagerWebApp.Pages.TrainerAssignments
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public List<ReadTrainerAssignmentDto> AssignmentsList { get; set; } = new();

        public IndexModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _factory.CreateClient("GymApi");
                var response = await client.GetAsync("api/trainer-assignments/me");
                if (response.IsSuccessStatusCode)
                {
                    AssignmentsList = await response.Content
                        .ReadFromJsonAsync<List<ReadTrainerAssignmentDto>>()
                        ?? new List<ReadTrainerAssignmentDto>();
                }
                else
                {
                    ViewData["ErrorMessage"] = $"B��d serwera: {(int)response.StatusCode} {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "B��d przy pobieraniu przypisa�: " + ex.Message;
            }
        }
    }

    public class ReadTrainerAssignmentDto
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int MemberId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
