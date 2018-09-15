namespace ParcelDelivery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_feedback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarrierId = c.Int(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        Date = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carriers", t => t.CarrierId, cascadeDelete: true)
                .Index(t => t.CarrierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "CarrierId", "dbo.Carriers");
            DropIndex("dbo.Feedbacks", new[] { "CarrierId" });
            DropTable("dbo.Feedbacks");
        }
    }
}
