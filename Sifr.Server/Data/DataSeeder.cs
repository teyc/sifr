using Sifr.Shared.Models;
using Sifr.Server;

namespace Sifr.Server.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Accounts.Any())
            {
                return;   // DB has been seeded
            }

            var now = DateTime.UtcNow;
            var accounts = new Account[]
            {
                new Account(Guid.NewGuid(), "200", "Sales", "Revenue", "OUTPUT", null, now, now),
                new Account(Guid.NewGuid(), "300", "Purchases", "Expense", "INPUT", null, now, now),
                new Account(Guid.NewGuid(), "090", "Business Bank Account", "Bank", "NONE", null, now, now),
                new Account(Guid.NewGuid(), "800", "Accounts Receivable", "Asset", "NONE", null, now, now),
                new Account(Guid.NewGuid(), "801", "Accounts Payable", "Liability", "NONE", null, now, now)
            };

            context.Accounts.AddRange(accounts);
            context.SaveChanges();
        }
    }
}
