using DynamicClassProj.Models.Data;
using DynamicClassProj.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DynamicClassProj.Models.Services
{
    public class DbService
    {
        private readonly AppDbContext _appDbContext;
        public DbService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }
        

    }
}