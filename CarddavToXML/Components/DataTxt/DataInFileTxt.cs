using PhonebookConverter.Data;
using PhonebookConverter.UIAndExceptions;
using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using PhonebookConverterL.Data.Entities;
using System.Text;

namespace PhonebookConverter.Components.DataTxt
{
    public class DataInFileTxt : IDataInFileTxt
    {
        private readonly string filePath;
        private readonly IValidation _validation;
        private readonly IDataFromUser _dataFromUser;
        private readonly FileContext _fileContext;

        public DataInFileTxt(IValidation validation ,IDataFromUser dataFromUser, FileContext fileContext )
        {
            filePath = "DataInCsv.csv";
            _validation = validation;
            _dataFromUser = dataFromUser;
            _fileContext = fileContext;
        }        
        public void EditByID(int? id)
        {
            do
            {
                var contactsFromFile = _fileContext.ReadAllContactsFromFile().FirstOrDefault(c => c.Id == id);
                var choise = _dataFromUser.DataOperationsEditByIDGetChoise(contactsFromFile);
                if (choise == "0") break;
                choise = _validation.DataOperationsEditByIdChoseParameter(choise);
                var parameter = _dataFromUser.DataOperationsEditByIdGetParameter();
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] values = lines[i].Split(',');
                    if (values[0] == contactsFromFile.Id.ToString())
                    {
                        values[int.Parse(choise)] = parameter;
                        lines[i] = string.Join(",", values);
                        break;
                    }
                }
                File.WriteAllLines(filePath, lines, Encoding.UTF8);
                break;
            } while (true);
            Console.WriteLine("Dane zostały zaktualizowane w pliku CSV.");
        }
        public void AddNewEntry()
        {
            var contacts = _fileContext.ReadAllContactsFromFile();
            var contact = _dataFromUser.DataOperationsAddNewEntryGetData();
            contacts.Add(new ContactInDb
            {
                Name = contact.Name,
                Phone1 = contact.Phone1,
                Phone2 = contact.Phone2,
                Phone3 = contact.Phone3
            });
            contact.Id = contacts.Count;
            using (var writer = File.AppendText(filePath))
            {
                string data = $"{contact.Id},{contact.Name},{contact.Phone1},{contact.Phone2},{contact.Phone3}";
                writer.WriteLine(data);
            }

            Console.WriteLine("Dane zostały dodane do pliku CSV.");
        }
        public void DeleteByID(int? id)
        {
            var toRemove = _fileContext.ReadAllContactsFromFile().FirstOrDefault(c => c.Id == id);
            toRemove = (ContactInDb)_validation.DataOperationsGetID(toRemove);
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                if (values[0] == toRemove.Name
                    && values[1] == toRemove.Phone1.ToString()
                    && values[2] == toRemove.Phone2.ToString()
                    && values[3] == toRemove.Phone3.ToString())
                {
                    lines[i] = null;
                }
            }
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
            Console.WriteLine("Dane zostały usunięte z pliku CSV.");
        }
        public void ShowAllContacts()
        {
            var contacts = _fileContext.ReadAllContactsFromFile();
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
            var contactsFromDb = _fileContext.ReadAllContactsFromFile().ToList();
            Console.WriteLine("Proszę podać lokalizację nowego pliku");
            string fileName = _validation.DataOperationsExportToTxtDirectoryExist(Console.ReadLine());
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
        }

    }
}



