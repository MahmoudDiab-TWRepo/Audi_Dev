namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnquiryRequist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnquiryRequist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        Message = c.String(),
                        BookMe = c.Boolean(nullable: false),
                        CarID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EnquiryRequist");
        }
    }
}
