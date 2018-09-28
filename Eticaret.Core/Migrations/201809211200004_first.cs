namespace Eticaret.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: false)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomTypeId = c.Int(nullable: false),
                        Sale = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeId, cascadeDelete: false)
                .Index(t => t.RoomTypeId);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PersonCount = c.Int(nullable: false),
                        Wifi = c.Boolean(nullable: false),
                        TV = c.Boolean(nullable: false),
                        Bathroom = c.Boolean(nullable: false),
                        airConditioning = c.Boolean(nullable: false),
                        Fund = c.Boolean(nullable: false),
                        Telephone = c.Boolean(nullable: false),
                        MiniBar = c.Boolean(nullable: false),
                        Jakuzi = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.Int(nullable: false),
                        Name = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.Int(nullable: false),
                        TelNum = c.String(),
                        HotelName = c.String(),
                        Address = c.String(),
                        Park = c.Boolean(nullable: false),
                        Restaurant = c.Boolean(nullable: false),
                        HotelBar = c.Boolean(nullable: false),
                        Spa = c.Boolean(nullable: false),
                        AreaId = c.Int(nullable: false),
                        Terrace = c.Boolean(nullable: false),
                        WashingMachine = c.Boolean(nullable: false),
                        RoomService = c.Boolean(nullable: false),
                        Gym = c.Boolean(nullable: false),
                        Pool = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: false)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: false)
                .Index(t => t.CustomerId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OtelId = c.Int(nullable: false),
                        RoomTypeId = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.OtelId, cascadeDelete: false)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeId, cascadeDelete: false)
                .Index(t => t.OtelId)
                .Index(t => t.RoomTypeId);
            
            CreateTable(
                "dbo.RoomImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Path = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        RoomId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: false)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomImages", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "RoomTypeId", "dbo.RoomTypes");
            DropForeignKey("dbo.Rooms", "OtelId", "dbo.Hotels");
            DropForeignKey("dbo.Reservations", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Hotels", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.Campaigns", "RoomTypeId", "dbo.RoomTypes");
            DropForeignKey("dbo.Areas", "CountryId", "dbo.Countries");
            DropIndex("dbo.RoomImages", new[] { "RoomId" });
            DropIndex("dbo.Rooms", new[] { "RoomTypeId" });
            DropIndex("dbo.Rooms", new[] { "OtelId" });
            DropIndex("dbo.Reservations", new[] { "RoomId" });
            DropIndex("dbo.Reservations", new[] { "CustomerId" });
            DropIndex("dbo.Hotels", new[] { "AreaId" });
            DropIndex("dbo.Campaigns", new[] { "RoomTypeId" });
            DropIndex("dbo.Areas", new[] { "CountryId" });
            DropTable("dbo.RoomImages");
            DropTable("dbo.Rooms");
            DropTable("dbo.Reservations");
            DropTable("dbo.Hotels");
            DropTable("dbo.Customers");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.Campaigns");
            DropTable("dbo.Countries");
            DropTable("dbo.Areas");
        }
    }
}
