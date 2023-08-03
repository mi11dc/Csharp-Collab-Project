namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableMatch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Team1Id = c.Int(nullable: false),
                        Team2Id = c.Int(nullable: false),
                        TournamentVenueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team1Id, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.Team2Id, cascadeDelete: false)
                .ForeignKey("dbo.TournamentVenueAssociations", t => t.TournamentVenueId, cascadeDelete: false)
                .Index(t => t.Team1Id)
                .Index(t => t.Team2Id)
                .Index(t => t.TournamentVenueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "TournamentVenueId", "dbo.TournamentVenueAssociations");
            DropForeignKey("dbo.Matches", "Team2Id", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Team1Id", "dbo.Teams");
            DropIndex("dbo.Matches", new[] { "TournamentVenueId" });
            DropIndex("dbo.Matches", new[] { "Team2Id" });
            DropIndex("dbo.Matches", new[] { "Team1Id" });
            DropTable("dbo.Matches");
        }
    }
}
