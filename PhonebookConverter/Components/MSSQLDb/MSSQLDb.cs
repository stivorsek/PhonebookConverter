using PhonebookConverter.Data.Entities;
using PhonebookConverter.Data;
using PhonebookConverter.UIAndValidation.Validation;
using PhonebookConverter.UIAndValidationm;

namespace PhonebookConverter.Components.MSQSQLDb
{
    public class MSSQLDb : IMSSQLDb
    {
        private readonly PhonebookDbContext phonebookDbContext;
        private readonly IValidation validation;
        private readonly IDataFromUser dataFromUser;

        public MSSQLDb(PhonebookDbContext phonebookDbContext, IValidation validation, IDataFromUser dataFromUser)
        {
            this.phonebookDbContext = phonebookDbContext;
            this.validation = validation;
            this.dataFromUser = dataFromUser;
        }
        public void AddContact()
        {
            var contact = dataFromUser.AddContactGetData();
            phonebookDbContext.Add(contact);
            phonebookDbContext.SaveChanges();

            Console.WriteLine("Data has been added to the database");
            Console.WriteLine("");
        }
        public void Edit(ContactInDb? contactFromDb)
        {
            do
            {
                var choise = dataFromUser.GetParameterChoise(contactFromDb);
                if (choise == "0")
                {
                    Console.Clear();
                    break;
                }
                string parameter = dataFromUser.EditGetParameter(choise);
                switch (choise)
                {
                    case "1":
                        contactFromDb.Name = parameter;
                        phonebookDbContext.SaveChanges();
                        break;
                    case "2":
                        contactFromDb.Phone1 = int.Parse(parameter);
                        phonebookDbContext.SaveChanges();
                        break;
                    case "3":
                        contactFromDb.Phone2 = int.Parse(parameter);
                        phonebookDbContext.SaveChanges();
                        break;
                    case "4":
                        contactFromDb.Phone3 = int.Parse(parameter);
                        phonebookDbContext.SaveChanges();
                        break;
                }                
                Console.Clear();
                Console.WriteLine("Data has been updated to the database");
                Console.WriteLine("");
                break;
            } while (true);
        }
        public void DeleteByID(int? id)
        {
            var toRemove = phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == id);
            toRemove = (ContactInDb)validation.GetID(toRemove);
            phonebookDbContext.Phonebook.Remove(toRemove);
            phonebookDbContext.SaveChanges();
            Console.Clear();
            Console.WriteLine("Data has been deleted to the database");
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
            string choiseExportMSSQL = dataFromUser.ExportToTxt();
            if (choiseExportMSSQL == "2") return;
            SaveDataFromDbToTxt();
        }
        public void SaveDataFromDbToTxt()
        {
            Console.Clear();
            var contactsFromDb = phonebookDbContext.Phonebook.ToList();
            Console.WriteLine("Please enter the directory location");
            string fileName = validation.ExportToTxtDirectoryExist(Console.ReadLine());
            fileName = fileName + "\\DataFromDb.txt";
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
            Console.WriteLine($"Data has been saved in txt file : {fileName}");
            Console.WriteLine();
        }
        public void Delete(ContactInDb contact)
        {
            phonebookDbContext.Phonebook.Remove(contact);
            phonebookDbContext.SaveChanges();
            Console.Clear();
            Console.WriteLine("Data has been deleted to the database");
            Console.WriteLine();
        }
        public void FindAndManipulatContact(string dataStorage)
        {
            try
            {
                var searchType = dataFromUser.SearchType();
                if (searchType == "0") throw new Exception("Go back to last menu");
                var contact = dataFromUser.FindContact(dataStorage, searchType);
                if (contact == null) throw new Exception("Go back to last menu");
                var typeOperation = dataFromUser.GetTypeOperationChoise(contact);
                if (typeOperation == "0") throw new Exception("Go back to last menu");
                if (typeOperation == "1") Delete(contact);
                if (typeOperation == "2") Edit(contact);
            }
            finally { }
        }
    }
}
