using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagerWebApp.Pages.ProgressPhotos
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public List<ReadProgressPhotoDto> PhotosList { get; set; } = new();

        public IndexModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _factory.CreateClient("GymApi");
                var response = await client.GetAsync("api/progress-photos");

                if (response.IsSuccessStatusCode)
                {
                    PhotosList = await response.Content
                        .ReadFromJsonAsync<List<ReadProgressPhotoDto>>()
                        ?? new List<ReadProgressPhotoDto>();
                }
                else
                {
                    ViewData["ErrorMessage"] = $"B³¹d serwera: {(int)response.StatusCode} {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "B³¹d przy pobieraniu zdjêæ progresu: " + ex.Message;
            }
        }
    }

    public class ReadProgressPhotoDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public string? Comment { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
    }
}
