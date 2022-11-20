namespace TeacherHelper.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class settins : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JournalSettings",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        YearStudy = c.Int(nullable: false),
                        Group = c.String(unicode: false),
                        ClassSubjectCount = c.Int(nullable: false),
                        ClassStudentCount = c.Int(nullable: false),
                        Quarter1Start = c.DateTime(nullable: false, precision: 0),
                        Quarter2Start = c.DateTime(nullable: false, precision: 0),
                        Quarter3Start = c.DateTime(nullable: false, precision: 0),
                        Quarter4Start = c.DateTime(nullable: false, precision: 0),
                        Quarter1End = c.DateTime(nullable: false, precision: 0),
                        Quarter2End = c.DateTime(nullable: false, precision: 0),
                        Quarter3End = c.DateTime(nullable: false, precision: 0),
                        Quarter4End = c.DateTime(nullable: false, precision: 0),
                        FormMaster_Guid = c.Guid(),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Teachers", t => t.FormMaster_Guid)
                .Index(t => t.FormMaster_Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalSettings", "FormMaster_Guid", "dbo.Teachers");
            DropIndex("dbo.JournalSettings", new[] { "FormMaster_Guid" });
            DropTable("dbo.JournalSettings");
        }
    }
}
