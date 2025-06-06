using System.Security.Claims;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminCreateDto = GymManager.Shared.DTOs.Admin.CreateServiceRequestDto;
using AdminUpdateDto = GymManager.Shared.DTOs.Admin.UpdateServiceRequestDto;
using MemberCreateDto = GymManager.Shared.DTOs.Member.CreateServiceRequestDto;
using TrainerCreateDto = GymManager.Shared.DTOs.Trainer.CreateServiceRequestDto;

namespace GymManager.Controllers;

[ApiController]
[Route("api/service-requests")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ServiceRequestsController : ControllerBase
{
    private readonly AdminServiceRequestService _admin;
    private readonly MemberServiceRequestService _member;
    private readonly TrainerServiceRequestService _trainer;

    public ServiceRequestsController(
        AdminServiceRequestService admin,
        MemberServiceRequestService member,
        TrainerServiceRequestService trainer)
    {
        _admin = admin;
        _member = member;
        _trainer = trainer;
    }

    private string Role => User.FindFirstValue(ClaimTypes.Role)!;

    // get - admin

    [HttpGet]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> GetAll()
    {
        var list = await _admin.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> GetById(int id)
    {
        var dto = await _admin.GetByIdAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    // put/delete - admin

    [HttpPatch("{id}/toggle")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> ToggleResolved(int id)
    {
        var ok = await _admin.ToggleResolvedAsync(id);
        return ok ? NoContent() : NotFound();
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Patch(int id, [FromBody] AdminUpdateDto dto)
    {
        var ok = await _admin.PatchAsync(id, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _admin.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }

    // post member/trainer

    [HttpPost("member")]
    [Authorize(Roles = $"{RoleConstants.Member},{RoleConstants.Trainer}")]
    public async Task<IActionResult> CreateAsMember([FromBody] MemberCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.ServiceProblemTitle) || string.IsNullOrWhiteSpace(dto.ProblemNote))
            return BadRequest(new { message = "Missing required fields." });

        var ok = await _member.CreateAsync(dto);
        return ok
            ? Accepted(new { message = "Your request has been accepted." })
            : BadRequest(new { message = "Something went wrong." });
    }
}
