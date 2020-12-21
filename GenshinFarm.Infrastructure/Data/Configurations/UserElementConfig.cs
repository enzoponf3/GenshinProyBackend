using GenshinFarm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinFarm.Infrastructure.Data.Configurations
{
    public class UserElementConfig : IEntityTypeConfiguration<UserElement>
    {
        public void Configure(EntityTypeBuilder<UserElement> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Lvl)
                .IsRequired();
        }
    }
}
