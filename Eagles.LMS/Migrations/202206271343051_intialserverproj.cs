namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialserverproj : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        color = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ModelYear = c.Int(nullable: false),
                        Power = c.Decimal(nullable: false, precision: 18, scale: 2),
                        fuel = c.Int(nullable: false),
                        DriveTrain = c.Int(nullable: false),
                        MainImageOne = c.String(),
                        MainImageTwo = c.String(),
                        Description = c.String(),
                        NearestLocation = c.Decimal(precision: 18, scale: 2),
                        CategoryId = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Types", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.TypeID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        MainImageOne = c.String(),
                        MainImageTwo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CategoryID = c.Int(),
                        MainImageOne = c.String(),
                        MainImageTwo = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        assistance_Systems = c.Int(nullable: false),
                        infotainments = c.Int(nullable: false),
                        Headlights = c.Int(nullable: false),
                        Seats = c.Int(nullable: false),
                        interior = c.Boolean(),
                        exterior = c.Boolean(),
                        CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.ShownImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.GroupPriviages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrivilageId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Privilages", t => t.PrivilageId, cascadeDelete: true)
                .Index(t => t.PrivilageId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mobile = c.String(nullable: false),
                        EmailAddress = c.String(),
                        PasswordHash = c.Binary(nullable: false),
                        PasswordSalt = c.Binary(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UserTybe = c.Int(nullable: false),
                        AccountState = c.Int(nullable: false),
                        FireBaseToken = c.String(),
                        FullName = c.String(nullable: false),
                        latitude = c.String(),
                        altitude = c.String(),
                        Image = c.String(),
                        GroupId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        OTP = c.String(),
                        OTP_Provider = c.String(),
                        OTPTIME = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Privilages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenueName = c.String(),
                        ParentId = c.Int(),
                        IsRoute = c.Boolean(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ShowInMenue = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrivilageRoutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Area = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        Url = c.String(),
                        PrivilageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Privilages", t => t.PrivilageId, cascadeDelete: true)
                .Index(t => t.PrivilageId);
            
            CreateTable(
                "dbo.UserForLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FireBaseToken = c.String(),
                        Rememberme = c.Boolean(nullable: false),
                        CurrentTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrivilageRoutes", "PrivilageId", "dbo.Privilages");
            DropForeignKey("dbo.GroupPriviages", "PrivilageId", "dbo.Privilages");
            DropForeignKey("dbo.Users", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupPriviages", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.ShownImages", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Equipments", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Types", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Cars", "TypeID", "dbo.Types");
            DropForeignKey("dbo.Cars", "CategoryId", "dbo.Categories");
            DropIndex("dbo.PrivilageRoutes", new[] { "PrivilageId" });
            DropIndex("dbo.Users", new[] { "GroupId" });
            DropIndex("dbo.GroupPriviages", new[] { "GroupId" });
            DropIndex("dbo.GroupPriviages", new[] { "PrivilageId" });
            DropIndex("dbo.ShownImages", new[] { "CarId" });
            DropIndex("dbo.Equipments", new[] { "CarId" });
            DropIndex("dbo.Types", new[] { "CategoryID" });
            DropIndex("dbo.Cars", new[] { "TypeID" });
            DropIndex("dbo.Cars", new[] { "CategoryId" });
            DropTable("dbo.UserForLogins");
            DropTable("dbo.PrivilageRoutes");
            DropTable("dbo.Privilages");
            DropTable("dbo.Users");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupPriviages");
            DropTable("dbo.ShownImages");
            DropTable("dbo.Equipments");
            DropTable("dbo.Types");
            DropTable("dbo.Categories");
            DropTable("dbo.Cars");
        }
    }
}
