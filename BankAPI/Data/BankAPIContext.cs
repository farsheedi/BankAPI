using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Models
{
    public class BankAPIContext : DbContext
    {
        public BankAPIContext (DbContextOptions<BankAPIContext> options)
            : base(options)
        {
        }

        public DbSet<BankAPI.Models.BankItem> BankItem { get; set; }
    }
}
