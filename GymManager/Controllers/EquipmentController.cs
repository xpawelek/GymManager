using GymManager.Models.DTOs.Admin;
using GymManager.Models.Entities;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Controllers;

[ApiController]
[Route("api/equipment")]
public class EquipmentController : ControllerBase
{
    private readonly AdminEquipmentService _adminService;
    private readonly MemberEquipmentService _memberService;
    private readonly TrainerEquipmentService _trainerService;
    private readonly IHttpContextAccessor _httpContext;

    public EquipmentController(AdminEquipmentService adminService,
        MemberEquipmentService memberService,
        TrainerEquipmentService trainerService,
        IHttpContextAccessor httpContextAccessor)
    {
        _adminService = adminService;
        _memberService = memberService;
        _trainerService = trainerService;
        _httpContext = httpContextAccessor;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var role = GetUserRole();

        switch (role)
        {
            case "Admin":
                return Ok(await _adminService.GetAllAsync());
            case "Member":
                return Ok(await _memberService.GetAllAsync());
            case "Trainer":
                return Ok(await _trainerService.GetAllAsync());
            default:
                return Forbid();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddEquipment([FromBody] CreateEquipmentDto dto)
    {
        if (GetUserRole() != "Admin")
            return Forbid();

        var result = await _adminService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] UpdateEquipmentDto dto)
    {
        if (GetUserRole() != "Admin")
            return Forbid();

        var success = await _adminService.PatchAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if(GetUserRole() != "Admin")
            return Forbid();

        var success = await _adminService.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
    private string GetUserRole()
    {
        return "Admin";
    }
}