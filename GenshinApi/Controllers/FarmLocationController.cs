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
    public class FarmLocationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FarmLocationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieve all Farm Locations.
        /// </summary>
        /// <returns></returns>
        // GET: CharacterController
        [HttpGet(Name = nameof(FarmLocations))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<FarmLocationDto>))]
        [Authorize]
        public ActionResult FarmLocations()
        {
            var weapon = _unitOfWork.FarmLocationRepository.GetAll();
            var weaponDtos = _mapper.Map<IEnumerable<FarmLocationDto>>(weapon);
            return Ok(weaponDtos);
        }
        /// <summary>
        /// Retrieve the Farm Location specified by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<FarmLocationDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task<ActionResult> FarmLocation(string id)
        {
            var farmLocation = await _unitOfWork.FarmLocationRepository.GetById(id);
            if (farmLocation == null) { return BadRequest($"There is not Location with the id: {id}."); }
            var farmLocationDto = _mapper.Map<FarmLocationDto>(farmLocation);
            return Ok(farmLocationDto);
        }

        /// <summary>
        /// Update a Location.
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(FarmLocationDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Update(FarmLocationDto locationDto)
        {
            if(await _unitOfWork.FarmLocationRepository.GetById(locationDto.Id) == null) { return BadRequest($"There is not Location with the id: {locationDto.Id}."); }
            var location = _mapper.Map<FarmLocation>(locationDto);
            _unitOfWork.FarmLocationRepository.Update(location);
            await _unitOfWork.SaveChangesAsync();
            return Ok(locationDto);
        }
    }
}
