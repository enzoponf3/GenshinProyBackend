using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using GenshinFarm.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GenshinFarm.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;
        public readonly IMapper _mapper;

        public TokenController(IUserService userService, IPasswordService passwordService, IConfiguration configuration, IMapper mapper )
        {
            _userService = userService;
            _passwordService = passwordService;
            _configuration = configuration;
            _mapper = mapper;
        }
        /// <summary>
        /// Login the user.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>JWT</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK,Type = typeof(TokenDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(UserLogin))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(UserLogin))]
        public async Task<IActionResult> Authentication(UserLogin login)
        {
            try
            {
                var validation = await IsValidUser(login);
                if (validation.Item1)
                {
                    var token = TokenGenerator(validation.Item2);
                    return Ok(token);
                }
                return BadRequest(login);
            }
            catch(Exception e) 
            {
                return NotFound(login);
            }
            
        }
        /// <summary>
        /// Refresh the JWT.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TokenDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Refresh(string username)
        {
            User user = await _userService.GetLoginByCredentials(new UserLogin { Username = username });
            if (user != null)
            {
                var token = TokenGenerator(user);
                return Ok(token);
            }
            return NotFound();
        }

        private async Task<(bool, User)> IsValidUser(UserLogin login)
        {
            User user = await _userService.GetLoginByCredentials(login);
            if(user == null) { throw new ArgumentException(); }
            var isValid = _passwordService.Check(user.Password, user.Salt, login.Password);
            return (isValid, user);
        }

        private TokenDto TokenGenerator(User user)
        {
            //Header
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var singingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(singingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim("UserId", user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            //Payload
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddDays(7)
            );

            var token = new JwtSecurityToken(header, payload);
            var formedToken = new JwtSecurityTokenHandler().WriteToken(token);
            TokenDto tokenDto = new TokenDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = formedToken
            };
            return tokenDto;
        }
    }
}
