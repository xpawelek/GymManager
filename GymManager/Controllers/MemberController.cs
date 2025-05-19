using System.Security.Claims;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.DTOs.Member;
using GymManager.Models.DTOs.Trainer;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MemberController : ControllerBase
    {
        private readonly AdminMemberService _adminService;
        private readonly MemberSelfService _memberSelfService;
        private readonly TrainerMemberService _trainerService;

        public MemberController(
            AdminMemberService adminService,
            MemberSelfService memberSelfService,
            TrainerMemberService trainerService)
        {
            _adminService = adminService;
            _memberSelfService = memberSelfService;
            _trainerService = trainerService;
        }

        private string GetUserRole() => "Admin";

        private int? GetUserId() =>
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

        // GET /api/members
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
                    return Ok(await _memberSelfService.GetOwnAsync());
                default:
                    return Forbid();
            }
        }

        // GET /api/members/{id}
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
                    if (GetUserId() != id) return Forbid();
                    return Ok(await _memberSelfService.GetOwnAsync());
                default:
                    return Forbid();
            }
        }

        // POST /api/members
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMemberDto dto)
        {
            if (GetUserRole() != "Admin")
                return Forbid();

            var result = await _adminService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PATCH /api/members/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateMemberDto dto)
        {
            if (GetUserRole() == "Admin")
            {
                var success = await _adminService.PatchAsync(id, dto);
                return success ? NoContent() : NotFound();
            }
            if (GetUserRole() == "Member" && GetUserId() == id)
            {
                var success = await _memberSelfService.UpdateOwnAsync(dto);
                return success ? NoContent() : NotFound();
            }
            return Forbid();
        }   

        // DELETE /api/members/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (GetUserRole() != "Admin")
                return Forbid();

            var success = await _adminService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
