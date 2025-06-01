using System.Security.Claims;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Member;
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
    [Route("api/members")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MemberController : ControllerBase
    {
        private readonly AdminMemberService _admin;
        private readonly MemberSelfService _self;
        private readonly TrainerMemberService _trainer;

        public MemberController(
            AdminMemberService admin,
            MemberSelfService self,
            TrainerMemberService trainer)
        {
            _admin = admin;
            _self = self;
            _trainer = trainer;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;
        private int Id => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

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

        /*
        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Admin}, {RoleConstants.Receptionist}")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateMemberDto dto)
        {
            var r = await _admin.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAdmin), new { id = r.Id }, r);
        }
        */

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> PatchAdmin(int id, [FromBody] UpdateMemberDto dto)
            => (await _admin.PatchAsync(id, dto)) ? NoContent() : NotFound();

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteAdmin(int id)
            => (await _admin.DeleteAsync(id)) ? NoContent() : NotFound();


        [HttpGet("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetSelf()
        {
            var dto = await _self.GetOwnAsync();
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPatch("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> UpdateSelf([FromBody] UpdateSelfMemberDto dto)
            => (await _self.UpdateOwnAsync(dto)) ? NoContent() : NotFound();


        [HttpGet("assigned")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetAssigned()
            => Ok(await _trainer.GetAllAsync());

        [HttpGet("assigned/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetAssignedById(int id)
        {
            var dto = await _trainer.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }
    }
}
