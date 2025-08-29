namespace DynamicClassProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractSettlements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        NC = c.String(nullable: false, maxLength: 10),
                        ContractNumber = c.String(nullable: false, maxLength: 50),
                        ContractDate = c.String(nullable: false, maxLength: 20),
                        FacilityAmount = c.Long(nullable: false),
                        TotalInstallments = c.Long(nullable: false),
                        DueAmount = c.Long(nullable: false),
                        PaymentAmount = c.Long(nullable: false),
                        InstallmentBalance = c.Long(nullable: false),
                        DelayInterest = c.Long(nullable: false),
                        PaymentDelayInterest = c.Long(nullable: false),
                        InterestRemain = c.Long(nullable: false),
                        DelayPenalty = c.Long(nullable: false),
                        PaymentDelayPenalty = c.Long(nullable: false),
                        PenaltyRemain = c.Long(nullable: false),
                        FutureInstallments = c.Long(nullable: false),
                        PaymentFutureInstallments = c.Long(nullable: false),
                        FutureInstallmentsRemain = c.Long(nullable: false),
                        FutureInstallmentsType = c.String(maxLength: 50),
                        DynamicPercentofSubFutureInstallments = c.Long(nullable: false),
                        RemainDynamicPercentofSubFutureInstallments = c.Long(nullable: false),
                        PayableValue = c.Long(),
                        DynamicPercentofSubFutureInstallmentsFormula = c.String(nullable: false, maxLength: 100),
                        RemainDynamicPercentofSubFutureInstallmentsFormula = c.String(nullable: false, maxLength: 100),
                        PayableValueFormula = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Formulae",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 50),
                        FieldName = c.String(nullable: false, maxLength: 50),
                        Expression = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Formulae");
            DropTable("dbo.ContractSettlements");
        }
    }
}
