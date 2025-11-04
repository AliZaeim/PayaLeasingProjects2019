namespace DynamicClassProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Formulas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 50),
                        FieldName = c.String(nullable: false, maxLength: 100),
                        Expression = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Formulas");
        }
    }
}
