using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BankAPIContext(
                serviceProvider.GetRequiredService<DbContextOptions<BankAPIContext>>()))
            {
                // Look for any movies.
                if (context.BankItem.Count() > 0)
                {
                    return;   // DB has been seeded
                }

                context.BankItem.AddRange(
                    new BankItem
                    {
                        User = "fresh",
                        Asset = "car",
                        AssetType = "vehicle",
                        Value = "3500",
                    }


                );
                context.SaveChanges();
            }
        }
    }
}
