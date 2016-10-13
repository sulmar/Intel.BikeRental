namespace Intel.BikeRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "rentals.PricingLists",
                c => new
                    {
                        PricingListId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PricingListId);
            
            CreateTable(
                "rentals.Rentals",
                c => new
                    {
                        RentalId = c.Int(nullable: false, identity: true),
                        DateFrom = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StationFrom_StationId = c.Int(),
                        StationTo_StationId = c.Int(),
                        User_UserKey = c.Int(),
                        Vehicle_VehicleId = c.Int(),
                    })
                .PrimaryKey(t => t.RentalId)
                .ForeignKey("rentals.Stations", t => t.StationFrom_StationId)
                .ForeignKey("rentals.Stations", t => t.StationTo_StationId)
                .ForeignKey("rentals.Users", t => t.User_UserKey)
                .ForeignKey("rentals.Vehicles", t => t.Vehicle_VehicleId)
                .Index(t => t.StationFrom_StationId)
                .Index(t => t.StationTo_StationId)
                .Index(t => t.User_UserKey)
                .Index(t => t.Vehicle_VehicleId);
            
            CreateTable(
                "rentals.Stations",
                c => new
                    {
                        StationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        Location_Latitude = c.Double(nullable: false),
                        Location_Longitude = c.Double(nullable: false),
                        Address = c.String(),
                        OpenTimeFrom = c.Time(nullable: false, precision: 7),
                        OpenTimeTo = c.Time(nullable: false, precision: 7),
                        Capacity = c.Byte(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StationId);
            
            CreateTable(
                "rentals.Users",
                c => new
                    {
                        UserKey = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        PhoneNumber = c.String(),
                        Gender = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserKey);
            
            CreateTable(
                "rentals.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        Color = c.String(maxLength: 20, unicode: false),
                        Number = c.String(nullable: false, maxLength: 10, unicode: false),
                        Active = c.Boolean(nullable: false),
                        BikeType = c.Int(),
                        EngineCapacity = c.Int(),
                        MaxSpeed = c.Byte(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.VehicleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("rentals.Rentals", "Vehicle_VehicleId", "rentals.Vehicles");
            DropForeignKey("rentals.Rentals", "User_UserKey", "rentals.Users");
            DropForeignKey("rentals.Rentals", "StationTo_StationId", "rentals.Stations");
            DropForeignKey("rentals.Rentals", "StationFrom_StationId", "rentals.Stations");
            DropIndex("rentals.Rentals", new[] { "Vehicle_VehicleId" });
            DropIndex("rentals.Rentals", new[] { "User_UserKey" });
            DropIndex("rentals.Rentals", new[] { "StationTo_StationId" });
            DropIndex("rentals.Rentals", new[] { "StationFrom_StationId" });
            DropTable("rentals.Vehicles");
            DropTable("rentals.Users");
            DropTable("rentals.Stations");
            DropTable("rentals.Rentals");
            DropTable("rentals.PricingLists");
        }
    }
}
