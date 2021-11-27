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

    public class PostsConfigurations : IEntityTypeConfiguration<Post>
    {

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(a => a.Title).HasColumnType("varchar(30)");
            builder.Property(g => g.Title).IsRequired();
            builder.Property(b => b.Slug).HasColumnType("varchar(30)");
            builder.Property(g => g.Slug).IsRequired();
        }

    }
}

