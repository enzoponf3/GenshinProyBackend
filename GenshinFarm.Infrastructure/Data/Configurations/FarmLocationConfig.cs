using GenshinFarm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinFarm.Infrastructure.Data.Configurations
{
    class FarmLocationConfig : IEntityTypeConfiguration<FarmLocation>
    {
        public void Configure(EntityTypeBuilder<FarmLocation> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Slug)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Weekly)
                .IsRequired();
        }
    }
}
