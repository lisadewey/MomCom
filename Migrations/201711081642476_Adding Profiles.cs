namespace MomCom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingProfiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AgeRange = c.String(),
                        Gender = c.String(),
                        Park = c.Boolean(nullable: false),
                        Playground = c.Boolean(nullable: false),
                        Pool = c.Boolean(nullable: false),
                        City = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Profiles");
        }
    }
}
