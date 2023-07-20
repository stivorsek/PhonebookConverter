using PhonebookConverter.Data;
using PhonebookConverter.UIAndValidation.Validation;
using PhonebookConverter.UIAndValidationm;
using PhonebookConverterL.Data;
using PhonebookConverterL.Data.Entities;
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
            var contact = dataFromUser.DataOperationsAddNewEntryGetData();
            contact.Id = contacts[contacts.Count - 1].Id + 1;
            contacts.Add(contact);
            phonebookFileContext.SaveChanges(contacts);
            Console.WriteLine("Data has been added to the file");
            Console.WriteLine("");
        }
        public void EditByID(int? id)
        {
            var contacts = phonebookFileContext.ReadAllContactsFromFile();
            var contactFromFile = contacts.FirstOrDefault(c => c.Id == id);
            Edit(contactFromFile, contacts);
        }
        public void EditByName(string name)
        {
            var contacts = phonebookFileContext.ReadAllContactsFromFile();
            var contactFromFile = contacts.FirstOrDefault(c => c.Name == name);
            Edit(contactFromFile, contacts);
        }
        public void Edit(ContactInDb? contactFromFile, List<ContactInDb> contacts)
        {
            do
            {
                var choise = dataFromUser.DataOperationsEditGetChoise(contactFromFile);
                if (choise == "0")
                {
                    Console.Clear();
                    break;
                }
                var parameter = dataFromUser.DataOperationsEditGetParameter();
                switch (choise)
                {
                    case "1":
                        contactFromFile.Name = parameter;
                        phonebookFileContext.SaveChanges(contacts);
                        break;
                    case "2":
                        contactFromFile.Phone1 = validation.IntParseValidation(parameter);
                        phonebookFileContext.SaveChanges(contacts);
                        break;
                    case "3":
                        contactFromFile.Phone2 = validation.IntParseValidation(parameter);
                        phonebookFileContext.SaveChanges(contacts);
                        break;
                    case "4":
                        contactFromFile.Phone3 = validation.IntParseValidation(parameter);
                        phonebookFileContext.SaveChanges(contacts);
                        break;
                }
                Console.Clear();
                Console.WriteLine("Data has been updated in the file");
                Console.WriteLine("");
                break;
            } while (true);
        }
        public void DeleteByID(int? id)
        {
            var contacts = phonebookFileContext.ReadAllContactsFromFile();
            var toRemove = contacts.FirstOrDefault(c => c.Id == id);
            toRemove = (ContactInDb)validation.DataOperationsGetID(toRemove);
            contacts.Remove(toRemove);
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
        }
        public void SaveDataFromFileToTxt()
        {
            Console.Clear();
            var contactsFromDb = phonebookFileContext.ReadAllContactsFromFile().ToList();
            Console.WriteLine("Please enter the directory location");
            string fileName = validation.DataOperationsExportToTxtDirectoryExist(Console.ReadLine());
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

    }
}



