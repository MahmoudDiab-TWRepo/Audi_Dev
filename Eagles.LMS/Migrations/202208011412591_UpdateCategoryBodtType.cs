namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategoryBodtType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ShowInBodyType", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ShowInBodyType");
        }
    }
}
