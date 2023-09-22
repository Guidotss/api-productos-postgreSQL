using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Id)
                .IsRequired()
                .HasDefaultValueSql("gen_random_uuid()"); 

            builder.Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(user => user.Password)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasKey(user => user.Name).HasName("PK_User_Name"); 
        }
    }
}
