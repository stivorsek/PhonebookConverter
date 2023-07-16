using PhonebookConverterL.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace PhonebookConverterL.Data
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
