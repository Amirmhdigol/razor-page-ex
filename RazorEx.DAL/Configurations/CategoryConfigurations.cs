using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorEx.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(a => a.Title).HasColumnType("varchar(40)");
            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Slug).HasColumnType("varchar(50)");
            builder.Property(a => a.Slug).IsRequired();
            builder.Property(a => a.MetaDescription).HasColumnType("varchar(250)");
            builder.Property(a => a.MetaTag).HasColumnType("varchar(50)");
        }
    }
}
