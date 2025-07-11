﻿using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Auth;
using GymManager.Shared.DTOs.Member;
using GymManager.Shared.DTOs.Receptionist;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Entities;
using GymManager.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly GymDbContext _dbContext;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        GymDbContext dbContext,
        JwtTokenGenerator jwtTokenGenerator,
        ILogger<AuthController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost("admin/register-trainer")]
    public async Task<IActionResult> RegisterTrainer([FromBody] RegisterTrainerDto dto)
    {
        try
        {
            var entity = _dbContext.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (entity != null)
                return BadRequest("Email is already in use.");

            var entity_trainer = _dbContext.Trainers.FirstOrDefault(x => x.PhoneNumber == dto.PhoneNumber);
            if (entity_trainer != null)
                return BadRequest("Phone number is already in use.");

            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(new[] { "User creation failed." });

            await _userManager.AddToRoleAsync(user, RoleConstants.Trainer);

            var trainer = new Trainer
            {
                UserId = user.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Description = dto.Description,
                PhotoPath = dto.PhotoPath
            };

            _dbContext.Trainers.Add(trainer);
            await _dbContext.SaveChangesAsync();

            return Ok("Trainer registered successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering a trainer.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost("admin/register-receptionist")]
    public async Task<IActionResult> RegisterReceptionist([FromBody] RegisterReceptionistDto dto)
    {
        try
        {
            var entity = _dbContext.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (entity != null)
                return BadRequest("Email is already in use.");

            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(new[] { "User creation failed." });

            await _userManager.AddToRoleAsync(user, RoleConstants.Receptionist);

            return Ok("Receptionist registered successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering a receptionist.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    private async Task<IActionResult> RegisterMemberInternal(RegisterMemberDto dto)
    {
        try
        {
            if (User.Identity?.IsAuthenticated == true &&
                User.IsInRole(RoleConstants.Member))
            {
                return Forbid("Logged-in members cannot register new accounts.");
            }

            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            string? token = null;

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                token = authHeader.Substring("Bearer ".Length);

            var email = User.FindFirstValue(ClaimTypes.Email);

            if (await _userManager.FindByEmailAsync(dto.Email) is not null)
                return BadRequest("Email is already in use.");

            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            await _userManager.AddToRoleAsync(user, RoleConstants.Member);
            await _dbContext.SaveChangesAsync();

            var member = new Member
            {
                UserId = user.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth
            };

            _dbContext.Members.Add(member);
            await _dbContext.SaveChangesAsync();

            return Ok($"Member registered successfully. Token: {token ?? "not found"}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering a member.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost("admin/register-member")]
    public Task<IActionResult> RegisterMemberByAdmin([FromBody] RegisterMemberDto dto) =>
        RegisterMemberInternal(dto);

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = RoleConstants.Receptionist)]
    [HttpPost("receptionist/register-member")]
    public Task<IActionResult> RegisterMemberByReceptionist([FromBody] RegisterMemberDto dto) =>
        RegisterMemberInternal(dto);

    [AllowAnonymous]
    [HttpPost("register-member")]
    public Task<IActionResult> RegisterMemberSelf([FromBody] RegisterMemberDto dto) =>
        RegisterMemberInternal(dto);

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid email or password.");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.generateToken(user, roles);

            return Ok(new
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                Roles = roles
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during login.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Unauthorized("User not found.");

            var roles = await _userManager.GetRolesAsync(user);

            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            string? token = null;

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                token = authHeader.Substring("Bearer ".Length);

            return Ok(new
            {
                Email = user?.Email,
                Roles = roles,
                Token = token
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving current user.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Unauthorized("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

            if (!result.Succeeded)
                return BadRequest("Password change failed.");

            return Ok("Password changed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while changing the password.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}
