namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableTournamentVenueAssociation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TournamentVenueAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RentedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VenueId = c.Int(nullable: false),
                        TournamentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: false)
                .ForeignKey("dbo.Venues", t => t.VenueId, cascadeDelete: false)
                .Index(t => t.VenueId)
                .Index(t => t.TournamentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TournamentVenueAssociations", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.TournamentVenueAssociations", "TournamentId", "dbo.Tournaments");
            DropIndex("dbo.TournamentVenueAssociations", new[] { "TournamentId" });
            DropIndex("dbo.TournamentVenueAssociations", new[] { "VenueId" });
            DropTable("dbo.TournamentVenueAssociations");
        }
    }
}
