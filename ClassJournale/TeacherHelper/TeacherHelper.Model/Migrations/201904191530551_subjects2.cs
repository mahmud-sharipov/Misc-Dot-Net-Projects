namespace TeacherHelper.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subjects2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subjects");
        }
    }
}
