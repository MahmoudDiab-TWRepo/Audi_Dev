namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComparisonAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comparison",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarID = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comparison");
        }
    }
}
