namespace LegalProject.Migrations
{
    using LegalProject.Models.Entities;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Models.Data.AppDbContext context)
        {
            List<MainModel> list = new List<MainModel>()
            {
                new MainModel {Id= 1},
                new MainModel {Id= 2},
                new MainModel {Id= 3},
            };
            context.MainModels.AddRange(list);

        }
    }
}
