namespace Calendar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.People", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "UserID", c => c.String());
        }
    }
}
