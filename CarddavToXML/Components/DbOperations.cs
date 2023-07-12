using CarddavToXML.Data.Entities;
using CarddavToXML.Data;

namespace PhonebookConverter.Components
{
    public class DbOperations : IDbOperations
    {
        private readonly PhonebookDbContext _phonebookDbContext;
        public DbOperations( PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
        }
        public void AddNewDbEntry()
        {
            Console.WriteLine("Podaj Nazwę");
            var Name = Console.ReadLine();
            Console.WriteLine("Podaj pierwszy numer telefonu");
            var Phone1 = Console.ReadLine();
            Console.WriteLine("Podaj drugi numer telefonu");
            var Phone2 = Console.ReadLine();
            Console.WriteLine("Podaj trzeci numer telefonu");
            var Phone3 = Console.ReadLine();
            _phonebookDbContext.Add(new ContactInDb()
            {
                Name = Name,
                Phone1 = Phone1,
                Phone2 = Phone2,
                Phone3 = Phone3
            });
            _phonebookDbContext.SaveChanges();
            
        }
        public void EditFromDbByID(string? id)
        {
            var Id = int.Parse(id);
            var contactFromDb = _phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == Id);
            if (contactFromDb == null) 
            {
                throw new ArgumentException("Podane ID nie istnieje w bazie danych!!!");
            }
            Console.WriteLine($"\t1) Name : {contactFromDb.Name}");
            Console.WriteLine($"\t2) Phone1 : {contactFromDb.Phone1}");
            Console.WriteLine($"\t3) Phone2 : {contactFromDb.Phone2}");
            Console.WriteLine($"\t4) Phone3 : {contactFromDb.Phone3}");
            Console.WriteLine("");
            Console.WriteLine("Który parametr chcesz zmienić?");
            var choise = Console.ReadLine();
            Console.WriteLine("Podaj na co chcesz zmienić parametr");
            var parameter = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    contactFromDb.Name = parameter;
                    _phonebookDbContext.SaveChanges();
                    break;
                case "2":
                    contactFromDb.Phone1 = parameter;
                    _phonebookDbContext.SaveChanges();
                    break;
                case "3":
                    contactFromDb.Phone2 = parameter;
                    _phonebookDbContext.SaveChanges();
                    break;
                case "4":
                    contactFromDb.Phone3 = parameter;
                    _phonebookDbContext.SaveChanges();
                    break;
                default:
                    throw new ArgumentException("Nie ma takiego parametru!!!");
            }
        }
        public void DeleteFromDbByID(string? id)
        {
            int Id = int.Parse(id);
            var toRemove = _phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == Id);
            if (toRemove == null)
            {
                throw new ArgumentException("Podane ID nie istnieje w bazie danych!!!");
            }
            _phonebookDbContext.Phonebook.Remove(toRemove);
            _phonebookDbContext.SaveChanges();            
        }
        public void ReadAllContactsFromDb()
        {
            var contactsFromDb = _phonebookDbContext.Phonebook.ToList();
            Console.WriteLine("===============================");
            foreach (var contactFromDb in contactsFromDb)
            {
                Console.WriteLine($"\t ID: {contactFromDb.Id}");
                Console.WriteLine($"\t Nazwa: {contactFromDb.Name}");
                Console.WriteLine($"\t Phone1: {contactFromDb.Phone1}");
                Console.WriteLine($"\t Phone2: {contactFromDb.Phone2}");
                Console.WriteLine($"\t Phone3{contactFromDb.Phone3}");
                Console.WriteLine("===============================");
            }
           
            
        }
        public void SaveDataFromDbToTxt()
        {
            var contactsFromDb = _phonebookDbContext.Phonebook.ToList();
            Console.WriteLine("Proszę podać lokalizację nowego pliku");
            string fileName = Console.ReadLine();
            if (File.Exists(fileName))
            {
                fileName =fileName + "\\DaneZBazyDanych.txt";
                using (var writer = File.AppendText(fileName))
                {
                    foreach (var contact in contactsFromDb)
                    {
                        writer.WriteLine($"\t ID: {contact.Id}");
                        writer.WriteLine($"\t Name: {contact.Name}");
                        writer.WriteLine($"\t Phone1: {contact.Phone1}");
                        writer.WriteLine($"\t Phone2: {contact.Phone2}");
                        writer.WriteLine($"\t Phone3: {contact.Phone3}");
                        writer.WriteLine("=========================");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Podana ścierzka pliku nie istnieje!!!");
            }
        }
    }
}
