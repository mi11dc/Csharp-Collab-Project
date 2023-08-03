namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableVenue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Location = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Venues");
        }
    }
}
