namespace Intel.BikeRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStationName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("rentals.Stations", "Name", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("rentals.Stations", "Name", c => c.String());
        }
    }
}
