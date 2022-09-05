namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubItemIDINCareTwo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "SubItem_Id", "dbo.SubItem");
            DropIndex("dbo.Cars", new[] { "SubItem_Id" });
            AddColumn("dbo.Cars", "subItems_Id", c => c.Int());
            CreateIndex("dbo.Cars", "subItems_Id");
            AddForeignKey("dbo.Cars", "subItems_Id", "dbo.SubItem", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "subItems_Id", "dbo.SubItem");
            DropIndex("dbo.Cars", new[] { "subItems_Id" });
            DropColumn("dbo.Cars", "subItems_Id");
            CreateIndex("dbo.Cars", "SubItem_Id");
            AddForeignKey("dbo.Cars", "SubItem_Id", "dbo.SubItem", "Id");
        }
    }
}
