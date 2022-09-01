namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderEnquiry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderEnquiry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImAnOudiowner = c.Boolean(nullable: false),
                        IWantToBeAnOudiowner = c.Boolean(nullable: false),
                        FullName = c.String(),
                        MobileNumber = c.String(),
                        EmailAddress = c.String(),
                        OldCarModel = c.String(),
                        OldEnginCapacity = c.String(),
                        OldModelYear = c.String(),
                        OldMileage = c.String(),
                        OldComment = c.String(),
                        ChassisNumber = c.String(),
                        CarModel = c.String(),
                        EnginCapacity = c.String(),
                        ModelYear = c.String(),
                        Mileage = c.String(),
                        Comment = c.String(),
                        CarCode = c.String(),
                        CarID = c.String(),
                        Sendtime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderEnquiry");
        }
    }
}
