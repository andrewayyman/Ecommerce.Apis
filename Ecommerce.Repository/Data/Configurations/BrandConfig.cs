using Ecommerce.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Data.Configurations
{
    public class BrandConfig : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure( EntityTypeBuilder<ProductBrand> builder )
        {
            builder.Property(b => b.Name)
                   .IsRequired();


        }
    }
}
