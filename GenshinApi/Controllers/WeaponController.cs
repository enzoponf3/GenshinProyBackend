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
    public class WeaponController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WeaponController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieve all Weapons.
        /// </summary>
        /// <returns></returns>
        // GET: CharacterController
        [HttpGet(Name = nameof(Weapons))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<WeaponDto>))]
        public ActionResult Weapons()
        {
            var weapon = _unitOfWork.WeaponRepository.GetAll();
            var weaponDtos = _mapper.Map<IEnumerable<WeaponDto>>(weapon);
            return Ok(weaponDtos);
        }
        /// <summary>
        /// Retrieve the Weapon spicified by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<WeaponDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Weapons(string id)
        {
            var weapon = await _unitOfWork.WeaponRepository.GetById(id);
            if (weapon == null) { return BadRequest($"There is not Weapon with the id: {id}."); }
            var weaponDtos = _mapper.Map<WeaponDto>(weapon);
            return Ok(weaponDtos);
        }
    }
}
