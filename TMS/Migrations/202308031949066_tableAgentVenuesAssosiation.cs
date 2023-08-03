namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableAgentVenuesAssosiation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgentVenuesAssosiations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VenueId = c.Int(nullable: false),
                        AgentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.AgentId, cascadeDelete: false)
                .ForeignKey("dbo.Venues", t => t.VenueId, cascadeDelete: false)
                .Index(t => t.VenueId)
                .Index(t => t.AgentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AgentVenuesAssosiations", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.AgentVenuesAssosiations", "AgentId", "dbo.UserDetails");
            DropIndex("dbo.AgentVenuesAssosiations", new[] { "AgentId" });
            DropIndex("dbo.AgentVenuesAssosiations", new[] { "VenueId" });
            DropTable("dbo.AgentVenuesAssosiations");
        }
    }
}
