﻿using PhonebookConverterL.Data.Entities;
using PhonebookConverterL.Data;
using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using PhonebookConverter.UIAndExceptions;

namespace PhonebookConverter.Components.Database
{
    public class DbOperations : IDbOperations
    {
        private readonly PhonebookDbContext _phonebookDbContext;        
        private readonly IValidation _validation;
        private readonly IDataFromUser _dataFromUser;

        public DbOperations(PhonebookDbContext phonebookDbContext, IValidation validation, IDataFromUser dataFromUser)
        {
            _phonebookDbContext = phonebookDbContext;           
            _validation = validation;
            _dataFromUser = dataFromUser;
        }
        public void AddNewEntry()
        {
            var contact = _dataFromUser.DataOperationsAddNewEntryGetData();
            _phonebookDbContext.Add(contact);
            _phonebookDbContext.SaveChanges();
        }
        public void EditByID(int? id)
        {
            do
            {
                var contactFromDb = _phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == id);
                var choise = _dataFromUser.DataOperationsEditByIDGetChoise(contactFromDb);
                if (choise == "0") break;
                choise = _validation.DataOperationsEditByIdChoseParameter(choise);
                var parameter = _dataFromUser.DataOperationsEditByIdGetParameter();
                switch (choise)
                {
                    case "1":
                        contactFromDb.Name = parameter;
                        _phonebookDbContext.SaveChanges();
                        break;
                    case "2":
                        contactFromDb.Phone1 = _validation.IntParseValidation(parameter);
                        _phonebookDbContext.SaveChanges();
                        break;
                    case "3":
                        contactFromDb.Phone2 = _validation.IntParseValidation(parameter);
                        _phonebookDbContext.SaveChanges();
                        break;
                    case "4":
                        contactFromDb.Phone3 = _validation.IntParseValidation(parameter);
                        _phonebookDbContext.SaveChanges();
                        break;
                }
                break;

            } while (true);
        }
        public void DeleteByID(int? id)
        {
            var toRemove = _phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == id);
            toRemove = (ContactInDb)_validation.DataOperationsGetID(toRemove);
            _phonebookDbContext.Phonebook.Remove(toRemove);
            _phonebookDbContext.SaveChanges();
        }
        public void ShowAllContacts()
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
