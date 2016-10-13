using Intel.BikeRental.DAL.Configurations;
using Intel.BikeRental.DAL.Conventions;
using Intel.BikeRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.DAL
{
    public class BikeRentalContext : DbContext
    {

       // public DbSet<Bike> Bikes { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<PricingList> PricingLists { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public BikeRentalContext()
            : base("BikeRentalConnection")
        {

            ObjectContext.ObjectMaterialized += ObjectContext_ObjectMaterialized;

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
       
        }

        private void ObjectContext_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (e.Entity is User)
            {
                User user = e.Entity as User;
               
                // TODO: deserializacja
                user.Parameters = new Parameters { P1 = 10, P2 = 200 };
            }   
        }

        public override int SaveChanges()
        {
            var users = this.ChangeTracker.Entries<User>()
                .Where(e=>e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e=>e.Entity);

            foreach (var user in users)
            {
                user.SerializedParameters = user.Parameters?.ToString();
            }

            return base.SaveChanges();
        }

        public ObjectContext ObjectContext
        {
            get
            {
                return ((IObjectContextAdapter)this).ObjectContext;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new BikeConfiguration());
            modelBuilder.Configurations.Add(new RentalConfiguration());
            modelBuilder.Configurations.Add(new StationConfiguration());


            // modelBuilder.ComplexType<Location>();

            // TPT
            //modelBuilder.Entity<Bike>().ToTable("Bikes");
            //modelBuilder.Entity<Scooter>().ToTable("Scooters");


            // TPC
            //modelBuilder.Entity<Bike>().Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("Bikes");
            //});

            //modelBuilder.Entity<Scooter>().Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("Scooters");
            //});

            modelBuilder.Conventions.Add(new DateTime2Convention());
            modelBuilder.Conventions.Add(new KeyConvention());
           // modelBuilder.Conventions.Remove(new IdKeyDiscoveryConvention());
            modelBuilder.HasDefaultSchema("rentals");

            base.OnModelCreating(modelBuilder);
        }


    }
}
