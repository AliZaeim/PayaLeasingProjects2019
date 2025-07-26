using System.Data.Entity;

namespace PayaInsProj.Models.PayaInsureDbData
{
    public partial class PIDbContext : DbContext
    {
        public PIDbContext()
            : base("name=PIConnectionString")
        {
        }

        public virtual DbSet<FileUpoadHistory> FileUpoadHistories { get; set; }
        public virtual DbSet<InsuranceCustomerContract> InsuranceCustomerContracts { get; set; }
        public virtual DbSet<InsuranceDetailCustomerContract> InsuranceDetailCustomerContracts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.Nationalcode)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.GoodPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.InsuranceAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.InsuranceIssueDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.InsurerContractDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.InsurerContractBeginDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.InsurerContractEndDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.InsertDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.InsertTime)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.ConfirmIssuDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.ConfirmIssuTime)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.InsurancAmountInput)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.InsurancAmountInputTax)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.InsurancAmountSumInput)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.ManualUpdateDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceCustomerContract>()
                .Property(e => e.ManualUpdateTime)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.InsuranceIssueDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.InsuranceBeginDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.InsuranceEndDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.InsuranceAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.InsertDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.InsertTime)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.ConfirmIssuDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.ConfirmIssuTime)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.InsurancAmountInput)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.InsurancAmountInputTax)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.InsurancAmountSumInput)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.ManualUpdateDate)
                .IsFixedLength();

            modelBuilder.Entity<InsuranceDetailCustomerContract>()
                .Property(e => e.ManualUpdateTime)
                .IsFixedLength();
        }
    }
}
