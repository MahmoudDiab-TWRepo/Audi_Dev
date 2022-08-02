namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColor5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "color_Id", "dbo.Color");
            DropIndex("dbo.Cars", new[] { "color_Id" });
            RenameColumn(table: "dbo.Cars", name: "color_Id", newName: "ColorId");
            AlterColumn("dbo.Cars", "ColorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "ColorId");
            AddForeignKey("dbo.Cars", "ColorId", "dbo.Color", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "ColorId", "dbo.Color");
            DropIndex("dbo.Cars", new[] { "ColorId" });
            AlterColumn("dbo.Cars", "ColorId", c => c.Int());
            RenameColumn(table: "dbo.Cars", name: "ColorId", newName: "color_Id");
            CreateIndex("dbo.Cars", "color_Id");
            AddForeignKey("dbo.Cars", "color_Id", "dbo.Color", "Id");
        }
    }
}
