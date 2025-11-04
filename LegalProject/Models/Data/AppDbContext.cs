using LegalProject.Models.Entities;
using System.Data.Entity;

namespace LegalProject.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext")
        {

        }
        public DbSet<MainModel> MainModels { get; set; }
        public DbSet<DetailsModel> DetailsModels { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}