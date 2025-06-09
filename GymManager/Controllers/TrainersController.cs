using System.Security.Claims;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Trainer;
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
    [Route("api/trainers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrainersController : ControllerBase
    {
        private readonly AdminTrainerService _adminSvc;
        private readonly MemberTrainerService _memberSvc;
        private readonly TrainerProfileService _trainerSvc;
        private readonly ILogger<TrainersController> _logger;

        public TrainersController(
            AdminTrainerService adminSvc,
            MemberTrainerService memberSvc,
            TrainerProfileService trainerSvc,
            ILogger<TrainersController> logger)
        {
            _adminSvc = adminSvc;
            _memberSvc = memberSvc;
            _trainerSvc = trainerSvc;
            _logger = logger;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetAllAdmin()
        {
            try
            {
                return Ok(await _adminSvc.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all trainers (admin).");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetByIdAdmin(int id)
        {
            try
            {
                var dto = await _adminSvc.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting trainer by ID {Id} (admin).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("{id}/upload-photo")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UploadTrainerPhoto(int id, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file selected");

                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
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

                var updated = await _adminSvc.UpdateAsync(id, new UpdateTrainerDto { PhotoPath = fullUrl });
                return updated ? Ok(fullUrl) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading trainer photo for ID {Id}.", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] UpdateTrainerDto dto)
        {
            try
            {
                return (await _adminSvc.UpdateAsync(id, dto)) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating trainer {Id}.", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                return (await _adminSvc.DeleteAsync(id)) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting trainer {Id}.", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMember()
        {
            try
            {
                return Ok(await _memberSvc.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching public trainer list.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("public/{id}")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetByIdMember(int id)
        {
            try
            {
                var dto = await _memberSvc.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting public trainer by ID {Id}.", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetMyProfile()
        {
            try
            {
                var dto = await _trainerSvc.GetMyProfileAsync();
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting own trainer profile.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateSelfTrainerDto dto)
        {
            try
            {
                return (await _trainerSvc.UpdateMyProfileAsync(dto)) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating own trainer profile.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("assigned-trainer")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetAssignedTrainer()
        {
            try
            {
                var trainer = await _memberSvc.GetAssignedTrainerAsync();
                return trainer == null ? NotFound() : Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching assigned trainer.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("contacted")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetContactedTrainers()
        {
            try
            {
                var list = await _memberSvc.GetAllContactedAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching contacted trainers.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
