using System.Security.Claims;
using GymManager.Models.DTOs.Admin;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Controllers;

    [ApiController]
    [Route("api/membership-types")]
    public class MembershipTypeController : ControllerBase
    {
    private readonly AdminMembershipTypeService _adminService;
    private readonly MemberMembershipTypeService _memberService;
    private readonly TrainerMembershipTypeService _trainerService;

    public MembershipTypeController(
        AdminMembershipTypeService adminService,
        MemberMembershipTypeService memberSelfService,
        TrainerMembershipTypeService trainerService)
    {
        _adminService = adminService;
        _memberService = memberSelfService;
        _trainerService = trainerService;
    }

    private string GetUserRole() => "Admin";

        private int? GetUserId() =>
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

        // GET /api/membership-types
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            switch (GetUserRole())
            {
                case "Admin":
                    return Ok(await _adminService.GetAllAsync());
                case "Trainer":
                    return Ok(await _trainerService.GetAllAsync());
                case "Member":
                    return Ok(await _memberService.GetAllAsync());
                default:
                    return Forbid();
            }
        }

        // GET /api/membership-types/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            switch (GetUserRole())
            {
                case "Admin":
                    return Ok(await _adminService.GetByIdAsync(id));
                case "Trainer":
                    return Ok(await _trainerService.GetByIdAsync(id));
                case "Member":
                    return Ok(await _memberService.GetByIdAsync(id));
                default:
                    return Forbid();
            }
        }

        // POST /api/membership-types
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMembershipTypeDto dto)
        {
            if (GetUserRole() != "Admin") return Forbid();
            var result = await _adminService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PATCH /api/membership-types/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateMembershipTypeDto dto)
        {
            if (GetUserRole() != "Admin") return Forbid();
            var success = await _adminService.PatchAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        // DELETE /api/membership-types/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (GetUserRole() != "Admin") return Forbid();
            var success = await _adminService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }