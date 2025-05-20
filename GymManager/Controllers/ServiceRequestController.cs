using System.Security.Claims;
using System.Text.Json;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminCreateDto = GymManager.Models.DTOs.Admin.CreateServiceRequestDto;
using AdminUpdateDto = GymManager.Models.DTOs.Admin.UpdateServiceRequestDto;
using MemberCreateDto = GymManager.Models.DTOs.Member.CreateServiceRequestDto;
using TrainerCreateDto = GymManager.Models.DTOs.Trainer.CreateServiceRequestDto;

namespace GymManager.Controllers
{
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

        // GET all – tylko Admin
        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _admin.GetAllAsync();
            return Ok(list);
        }

        // GET by id – tylko Admin
        [HttpGet("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _admin.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        // POST – Admin/Member/Trainer mogą zgłaszać
        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Member + "," + RoleConstants.Trainer)]
        public async Task<IActionResult> Create([FromBody] object rawDto)
        {
            switch (Role)
            {
                case RoleConstants.Admin:
                    {
                        var dto = JsonSerializer.Deserialize<AdminCreateDto>(rawDto.ToString()!)!;
                        var r = await _admin.CreateAsync(dto);
                        return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
                    }
                case RoleConstants.Member:
                    {
                        var dto = JsonSerializer.Deserialize<MemberCreateDto>(rawDto.ToString()!)!;
                        var r = await _member.CreateAsync(dto);
                        return Accepted(new { message = "Your request has been accepted." });
                    }
                case RoleConstants.Trainer:
                    {
                        var dto = JsonSerializer.Deserialize<TrainerCreateDto>(rawDto.ToString()!)!;
                        var r = await _trainer.CreateAsync(dto);
                        return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
                    }
                default:
                    return Forbid();
            }
        }

        // PATCH – tylko Admin
        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Patch(int id, [FromBody] AdminUpdateDto dto)
        {
            var ok = await _admin.PatchAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        // DELETE – tylko Admin
        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _admin.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
