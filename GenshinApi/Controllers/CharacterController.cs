using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Enumerations;
using GenshinFarm.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GenshinFarm.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CharacterController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieve all Characters.
        /// </summary>
        /// <returns></returns>
        // GET: CharacterController
        [HttpGet(Name = nameof(Characters))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<CharacterDto>))]
        [Authorize]
        public ActionResult Characters()
        {
            var characters = _unitOfWork.CharacterRepository.GetAll();
            var charDtos = _mapper.Map<IEnumerable<CharacterDto>>(characters);
            return Ok(charDtos);
        }
        /// <summary>
        /// Retrieve the Character spicified by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<CharacterDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task<ActionResult> Character(string id)
        {
            var character = await _unitOfWork.CharacterRepository.GetById(id);
            if(character == null) { return BadRequest(($"There is not Character with the id: {id}.")); }
            var charDto = _mapper.Map<CharacterDto>(character);
            return Ok(charDto);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CharacterDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Update(CharacterDto characterDto)
        {
            var character = _mapper.Map<Character>(characterDto);
            if( await _unitOfWork.CharacterRepository.GetById(character.Id) == null) { return BadRequest(characterDto); }
            _unitOfWork.CharacterRepository.Update(character);
            await _unitOfWork.SaveChangesAsync();
            characterDto = _mapper.Map<CharacterDto>(character);
            return Ok(characterDto);
        }

    }
}
