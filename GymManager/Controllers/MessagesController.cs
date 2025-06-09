using System.Security.Claims;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MCreateDto = GymManager.Shared.DTOs.Member.CreateMessageDto;
using MReadDto = GymManager.Shared.DTOs.Member.ReadSelfMessageDto;
using MUpdateDto = GymManager.Shared.DTOs.Member.UpdateMessageDto;
using TCreateDto = GymManager.Shared.DTOs.Trainer.CreateSelfMessageDto;
using TReadDto = GymManager.Shared.DTOs.Trainer.ReadSelfMessageDto;
using TUpdateDto = GymManager.Shared.DTOs.Trainer.UpdateSelfMessageDto;

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
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(
            MemberMessageService memberSvc,
            TrainerMessageService trainerSvc,
            AdminMessageService adminSvc,
            ILogger<MessagesController> logger)
        {
            _memberSvc = memberSvc;
            _trainerSvc = trainerSvc;
            _adminSvc = adminSvc;
            _logger = logger;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (User.IsInRole(RoleConstants.Member))
                    return Ok(await _memberSvc.GetAllAsync());

                if (User.IsInRole(RoleConstants.Trainer))
                    return Ok(await _trainerSvc.GetAllAsync());

                if (User.IsInRole(RoleConstants.Admin))
                    return Ok(await _adminSvc.GetAllAsync());

                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while fetching all messages.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while fetching message by ID.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Trainer}, {RoleConstants.Member}")]
        public async Task<IActionResult> Create([FromBody] object raw)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while creating message.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = $"{RoleConstants.Trainer}, {RoleConstants.Member}")]
        public async Task<IActionResult> Patch(int id, [FromBody] object raw)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while updating message with ID {MessageId}.", DateTime.Now, id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
