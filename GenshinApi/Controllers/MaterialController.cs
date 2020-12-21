using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GenshinFarm.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class MaterialController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieve all Materials.
        /// </summary>
        /// <returns></returns>
        // GET: CharacterController
        [HttpGet(Name = nameof(Materials))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<MaterialDto>))]
        public ActionResult Materials()
        {
            var characters = _unitOfWork.MaterialRepository.GetAll();
            var charDtos = _mapper.Map<IEnumerable<MaterialDto>>(characters);
            return Ok(charDtos);
        }
        /// <summary>
        /// Retrieve the Material spicified by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<MaterialDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Materials(string id)
        {
            var material = await _unitOfWork.MaterialRepository.GetById(id);
            if (material == null) { return BadRequest(($"There is not Material with the id: {id}.")); }
            var materialDto = _mapper.Map<MaterialDto>(material);
            return Ok(materialDto);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateMultiple(IEnumerable<MaterialDto> materialsDtos)
        {

            IEnumerable<Material> materials = _mapper.Map<IEnumerable<Material>>(materialsDtos);
            foreach (var mat in materials) 
            {
                _unitOfWork.MaterialRepository.Update(mat);
                await _unitOfWork.SaveChangesAsync();
            }

            return Ok();

        }

    }
}
