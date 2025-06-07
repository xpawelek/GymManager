using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Member;

using AdminUpdateDto = GymManager.Shared.DTOs.Admin.UpdateMembershipDto;
using MemberUpdateDto = GymManager.Shared.DTOs.Member.UpdateMembershipDto;


namespace GymManager.Client.Services
{
    public class MembershipService
    {
        private readonly HttpClient _http;

        public MembershipService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/memberships
        // Admin: wszystkie, Member: tylko własne
        public async Task<HttpResponseMessage> GetAllRawAsync()
        {
            return await _http.GetAsync("api/memberships");
        }

        public async Task<List<ReadMembershipDto>?> GetAllAdminAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadMembershipDto>>("api/memberships");
        }

        public async Task<ReadMembershipDto?> GetOwnAsync()
        {
            return await _http.GetFromJsonAsync<ReadMembershipDto>("api/memberships");
        }

        // [GET] /api/memberships/{memberId}
        public async Task<ReadMembershipDto?> GetByMemberIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ReadMembershipDto>($"api/memberships/{id}");
        }

        public async Task<ReadSelfMembershipDto?> GetMyActiveMembershipAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<ReadSelfMembershipDto>("api/memberships");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        // [POST] /api/memberships
        // Admin / Member
        public async Task<ReadMembershipDto?> CreateAsAdminAsync(CreateMembershipDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PostAsync("api/memberships", new StringContent(json, Encoding.UTF8, "application/json"));
            return await response.Content.ReadFromJsonAsync<ReadMembershipDto>();
        }

        public async Task<ReadMembershipDto?> CreateAsMemberAsync(CreateSelfMembershipDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PostAsync("api/memberships", new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("CreateAsMemberAsync failed: " + content); 
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ReadMembershipDto>();
        }

        // [PATCH] /api/memberships/admin/{id}
        // Admin
        public async Task<bool> PatchAdminAsync(int id, AdminUpdateDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/memberships/admin/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // [PATCH] /api/memberships/self
        // Member
        public async Task<bool> PatchOwnAsync(MemberUpdateDto dto)
        {
            var response = await _http.PatchAsJsonAsync("api/memberships/self", dto);
            return response.IsSuccessStatusCode;
        }

        // [DELETE] /api/memberships/{id}
        // Admin
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/memberships/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
