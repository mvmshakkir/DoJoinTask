using DoJoinTask.Models;
using DoJoinTask.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly SignInManager<IdentityUser> _signInManager;
	private readonly IConfiguration _configuration;
	private readonly IUserRepository _userRepository;
	
	public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IUserRepository userRepository)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_configuration = configuration;
		_userRepository = userRepository;
	}

	// POST /User/Register
	[HttpPost("Register")]
	public async Task<IActionResult> Register([FromBody] RegisterModel model)
	{
		var user = new IdentityUser { UserName = model.Username, Email = model.Email };
		var result = await _userManager.CreateAsync(user, model.Password);

		if (!result.Succeeded)
			return BadRequest(result.Errors);

		return Ok(new { Message = "User created successfully!" });
	}
	/// <summary>
	/// Handles user login by validating credentials and generating a JWT token.
	/// </summary>
	/// <param name="model">LoginModel object containing username and password.</param>
	// POST /User/Login
	[HttpPost("Login")]
	public async Task<IActionResult> Login([FromBody] LoginModel model)
	{
		var user = await _userRepository.GetUserByUsernameAsync(model.Username);
		if (user == null)
			return Unauthorized("Invalid credentials.");

		var signInResult = await _userRepository.CheckUserPasswordAsync(user, model.Password);
		if (signInResult.Succeeded)
		{
			var claims = new[]
			{
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(ClaimTypes.Email, user.Email)
		};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds
			);

			return Ok(new
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token)
			});
		}

		return Unauthorized("Invalid credentials.");
	}
}