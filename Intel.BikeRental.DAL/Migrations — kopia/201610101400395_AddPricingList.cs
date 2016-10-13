namespace Intel.BikeRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPricingList : DbMigration
    {
        public override void Up()
        {
            SqlResource("Intel.BikeRental.DAL.Scripts.201610101400395_AddPricingList_Up.sql", suppressTransaction: true);

            CreateTable(
                "rentals.PricingLists",
                c => new
                    {
                        PricingListId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PricingListId);
            
            AddColumn("rentals.Users", "Gender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("rentals.Users", "Gender");
            DropTable("rentals.PricingLists");
        }
    }
}
