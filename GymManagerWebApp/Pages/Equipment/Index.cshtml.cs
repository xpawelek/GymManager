using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagerWebApp.Pages.Equipment
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public List<ReadEquipmentDto> EquipmentList { get; set; } = new();

        public IndexModel(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpFactory.CreateClient("GymApi");
                var response = await client.GetAsync("api/equipment");

                if (response.IsSuccessStatusCode)
                {
                    EquipmentList = await response.Content
                        .ReadFromJsonAsync<List<ReadEquipmentDto>>()
                        ?? new List<ReadEquipmentDto>();
                }
                else
                {
                    ViewData["ErrorMessage"] = $"B³¹d serwera: {(int)response.StatusCode} {response.ReasonPhrase}";
                    EquipmentList = new List<ReadEquipmentDto>();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Wyst¹pi³ b³¹d podczas pobierania danych: " + ex.Message;
                EquipmentList = new List<ReadEquipmentDto>();
            }
        }
    }

    public class ReadEquipmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
