﻿using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using GenshinFarm.Infrastructure.Data;
using System.Threading.Tasks;

namespace GenshinFarm.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GenshinDbContext _context;
        private readonly BaseRepository<Character> _characterRepository;
        private readonly BaseRepository<FarmLocation> _farmLocationRepository;
        private readonly BaseRepository<Material> _materialRepository;
        private readonly BaseRepository<Talent> _talentRepository;
        private readonly BaseRepository<Weapon> _weaponLocationRepository;
        private readonly UserRepository _userLocationRepository;
        private readonly BaseRepository<Team> _teamRepository;
        private readonly BaseRepository<CharacterWeapon> _characterWeaponRepository;
        private readonly AscensionCategoryRepository _ascensionCategoryRepository;

        public UnitOfWork(GenshinDbContext context)
        {
            _context = context;
        }
        public IRepository<Character> CharacterRepository => _characterRepository ?? new BaseRepository<Character>(_context);
        public IRepository<FarmLocation> FarmLocationRepository => _farmLocationRepository ?? new BaseRepository<FarmLocation>(_context);
        public IRepository<Material> MaterialRepository => _materialRepository ?? new BaseRepository<Material>(_context);
        public IRepository<Talent> TalentRepository => _talentRepository ?? new BaseRepository<Talent>(_context);
        public IRepository<Weapon> WeaponRepository => _weaponLocationRepository ?? new BaseRepository<Weapon>(_context);
        public IUserRepository UserRepository => _userLocationRepository ?? new UserRepository(_context);
        public IRepository<Team> TeamRepository => _teamRepository ?? new BaseRepository<Team>(_context);
        public IRepository<CharacterWeapon> CharacterWeaponRepository => _characterWeaponRepository ?? new BaseRepository<CharacterWeapon>(_context);
        public IAscensionRespository AscensionCategoryRepository => _ascensionCategoryRepository ?? new AscensionCategoryRepository(_context);
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
