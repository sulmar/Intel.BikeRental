namespace Intel.BikeRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVehicle1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "rentals.Bikes", newName: "Vehicles");
            DropForeignKey("rentals.Rentals", "Bike_BikeId", "rentals.Bikes");
            RenameColumn(table: "rentals.Rentals", name: "Bike_BikeId", newName: "Vehicle_VehicleId");
            RenameIndex(table: "rentals.Rentals", name: "IX_Bike_BikeId", newName: "IX_Vehicle_VehicleId");
            DropPrimaryKey("rentals.Vehicles");
            AddColumn("rentals.Vehicles", "EngineCapacity", c => c.Int());
            AddColumn("rentals.Vehicles", "MaxSpeed", c => c.Byte());
            AddColumn("rentals.Vehicles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("rentals.Vehicles", "BikeId", c => c.Int());
            AlterColumn("rentals.Vehicles", "BikeType", c => c.Int());
            AlterColumn("rentals.Vehicles", "VehicleId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("rentals.Vehicles", "VehicleId");
            AddForeignKey("rentals.Rentals", "Vehicle_VehicleId", "rentals.Vehicles", "VehicleId");
        }
        
        public override void Down()
        {
            DropForeignKey("rentals.Rentals", "Vehicle_VehicleId", "rentals.Vehicles");
            DropPrimaryKey("rentals.Vehicles");
            AlterColumn("rentals.Vehicles", "VehicleId", c => c.Int(nullable: false));
            AlterColumn("rentals.Vehicles", "BikeType", c => c.Int(nullable: false));
            AlterColumn("rentals.Vehicles", "BikeId", c => c.Int(nullable: false, identity: true));
            DropColumn("rentals.Vehicles", "Discriminator");
            DropColumn("rentals.Vehicles", "MaxSpeed");
            DropColumn("rentals.Vehicles", "EngineCapacity");
            AddPrimaryKey("rentals.Vehicles", "BikeId");
            RenameIndex(table: "rentals.Rentals", name: "IX_Vehicle_VehicleId", newName: "IX_Bike_BikeId");
            RenameColumn(table: "rentals.Rentals", name: "Vehicle_VehicleId", newName: "Bike_BikeId");
            AddForeignKey("rentals.Rentals", "Bike_BikeId", "rentals.Bikes", "BikeId");
            RenameTable(name: "rentals.Vehicles", newName: "Bikes");
        }
    }
}
