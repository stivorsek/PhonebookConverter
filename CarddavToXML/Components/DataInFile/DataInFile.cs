﻿using PhonebookConverter.Data;
using PhonebookConverter.UIAndValidation.Validation;
using PhonebookConverter.UIAndValidationm;
using PhonebookConverterL.Data.Entities;
using System.Text;

namespace PhonebookConverter.Components.DataTxt
{
    public class DataInFile : IDataInFile
    {
        private readonly string filePath;
        private readonly IValidation validation;
        private readonly IDataFromUser dataFromUser;
        private readonly PhonebookFileContext fileContext;
        private readonly object fileLock = new object();

        public DataInFile(IValidation validation, IDataFromUser dataFromUser, PhonebookFileContext fileContext)
        {
            filePath = "DataInCsv.csv";
            this.validation = validation;
            this.dataFromUser = dataFromUser;
            this.fileContext = fileContext;
        }
        public void AddNewEntry()
        {

            var contacts = fileContext.ReadAllContactsFromFile();
            var contact = dataFromUser.DataOperationsAddNewEntryGetData();
            contact.Id = contacts[contacts.Count - 1].Id + 1;
            contacts.Add(new ContactInDb
            {
                Id = contact.Id,
                Name = contact.Name,
                Phone1 = contact.Phone1,
                Phone2 = contact.Phone2,
                Phone3 = contact.Phone3
            });
            lock (fileLock)
            {
                using (var writer = File.AppendText(filePath))
                {
                    string data = $"{contact.Id},{contact.Name},{contact.Phone1},{contact.Phone2},{contact.Phone3}";
                    writer.WriteLine(data);
                }
            }
            Console.WriteLine("Data has been added to the file");
            Console.WriteLine("");
        }
        public void EditByID(int? id)
        {
            do
            {
                var contactsFromFile = fileContext.ReadAllContactsFromFile().FirstOrDefault(c => c.Id == id);
                var choise = dataFromUser.DataOperationsEditByIDGetChoise(contactsFromFile);
                if (choise == "0") break;
                choise = validation.DataOperationsEditByIdChoseParameter(choise);
                var parameter = dataFromUser.DataOperationsEditByIdGetParameter();
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
                lock (fileLock)
                {
                    File.WriteAllLines(filePath, lines, Encoding.UTF8);
                }
                break;
            } while (true);
            Console.Clear();
            Console.WriteLine("Data has been updated in the file");
            Console.WriteLine("");
        }
        public void DeleteByID(int? id)
        {
            var toRemove = fileContext.ReadAllContactsFromFile().FirstOrDefault(c => c.Id == id);
            toRemove = (ContactInDb)validation.DataOperationsGetID(toRemove);
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                if (values[0] == toRemove.Id.ToString()
                    && values[1] == toRemove.Name
                    && values[2] == toRemove.Phone1.ToString()
                    && values[3] == toRemove.Phone2.ToString()
                    && values[4] == toRemove.Phone3.ToString())
                {
                    lines[i] = null;
                }
            }
            lock (fileLock)
            {
                File.WriteAllLines(filePath, lines, Encoding.UTF8);
            }
            Console.Clear();
            Console.WriteLine("Data has been deleted in the file");
            Console.WriteLine("");
        }
        public void ShowAllContacts()
        {
            var contacts = fileContext.ReadAllContactsFromFile();
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
            var contactsFromDb = fileContext.ReadAllContactsFromFile().ToList();
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


