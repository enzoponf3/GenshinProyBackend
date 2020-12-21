using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Enumerations;
using System;

namespace GenshinFarm.Infrastructure.Data.Configurations
{
    class CharacterConfig : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Slug)
                .HasMaxLength(150);
            
            builder.Property(c => c.Rarity)
                .IsRequired()
                .HasMaxLength(30)
                .HasConversion<string>(
                    x => x.ToString(),
                    x => (Rarity)Enum.Parse(typeof(Rarity),x)
                ) ;

            builder.Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(30)
                .HasConversion<string>(
                    x=>x.ToString(),
                    x => (ElementalType)Enum.Parse(typeof(ElementalType),x)
                );

            builder.Property(c => c.WeaponType)
                .IsRequired()
                .HasMaxLength(30)
                .HasConversion<string>(
                    c=>c.ToString(),
                    c=>(WeaponType)Enum.Parse(typeof(WeaponType), c)
                );
        }

    }
}
