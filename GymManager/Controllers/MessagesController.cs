using System.Security.Claims;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using GymManager.Models.Identity;
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

        public MessagesController(
            MemberMessageService memberSvc,
            TrainerMessageService trainerSvc)
        {
            _memberSvc = memberSvc;
            _trainerSvc = trainerSvc;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;

        // GET /api/messages
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Role switch
            {
                RoleConstants.Member => Ok(await _memberSvc.GetAllAsync()),
                RoleConstants.Trainer => Ok(await _trainerSvc.GetAllAsync()),
                _ => Forbid()
            };
        }

        // GET /api/messages/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Role switch
            {
                RoleConstants.Member => (await _memberSvc.GetByIdAsync(id)) is MReadDto m
                                          ? Ok(m) : NotFound(),
                RoleConstants.Trainer => (await _trainerSvc.GetByIdAsync(id)) is TReadDto t
                                          ? Ok(t) : NotFound(),
                _ => Forbid()
            };
        }

        // POST /api/messages
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
