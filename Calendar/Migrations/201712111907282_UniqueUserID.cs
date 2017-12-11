namespace Calendar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueUserID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "UserID", c => c.String(nullable: false, maxLength: 32));
            CreateIndex("dbo.People", "UserID", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.People", new[] { "UserID" });
            AlterColumn("dbo.People", "UserID", c => c.String(maxLength: 32));
        }
    }
}
