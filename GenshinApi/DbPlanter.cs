using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Enumerations;
using GenshinFarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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

                //this.AddCharacters();
                //this.AddFarmLocations();
                //this.AddWeapons();
                //this.AddDays();
                //this.AddTalents();
                //this.AddMaterials();
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
        public void AddDays()
        {
            List<DaysOfWeek> days = new List<DaysOfWeek>()
            {
                new DaysOfWeek {Id= Guid.NewGuid().ToString(), Name="Moday"},
                new DaysOfWeek {Id= Guid.NewGuid().ToString(),Name="Tuesday"},
                new DaysOfWeek {Id= Guid.NewGuid().ToString(),Name="Wednesday"},
                new DaysOfWeek {Id= Guid.NewGuid().ToString(),Name="Thursday"},
                new DaysOfWeek {Id= Guid.NewGuid().ToString(),Name="Friday"},
                new DaysOfWeek {Id= Guid.NewGuid().ToString(),Name="Saturday"},
                new DaysOfWeek {Id= Guid.NewGuid().ToString(),Name="Sunday"}
            };
            _context.DaysOfWeeks.AddRange(days);
            _context.SaveChanges();
        }

        public void AddCharacters()
        {
            StreamReader  r = new StreamReader("TempDataFiles/characters.json");
            string json = r.ReadToEnd();

            dynamic array = JsonConvert.DeserializeObject(json);

            List<Character> characters = new List<Character>();

            foreach( var c in array)
            {
                Character character = new Character
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = c.name,
                    Slug = c.slug,
                    Rarity = c.rarity,
                    WeaponType = c.weapon,
                    Type = c.vision,
                    PowerLvl = 0
                };
                characters.Add(character);
            }
            _context.Characters.AddRange(characters);
            _context.SaveChanges();
        }

        public void AddWeapons()
        {
            StreamReader r = new StreamReader("TempDataFiles/weapons.json");
            string json = r.ReadToEnd();
            dynamic array = JsonConvert.DeserializeObject(json);
            List<Weapon> weapons = new List<Weapon>();
            foreach(var w in array)
            {
                Weapon weapon = new Weapon
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = w.name,
                    Slug = w.slug,
                    Rarity = w.rarity,
                    Attack = w.atk,
                    Type = w.type,
                    PowerLvl = 0
                };
                weapons.Add(weapon);
            }
            _context.Weapons.AddRange(weapons);
            _context.SaveChanges();
        }

        public void AddFarmLocations()
        {
            var objs = GetLines("TempDataFiles/farm_places.csv");
            List<FarmLocation> locations = new List<FarmLocation>();
            foreach( var place in objs)
            {

                FarmLocation location = new FarmLocation
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = place[0],
                    Slug = place[1],
                    Weekly = place[2] == "True" ? true : false
                };
                locations.Add(location);
            }
            _context.FarmLocations.AddRange(locations);
            _context.SaveChanges();
        }
        public void AddTalents()
        {
            var lines = GetLines("TempDataFiles/talents.csv");
            List<Talent> talents = new List<Talent>();
            foreach( var t in lines)
            {
                Talent talent = new Talent
                {
                    Id = Guid.NewGuid().ToString(),
                    Character = _context.Characters.FirstOrDefault(x => x.Slug == t[0]),
                    Name = t[1],
                    Slug = t[2]
                };
                talents.Add(talent);
            }
            _context.Talents.AddRange(talents);
            _context.SaveChanges();
        }

        public void AddMaterials()
        {
            var lines = GetLines("TempDataFiles/materials-cambiado.csv");
            List<Material> materials = new List<Material>();
            foreach(var m in lines)
            {
                Material material = new Material() 
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = m[0],
                    Slug = m[1],
                };
                var farmLocation = (_context.FarmLocations.FirstOrDefault(x => x.Name == m[5]) ?? _context.FarmLocations.FirstOrDefault(x => x.Slug == m[5]));
                farmLocation.Materials.Add(material);
                materials.Add(material);
                if (m[2] != "None")
                {
                    _context.DaysOfWeeks.FirstOrDefault(x => x.Name == m[2]).Materials.Add(material);
                    _context.DaysOfWeeks.FirstOrDefault(x => x.Name == m[3]).Materials.Add(material);
                    _context.DaysOfWeeks.FirstOrDefault(x => x.Name == m[4]).Materials.Add(material);
                }
            }
            _context.Materials.AddRange(materials);
            _context.SaveChanges();
        }

        private List<string[]> GetLines(string file)
        {
            var text = File.ReadAllText(file);
            List<string[]> lines = new List<string[]>(); 
            foreach(var obj in text.Split("\n"))
            {
                lines.Add(obj.Split(","));
            }
            return lines;

        }
    }
}