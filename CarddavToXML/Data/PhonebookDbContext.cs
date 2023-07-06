using CarddavToXML.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace CarddavToXML.Data
{
    public class PhonebookDbContext : DbContext
    {
        public PhonebookDbContext(DbContextOptions<PhonebookDbContext> options)
            : base(options)
        {
        }
        public DbSet<YealinkCsv> Phonebook {get;set;}
    }
}
