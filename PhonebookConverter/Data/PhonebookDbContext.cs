using PhonebookConverter.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace PhonebookConverter.Data
{
    public class PhonebookDbContext : DbContext
    {
        public PhonebookDbContext(DbContextOptions<PhonebookDbContext> options)
            : base(options)
        {
        }
        public DbSet<ContactInDb> Phonebook {get;set;}
    }
}
