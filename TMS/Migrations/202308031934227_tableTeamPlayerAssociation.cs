namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableTeamPlayerAssociation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamPlayerAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.PlayerId, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: false)
                .Index(t => t.TeamId)
                .Index(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamPlayerAssociations", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamPlayerAssociations", "PlayerId", "dbo.UserDetails");
            DropIndex("dbo.TeamPlayerAssociations", new[] { "PlayerId" });
            DropIndex("dbo.TeamPlayerAssociations", new[] { "TeamId" });
            DropTable("dbo.TeamPlayerAssociations");
        }
    }
}
