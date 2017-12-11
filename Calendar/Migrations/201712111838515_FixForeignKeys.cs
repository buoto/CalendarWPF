namespace Calendar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "Appointment_AppointmentId", "dbo.Appointments");
            DropForeignKey("dbo.Attendances", "Person_PersonId", "dbo.People");
            DropIndex("dbo.Attendances", new[] { "Appointment_AppointmentId" });
            DropIndex("dbo.Attendances", new[] { "Person_PersonId" });
            RenameColumn(table: "dbo.Attendances", name: "Appointment_AppointmentId", newName: "AppointmentId");
            RenameColumn(table: "dbo.Attendances", name: "Person_PersonId", newName: "PersonId");
            AlterColumn("dbo.Attendances", "AppointmentId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Attendances", "PersonId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Attendances", "AppointmentId");
            CreateIndex("dbo.Attendances", "PersonId");
            AddForeignKey("dbo.Attendances", "AppointmentId", "dbo.Appointments", "AppointmentId", cascadeDelete: true);
            AddForeignKey("dbo.Attendances", "PersonId", "dbo.People", "PersonId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "PersonId", "dbo.People");
            DropForeignKey("dbo.Attendances", "AppointmentId", "dbo.Appointments");
            DropIndex("dbo.Attendances", new[] { "PersonId" });
            DropIndex("dbo.Attendances", new[] { "AppointmentId" });
            AlterColumn("dbo.Attendances", "PersonId", c => c.Guid());
            AlterColumn("dbo.Attendances", "AppointmentId", c => c.Guid());
            RenameColumn(table: "dbo.Attendances", name: "PersonId", newName: "Person_PersonId");
            RenameColumn(table: "dbo.Attendances", name: "AppointmentId", newName: "Appointment_AppointmentId");
            CreateIndex("dbo.Attendances", "Person_PersonId");
            CreateIndex("dbo.Attendances", "Appointment_AppointmentId");
            AddForeignKey("dbo.Attendances", "Person_PersonId", "dbo.People", "PersonId");
            AddForeignKey("dbo.Attendances", "Appointment_AppointmentId", "dbo.Appointments", "AppointmentId");
        }
    }
}
