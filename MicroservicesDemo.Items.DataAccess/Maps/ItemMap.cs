using MicroservicesDemo.Items.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Items.DataAccess.Maps
{
    internal class ItemMap : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(t => t.ItemName).IsRequired().HasMaxLength(400);
            builder.Property(t => t.ItemType).IsRequired().HasMaxLength(400);
        }
    }
}
