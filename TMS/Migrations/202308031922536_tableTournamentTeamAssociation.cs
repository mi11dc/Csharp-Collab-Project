namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableTournamentTeamAssociation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TournamentTeamAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: false)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: false)
                .Index(t => t.TournamentId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TournamentTeamAssociations", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentTeamAssociations", "TeamId", "dbo.Teams");
            DropIndex("dbo.TournamentTeamAssociations", new[] { "TeamId" });
            DropIndex("dbo.TournamentTeamAssociations", new[] { "TournamentId" });
            DropTable("dbo.TournamentTeamAssociations");
        }
    }
}
