using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ISecurityService securityService;

        public TokenController(IConfiguration _configuration, ISecurityService _securityService)
        {
            configuration = _configuration;
            securityService = _securityService;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticationAsync(UserLogin login)
        {
            var validation = await IsValidUserAsync(login);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);

                return Ok(new { token });
            }
            else
                return NotFound();
        }

        private async Task<(bool, Security)> IsValidUserAsync(UserLogin login)
        {
            var user = await securityService.GetLoginByCredentials(login);
            return (user != null,user);
        }

        private string GenerateToken(Security security)
        {
            //header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //claims
            var claims = new[]{
                new Claim(ClaimTypes.Name, security.UserName),
                new Claim("User", security.User),
                new Claim(ClaimTypes.Role, security.Role.ToString())
            };

            //payload
            var payload = new JwtPayload
            (
                configuration["Authentication:Issuer"],
                configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(10)
            );

            //token
            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
