using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Auth;
using GymManager.Models.DTOs.Member;
using GymManager.Models.DTOs.Receptionist;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Entities;
using GymManager.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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

    public AuthController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        GymDbContext dbContext,
        JwtTokenGenerator jwtTokenGenerator)

    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost("admin/register-trainer")]
    public async Task<IActionResult> RegisterTrainer([FromBody] RegisterTrainerDto dto)
    {
        //dodac walidacje, email zajety, wystarczajace haslo etc
        var user = new ApplicationUser
        {
            Email = dto.Email,
            UserName = dto.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new String[] { "some errors" });
        }

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

        return Ok("Trainer registered successfully");
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost("admin/register-receptionist")]
    public async Task<IActionResult> RegisterReceptionist([FromBody] RegisterReceptionistDto dto)
    {
        //dodac walidacje, email zajety, wystarczajace haslo etc
        var user = new ApplicationUser
        {
            Email = dto.Email,
            UserName = dto.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new String[] { "some errors" });
        }

        await _userManager.AddToRoleAsync(user, RoleConstants.Receptionist);

        return Ok("Trainer registered successfully");
    }

    private async Task<IActionResult> RegisterMemberInternal(RegisterMemberDto dto)
    {
        if (User.Identity?.IsAuthenticated == true ||
            User.IsInRole(RoleConstants.Member))
        {
            return Forbid("Logged-in members cannot create new accounts.");
        }

        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        string? token = null;

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            token = authHeader.Substring("Bearer ".Length);
        }
        
        var email = User.FindFirstValue(ClaimTypes.Email);
       // Console.WriteLine("Executing user roles: " + string.Join(", ", executingUserRoles));
        
        if (await _userManager.FindByEmailAsync(dto.Email) is not null)
            return BadRequest("Email is already taken.");

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

        return Ok($"Member registered successfully. email: {token ?? "not found"}");
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost("admin/register-member")]
    public Task<IActionResult> RegisterMemberByAdmin([FromBody] RegisterMemberDto dto) => RegisterMemberInternal(dto);

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = RoleConstants.Receptionist)]
    [HttpPost("receptionist/register-member")]
    public Task<IActionResult> RegisterMemberByReceptionist([FromBody] RegisterMemberDto dto) =>
        RegisterMemberInternal(dto);
    
    [AllowAnonymous]
    [HttpPost("register-member")]
    public Task<IActionResult> RegisterMemberSelf([FromBody] RegisterMemberDto dto) => RegisterMemberInternal(dto);

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            return Unauthorized("Invalid username or password.");
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized("Invalid username or password.");
        }
        
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
    
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Unauthorized("User not found");
        else
        {
            System.Console.WriteLine(user.Id);
        }
        var roles = await _userManager.GetRolesAsync(user);
        
        Console.WriteLine("IsAuthenticated: " + User.Identity?.IsAuthenticated);
        Console.WriteLine("Claims:");
        foreach (var c in User.Claims)
        {
            Console.WriteLine($" - {c.Type}: {c.Value}");
        }
        
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        string? token = null;

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            token = authHeader.Substring("Bearer ".Length);
        }

        return Ok(new
        {
            Email = user?.Email,
            Roles = roles,
            Token = token
        });
    }
}

