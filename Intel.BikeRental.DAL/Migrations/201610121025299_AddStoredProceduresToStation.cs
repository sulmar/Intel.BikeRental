namespace Intel.BikeRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStoredProceduresToStation : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "rentals.Station_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 10),
                        Location_Latitude = p.Double(),
                        Location_Longitude = p.Double(),
                        Address = p.String(),
                        OpenTimeFrom = p.Time(),
                        OpenTimeTo = p.Time(),
                        Capacity = p.Byte(),
                        IsActive = p.Boolean(),
                    },
                body:
                    @"INSERT [rentals].[Stations]([Name], [Location_Latitude], [Location_Longitude], [Address], [OpenTimeFrom], [OpenTimeTo], [Capacity], [IsActive])
                      VALUES (@Name, @Location_Latitude, @Location_Longitude, @Address, @OpenTimeFrom, @OpenTimeTo, @Capacity, @IsActive)
                      
                      DECLARE @StationId int
                      SELECT @StationId = [StationId]
                      FROM [rentals].[Stations]
                      WHERE @@ROWCOUNT > 0 AND [StationId] = scope_identity()
                      
                      SELECT t0.[StationId]
                      FROM [rentals].[Stations] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[StationId] = @StationId"
            );
            
            CreateStoredProcedure(
                "rentals.Station_Update",
                p => new
                    {
                        StationId = p.Int(),
                        Name = p.String(maxLength: 10),
                        Location_Latitude = p.Double(),
                        Location_Longitude = p.Double(),
                        Address = p.String(),
                        OpenTimeFrom = p.Time(),
                        OpenTimeTo = p.Time(),
                        Capacity = p.Byte(),
                        IsActive = p.Boolean(),
                    },
                body:
                    @"UPDATE [rentals].[Stations]
                      SET [Name] = @Name, [Location_Latitude] = @Location_Latitude, [Location_Longitude] = @Location_Longitude, [Address] = @Address, [OpenTimeFrom] = @OpenTimeFrom, [OpenTimeTo] = @OpenTimeTo, [Capacity] = @Capacity, [IsActive] = @IsActive
                      WHERE ([StationId] = @StationId)"
            );
            
            CreateStoredProcedure(
                "rentals.Station_Delete",
                p => new
                    {
                        StationId = p.Int(),
                    },
                body:
                    @"DELETE [rentals].[Stations]
                      WHERE ([StationId] = @StationId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("rentals.Station_Delete");
            DropStoredProcedure("rentals.Station_Update");
            DropStoredProcedure("rentals.Station_Insert");
        }
    }
}
