using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using GenshinFarm.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GenshinFarm.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public readonly IPasswordService _passwordService;

        public UserController(IMapper mapper, IUserService userService, IPasswordService passwordService)
        {
            _mapper = mapper;
            _userService = userService;
            _passwordService = passwordService;
        }
        /// <summary>
        /// Retrieve all Users.
        /// </summary>
        /// <returns></returns>
        // GET: CharacterController
        [HttpGet(Name = nameof(Users))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<UserDto>))]
        public ActionResult Users()
        {
            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }
        /// <summary>
        /// Retrieve the User spicified by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> User(string id)
        {
            User user;
            try
            {
                user = await _userService.GetById(id);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
        /// <summary>
        /// Adds a User to the app.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDto))]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Add(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            List<string> passwordHashed = _passwordService.Hash(user.Password);
            user.Password = passwordHashed[0];
            user.Salt = passwordHashed[1];
            try
            {
                await _userService.Add(user);
            }
            catch (Exception e)
            {
                if(e is ArgumentOutOfRangeException) { return BadRequest(e.Message); }
                return Conflict(e.Message);
            }
            userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            if(!await _userService.Update(user))
            {
                return BadRequest("User doesn't exists.");
            }
            userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(string id)
        {
            if(await _userService.Delete(id)) { return BadRequest("User doesn't exists."); }
            return Ok();
        }
    }
}
