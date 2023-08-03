namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableTournament : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Teams", name: "UserDetailId", newName: "OwnerId");
            RenameIndex(table: "dbo.Teams", name: "IX_UserDetailId", newName: "IX_OwnerId");
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        OrganizerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.OrganizerId, cascadeDelete: true)
                .Index(t => t.OrganizerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tournaments", "OrganizerId", "dbo.UserDetails");
            DropIndex("dbo.Tournaments", new[] { "OrganizerId" });
            DropTable("dbo.Tournaments");
            RenameIndex(table: "dbo.Teams", name: "IX_OwnerId", newName: "IX_UserDetailId");
            RenameColumn(table: "dbo.Teams", name: "OwnerId", newName: "UserDetailId");
        }
    }
}
