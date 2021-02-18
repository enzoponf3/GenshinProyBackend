using GenshinFarm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinFarm.Infrastructure.Data.Configurations
{
    public class AscensionCategoryConfig : IEntityTypeConfiguration<AscensionCategory>
    {
        public void Configure(EntityTypeBuilder<AscensionCategory> builder)
        {
            builder.HasKey(ac => ac.Id);

            builder.Property(ac => ac.Category);
        }
    }
}
