using MicroservicesDemo.Users.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Users.DataAccess.Maps
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(t => t.FirstName).HasMaxLength(400);
            builder.Property(t => t.LastName).HasMaxLength(400);
            builder.Property(t => t.PasswordHash).IsRequired();
            builder.Property(t => t.Email).IsRequired().HasMaxLength(400);
            builder.HasAlternateKey(t => t.Email);
        }
    }
}
