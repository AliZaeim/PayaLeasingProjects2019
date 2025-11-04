namespace DynamicClassProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_Reqired_of_ClassInfoId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassDatas", "ClassInfoId", "dbo.ClassInfoes");
            DropIndex("dbo.ClassDatas", new[] { "ClassInfoId" });
            AlterColumn("dbo.ClassDatas", "ClassInfoId", c => c.Int());
            CreateIndex("dbo.ClassDatas", "ClassInfoId");
            AddForeignKey("dbo.ClassDatas", "ClassInfoId", "dbo.ClassInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassDatas", "ClassInfoId", "dbo.ClassInfoes");
            DropIndex("dbo.ClassDatas", new[] { "ClassInfoId" });
            AlterColumn("dbo.ClassDatas", "ClassInfoId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClassDatas", "ClassInfoId");
            AddForeignKey("dbo.ClassDatas", "ClassInfoId", "dbo.ClassInfoes", "Id", cascadeDelete: true);
        }
    }
}
