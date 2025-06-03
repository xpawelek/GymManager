using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Admin;

namespace GymManager.Client.Services
{
    using AdminCreateDto = GymManager.Shared.DTOs.Admin.CreateMembershipTypeDto;
    using AdminUpdateDto = GymManager.Shared.DTOs.Admin.UpdateMembershipTypeDto;
    using ReadDto = GymManager.Shared.DTOs.Member.ReadMembershipTypeDto;

    public class MembershipTypeService
    {
        private readonly HttpClient _http;

        public MembershipTypeService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/membership-types/public
        // Dostęp publiczny
        public async Task<List<ReadDto>?> GetPublicAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadDto>>("api/membership-types/public");
        }

        // [GET] /api/membership-types
        // Zwraca listę typów subskrypcji dla aktualnie zalogowanej roli
        public async Task<List<ReadDto>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadDto>>("api/membership-types");
        }

        // [GET] /api/membership-types/{id}
        // Zwraca jeden typ dla aktualnej roli
        public async Task<ReadDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ReadDto>($"api/membership-types/{id}");
        }

        // [POST] /api/membership-types
        // Tylko Admin
        public async Task<ReadDto?> CreateAsync(AdminCreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/membership-types", dto);
            return await response.Content.ReadFromJsonAsync<ReadDto>();
        }

        // [PATCH] /api/membership-types/{id}
        // Tylko Admin
        public async Task<bool> PatchAsync(int id, AdminUpdateDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/membership-types/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // [DELETE] /api/membership-types/{id}
        // Tylko Admin
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/membership-types/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
