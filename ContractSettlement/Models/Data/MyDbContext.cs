using ContractSettlement.Models.Entities;
using System.Data.Entity;

namespace ContractSettlement.Models.Data
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=MyDbContext")
        {
        }

        public virtual DbSet<ClassInfo> ClassInfoes { get; set; }
        public virtual DbSet<PropInfo> PropInfoes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassInfo>()
                .Property(e => e.ClassName)
                .IsFixedLength();

            

            modelBuilder.Entity<PropInfo>()
                .Property(e => e.PropName)
                .IsFixedLength();

            modelBuilder.Entity<PropInfo>()
                .Property(e => e.PropFormula)
                .IsFixedLength();
        }

        public System.Data.Entity.DbSet<ContractSettlement.Models.ClassSettings> ClassSettings { get; set; }
    }
}
