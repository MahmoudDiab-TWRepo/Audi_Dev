namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MainImageOne = c.String(),
                        MainImageTwo = c.String(),
                        CategoryID = c.Int(),
                        TypeID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Types", t => t.TypeID)
                .Index(t => t.CategoryID)
                .Index(t => t.TypeID);
            
            AddColumn("dbo.Cars", "SubItem_Id", c => c.Int());
            CreateIndex("dbo.Cars", "SubItem_Id");
            AddForeignKey("dbo.Cars", "SubItem_Id", "dbo.SubItem", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubItem", "TypeID", "dbo.Types");
            DropForeignKey("dbo.SubItem", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Cars", "SubItem_Id", "dbo.SubItem");
            DropIndex("dbo.SubItem", new[] { "TypeID" });
            DropIndex("dbo.SubItem", new[] { "CategoryID" });
            DropIndex("dbo.Cars", new[] { "SubItem_Id" });
            DropColumn("dbo.Cars", "SubItem_Id");
            DropTable("dbo.SubItem");
        }
    }
}
