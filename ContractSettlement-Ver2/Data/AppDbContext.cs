using System.Data.Entity;
using ContractSettlement_Ver2.Models.Entites;
//using FormulaMvcProject.Models;

namespace ContractSettlement_Ver2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<SalaryFormula> SalaryFormulas { get; set; }
    }
}