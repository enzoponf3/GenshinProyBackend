using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GenshinFarm.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
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
        public ActionResult Talents()
        {
            var talent = _unitOfWork.TalentRepository.GetAll();
            var talentDtos = _mapper.Map<IEnumerable<TalentDto>>(talent);
            return Ok(talentDtos);
        }
        /// <summary>
        /// Retrieve the Talent spicified by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<WeaponDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Talents(string id)
        {
            var talent = await _unitOfWork.TalentRepository.GetById(id);
            if (talent == null) { return BadRequest($"There is not Talent with the id: {id}."); }
            var talentDtos = _mapper.Map<TalentDto>(talent);
            return Ok(talentDtos);
        }
    }
}
