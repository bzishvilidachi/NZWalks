using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly ITokenRepository tokenRepository;

		public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
		{
			this.userManager = userManager;
			this.tokenRepository = tokenRepository;
		}
		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
		{
			var identityUser = new IdentityUser
			{
				UserName = registerRequestDto.Username,
				Email = registerRequestDto.Username
			};

			var IdentityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

			if (IdentityResult.Succeeded)
			{
				if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
				{
					IdentityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

					if(IdentityResult.Succeeded)
					{
						
						return Ok("User was registered!");
					}
				}
				
			}
			return BadRequest("Something went wrong");
		}


		[HttpPost] 
		[Route("Login")]

		public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
		{
			var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

			if (user != null) 
			{
				var checkPass = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
				if(!checkPass)
				{
					var roles = await userManager.GetRolesAsync(user);

					if (roles != null)
					{
						var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

						var response = new LoginResponseDto
						{
							JwtToken = jwtToken
						};
						return Ok(response);
					}
							

					return Ok("Login succesful");
				}
			}

			return BadRequest("Username or password is incorrect");
		}
	}
}
