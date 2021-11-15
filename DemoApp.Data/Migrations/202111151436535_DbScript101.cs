namespace DemoApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbScript101 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        GUID = c.String(nullable: false, maxLength: 128),
                        EmailAddress = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.GUID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
