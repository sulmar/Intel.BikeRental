using Intel.BikeRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.DAL.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
           Property(p => p.FirstName)
               .HasMaxLength(50);

           Property(p => p.LastName)
                .HasMaxLength(50);

            //    ToTable("Uzytkownicy");


            // 
            //    HasKey(p => p.UserId);


            // klucz zlozony
            //
            //    HasKey(p => new { p.FirstName, p.LastName });


            Ignore(p => p.IsLogged);

            Ignore(p => p.Parameters);

            Property(p => p.SerializedParameters)
                .HasColumnName("Parameters");


            //Property(p => p.PhoneNumber)
            //    .IsConcurrencyToken();

            //Property(p => p.FirstName)
            //  .IsConcurrencyToken();

            Property(p => p.RowVersion)
                .IsRowVersion();

        }
    }
}
