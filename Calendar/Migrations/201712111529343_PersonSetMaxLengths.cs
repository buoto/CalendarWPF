namespace Calendar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonSetMaxLengths : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "FirstName", c => c.String(maxLength: 32));
            AlterColumn("dbo.People", "LastName", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "LastName", c => c.String());
            AlterColumn("dbo.People", "FirstName", c => c.String());
        }
    }
}
