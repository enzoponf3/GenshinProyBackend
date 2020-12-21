using GenshinFarm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GenshinFarm.Infrastructure.Data
{
    public partial class GenshinDbContext : DbContext
    {
        public GenshinDbContext(DbContextOptions<GenshinDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Weapon> Weapons { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Talent> Talents { get; set; }
        public virtual DbSet<FarmLocation> FarmLocations { get; set; }
        public virtual DbSet<DaysOfWeek> DaysOfWeeks { get; set; }
        public virtual DbSet<UserElement> UserElement { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
