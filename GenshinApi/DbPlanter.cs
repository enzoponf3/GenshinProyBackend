using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Enumerations;
using GenshinFarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GenshinApi
{
    internal class DbPlanter
    {
        private readonly GenshinDbContext _context;

        public DbPlanter(IServiceCollection services)
        {
           
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                _context = scope.ServiceProvider.GetService<GenshinDbContext>();

                //this.AddDays();
                //this.AddCharacters();
                //this.AddFarmLocations();
                //this.AddWeapons();
                //this.AddTalents();
                //this.AddMaterials();
                //this.dayMaterial();
                //this.locationMaterial();
                this.AddAscensionCategory();
                var characters = _context.Characters
                                        .Include(c => c.AscensionMaterials)
                                        .Include(c => c.TalentMaterials)
                                        .OrderBy(c => c.Name);
                var materials = _context.Materials
                                         .Include(m => m.DaysAvailable)
                                         .Include(a => a.FarmLocation)
                                         .OrderBy(n => n.Name);
                var talents = _context.Talents
                                         .Include(m => m.Character)
                                         .OrderBy(n => n.Name);
                var days = _context.DaysOfWeeks
                                         .Include(m => m.Materials)
                                         .OrderBy(n => n.Name);
                var places = _context.FarmLocations
                                         .Include(m => m.Materials)
                                         .OrderBy(n => n.Name);

            }
        }
        private void AddAscensionCategory()
        {
            List<AscensionCategory> ascensionCategories = new List<AscensionCategory>()
            {
                new AscensionCategory(){ Category = 0},
                new AscensionCategory(){ Category = 1},
                new AscensionCategory(){ Category = 2},
                new AscensionCategory(){ Category = 3},
                new AscensionCategory(){ Category = 4},
                new AscensionCategory(){ Category = 5},
                new AscensionCategory(){ Category = 6}
            };
            _context.AscensionCategories.AddRange(ascensionCategories);
            _context.SaveChanges();
            return;
        }

        public void AddDays()
        {
            if(_context.DaysOfWeeks.Count<DaysOfWeek>() == 0) 
            {
                var arr = GetLines("csvBackup/daysOfWeek.csv");
                List<DaysOfWeek> days = new List<DaysOfWeek> ();

                foreach(var day in arr)
                {
                    days.Add(new DaysOfWeek { Id = day[0], Name = day[1] });
                }

                _context.DaysOfWeeks.AddRange(days);
                _context.SaveChanges();
            }
        }

        public void AddCharacters()
        {
            if (_context.Characters.Count<Character>() == 0)
            {
                var arr = GetLines("csvBackup/characters.csv");
                List<Character> chars = new List<Character>();

                foreach (var c in arr)
                {
                    //Id;Name;Slug;PowerLvl;Rarity;Type;WeaponType
                    Character character = new Character
                    {
                        Id = c[0],
                        Name = c[1],
                        Slug = c[2],
                        Rarity = (Rarity)Enum.Parse(typeof(Rarity), c[4]),
                        Type = (ElementalType)Enum.Parse(typeof(ElementalType), c[5]),
                        WeaponType = (WeaponType)Enum.Parse(typeof(WeaponType), c[6])
                    };
                    chars.Add(character);
                }

                _context.Characters.AddRange(chars);
                _context.SaveChanges();
            }
        }

        public void AddWeapons()
        {
            if (_context.Weapons.Count<Weapon>() == 0)
            {
                var arr = GetLines("csvBackup/weapons.csv");
                List<Weapon> weapons = new List<Weapon>();

                foreach (var c in arr)
                {
                    //Id;Name;Slug;Rarity;Type;Attack;PowerLvl;Desciption
                    Weapon weapon = new Weapon
                    {
                        Id = c[0],
                        Name = c[1],
                        Slug = c[2],
                        Rarity = (Rarity)Enum.Parse(typeof(Rarity), c[3]),
                        Type = (WeaponType)Enum.Parse(typeof(WeaponType), c[4]),
                        Attack = Int32.Parse(c[5]),
                        Desciption = c[7]
                    };
                    weapons.Add(weapon);
                }

                _context.Weapons.AddRange(weapons);
                _context.SaveChanges();
            }
        }

        public void AddFarmLocations()
        {
            if (_context.FarmLocations.Count<FarmLocation>() == 0)
            {
                var arr = GetLines("csvBackup/farmLocations.csv");
                List<FarmLocation> locations = new List<FarmLocation>();

                foreach (var c in arr)
                {
                    //Id;Name;Slug;Weekly
                    FarmLocation location = new FarmLocation
                    {
                        Id = c[0],
                        Name = c[1],
                        Slug = c[2],
                        Weekly = Convert.ToBoolean(Int32.Parse(c[3]))
                    };
                    locations.Add(location);
                }

                _context.FarmLocations.AddRange(locations);
                _context.SaveChanges();
            }
        }
        public void AddTalents()
        {
            if (_context.Talents.Count<Talent>() == 0)
            {
                var arr = GetLines("csvBackup/talents.csv");
                List<Talent> talents = new List<Talent>();

                foreach (var c in arr)
                {
                    //Id;Name;Slug;Level;PowerLvl;CharacterId
                    Talent talent = new Talent
                    {
                        Id = c[0],
                        Name = c[1],
                        Slug = c[2],
                        Level = Int32.Parse(c[3]),
                        PowerLvl = Int32.Parse(c[4]),
                        Character = _context.Characters.FirstOrDefault( d => d.Id == c[5])
                    };
                    talents.Add(talent);
                }

                _context.Talents.AddRange(talents);
                _context.SaveChanges();
            }
        }

        public void AddMaterials()
        {
            if (_context.Materials.Count<Material>() == 0)
            {
                var arr = GetLines("csvBackup/materials.csv");
                List<Material> materials = new List<Material>();

                foreach (var c in arr)
                {
                    //Id;Name;Slug;Discriminator;CharacterId;Rarity
                    Material material = new Material
                    {
                        Id = c[0],
                        Name = c[1],
                        Slug = c[2],
                        Rarity = (Rarity) Enum.Parse(typeof(Rarity), c[5])
                    };
                    materials.Add(material);
                }

                _context.Materials.AddRange(materials);
                _context.SaveChanges();
            }
        }

        public void dayMaterial()
        {
            var arr = GetLines("csvBackup/daysMaterial.csv");

            foreach (var c in arr)
            {
                //DaysAvailableId;MaterialsId
                DaysOfWeek day = _context.DaysOfWeeks.FirstOrDefault(d => d.Id == c[0]);
                Material material = _context.Materials.FirstOrDefault(m => m.Id == c[1]);
                day.Materials.Add(material);
                _context.DaysOfWeeks.Update(day);
            }

            _context.SaveChanges();
        }

        public void locationMaterial()
        {
            var arr = GetLines("csvBackup/locationMaterial.csv");

            foreach (var c in arr)
            {
                //FarmLocationId; MaterialsId
                FarmLocation location = _context.FarmLocations.FirstOrDefault(d => d.Id == c[0]);
                Material material = _context.Materials.FirstOrDefault(m => m.Id == c[1]);
                location.Materials.Add(material);
                _context.FarmLocations.Update(location);
            }

            _context.SaveChanges();
        }

        private List<string[]> GetLines(string file)
        {
            var text = File.ReadAllText(file);
            List<string[]> lines = new List<string[]>(); 
            foreach(var obj in text.Split("\r\n"))
            {
                lines.Add(obj.Split(";"));
            }
            return lines;

        }
    }
}