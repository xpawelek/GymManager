using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GymManager.Controllers;
using GymManager.Models.Identity;
using GymManager.Data;
using GymManager.Shared.DTOs.Auth;
using GymManager.Tests.Helpers;

// 👇 alias unikający konfliktu z MVC
using IdentitySignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace GymManager.Tests.Controllers;

public class AuthControllerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManager;
    private readonly Mock<SignInManager<ApplicationUser>> _signInManager;
    private readonly Mock<GymDbContext> _dbContext;
    private readonly Mock<JwtTokenGenerator> _jwtGenerator;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        _userManager = new Mock<UserManager<ApplicationUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null
        );

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        _signInManager = new Mock<SignInManager<ApplicationUser>>(
            _userManager.Object,
            httpContextAccessorMock.Object,
            userPrincipalFactoryMock.Object,
            null, null, null, null
        );

        _dbContext = new Mock<GymDbContext>();
        _jwtGenerator = new Mock<JwtTokenGenerator>();

        _controller = new AuthController(
            _userManager.Object,
            _signInManager.Object,
            _dbContext.Object,
            _jwtGenerator.Object,
            TestHelper.GetLogger<AuthController>()
        );
    }

    [Fact]
    public void Controller_InitializesCorrectly()
    {
        _controller.Should().NotBeNull();
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenUserNotFound()
    {
        // Arrange
        var dto = new LoginDto { Email = "nonexistent@example.com", Password = "pass" };
        _userManager.Setup(x => x.FindByEmailAsync(dto.Email)).ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _controller.Login(dto);

        // Assert
        result.Should().BeOfType<UnauthorizedObjectResult>();
    }

    [Fact]
    public async Task Login_ShouldReturnToken_OnSuccess()
    {
        // Arrange
        var dto = new LoginDto { Email = "test@test.com", Password = "pass" };
        var user = new ApplicationUser { Email = dto.Email, Id = "1" };

        _userManager.Setup(x => x.FindByEmailAsync(dto.Email)).ReturnsAsync(user);
        _signInManager.Setup(x => x.CheckPasswordSignInAsync(user, dto.Password, false))
                      .ReturnsAsync(IdentitySignInResult.Success);
        _userManager.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Member" });
        _jwtGenerator.Setup(x => x.generateToken(user, It.IsAny<IList<string>>()))
                     .Returns("mocked_token");

        // Act
        var result = await _controller.Login(dto);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value!.ToString().Should().Contain("mocked_token");
    }
}
