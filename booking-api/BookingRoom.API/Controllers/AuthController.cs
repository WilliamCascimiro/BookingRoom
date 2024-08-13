using BookingRoom.Application.DTOs.Auth;
using BookingRoom.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingRoom.API.Controllers
{
    [Route("Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IResult> Login([FromBody] UserLoginDTOInput userLogin)
        {
            if (!ModelState.IsValid)
                return Results.BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _userService.GetUser(userLogin);

            if (!result.IsSuccess)
                return Results.Problem(result.Error, statusCode: result.StatusCode);

            var claims = await GetClaims(result.Value);
            var token = GenerateJWTToken(claims);
            return Results.Ok(new AuthenticatedResponse(token));
        }

        private async Task<List<Claim>> GetClaims(UserLoginDTOOutput userLogin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userLogin.Name),
                new Claim(ClaimTypes.Role, userLogin.Role),
                new Claim("userId", userLogin.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Ticks.ToString()),
            };

            return claims;
        }

        private string GenerateJWTToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));
            
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiration,
                signingCredentials: credenciais
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}
