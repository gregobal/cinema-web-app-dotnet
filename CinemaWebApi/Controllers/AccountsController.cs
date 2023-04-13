using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CinemaWebApi.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CinemaWebApi.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController: ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<UserToken>> CreateUser([FromBody]UserInfo userInfo)
    {
        var user = new IdentityUser
        {
            UserName = userInfo.EmailAddress,
            Email = userInfo.EmailAddress
        };
        var result = await _userManager.CreateAsync(user, userInfo.Password);

        if (result.Succeeded)
        {
            return BuildToken(userInfo);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
    {
        var result = await _signInManager.PasswordSignInAsync(userInfo.EmailAddress, userInfo.Password,
            isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return BuildToken(userInfo);
        }

        return BadRequest("Invalid login attempt");
    }

    [HttpPost("renew")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<UserToken> renewToken()
    {
        var userInfo = new UserInfo
        {
            EmailAddress = HttpContext.User.Identity.Name
        };

        return BuildToken(userInfo);
    }

    private UserToken BuildToken(UserInfo userInfo)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userInfo.EmailAddress),
            new Claim(ClaimTypes.Email, userInfo.EmailAddress)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddDays(1);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        return new UserToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}