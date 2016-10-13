namespace Intel.BikeRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TruncateTest : DbMigration
    {
        public override void Up()
        {

            //using (var context = new BikeRentalContext())
            //{
            //    var users = context.Users;

            //    foreach (var item in users)
            //    {
            //        item.FirstName = item.FirstName.Substring(0, 3);
            //    }

            //    context.SaveChanges();
            //}
        }
        
        public override void Down()
        {
        }
    }
}
