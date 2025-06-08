using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Member;
using GymManager.Shared.DTOs.Trainer;
using Microsoft.AspNetCore.Mvc;
using ACreateDto = GymManager.Shared.DTOs.Admin.CreateTrainingSessionDto;
using AUpdateDto = GymManager.Shared.DTOs.Admin.UpdateTrainingSessionDto;
using AReadDto = GymManager.Shared.DTOs.Admin.ReadTrainingSessionDto;

using MCreateDto = GymManager.Shared.DTOs.Member.CreateTrainingSessionDto;
using MUpdateDto = GymManager.Shared.DTOs.Member.UpdateTrainingSessionDto;
using MReadDto = GymManager.Shared.DTOs.Member.ReadTrainingSessionDto;

using TReadDto = GymManager.Shared.DTOs.Trainer.ReadTrainingSessionDto;

namespace GymManager.Client.Services
{
    public class TrainingSessionService
    {
        private readonly HttpClient _http;

        public TrainingSessionService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/training-sessions
        // admin
        public async Task<List<AReadDto>?> GetAllAdminAsync()
        {
            return await _http.GetFromJsonAsync<List<AReadDto>>("api/training-sessions");
        }

        // [GET] /api/training-sessions/{id}
        // admin
        public async Task<AReadDto?> GetByIdAdminAsync(int memberId)
        {
            return await _http.GetFromJsonAsync<AReadDto>($"api/training-sessions/{memberId}");
        }
        
        // [GET] /api/training-sessions/member/{memberId}/personal
        // admin
        public async Task<List<AReadDto>?> GetPersonalSessionsForMemberAsync(int memberId)
        {
            return await _http.GetFromJsonAsync<List<AReadDto>>(
                $"api/training-sessions/member/{memberId}/personal");
        }
        
        // [GET] /api/training-sessions/trainer/{trainerId}
        // admin
        public async Task<List<AReadDto>?> GetSessionsForTrainerAsync(int trainerId)
        {
            return await _http.GetFromJsonAsync<List<AReadDto>>(
                $"api/training-sessions/trainer/{trainerId}");
        }

        // [POST] /api/training-sessions
        // admin
        public async Task<AReadDto?> CreateAdminAsync(ACreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/training-sessions", dto);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                if (!string.IsNullOrWhiteSpace(content) && content.Trim().StartsWith("{"))
                {
                    try
                    {
                        using var doc = JsonDocument.Parse(content);
                        if (doc.RootElement.TryGetProperty("error", out var errorElement))
                        {
                            throw new Exception(errorElement.GetString());
                        }
                    }
                    catch
                    {
                        throw new Exception(content);
                    }
                }
                
                throw new Exception(content);
            }

            return await response.Content.ReadFromJsonAsync<AReadDto>();
        }
        
        // [PATCH] /api/training-sessions/{id}
        // admin
        public async Task<bool> PatchAdminAsync(int id, AUpdateDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/training-sessions/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // [DELETE] /api/training-sessions/{id}
        // admin
        public async Task<bool> DeleteAdminAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/training-sessions/{id}");
            return response.IsSuccessStatusCode;
        }
        
        // [DELETE] /api/training-sessions/self/{id}
        // mebmer
        public async Task<bool> DeleteMemberAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/training-sessions/self/{id}");
            return response.IsSuccessStatusCode;
        }

        // [POST] /api/training-sessions/self
        // member
        public async Task<MReadDto?> CreateMemberAsync(MCreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/training-sessions/self", dto);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                if (!string.IsNullOrWhiteSpace(content) && content.Trim().StartsWith("{"))
                {
                    try
                    {
                        using var doc = JsonDocument.Parse(content);
                        if (doc.RootElement.TryGetProperty("error", out var errorElement))
                        {
                            throw new Exception(errorElement.GetString());
                        }
                    }
                    catch
                    {
                        throw new Exception(content);
                    }
                }
                
                throw new Exception(content);
            }

            return await response.Content.ReadFromJsonAsync<MReadDto>();
        }

        // [PATCH] /api/training-sessions/self/{id}
        // member
        public async Task<bool> PatchMemberAsync(int id, MUpdateDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/training-sessions/self/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // [GET] /api/training-sessions/self
        // member
        public async Task<List<MReadDto>?> GetAllMemberPersonalAsync()
        {
            return await _http.GetFromJsonAsync<List<MReadDto>>("api/training-sessions/self");
        }

        // [GET] /api/training-sessions/self/{id}
        // member
        public async Task<MReadDto?> GetByIdMemberAsync(int id)
        {
            return await _http.GetFromJsonAsync<MReadDto>($"api/training-sessions/self/{id}");
        }

        // [GET] /api/training-sessions/public
        // public
        public async Task<List<MReadDto>?> GetAllPublicGroupAsync()
        {
            return await _http.GetFromJsonAsync<List<MReadDto>>("api/training-sessions/public");
        }

        // [GET] /api/training-sessions/me
        // trainer
        public async Task<List<TReadDto>?> GetAllTrainerAsync<TReadDto>()
        {
            return await _http.GetFromJsonAsync<List<TReadDto>>("api/training-sessions/me");
        }

        // [GET] /api/training-sessions/me/{id}
        // trainer
        public async Task<TReadDto?> GetByIdTrainerAsync(int id)
        {
            return await _http.GetFromJsonAsync<TReadDto>($"api/training-sessions/me/{id}");
        }
    }
}
