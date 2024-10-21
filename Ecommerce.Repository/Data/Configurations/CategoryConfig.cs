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
    public class CategoryConfig : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure( EntityTypeBuilder<ProductCategory> builder )
        {
            builder.Property(c=>c.Name)
                    .IsRequired();

        }
    }
}
