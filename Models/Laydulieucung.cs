using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThiThuToiec2.Models
{
    public class Laydulieucung
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ThiThuToiec2Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ThiThuToiec2Context>>()))
            {
                // Look for any movies.
                if (context.Accounts.Any())
                {
                    return;   // DB has been seeded
                }

                context.Accounts.AddRange(
                    new Account
                    {
                        Id=1,
                        Name="luan"

                    }
                );
                context.SaveChanges();
            }
        }
    }
}
