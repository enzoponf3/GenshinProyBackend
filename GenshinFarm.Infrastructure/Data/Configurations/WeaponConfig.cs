using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinFarm.Infrastructure.Data.Configurations
{
    class WeaponConfig : IEntityTypeConfiguration<Weapon>
    {
        public void Configure(EntityTypeBuilder<Weapon> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Slug)
                .HasMaxLength(150);

            builder.Property(c => c.Attack);

            builder.Property(c => c.Desciption)
                .HasMaxLength(500);

            builder.Property(c => c.Rarity)
                .IsRequired()
                .HasMaxLength(30)
                .HasConversion(
                    x => x.ToString(),
                    x => (Rarity)Enum.Parse(typeof(Rarity), x)
                );

            builder.Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(30)
                .HasConversion(
                    x => x.ToString(),
                    x => (WeaponType)Enum.Parse(typeof(WeaponType), x)
                );
        }
    }
}
