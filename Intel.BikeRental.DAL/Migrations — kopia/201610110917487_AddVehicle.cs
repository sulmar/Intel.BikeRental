namespace Intel.BikeRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVehicle : DbMigration
    {
        public override void Up()
        {
            AddColumn("rentals.Bikes", "VehicleId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("rentals.Bikes", "VehicleId");
        }
    }
}
