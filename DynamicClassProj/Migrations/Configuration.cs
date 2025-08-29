namespace DynamicClassProj.Migrations
{
    using DynamicClassProj.Models.Test;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DynamicClassProj.Models.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DynamicClassProj.Models.Data.AppDbContext context)
        {
            context.Formulas.AddOrUpdate(f => f.FieldName,
            new Formula { ClassName = "TestModel", FieldName = "C", Expression = "A + B", Description = "C = A + B" },
            new Formula { ClassName = "TestModel" , FieldName = "D", Expression = "A * (B + C)", Description = "D = A*(B + C)" }
        );
        }
    }
}
