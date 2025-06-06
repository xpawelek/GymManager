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

        public TrainersController(
            AdminTrainerService adminSvc,
            MemberTrainerService memberSvc,
            TrainerProfileService trainerSvc)
        {
            _adminSvc = adminSvc;
            _memberSvc = memberSvc;
            _trainerSvc = trainerSvc;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetAllAdmin()
            => Ok(await _adminSvc.GetAllAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetByIdAdmin(int id)
        {
            var dto = await _adminSvc.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        /*
        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateTrainerDto dto)
        {
            var r = await _adminSvc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAdmin), new { id = r.Id }, r);
        }
        */

        [HttpPost("{id}/upload-photo")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UploadTrainerPhoto(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file selected");

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);


            var relativePath = $"/uploads/{fileName}";
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var fullUrl = $"{baseUrl.TrimEnd('/')}/{relativePath.TrimStart('/')}";

            var updated = await _adminSvc.UpdateAsync(id, new UpdateTrainerDto { PhotoPath = fullUrl });

            return updated ? Ok(fullUrl) : NotFound();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] UpdateTrainerDto dto)
            => (await _adminSvc.UpdateAsync(id, dto)) ? NoContent() : NotFound();

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteAdmin(int id)
            => (await _adminSvc.DeleteAsync(id)) ? NoContent() : NotFound();


        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMember()
            => Ok(await _memberSvc.GetAllAsync());

        [HttpGet("public/{id}")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetByIdMember(int id)
        {
            var dto = await _memberSvc.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }


        [HttpGet("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetMyProfile()
        {
            var dto = await _trainerSvc.GetMyProfileAsync();
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPatch("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateSelfTrainerDto dto)
            => (await _trainerSvc.UpdateMyProfileAsync(dto)) ? NoContent() : NotFound();
    }
}
