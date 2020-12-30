using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using GenshinFarm.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GenshinFarm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public TokenController(IUserService userService, IPasswordService passwordService, IConfiguration configuration )
        {
            _userService = userService;
            _passwordService = passwordService;
            _configuration = configuration;
        }
        /// <summary>
        /// Login the user.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>JWT</returns>
        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin login)
        {
            var validation = await IsValidUser(login);
            if (validation.Item1)
            {
                var token = TokenGenerator(validation.Item2);
                return Ok(new { token });
            }
            return NotFound();
        }
        /// <summary>
        /// Refresh the JWT.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Refresh(string username)
        {
            User user = await _userService.GetLoginByCredentials(new UserLogin { Username = username });
            if (user != null)
            {
                var token = TokenGenerator(user);
                return Ok(new { token });
            }
            return NotFound();
        }

        private async Task<(bool, User)> IsValidUser(UserLogin login)
        {
            User user = await _userService.GetLoginByCredentials(login);
            var isValid = _passwordService.Check(user.Password, user.Salt, login.Password);
            return (isValid, user);
        }

        private string TokenGenerator(User user)
        {
            //Header
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var singingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(singingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            //Payload
            var payload = new JwtPayload
            (
                _configuration["Issuer"],
                _configuration["Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddDays(7)
            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
