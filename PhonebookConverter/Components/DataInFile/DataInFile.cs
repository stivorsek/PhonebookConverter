using PhonebookConverter.Components.MSQSQLDb;
using PhonebookConverter.Data;
using PhonebookConverter.UIAndValidation.Validation;
using PhonebookConverter.UIAndValidationm;
using PhonebookConverter.Data;
using PhonebookConverter.Data.Entities;
using System.Text;

namespace PhonebookConverter.Components.DataTxt
{
    public class DataInFile : IDataInFile
    {
        private readonly string filePath;
        private readonly IValidation validation;
        private readonly IDataFromUser dataFromUser;
        private readonly PhonebookFileContext phonebookFileContext;
        private readonly object fileLock = new object();

        public DataInFile(IValidation validation, IDataFromUser dataFromUser, PhonebookFileContext phonebookFileContext)
        {
            filePath = "DataInCsv.csv";
            this.validation = validation;
            this.dataFromUser = dataFromUser;
            this.phonebookFileContext = phonebookFileContext;
        }
        public void AddNewEntry()
        {
            var contacts = phonebookFileContext.ReadAllContactsFromFile();
            var contact = dataFromUser.AddContactGetData();
            contact.Id = contacts[contacts.Count - 1].Id + 1;
            contacts.Add(contact);
            phonebookFileContext.SaveChanges(contacts);
            Console.WriteLine("Data has been added to the file");
            Console.WriteLine("");            
        }        
        public void Edit(ContactInDb contact)
        {
            var contacts = phonebookFileContext.ReadAllContactsFromFile();            
            do
            {
                var choise = dataFromUser.GetParameterChoise(contact);
                if (choise == "0")
                {
                    Console.Clear();
                    break;
                }
                var parameter = dataFromUser.EditGetParameter(choise);
                switch (choise)
                {
                    case "1":
                        contact.Name = parameter;
                        phonebookFileContext.SaveChanges(contacts);
                        break;
                    case "2":
                        contact.Phone1 = validation.IntParseValidation(parameter);
                        phonebookFileContext.SaveChanges(contacts);
                        break;
                    case "3":
                        contact.Phone2 = validation.IntParseValidation(parameter);
                        phonebookFileContext.SaveChanges(contacts);
                        break;
                    case "4":
                        contact.Phone3 = validation.IntParseValidation(parameter);
                        phonebookFileContext.SaveChanges(contacts);
                        break;
                }
                Console.Clear();
                Console.WriteLine("Data has been updated in the file");
                Console.WriteLine("");
                break;
            } while (true);
        }
        public void Delete(ContactInDb contact)
        {
            var contacts = phonebookFileContext.ReadAllContactsFromFile();            
            contacts.Remove(contact);
            phonebookFileContext.SaveChanges(contacts);
            Console.Clear();
            Console.WriteLine("Data has been deleted in the file");
            Console.WriteLine("");
        }
        public void ShowAllContacts()
        {
            var contacts = phonebookFileContext.ReadAllContactsFromFile();
            Console.WriteLine("===============================");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"\t ID: {contact.Id}");
                Console.WriteLine($"\t Nazwa: {contact.Name}");
                Console.WriteLine($"\t Phone1: {contact.Phone1}");
                Console.WriteLine($"\t Phone2: {contact.Phone2}");
                Console.WriteLine($"\t Phone3: {contact.Phone3}");
                Console.WriteLine("===============================");
            }
            string choiseExportFile = dataFromUser.ExportToTxt();
            if (choiseExportFile == "2") return;
            SaveDataFromFileToTxt();
        }
        public void SaveDataFromFileToTxt()
        {
            Console.Clear();
            var contactsFromDb = phonebookFileContext.ReadAllContactsFromFile().ToList();
            Console.WriteLine("Please enter the directory location");
            string fileName = validation.ExportToTxtDirectoryExist(Console.ReadLine());
            fileName = fileName + "\\DataFromFile.txt";
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
        public void FindAndManipulatContactIn(string dataStorage)
        {
            try
            {
                var searchType = dataFromUser.GetSearchType();
                if (searchType == "0") throw new Exception("Go back to main menu") ;
                var contact = dataFromUser.FindContact(dataStorage, searchType);
                if (contact == null) throw new Exception("Go back to main menu");
                var typeOperation = dataFromUser.GetTypeOperationChoise(contact);
                switch(typeOperation)
                {
                    case "0":
                        throw new Exception("Go back to main menu");
                    case "1":
                        Delete(contact);
                        break;
                    case "2":
                        Edit(contact);
                        break;
                }
            }
            finally { Console.Clear(); }
        }
    }
}



