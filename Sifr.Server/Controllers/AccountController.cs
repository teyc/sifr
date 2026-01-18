using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sifr.Shared.Models;

namespace Sifr.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, request.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return Ok();
        }
        return Unauthorized();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = new IdentityUser { UserName = request.Username, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            // Assign role, e.g., Owner
            await _userManager.AddToRoleAsync(user, "Owner");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok();
        }
        return BadRequest(result.Errors);
    }
}

public record LoginRequest(string Username, string Password, bool RememberMe);
public record RegisterRequest(string Username, string Email, string Password);