using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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

    public class TeamController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TeamController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Retrieve all the Teams in the User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICollection<Team>))]
        [Authorize]
        public async Task<ActionResult> Teams(string userId)
        {
            User user = await _unitOfWork.UserRepository.GetById(userId);
            ICollection<Team> teams = user.Teams;
            return Ok(teams);
        }
        /// <summary>
        /// Retreive the Team specified by Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICollection<Team>))]
        [Authorize]
        public async Task<ActionResult> Team(string teamId)
        {
            var team = _unitOfWork.TeamRepository.GetById(teamId);
            if (team == null) { return NotFound($"TeamId: {teamId}"); }
            return Ok(team);
        }
        /// <summary>
        /// Adds a Team to the User.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Team))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task<ActionResult> AddTeam(string userId, ICollection<CharacterDto> characters)
        {
            if (characters.Count == 0) { return BadRequest("The team must contain at least one character."); }
            Team team = new Team();
            foreach (CharacterDto characterDto in characters)
            {
                Character character = _mapper.Map<Character>(characterDto);
                CharacterWeapon characterWeapon = new CharacterWeapon(0) { Character = character };
                team.AddCharacter(characterWeapon);
            }
            User user = await _unitOfWork.UserRepository.GetById(userId);
            user.Teams.Add(team);
            await _unitOfWork.SaveChangesAsync();
            return Ok(characters);
        }
        /// <summary>
        /// Delete the Team belonging to the user specified by id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Team))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Authorize]
        public async Task<ActionResult> DeleteTeam(string teamId)
        {
            var team = _unitOfWork.TeamRepository.GetById(teamId);
            if (team == null) { return NotFound($"TeamId: {teamId}"); }
            await _unitOfWork.TeamRepository.Delete(teamId);
            return Ok(team);
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateLevels(string userId, string teamId, string characterWeaponId, int charLvl, int weaponLvl)
        {
            User user = await _unitOfWork.UserRepository.GetById(userId);
            CharacterWeapon characterWeapon = user.Teams.FirstOrDefault(c => c.Id == teamId)
                                                    .CharacterWeapons.FirstOrDefault(cw => cw.Id == characterWeaponId);
            foreach (Team team in user.Teams)
            {
                foreach (CharacterWeapon actualCW in team.CharacterWeapons)
                {
                    if (actualCW.Character == characterWeapon.Character)
                    {
                        actualCW.setPowerLvl(charLvl);
                        actualCW.setWeaponPower(weaponLvl);
                    }
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(characterWeapon);
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> AddCharacter(string teamId, CharacterDto characterDto, WeaponDto weaponDto, int charLvl, int weaponLvl)
        {
            Team team = await _unitOfWork.TeamRepository.GetById(teamId);
            Character character = _mapper.Map<Character>(characterDto);
            Weapon weapon = _mapper.Map<Weapon>(weaponDto);
            CharacterWeapon characterWeapon = new CharacterWeapon(charLvl)
            {
                Character = character,
                Weapon = weapon
            };
            characterWeapon.setWeaponPower(weaponLvl);
            if (team.AddCharacter(characterWeapon)) 
            {
                await _unitOfWork.SaveChangesAsync();
                return Ok(team);
            }
            return BadRequest("The maximum Characters per Team is 5.");
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteCharacter(string teamId, string characterId)
        {
            Team team = await _unitOfWork.TeamRepository.GetById(teamId);
            CharacterWeapon characterWeapon = team.CharacterWeapons.FirstOrDefault(c => c.Id == characterId);
            team.CharacterWeapons.Remove(characterWeapon);
            await _unitOfWork.SaveChangesAsync();
            return Ok(characterWeapon);
        }

    }
}
