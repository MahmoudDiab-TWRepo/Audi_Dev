namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Color",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageColor = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cars", "color_Id", c => c.Int());
            CreateIndex("dbo.Cars", "color_Id");
            AddForeignKey("dbo.Cars", "color_Id", "dbo.Color", "Id");
            DropColumn("dbo.Cars", "color");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "color", c => c.Int(nullable: false));
            DropForeignKey("dbo.Cars", "color_Id", "dbo.Color");
            DropIndex("dbo.Cars", new[] { "color_Id" });
            DropColumn("dbo.Cars", "color_Id");
            DropTable("dbo.Color");
        }
    }
}
