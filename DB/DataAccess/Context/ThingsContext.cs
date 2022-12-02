using Microsoft.EntityFrameworkCore;
using DB.Entities;

namespace DB.DataAccess.Context
{
    public class ThingsContext : DbContext
    {
        public ThingsContext(DbContextOptions options) :
            base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Things> Things { get; set; }

    }
}
