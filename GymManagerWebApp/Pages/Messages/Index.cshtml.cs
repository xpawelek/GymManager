using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace GymManagerWebApp.Pages.Messages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public List<ReadMessageDto> MessagesList { get; set; } = new();
        public bool IsMemberRole { get; set; }
        public bool IsTrainerRole { get; set; }

        public IndexModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _factory.CreateClient("GymApi");
                HttpResponseMessage response;

                if (User.IsInRole("Member"))
                {
                    IsMemberRole = true;
                    response = await client.GetAsync("api/messages");
                }
                else if (User.IsInRole("Trainer"))
                {
                    IsTrainerRole = true;
                    response = await client.GetAsync("api/messages");
                }
                else if (User.IsInRole("Admin"))
                {
                    response = await client.GetAsync("api/messages");
                }
                else
                {
                    // Je�li �adna rola, forbit
                    ViewData["ErrorMessage"] = "Brak dost�pu do tej sekcji.";
                    return;
                }

                if (response.IsSuccessStatusCode)
                {
                    MessagesList = await response.Content
                        .ReadFromJsonAsync<List<ReadMessageDto>>()
                        ?? new List<ReadMessageDto>();
                }
                else
                {
                    ViewData["ErrorMessage"] = $"B��d serwera: {(int)response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "B��d przy pobieraniu wiadomo�ci: " + ex.Message;
            }
        }
    }

    public class ReadMessageDto
    {
        public int Id { get; set; }
        public string MessageContent { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        // Je�li rola = Member, to w DTO b�dzie �TrainerId�
        public int? TrainerId { get; set; }

        // Je�li rola = Trainer, to w DTO b�dzie �MemberId�
        public int? MemberId { get; set; }
    }
}
