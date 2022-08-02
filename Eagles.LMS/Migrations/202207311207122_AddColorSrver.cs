namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColorSrver : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "Power", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "Power", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
