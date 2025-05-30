﻿using System.Security.Claims;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using GymManager.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminCreateDto = GymManager.Models.DTOs.Admin.CreateTrainerAssignmentDto;
using AdminUpdateDto = GymManager.Models.DTOs.Admin.UpdateTrainerAssignmentsDto;
using MemberCreateDto = GymManager.Models.DTOs.Member.CreateTrainerAssignmentDto;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/trainer-assignments")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrainerAssignmentsController : ControllerBase
    {
        private readonly AdminTrainerAssignmentService _admin;
        private readonly MemberSelfTrainerAssignmentService _member;
        private readonly TrainerSelfTrainerAssignmentService _trainer;

        public TrainerAssignmentsController(
            AdminTrainerAssignmentService admin,
            MemberSelfTrainerAssignmentService member,
            TrainerSelfTrainerAssignmentService trainer)
        {
            _admin = admin;
            _member = member;
            _trainer = trainer;
        }

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

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminCreateDto dto)
        {
            var result = await _admin.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAdmin), new { id = result.Id }, result);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> PatchAdmin(int id, [FromBody] AdminUpdateDto dto)
            => (await _admin.PatchAsync(id, dto)) ? NoContent() : NotFound();

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteAdmin(int id)
            => (await _admin.DeleteAsync(id)) ? NoContent() : NotFound();


        [HttpPost("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> CreateSelf([FromBody] MemberCreateDto dto)
        {
            var id = await _member.CreateAsync(dto);
            return Created($"api/trainer-assignments/self/{id}", new { id });
        }

        [HttpGet("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetMyAssignments()
            => Ok(await _trainer.GetAllAsync());

        [HttpGet("me/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetMyById(int id)
        {
            var dto = await _trainer.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }
    }
}
