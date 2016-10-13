using Intel.BikeRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.DAL.Configurations
{
    public class BikeConfiguration : EntityTypeConfiguration<Bike>
    {
        public BikeConfiguration()
        {
            Property(p => p.Number)
               .IsRequired()
               .HasMaxLength(10)
               .IsUnicode(false);

            Property(p => p.Color)
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}
