using DynamicClassProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DynamicClassProject.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext")
        {

        }

        public DbSet<Formula> Formulas { get; set; }
        public DbSet<ClassInfo> ClassInfos { get; set; }
        public DbSet<ClassData> ClassDatas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Formula>().ToTable("Formulas");
            base.OnModelCreating(modelBuilder);
        }
    }
}