namespace Calendar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "UserID", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "UserID");
        }
    }
}
