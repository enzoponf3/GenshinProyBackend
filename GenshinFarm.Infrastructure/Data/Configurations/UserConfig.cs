using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinFarm.Infrastructure.Data.Configurations
{
    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Username)
                .HasMaxLength(150);

            builder.Property(e => e.Email)
                .HasMaxLength(250);

            builder.Property(e => e.Password)
                .IsRequired();

            builder.Property(c => c.Role)
               .IsRequired()
               .HasMaxLength(30)
               .HasConversion<string>(
                   c => c.ToString(),
                   c => (RoleType)Enum.Parse(typeof(RoleType), c)
               );
        }
    }
}
