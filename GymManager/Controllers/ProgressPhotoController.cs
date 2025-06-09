using System.Security.Claims;
using System.Text.Json;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminUpdateDto = GymManager.Shared.DTOs.Admin.UpdateProgressPhotoDto;
using MemberCreateDto = GymManager.Shared.DTOs.Member.CreateProgressPhotoDto;
using MemberUpdateDto = GymManager.Shared.DTOs.Member.UpdateProgressPhotoDto;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/progress-photos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProgressPhotoController : ControllerBase
    {
        private readonly AdminProgressPhotoService _admin;
        private readonly MemberProgressPhotoService _member;
        private readonly TrainerProgressPhotoService _trainer;
        private readonly ILogger<ProgressPhotoController> _logger;

        public ProgressPhotoController(
            AdminProgressPhotoService admin,
            MemberProgressPhotoService member,
            TrainerProgressPhotoService trainer,
            ILogger<ProgressPhotoController> logger)
        {
            _admin = admin;
            _member = member;
            _trainer = trainer;
            _logger = logger;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Role switch
                {
                    RoleConstants.Admin => Ok(await _admin.GetAllAsync()),
                    RoleConstants.Member => Ok(await _member.GetAllAsync()),
                    RoleConstants.Trainer => Ok(await _trainer.GetAllAsync()),
                    _ => Forbid()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while getting all progress photos.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublic()
        {
            try
            {
                var role = User.FindFirstValue(ClaimTypes.Role);
                if (string.IsNullOrEmpty(role))
                    return Ok(await _member.GetAllPublic());

                return role switch
                {
                    RoleConstants.Admin => Ok(await _admin.GetAllPublic()),
                    RoleConstants.Member => Ok(await _member.GetAllPublic()),
                    RoleConstants.Trainer => Ok(await _trainer.GetAllPublic()),
                    _ => Forbid()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while getting public progress photos.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (Role == RoleConstants.Admin)
                {
                    var dto = await _admin.GetByIdAsync(id);
                    return dto == null ? NotFound() : Ok(dto);
                }
                if (Role == RoleConstants.Member)
                {
                    var dto = await _member.GetByIdAsync(id);
                    return dto == null ? NotFound() : Ok(dto);
                }
                if (Role == RoleConstants.Trainer)
                {
                    var dto = await _trainer.GetByIdAsync(id);
                    return dto == null ? NotFound() : Ok(dto);
                }

                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while getting progress photo by ID.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("assigned-members")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetAssignedMembersPhotos()
        {
            try
            {
                return Ok(await _trainer.GetAssignedMembersPhotosAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while getting assigned members' progress photos.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> Create([FromBody] MemberCreateDto dto)
        {
            try
            {
                var r = await _member.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while creating progress photo.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("upload-photo")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> UploadPhoto([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded");

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var path = Path.Combine(uploadsFolder, uniqueName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var relativePath = $"/uploads/{uniqueName}";
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var fullUrl = $"{baseUrl.TrimEnd('/')}/{relativePath.TrimStart('/')}";

                return Ok(new { path = fullUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while uploading progress photo.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] object raw)
        {
            try
            {
                switch (Role)
                {
                    case RoleConstants.Admin:
                        var aDto = JsonSerializer.Deserialize<AdminUpdateDto>(raw.ToString()!)!;
                        return await _admin.PatchAsync(id, aDto) ? NoContent() : NotFound();
                    case RoleConstants.Member:
                        var mDto = JsonSerializer.Deserialize<MemberUpdateDto>(raw.ToString()!)!;
                        return await _member.PatchAsync(id, mDto) ? NoContent() : NotFound();
                    default:
                        return Forbid();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while updating progress photo with ID {Id}.", DateTime.Now, id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Role switch
                {
                    RoleConstants.Admin => await _admin.DeleteAsync(id) ? NoContent() : NotFound(),
                    RoleConstants.Member => await _member.DeleteAsync(id) ? NoContent() : NotFound(),
                    _ => Forbid()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while deleting progress photo with ID {Id}.", DateTime.Now, id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
