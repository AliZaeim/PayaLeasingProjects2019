namespace DynamicClassProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ClassInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldName = c.String(nullable: false, maxLength: 50),
                        FieldValue = c.String(nullable: false, maxLength: 100),
                        FieldType = c.String(nullable: false, maxLength: 50),
                        FieldFormula = c.String(maxLength: 100),
                        ClassInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassInfoes", t => t.ClassInfoId, cascadeDelete: true)
                .Index(t => t.ClassInfoId);
            
            CreateTable(
                "dbo.ClassInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassDatas", "ClassInfoId", "dbo.ClassInfoes");
            DropIndex("dbo.ClassDatas", new[] { "ClassInfoId" });
            DropTable("dbo.ClassInfoes");
            DropTable("dbo.ClassDatas");
        }
    }
}
