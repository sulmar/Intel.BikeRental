using Intel.BikeRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.DAL.Configurations
{
    public class StationConfiguration : EntityTypeConfiguration<Station>
    {
        public StationConfiguration()
        {
            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(10);

            MapToStoredProcedures();

            // zmiana nazw procedur skladowanych
            //MapToStoredProcedures(
            //    s =>
            //    {
            //        s.Update(u => u.HasName("modify_station"));
            //        s.Insert(u => u.HasName("insert_station"));
            //        s.Delete(u => u.HasName("delete_station"));
            //    });


        }
    }
}
