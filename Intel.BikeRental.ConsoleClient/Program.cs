using Intel.BikeRental.DAL;
using Intel.BikeRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Intel.BikeRental.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateDocsTest();

            // GetLazyRentals();

            // GetRentals();

            // AsyncTest();

            // DoWorkAsync();

            // UpdateStationTest();

            // ConcurentTest();

            // DistrbutedTransactionScope();

            // TransactionTest();

            // UpdateParametersTest();

            // ExecuteSPTest();

            // GetSqlTest();

            // SqlTest();

            // SyntaxTest();

            // GroupByTest();

            // SelectTest();

            // AddVehiclesTest();

            // GetVehiclesTest();

            // AttachTest();

            // DeleteUserTest();

            // AddUserTest();

            // UpdateBikeTest();

            // AddStationTest();

            // AddRentalTest();

            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<BikeRentalContext, Configuration>());

            // CheckDatabaseTest();

            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();
        }

        private static void GenerateDocsTest()
        {
            using (var context = new BikeRentalContext())
            {
                var workspace = context.ObjectContext.MetadataWorkspace;

                var tables = workspace.GetItems<EntityType>(DataSpace.SSpace);

                foreach (var table in tables)
                {
                    Console.WriteLine(table.Name);
                    Console.WriteLine("===============");

                    foreach (var property in table.Properties)
                    {
                        Console.WriteLine(property.Name);
                    }
                }
            }
        }

        private static void DetectChangesTest()
        {
            using (var context = new BikeRentalContext())
            {
                // globalne wylaczenie sledzenia automatycznego
                context.Configuration.AutoDetectChangesEnabled = false;

                var user = context.Users.First();

                user.FirstName = "Bartosz";

                // wykrycie zmian
                context.ChangeTracker.DetectChanges();

                if (context.ChangeTracker.HasChanges())
                {
                    context.SaveChanges();
                }
            }
        }

        // Lazy
        private static void GetLazyRentals()
        {
            using (var context = new BikeRentalContext())
            {
                var rentals = context.Rentals
                    .ToList();

                rentals.ForEach(r => Console.WriteLine($"{r.DateFrom} - {r.User.FirstName}"));

                
            }
        }

        //Eager
        private static void GetRentals()
        {
            using (var context = new BikeRentalContext())
            {
                var rentals = context.Rentals
                    .Include(r=>r.User)
                    .ToList();

                rentals.ForEach(r => Console.WriteLine($"{r.DateFrom} - {r.User.FirstName}"));
            }
        }

        private static Task DoWorkAsync()
        {
            return Task.Run(() => DoWork());
        }

        private static void DoWork()
        {
            Console.WriteLine("Working...");

            Thread.Sleep(5000);

            Console.WriteLine("successed.");
        }

        private static async void AsyncTest()
        {
            var result1 = await CalculateAsync();

            var result2 = await CalculateAsync();

            using(var context = new BikeRentalContext())
            {
                var rental = await context.Rentals.FirstAsync();

                rental.Cost = result1 + result2;

                await context.SaveChangesAsync();

                Console.WriteLine("Zapisano cenę");
            }

        }

        private static Task<decimal> CalculateAsync()
        {
            return Task.Run(() => Calculate());
        }

        private static decimal Calculate()
        {
            Console.WriteLine("Calculating...");

            Thread.Sleep(5000);

            return 100.06m;
        }

        private static void UpdateStationTest()
        {
            using (var context = new BikeRentalContext())
            {
                var station = context.Stations.Find(3);

                station.IsActive = true;

                context.SaveChanges();
            }
        }

        private static void ConcurentTest()
        {
            using (var context1 = new BikeRentalContext())
            using (var context2 = new BikeRentalContext())
            {
                // pracownik #1
                var user1 = context1.Users.Find(1);
                user1.PhoneNumber = "555-555-555";

                // pracownik #2
                var user2 = context2.Users.Find(1);
                user2.PhoneNumber = "777-777-777";

                context2.SaveChanges();

                // przerwa na kawę
                Thread.Sleep(5000);

                try
                {
                    context1.SaveChanges();
                }
                catch(DbUpdateConcurrencyException e)
                {
                    Console.WriteLine("Ktos w miedzyczasie zmodyfikował rekord!");

                    Console.WriteLine("Czy chcesz odswiezyc dane?");

                    foreach (var entry in e.Entries)
                    {
                        entry.Reload();
                    }
                }
            }

        }

        private static void DistrbutedTransactionScope()
        {
            using (var transaction = new TransactionScope())
            {
                var user = new User { FirstName = "Łukasz" };

                using (var context1 = new BikeRentalContext())
                {
                    context1.Users.Add(user);
                    context1.SaveChanges();
                }


                using (var context2 = new BikeRentalContext())
                {
                    context2.Users.Add(user);
                    context2.SaveChanges();
                }

                // do zacommitowania
                transaction.Complete();
            }
        }

        private static void TransactionTest()
        {
            using (var context = new BikeRentalContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var user = new User { FirstName = "Agnieszka" };
                    var station = context.Stations.Find(1);
                    var vehicle = context.Vehicles.Find(1);

                    context.Users.Add(user);

                    // zapis #1
                    context.SaveChanges();

                    var rental = new Rental
                    {
                        User = user,
                        StationFrom = station,
                        Vehicle = vehicle,
                        DateFrom = DateTime.Now,
                    };

                    context.Rentals.Add(rental);


                    // zapis #2
                    context.SaveChanges();


                    transaction.Commit();
                }

                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        private static void UpdateParametersTest()
        {
            using (var context = new BikeRentalContext())
            {
                var user = context.Users.Find(1);

                user.Parameters = new Parameters { P1 = 10, P2 = 200 };

                context.Entry(user).State = EntityState.Modified;

                context.SaveChanges();

#if DEBUG
                Console.WriteLine("XXXX");

#endif

            }
        }

        private static void ExecuteSPTest()
        {
            var bikeId = 1;

            var bikeIdParameter = new SqlParameter("@BikeId", bikeId);

            using (var context = new BikeRentalContext())
            {
                context.Database.ExecuteSqlCommand("uspDeleteBike @BikeId", bikeIdParameter);
            }
        }

        private static void GetSqlTest()
        {
            var sql = "select * from [rentals].[Vehicles] where BikeType=0 AND Color='Red'";
          //  var sql2 = "select Number from [rentals].[Vehicles] where BikeType=0 AND Color='Red'";

            using (var context = new BikeRentalContext())
            {
        //        dynamic test;
        //        var results = context.Database.SqlQuery<dynamic>(sql2).ToList();

                var vehicles = context.Database.SqlQuery<Bike>(sql);

                foreach (var item in vehicles)
                {
                    Console.WriteLine(item.Number);
                }
            }
        }

        private static void SqlTest()
        {

            Console.WriteLine("Podaj kolor");

            var color = Console.ReadLine();

            // var sql = $"update [rentals].[Vehicles] set Active = 0 where Color = '{color}'";

            var sql = "update [rentals].[Vehicles] set Active = 0 where Color=@Color";

            var colorParameter = new SqlParameter("Color", color);

            using (var context = new BikeRentalContext())
            {
                context.Database.ExecuteSqlCommand(sql, colorParameter);
            }
        }

        private static void SyntaxTest()
        {
            using (var context = new BikeRentalContext())
            {
                var query = (from vehicle in context.Vehicles
                            where vehicle.Color == "Red"
                            orderby vehicle.Number
                            select vehicle)
                            .Where(v=>v.VehicleId == 1)
                            ;



            }
        }

        private static void GroupByTest()
        {
            using (var context = new BikeRentalContext())
            {
                var query = context.Vehicles
                    .GroupBy(vehicle => vehicle.Color)
                    .Select(g => new { Color = g.Key, Qty = g.Count() })
                    .ToList();

                var query2 = context.Vehicles
                    .GroupBy(vehicle => new { vehicle.Color, vehicle.Active })
                    .Select(g => new
                    {
                        Color = g.Key.Color,
                        IsActive = g.Key.Active,
                        Qty = g.Count()
                    })
                    .ToList();
            }
        }

        private static void SelectTest()
        {
            using (var context = new BikeRentalContext())
            {
                var vehicles = context.Vehicles
                    .Select(v => new { Id = v.VehicleId, Kolor = v.Color});


                foreach (var item in vehicles)
                {
                    Console.WriteLine(item); 
                }
            }
        }

        private static void GetVehiclesTest()
        {

            bool isSorted = true;

            using (var context = new BikeRentalContext())
            {
                var vehicles = context.Vehicles
                    .Where(v=>v.Color == "Red")
                    .Where(v =>v.Number == "R001")
                    ;


                if (isSorted)
                {
                    vehicles = vehicles
                        .OrderBy(v => v.Number)
                        .ThenBy(v => v.Color);
                }

                foreach (var item in vehicles)
                {
                    Console.WriteLine(item.Number);
                }
            }
        }

        private static void AddVehiclesTest()
        {
            var vehicles = new List<Vehicle>
            {
                new Bike { Number = "R001", BikeType = BikeType.City, Color="Green"  },
                new Bike { Number = "R002", BikeType = BikeType.Mountain, Color="Red"  },
                new Scooter { Number = "S003", Color = "Blue", EngineCapacity = 250, MaxSpeed = 90 },
            };

            using (var context = new BikeRentalContext())
            {
                context.Vehicles.AddRange(vehicles);

                context.SaveChanges();
            }
        }

        private static void AttachTest()
        {
            using (var context = new BikeRentalContext())
            {

                //var bike = context.Bikes.AsNoTracking().First(b=>b.BikeId==1);

                // deserializacja

                var bike2 = new Bike { VehicleId = 1, Active = true,
                    Color = "Brown", Number = "R001" };

                var bike3= new Bike
                {
                    VehicleId = 1,
                    Active = true,
                    Color = "Brown",
                    Number = "R001"
                };

                Console.WriteLine(context.Entry(bike2).State);
                context.Vehicles.Attach(bike2);
                context.Vehicles.Attach(bike3);
                Console.WriteLine(context.Entry(bike2).State);

                // ustawiamy stan wlasciwosci
                context.Entry(bike2).Property(p => p.Color).IsModified = true;

                try
                {
                    context.SaveChanges();
                }
                catch(Exception e)
                {

                }
              
            }

        }

        private static void DeleteUserTest()
        {
            using (var context = new BikeRentalContext())
            {
                var user = context.Users.Find(3);

                Console.WriteLine(context.Entry(user).State);

                context.Users.Remove(user);

                Console.WriteLine(context.Entry(user).State);
                context.SaveChanges();

                Console.WriteLine(context.Entry(user).State);
            }
        }

        private static void AddUserTest()
        {
            var user = new User { FirstName = "Agnieszka" };

            using (var context = new BikeRentalContext())
            {
                Console.WriteLine(context.Entry(user).State);

                context.Users.Add(user);

                Console.WriteLine(context.Entry(user).State  );

                context.SaveChanges();

                Console.WriteLine(context.Entry(user).State);
            }
        }

        private static void UpdateBikeTest()
        {
            using (var context = new BikeRentalContext())
            {

                var bikes = context.Vehicles
                    .OfType<Bike>()
                    .Where(c => c.BikeType == BikeType.City).AsNoTracking();



                var bike = context.Vehicles.AsNoTracking().First();
               

                var user = context.Users.Find(1);
                Console.WriteLine(context.Entry(bike).State);

                //bike.Color = "Yellow";
                //bike.Number = "R004";
                var items = context.ChangeTracker.Entries();


                context.Entry(bike).State = EntityState.Added;

                items = context.ChangeTracker.Entries();

                context.SaveChanges();
            }
        }

        private static void AddRentalTest()
        {
            using (var context = new BikeRentalContext())
            {
                var user = new User
                {
                    UserId = 99,
                    FirstName = "Marcin",
                    LastName = "Sulecki",
                    PhoneNumber = "555-666-777",
                };

                var bike = context.Vehicles.Find(1);

                var station = context.Stations
                    .FirstOrDefault(s => s.Name == "ST001");

                var rental = new Rental
                {
                    Vehicle = bike,
                    User = user,
                    StationFrom = station,
                    DateFrom = DateTime.Now,
                };

                context.Rentals.Add(rental);

                context.SaveChanges();


            }
        }

        private static void AddStationTest()
        {

            var stations = new List<Station>
            {
                new Station { Name = "ST001", Capacity = 10, Location = new Location(52.02, 21.03) },
                new Station { Name = "ST002", Capacity = 10, Location = new Location(52.12, 21.23) },
                new Station { Name = "ST003", Capacity = 10, Location = new Location(52.42, 20.43)  },
            };

            using (var context = new BikeRentalContext())
            {
                context.Stations.AddRange(stations);

                context.SaveChanges();
            }
        }

        private static void CheckDatabaseTest()
        {
            using (var context = new BikeRentalContext())
            {
                if (context.Database.CompatibleWithModel(false))
                {
                    Console.WriteLine("nieaktualna baza danych");
                    return;
                }
            }
        }

        private void AddBikeTest()
        {
            var bike = new Bike
            {
                BikeType = BikeType.City,
                Color = "Blue",
                Number = "R001",
                Active = true,
            };

            using (var context = new BikeRentalContext())
            {
                context.Vehicles.Add(bike);

                context.SaveChanges();
            }
        }
    }
}
