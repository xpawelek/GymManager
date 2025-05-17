using System.Security.Claims;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.DTOs.Member;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/memberships")]
    public class MembershipController : ControllerBase
    {
        private readonly AdminMembershipService _adminService;
        private readonly MemberSelfMembershipService _memberService;

        public MembershipController(
            AdminMembershipService adminService,
            MemberSelfMembershipService memberService)
        {
            _adminService = adminService;
            _memberService = memberService;
        }

        private string GetUserRole() => "Admin";
        private int? GetUserId() =>
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

        // GET /api/memberships
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            switch (GetUserRole())
            {
                case "Admin":
                    return Ok(await _adminService.GetAllAsync());
                case "Member":
                    return Ok(await _memberService.GetOwnAsync());
                default:
                    return Forbid();
            }
        }

        // GET /api/memberships/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            switch (GetUserRole())
            {
                case "Admin":
                    return Ok(await _adminService.GetByIdAsync(id));
                case "Member":
                    if (GetUserId() != id) return Forbid();
                    return Ok(await _memberService.GetOwnAsync());
                default:
                    return Forbid();
            }
        }

        // POST /api/memberships
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMembershipDto dto)
        {
            if (GetUserRole() != "Admin") return Forbid();
            var result = await _adminService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PATCH /api/memberships/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateMembershipDto dto)
        {
            if (GetUserRole() == "Admin")
            {
                var success = await _adminService.PatchAsync(id, dto);
                return success ? NoContent() : NotFound();
            }
            if (GetUserRole() == "Member" && GetUserId() == id)
            {
                var success = await _memberService.UpdateOwnAsync(dto);
                return success ? NoContent() : NotFound();
            }
            return Forbid();
        }

        // DELETE /api/memberships/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (GetUserRole() != "Admin") return Forbid();
            var success = await _adminService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
