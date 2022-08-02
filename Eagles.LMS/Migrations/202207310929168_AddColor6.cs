namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColor6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "ColorId", "dbo.Color");
            DropIndex("dbo.Cars", new[] { "ColorId" });
            AlterColumn("dbo.Cars", "ColorId", c => c.Int());
            CreateIndex("dbo.Cars", "ColorId");
            AddForeignKey("dbo.Cars", "ColorId", "dbo.Color", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "ColorId", "dbo.Color");
            DropIndex("dbo.Cars", new[] { "ColorId" });
            AlterColumn("dbo.Cars", "ColorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "ColorId");
            AddForeignKey("dbo.Cars", "ColorId", "dbo.Color", "Id", cascadeDelete: true);
        }
    }
}
