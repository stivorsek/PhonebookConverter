using CarddavToXML.Data.Entities;
using CarddavToXML.Data;
using PhonebookConverter.UI;

namespace PhonebookConverter.Components
{
    public class DbOperations : IDbOperations
    {
        private readonly PhonebookDbContext _phonebookDbContext;
        private readonly IExceptions _exceptions;

        public DbOperations( PhonebookDbContext phonebookDbContext, IExceptions exceptions)
        {
            _phonebookDbContext = phonebookDbContext;
            _exceptions = exceptions;
        }
        public void AddNewDbEntry()
        {
            Console.Clear();
            _exceptions.ExceptionsLoop(() =>
            {                
                Console.WriteLine("Podaj Nazwę");
                var Name = Console.ReadLine();
                Console.WriteLine("Podaj pierwszy numer telefonu");
                var Phone1 = IntParseValidation(Console.ReadLine());
                Console.WriteLine("Podaj drugi numer telefonu");
                var Phone2 = IntParseValidation(Console.ReadLine());
                Console.WriteLine("Podaj trzeci numer telefonu");
                var Phone3 = IntParseValidation(Console.ReadLine());
                _phonebookDbContext.Add(new ContactInDb()
                {
                    Name = Name,
                    Phone1 = Phone1,
                    Phone2 = Phone2,
                    Phone3 = Phone3
                });
                _phonebookDbContext.SaveChanges();
                Console.Clear();
            });
            
        }
        public void EditFromDbByID(int? id)
        {
            _exceptions.ExceptionsLoop(() =>
            {
                do
                {
                    var contactFromDb = _phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == id);
                    Console.WriteLine($"\t1) Name : {contactFromDb.Name}");
                    Console.WriteLine($"\t2) Phone1 : {contactFromDb.Phone1}");
                    Console.WriteLine($"\t3) Phone2 : {contactFromDb.Phone2}");
                    Console.WriteLine($"\t4) Phone3 : {contactFromDb.Phone3}");
                    Console.WriteLine("");
                    Console.WriteLine("Który parametr chcesz zmienić lub wybierz 0 aby cofnąć?");
                    var choise = Console.ReadLine();
                    if (choise == "0")
                    {
                        break;
                    }
                    if (choise != "1" && choise != "2" && choise != "3" && choise != "4")
                    { throw new ArgumentException("Nie ma takiego parametru!!!"); }
                    Console.WriteLine("Podaj na co chcesz zmienić parametr");
                    var parameter = Console.ReadLine();
                    switch (choise)
                    {
                        case "1":
                            contactFromDb.Name = parameter;
                            _phonebookDbContext.SaveChanges();
                            break;
                        case "2":
                            contactFromDb.Phone1 = int.Parse(parameter);
                            _phonebookDbContext.SaveChanges();
                            break;
                        case "3":
                            contactFromDb.Phone2 = int.Parse(parameter);
                            _phonebookDbContext.SaveChanges();
                            break;
                        case "4":
                            contactFromDb.Phone3 = int.Parse(parameter);
                            _phonebookDbContext.SaveChanges();
                            break;
                        default:
                            throw new ArgumentException("Nie ma takiego parametru!!!");
                    }
                    break;

                } while (true);
                });
        }
        public void DeleteFromDbByID(int? id)
        {            
                var toRemove = _phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == id);
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
                Console.WriteLine($"\t Phone3: {contactFromDb.Phone3}");
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
        public int? IntParseValidation(string data)
        {
            int? result = string.IsNullOrEmpty(data) ? (int?)null : int.Parse(data);
            return result;
        }
    }
}
