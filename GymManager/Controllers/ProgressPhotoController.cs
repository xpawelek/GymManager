using System.Security.Claims;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using Microsoft.AspNetCore.Mvc;

// aliasy bo są dwie takie samy nazwy..
using AdminUpdateDto = GymManager.Models.DTOs.Admin.UpdateProgressPhotoDto;
using MemberUpdateDto = GymManager.Models.DTOs.Member.UpdateProgressPhotoDto;
using MemberCreateDto = GymManager.Models.DTOs.Member.CreateProgressPhotoDto;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/progress-photos")]
    public class ProgressPhotoController : ControllerBase
    {
        private readonly AdminProgressPhotoService _adminService;
        private readonly MemberProgressPhotoService _memberService;

        public ProgressPhotoController(
            AdminProgressPhotoService adminService,
            MemberProgressPhotoService memberService)
        {
            _adminService = adminService;
            _memberService = memberService;
        }

        private string GetUserRole() => "Member";
        private int? GetUserId() =>
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

        // PATCH /api/progress-photos/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id,
            [FromBody] object rawDto)   // przyjmujemy raw, zamienimy w kodzie
        {
            if (GetUserRole() == "Admin")
            {
                // rzutuj na adminowy DTO
                var dto = System.Text.Json.JsonSerializer
                    .Deserialize<AdminUpdateDto>(rawDto.ToString()!);
                var success = await _adminService.PatchAsync(id, dto!);
                return success ? NoContent() : NotFound();
            }

            if (GetUserRole() == "Member")
            {
                // rzutuj na memberski DTO
                var dto = System.Text.Json.JsonSerializer
                    .Deserialize<MemberUpdateDto>(rawDto.ToString()!);
                var success = await _memberService.PatchAsync(id, dto!);
                return success ? NoContent() : NotFound();
            }

            return Forbid();
        }

        // POST /api/progress-photos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MemberCreateDto dto)
        {
            if (GetUserRole() != "Member") return Forbid();
            var result = await _memberService.CreateAsync(dto);
            return CreatedAtAction(nameof(Patch), new { id = result.Id }, result);
        }
    }
}
