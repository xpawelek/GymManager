using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagerWebApp.Pages.Memberships
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _factory;
        public List<ReadMembershipDto> MembershipsList { get; set; } = new();

        public IndexModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _factory.CreateClient("GymApi");
                var response = await client.GetAsync("api/memberships");

                if (response.IsSuccessStatusCode)
                {
                    MembershipsList = await response.Content
                        .ReadFromJsonAsync<List<ReadMembershipDto>>()
                        ?? new List<ReadMembershipDto>();
                }
                else
                {
                    ViewData["ErrorMessage"] = $"B³¹d serwera: {(int)response.StatusCode} {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "B³¹d przy pobieraniu karnetów: " + ex.Message;
            }
        }
    }

    public class ReadMembershipDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int MembershipTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
