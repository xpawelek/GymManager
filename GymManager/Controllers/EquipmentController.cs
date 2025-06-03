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

        public EquipmentController(
            AdminEquipmentService admin,
            MemberEquipmentService member,
            TrainerEquipmentService trainer)
        {
            _admin = admin;
            _member = member;
            _trainer = trainer;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetAllAdmin()
            => Ok(await _admin.GetAllAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetByIdAdmin(int id)
        {
            var dto = await _admin.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }
        
        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicEquipment()
        {
            var equipmentList = await _member.GetAllAsync(); 
            return Ok(equipmentList);
        }
        

        [HttpPost("{id}/upload-photo")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UploadEquipmentPhoto(int id, IFormFile file)
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
            }

            var relativePath = $"/uploads/{fileName}";

            var updated = await _admin.PatchAsync(id, new UpdateEquipmentDto { PhotoPath = relativePath });
            return updated ? Ok(relativePath) : NotFound();
        }


        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Admin}, {RoleConstants.Receptionist}")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateEquipmentDto dto)
        {
            var r = await _admin.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAdmin), new { id = r.Id }, r);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> PatchAdmin(int id, [FromBody] UpdateEquipmentDto dto)
            => (await _admin.PatchAsync(id, dto)) ? NoContent() : NotFound();

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteAdmin(int id)
            => (await _admin.DeleteAsync(id)) ? NoContent() : NotFound();


        [HttpGet("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetAllMember()
            => Ok(await _member.GetAllAsync());

        [HttpGet("self/{id}")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetByIdMember(int id)
        {
            var dto = await _member.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }


        [HttpGet("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetAllTrainer()
            => Ok(await _trainer.GetAllAsync());

        [HttpGet("me/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetByIdTrainer(int id)
        {
            var dto = await _trainer.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }
    }
}