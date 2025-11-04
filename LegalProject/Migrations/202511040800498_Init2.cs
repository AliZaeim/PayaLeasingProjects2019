namespace LegalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetailsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionType = c.Int(nullable: false),
                        ActionDate = c.String(nullable: false, maxLength: 50),
                        Comment = c.String(maxLength: 200),
                        CaseNumber = c.String(nullable: false, maxLength: 50),
                        TrackingCode = c.String(maxLength: 50),
                        MainModelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MainModels", t => t.MainModelId)
                .Index(t => t.MainModelId);
            
            CreateTable(
                "dbo.MainModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Family = c.String(maxLength: 50),
                        NationalId = c.String(maxLength: 50),
                        ContractNumber = c.String(maxLength: 50),
                        ChequeNumber = c.String(maxLength: 50),
                        ChequeDate = c.String(maxLength: 50),
                        ChequeState = c.Int(),
                        PatitionDate = c.String(maxLength: 50),
                        ExecArchiveNumber = c.String(maxLength: 50),
                        BranchCode = c.String(maxLength: 50),
                        ObtainFromBranch = c.String(maxLength: 50),
                        ReginProg = c.String(maxLength: 50),
                        TripleInquiry = c.String(maxLength: 50),
                        SeizureDate = c.String(maxLength: 50),
                        SeizureType = c.Int(),
                        ReturnOfComplaint = c.String(maxLength: 50),
                        Closed = c.String(maxLength: 50),
                        ClosedDate = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetailsModels", "MainModelId", "dbo.MainModels");
            DropIndex("dbo.DetailsModels", new[] { "MainModelId" });
            DropTable("dbo.MainModels");
            DropTable("dbo.DetailsModels");
        }
    }
}
