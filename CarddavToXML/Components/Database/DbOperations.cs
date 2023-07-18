using PhonebookConverterL.Data.Entities;
using PhonebookConverterL.Data;
using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using PhonebookConverter.UIAndExceptions;

namespace PhonebookConverter.Components.Database
{
    public class DbOperations : IDbOperations
    {
        private readonly PhonebookDbContext phonebookDbContext;        
        private readonly IValidation validation;
        private readonly IDataFromUser dataFromUser;

        public DbOperations(PhonebookDbContext phonebookDbContext, IValidation validation, IDataFromUser dataFromUser)
        {
            this.phonebookDbContext = phonebookDbContext;           
            this.validation = validation;
            this.dataFromUser = dataFromUser;
        }
        public void AddNewEntry()
        {
            var contact = dataFromUser.DataOperationsAddNewEntryGetData();
            phonebookDbContext.Add(contact);
            phonebookDbContext.SaveChanges();
            
            Console.WriteLine("Dane zostały dodane do pliku Bazy Danych");
            Console.WriteLine("");
        }
        public void EditByID(int? id)
        {
            do
            {
                var contactFromDb = phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == id);
                var choise = dataFromUser.DataOperationsEditByIDGetChoise(contactFromDb);
                if (choise == "0") break;
                choise = validation.DataOperationsEditByIdChoseParameter(choise);
                var parameter = dataFromUser.DataOperationsEditByIdGetParameter();
                switch (choise)
                {
                    case "1":
                        contactFromDb.Name = parameter;
                        phonebookDbContext.SaveChanges();
                        break;
                    case "2":
                        contactFromDb.Phone1 = validation.IntParseValidation(parameter);
                        phonebookDbContext.SaveChanges();
                        break;
                    case "3":
                        contactFromDb.Phone2 = validation.IntParseValidation(parameter);
                        phonebookDbContext.SaveChanges();
                        break;
                    case "4":
                        contactFromDb.Phone3 = validation.IntParseValidation(parameter);
                        phonebookDbContext.SaveChanges();
                        break;
                }
                break;

            } while (true);
            Console.Clear();
            Console.WriteLine("Dane zostały zaktualizowane w bazie danych");
            Console.WriteLine("");
        }
        public void DeleteByID(int? id)
        {
            var toRemove = phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == id);
            toRemove = (ContactInDb)validation.DataOperationsGetID(toRemove);
            phonebookDbContext.Phonebook.Remove(toRemove);
            phonebookDbContext.SaveChanges();
            Console.Clear();
            Console.WriteLine("Dane zostały usunięte z bazy danych");
            Console.WriteLine();
        }
        public void ShowAllContacts()
        {
            var contactsFromDb = phonebookDbContext.Phonebook.ToList();
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
            Console.Clear();
            var contactsFromDb = phonebookDbContext.Phonebook.ToList();
            Console.WriteLine("Proszę podać lokalizację nowego pliku");
            string fileName = validation.DataOperationsExportToTxtDirectoryExist(Console.ReadLine());
            fileName = fileName + "\\DaneZBazyDanych.txt";
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
            Console.Clear();
            Console.WriteLine($"Dane zostały exportowane do: {fileName}");
            Console.WriteLine();
        }
    }
}
