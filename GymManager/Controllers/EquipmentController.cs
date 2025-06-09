using System.Security.Claims;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/equipment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EquipmentController : ControllerBase
    {
        private readonly AdminEquipmentService _admin;
        private readonly MemberEquipmentService _member;
        private readonly TrainerEquipmentService _trainer;
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(
            AdminEquipmentService admin,
            MemberEquipmentService member,
            TrainerEquipmentService trainer,
            ILogger<EquipmentController> logger)
        {
            _admin = admin;
            _member = member;
            _trainer = trainer;
            _logger = logger;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetAllAdmin()
        {
            try
            {
                return Ok(await _admin.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all equipment (Admin).");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetByIdAdmin(int id)
        {
            try
            {
                var dto = await _admin.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching equipment by ID (Admin).");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicEquipment()
        {
            try
            {
                var equipmentList = await _member.GetAllAsync();
                return Ok(equipmentList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching public equipment.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("{id}/upload-photo")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UploadEquipmentPhoto(int id, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file selected.");

                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var relativePath = $"/uploads/{fileName}";
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var fullUrl = $"{baseUrl.TrimEnd('/')}/{relativePath.TrimStart('/')}";

                var updated = await _admin.PatchAsync(id, new UpdateEquipmentDto { PhotoPath = fullUrl });

                return updated ? Ok(fullUrl) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while uploading equipment photo.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Admin}, {RoleConstants.Receptionist}")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateEquipmentDto dto)
        {
            try
            {
                var r = await _admin.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByIdAdmin), new { id = r.Id }, r);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating equipment.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> PatchAdmin(int id, [FromBody] UpdateEquipmentDto dto)
        {
            try
            {
                return (await _admin.PatchAsync(id, dto)) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating equipment.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                return (await _admin.DeleteAsync(id)) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting equipment.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetAllMember()
        {
            try
            {
                return Ok(await _member.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching member equipment list.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("self/{id}")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetByIdMember(int id)
        {
            try
            {
                var dto = await _member.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching member equipment by ID.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetAllTrainer()
        {
            try
            {
                return Ok(await _trainer.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching trainer equipment list.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("me/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetByIdTrainer(int id)
        {
            try
            {
                var dto = await _trainer.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching trainer equipment by ID.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
