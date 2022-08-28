namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCareCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "CarCode", c => c.String());
            AddColumn("dbo.EnquiryRequist", "CarCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EnquiryRequist", "CarCode");
            DropColumn("dbo.Cars", "CarCode");
        }
    }
}
