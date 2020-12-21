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
        public async Task<ActionResult> Character(string id)
        {
            var character = await _unitOfWork.CharacterRepository.GetById(id);
            if(character == null) { return BadRequest(($"There is not Character with the id: {id}.")); }
            var charDto = _mapper.Map<CharacterDto>(character);
            return Ok(charDto);
        }

        // GET: CharacterController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return Ok();
        //}

        //// GET: CharacterController/Create
        //public ActionResult Create()
        //{
        //    return Ok();
        //}

        //// POST: CharacterController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: CharacterController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: CharacterController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: CharacterController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: CharacterController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
