namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableTeam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                        UserDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.UserDetailId, cascadeDelete: true)
                .Index(t => t.UserDetailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "UserDetailId", "dbo.UserDetails");
            DropIndex("dbo.Teams", new[] { "UserDetailId" });
            DropTable("dbo.Teams");
        }
    }
}
