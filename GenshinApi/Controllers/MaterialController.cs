using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Enumerations;
using GenshinFarm.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GenshinFarm.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
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
        [Authorize]
        public ActionResult Materials()
        {
            var characters = _unitOfWork.MaterialRepository.GetAll();
            var charDtos = _mapper.Map<IEnumerable<MaterialDto>>(characters);
            return Ok(charDtos);
        }
        /// <summary>
        /// Retrieve the Material specified by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<MaterialDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task<ActionResult> Material(string id)
        {
            var material = await _unitOfWork.MaterialRepository.GetById(id);
            if (material == null) { return BadRequest(($"There is not Material with the id: {id}.")); }
            var materialDto = _mapper.Map<MaterialDto>(material);
            return Ok(materialDto);
        }

        /// <summary>
        /// Updates a Material.
        /// </summary>
        /// <param name="Material"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MaterialDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Update(MaterialDto materialDto)
        {
            if(await _unitOfWork.MaterialRepository.GetById(materialDto.Id) == null) { return BadRequest($"There is not Material with the id: {materialDto.Id}"); }
            var material = _mapper.Map<Material>(materialDto);
            _unitOfWork.MaterialRepository.Update(material);
            await _unitOfWork.SaveChangesAsync();
            return Ok(materialDto);
        }

        /// <summary>
        /// Update multiple elements.
        /// </summary>
        /// <param name="Materials"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<MaterialDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> UpdateMultiple(IEnumerable<MaterialDto> materialsDtos)
        {

            IEnumerable<Material> materials = _mapper.Map<IEnumerable<Material>>(materialsDtos);
            foreach (var mat in materials) 
            {
                if(await _unitOfWork.MaterialRepository.GetById(mat.Id) == null) { return BadRequest($"There is not Material with the id: {mat.Id}."); }
                _unitOfWork.MaterialRepository.Update(mat);
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(materialsDtos);

        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetMaterials(CharacterWeapon characterWeapon)
        {
            ICollection<Material> cMaterials = characterWeapon.Character.TalentMaterials;
            ICollection<Material> wMaterials = characterWeapon.Weapon.Materials;
            AscensionCategory ascensionCategory = await _unitOfWork.AscensionCategoryRepository.GetByLvl(characterWeapon.PowerLvl);
            IEnumerable<Material> characterMaterials = cMaterials.Where(m => m.AscensionCategories.Contains(ascensionCategory));
            IEnumerable<Material> weaponMaterials = wMaterials.Where(m => m.AscensionCategories.Contains(ascensionCategory));
            MaterialsDto materialsDto = new MaterialsDto()
            {
                CharacterMaterials = characterMaterials,
                WeaponMaterials = weaponMaterials
            };
            return Ok(materialsDto);
        }
    }
}
