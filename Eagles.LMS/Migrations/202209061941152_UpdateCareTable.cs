namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCareTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "FWD", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Quattro", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Cruise_Control", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Hill_descent_control", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Hold_assist", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Rear_view_camera", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Head_up_display", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Rear_Eid_Pluss", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Rear_parking_Eid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Park_assist", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Pre_sense", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "LED", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Matrix_LED", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Audi_smartphone_interface", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Standard", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Sport_seats", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Sport_seats");
            DropColumn("dbo.Cars", "Standard");
            DropColumn("dbo.Cars", "Audi_smartphone_interface");
            DropColumn("dbo.Cars", "Matrix_LED");
            DropColumn("dbo.Cars", "LED");
            DropColumn("dbo.Cars", "Pre_sense");
            DropColumn("dbo.Cars", "Park_assist");
            DropColumn("dbo.Cars", "Rear_parking_Eid");
            DropColumn("dbo.Cars", "Rear_Eid_Pluss");
            DropColumn("dbo.Cars", "Head_up_display");
            DropColumn("dbo.Cars", "Rear_view_camera");
            DropColumn("dbo.Cars", "Hold_assist");
            DropColumn("dbo.Cars", "Hill_descent_control");
            DropColumn("dbo.Cars", "Cruise_Control");
            DropColumn("dbo.Cars", "Quattro");
            DropColumn("dbo.Cars", "FWD");
        }
    }
}
