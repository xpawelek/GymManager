using System.Security.Claims;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MCreateDto = GymManager.Models.DTOs.Member.CreateMessageDto;
using MReadDto = GymManager.Models.DTOs.Member.ReadSelfMessageDto;
using MUpdateDto = GymManager.Models.DTOs.Member.UpdateMessageDto;
using TCreateDto = GymManager.Models.DTOs.Trainer.CreateSelfMessageDto;
using TReadDto = GymManager.Models.DTOs.Trainer.ReadSelfMessageDto;
using TUpdateDto = GymManager.Models.DTOs.Trainer.UpdateSelfMessageDto;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/messages")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessagesController : ControllerBase
    {
        private readonly MemberMessageService _memberSvc;
        private readonly TrainerMessageService _trainerSvc;
        private readonly AdminMessageService _adminSvc;

        public MessagesController(
            MemberMessageService memberSvc,
            TrainerMessageService trainerSvc,
            AdminMessageService adminSvc)
        {
            _memberSvc = memberSvc;
            _trainerSvc = trainerSvc;
            _adminSvc = adminSvc;
        }
        

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;

        // GET /api/messages
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (User.IsInRole(RoleConstants.Member))
                return Ok(await _memberSvc.GetAllAsync());

            if (User.IsInRole(RoleConstants.Trainer))
                return Ok(await _trainerSvc.GetAllAsync());

            if (User.IsInRole(RoleConstants.Admin))
                return Ok(await _adminSvc.GetAllAsync());

            return Forbid();
        }

        // GET /api/messages/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (User.IsInRole(RoleConstants.Member))
            {
                var result = await _memberSvc.GetByIdAsync(id);
                return result is not null ? Ok(result) : NotFound();
            }

            if (User.IsInRole(RoleConstants.Trainer))
            {
                var result = await _trainerSvc.GetByIdAsync(id);
                return result is not null ? Ok(result) : NotFound();
            }

            if (User.IsInRole(RoleConstants.Admin))
            {
                var result = await _adminSvc.GetByIdAsync(id);
                return result is not null ? Ok(result) : NotFound();
            }

            return Forbid();
        }

        // POST /api/messages
        [Authorize(Roles = $"{RoleConstants.Trainer}, {RoleConstants.Member}")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] object raw)
        {
            if (Role == RoleConstants.Member)
            {
                var dto = System.Text.Json.JsonSerializer
                    .Deserialize<MCreateDto>(raw.ToString()!)!;
                var result = await _memberSvc.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }

            if (Role == RoleConstants.Trainer)
            {
                var dto = System.Text.Json.JsonSerializer
                    .Deserialize<TCreateDto>(raw.ToString()!)!;
                var result = await _trainerSvc.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }

            return Forbid();
        }

        // PATCH /api/messages/{id}
        [Authorize(Roles = $"{RoleConstants.Trainer}, {RoleConstants.Member}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] object raw)
        {
            if (Role == RoleConstants.Member)
            {
                var dto = System.Text.Json.JsonSerializer
                    .Deserialize<MUpdateDto>(raw.ToString()!)!;
                return await _memberSvc.UpdateAsync(id, dto)
                    ? NoContent() : NotFound();
            }

            if (Role == RoleConstants.Trainer)
            {
                var dto = System.Text.Json.JsonSerializer
                    .Deserialize<TUpdateDto>(raw.ToString()!)!;
                return await _trainerSvc.UpdateAsync(id, dto)
                    ? NoContent() : NotFound();
            }

            return Forbid();
        }
    }
}
