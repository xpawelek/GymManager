using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Member;

namespace GymManager.Client.Services
{
    public class MemberService
    {
        private readonly HttpClient _http;

        public MemberService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/members
        // Admin
        public async Task<List<ReadMemberDto>?> GetAllAdminAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadMemberDto>>("api/members");
        }

        // [GET] /api/members/{id}
        // Admin
        public async Task<ReadMemberDto?> GetByIdAdminAsync(int id)
        {
            return await _http.GetFromJsonAsync<ReadMemberDto>($"api/members/{id}");
        }
        
        // [POST] /api/auth/admin/register-member
        // Admin
        public async Task<string?> RegisterMemberAsync(RegisterMemberDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/admin/register-member", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsStringAsync(); 
        }

        // [PATCH] /api/members/{id}
        // Admin
        public async Task<bool> PatchAdminAsync(int id, UpdateMemberDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/members/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // [DELETE] /api/members/{id}
        // Admin
        public async Task<bool> DeleteAdminAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/members/{id}");
            return response.IsSuccessStatusCode;
        }


        // [GET] /api/members/self
        // member
        public async Task<ReadSelfMemberDto?> GetSelfAsync()
        {
            return await _http.GetFromJsonAsync<ReadSelfMemberDto>("api/members/self");
        }

        // [PATCH] /api/members/self
        // member
        public async Task<bool> UpdateSelfAsync(UpdateSelfMemberDto dto)
        {
            var response = await _http.PatchAsJsonAsync("api/members/self", dto);
            return response.IsSuccessStatusCode;
        }

        // [GET] /api/members/assigned
        // Trener
        public async Task<List<ReadMemberDto>?> GetAssignedAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadMemberDto>>("api/members/assigned");
        }

        // [GET] /api/members/assigned/{id}
        // Trener
        public async Task<ReadMemberDto?> GetAssignedByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ReadMemberDto>($"api/members/assigned/{id}");
        }
    }
}
