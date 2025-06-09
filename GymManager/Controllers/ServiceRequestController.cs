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
    private readonly ILogger<ServiceRequestsController> _logger;

    public ServiceRequestsController(
        AdminServiceRequestService admin,
        MemberServiceRequestService member,
        TrainerServiceRequestService trainer,
        ILogger<ServiceRequestsController> logger)
    {
        _admin = admin;
        _member = member;
        _trainer = trainer;
        _logger = logger;
    }

    private string Role => User.FindFirstValue(ClaimTypes.Role)!;

    [HttpGet]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = await _admin.GetAllAsync();
            return Ok(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{Time}] Error while retrieving all service requests.", DateTime.Now);
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var dto = await _admin.GetByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{Time}] Error while retrieving service request by ID {Id}.", DateTime.Now, id);
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpPatch("{id}/toggle")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> ToggleResolved(int id)
    {
        try
        {
            var ok = await _admin.ToggleResolvedAsync(id);
            return ok ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{Time}] Error while toggling service request status. ID: {Id}", DateTime.Now, id);
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Patch(int id, [FromBody] AdminUpdateDto dto)
    {
        try
        {
            var ok = await _admin.PatchAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{Time}] Error while updating service request. ID: {Id}", DateTime.Now, id);
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var ok = await _admin.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{Time}] Error while deleting service request. ID: {Id}", DateTime.Now, id);
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpPost("member")]
    [Authorize(Roles = $"{RoleConstants.Member},{RoleConstants.Trainer}")]
    public async Task<IActionResult> CreateAsMember([FromBody] MemberCreateDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.ServiceProblemTitle) || string.IsNullOrWhiteSpace(dto.ProblemNote))
                return BadRequest(new { message = "Missing required fields." });

            var ok = await _member.CreateAsync(dto);
            return ok
                ? Accepted(new { message = "Your request has been accepted." })
                : BadRequest(new { message = "Something went wrong." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{Time}] Error while creating service request from member.", DateTime.Now);
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}
