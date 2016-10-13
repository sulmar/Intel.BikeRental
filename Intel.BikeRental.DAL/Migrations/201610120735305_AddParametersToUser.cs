namespace Intel.BikeRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParametersToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("rentals.Users", "Parameters", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("rentals.Users", "Parameters");
        }
    }
}
