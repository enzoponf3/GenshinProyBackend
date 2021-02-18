using GenshinFarm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenshinFarm.Infrastructure.Data.Configurations
{
    public class CharacterWeaponConfig : IEntityTypeConfiguration<CharacterWeapon>
    {
        public void Configure(EntityTypeBuilder<CharacterWeapon> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Lvl)
                .IsRequired();
        }
    }
}
