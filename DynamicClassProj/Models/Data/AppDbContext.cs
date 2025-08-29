using DynamicClassProj.Models.Entities;
using DynamicClassProj.Models.Test;
using System.Data.Entity;

namespace DynamicClassProj.Models.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext():base("name=AppDbContext")
        {
            
        }
        public DbSet<ContractSettlement> ContractSettlements { get; set; }
        public DbSet<Formula> Formulas { get; set; }
    }
}