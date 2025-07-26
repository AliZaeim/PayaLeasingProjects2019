using System.Data.Entity;

namespace ContractSettlement.Models
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=ContractSettelmentConnection")
        {
        }

        public virtual DbSet<ClassSetting> ClassSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassSetting>()
                .Property(e => e.ClassName)
                .IsUnicode(false);

            modelBuilder.Entity<ClassSetting>()
                .Property(e => e.PropName)
                .IsUnicode(false);

            modelBuilder.Entity<ClassSetting>()
                .Property(e => e.PropFormula)
                .IsUnicode(false);
        }
    }
}
