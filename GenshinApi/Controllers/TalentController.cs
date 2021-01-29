using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GenshinFarm.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class TalentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TalentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieve all Talents.
        /// </summary>
        /// <returns></returns>
        // GET: CharacterController
        [HttpGet(Name = nameof(Talents))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TalentDto>))]
        [Authorize]
        public ActionResult Talents()
        {
            var talent = _unitOfWork.TalentRepository.GetAll();
            var talentDtos = _mapper.Map<IEnumerable<TalentDto>>(talent);
            return Ok(talentDtos);
        }
        /// <summary>
        /// Retrieve the Talent specified by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TalentDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task<ActionResult> Talent(string id)
        {
            var talent = await _unitOfWork.TalentRepository.GetById(id);
            if (talent == null) { return BadRequest($"There is not Talent with the id: {id}."); }
            var talentDtos = _mapper.Map<TalentDto>(talent);
            return Ok(talentDtos);
        }

        /// <summary>
        /// Update a Talent.
        /// </summary>
        /// <param name="Talent"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType ((int) HttpStatusCode.OK, Type = typeof(TalentDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Update (TalentDto talentDto)
        {
            if (await _unitOfWork.TalentRepository.GetById(talentDto.Id) == null) { return BadRequest($"There is not Talent with the id: {talentDto.Id}."); }
            var talent = _mapper.Map<Talent>(talentDto);
            _unitOfWork.TalentRepository.Update(talent);
            await _unitOfWork.SaveChangesAsync();
            return Ok(talentDto);
        }
    }
}
