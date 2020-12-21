using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GenshinFarm.Infrastructure.Data.Configurations
{
    class MaterialConfig : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(150);

            builder.Property(c => c.Slug)
                .HasMaxLength(150);

            builder.Property(c => c.Rarity)
                .IsRequired()
                .HasMaxLength(30)
                .HasConversion<string>(
                    x => x.ToString(),
                    x => (Rarity)Enum.Parse(typeof(Rarity), x)
                );
        }
    }
}
