namespace TeacherHelper.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        LastName = c.String(unicode: false),
                        FirstName = c.String(unicode: false),
                        Patronymic = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Birthday = c.DateTime(nullable: false, precision: 0),
                        PhoneNumber = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.StudentParents",
                c => new
                    {
                        StudentGuid = c.Guid(nullable: false),
                        ParentGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentGuid, t.ParentGuid })
                .ForeignKey("dbo.Students", t => t.StudentGuid)
                .ForeignKey("dbo.Parents", t => t.ParentGuid)
                .Index(t => t.StudentGuid)
                .Index(t => t.ParentGuid);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.Guid)
                .Index(t => t.Guid);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        ParentKind = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.Guid)
                .Index(t => t.Guid);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Login = c.String(unicode: false),
                        Password = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.Guid)
                .Index(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "Guid", "dbo.Users");
            DropForeignKey("dbo.Parents", "Guid", "dbo.Users");
            DropForeignKey("dbo.Students", "Guid", "dbo.Users");
            DropForeignKey("dbo.StudentParents", "ParentGuid", "dbo.Parents");
            DropForeignKey("dbo.StudentParents", "StudentGuid", "dbo.Students");
            DropIndex("dbo.Teachers", new[] { "Guid" });
            DropIndex("dbo.Parents", new[] { "Guid" });
            DropIndex("dbo.Students", new[] { "Guid" });
            DropIndex("dbo.StudentParents", new[] { "ParentGuid" });
            DropIndex("dbo.StudentParents", new[] { "StudentGuid" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Parents");
            DropTable("dbo.Students");
            DropTable("dbo.StudentParents");
            DropTable("dbo.Users");
        }
    }
}
