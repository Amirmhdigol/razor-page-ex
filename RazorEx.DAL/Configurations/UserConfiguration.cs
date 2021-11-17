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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(a => a.UserName).HasColumnType("varchar(30)");
            builder.Property(g => g.UserName).IsRequired();
            builder.Property(b => b.Password).HasColumnType("bigint");
            builder.Property(b => b.RePassword).HasColumnType("bigint");
        }
    }
}
