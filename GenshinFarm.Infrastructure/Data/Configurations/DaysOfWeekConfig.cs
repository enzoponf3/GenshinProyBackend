using GenshinFarm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinFarm.Infrastructure.Data.Configurations
{
    class DaysOfWeekConfig : IEntityTypeConfiguration<DaysOfWeek>
    {
        public void Configure(EntityTypeBuilder<DaysOfWeek> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
