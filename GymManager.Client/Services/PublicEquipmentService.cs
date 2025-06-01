using System.Net.Http.Json;
using GymManager.Shared.DTOs.Admin;


public class PublicEquipmentService
{
    private readonly HttpClient _http;

    public PublicEquipmentService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ReadEquipmentDto>?> GetPublicEquipmentAsync()
    {
        return await _http.GetFromJsonAsync<List<ReadEquipmentDto>>("api/equipment/public");
    }
}