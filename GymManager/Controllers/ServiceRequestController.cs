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
    [Route("api/service-requests")]
    public class ServiceRequestController : ControllerBase
    {
        private readonly AdminServiceRequestService _adminService;
        private readonly MemberServiceRequestService _memberService;
        private readonly TrainerServiceRequestService _trainerService;

        public ServiceRequestController(
            AdminServiceRequestService adminService,
            MemberServiceRequestService memberService,
            TrainerServiceRequestService trainerService)
        {
            _adminService = adminService;
            _memberService = memberService;
            _trainerService = trainerService;
        }

        private string GetUserRole() => "Member";
        private int? GetUserId() =>
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

        // GET /api/service-requests
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (GetUserRole() != "Admin")
                return Forbid();

            return Ok(await _adminService.GetAllAsync());
        }

        // GET /api/service-requests/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (GetUserRole() != "Admin")
                return Forbid();

            return Ok(await _adminService.GetByIdAsync(id));
        }

        // PATCH /api/service-requests/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateServiceRequestDto dto)
        {
            if (GetUserRole() != "Admin")
                return Forbid();

            var ok = await _adminService.PatchAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        // DELETE /api/service-requests/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (GetUserRole() != "Admin")
                return Forbid();

            var ok = await _adminService.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }

        // POST /api/service-requests
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceRequestDto dto)
        {
            if (GetUserRole() == "Member")
            {
                var r = await _memberService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
            }
            if (GetUserRole() == "Trainer")
            {
                var r = await _trainerService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
            }

            // tylko Member i Trainer mogą tu POSTować
            return Forbid();
        }
    }
}
