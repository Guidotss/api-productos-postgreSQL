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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(prod => prod.Id).HasDefaultValueSql("gen_random_uuid()"); 
            builder.Property(prod => prod.Name).IsRequired();
            builder.Property(prod => prod.Description).IsRequired();
            builder.Property(prod => prod.Price).IsRequired();
            builder.Property(prod => prod.Quantity).IsRequired();
        }
    }
}
