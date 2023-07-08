﻿using CarddavToXML.Components;
using CarddavToXML.Data;
using CarddavToXML.Data.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PhonebookConverter.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarddavToXML.UI
{
    public class UserIntarface : IChoise
    {
        private readonly ICsvReader _csvReader;
        private readonly PhonebookDbContext _phonebookDbContext;
        private readonly IXmlWriter _xmlWriter;
        private readonly IXmlReader _xmlReader;

        public UserIntarface(ICsvReader csvReader, IXmlWriter xmlWriter, PhonebookDbContext phonebookDbContext, IXmlReader xmlReader)
        {
            _csvReader = csvReader;
            _phonebookDbContext = phonebookDbContext;
            _phonebookDbContext.Database.EnsureCreated();
            _xmlWriter = xmlWriter;
            _xmlReader = xmlReader;
        }
        public void FirstUIChoise()
        {

            Console.WriteLine("\tWitam w programie do konwertowania plików xml na inne");
            UISeparator();
            Console.WriteLine("\tProszę wybierz co będziemy dzisiaj robić");
            Console.WriteLine("1) Załaduj Dane do bazy danych z pliku CSV");
            Console.WriteLine("2) Załaduj Dane do bazy danych z pliku XML");
            Console.WriteLine("3) Exportuj dane do XML");
            Console.WriteLine("4) Wyświetl liste kontaktow w bazie danych");
            Console.WriteLine("5) Aby wybrać operacje na bazie danych");
            Console.WriteLine("6) Aby zakończyć program");
            var choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    UISeparator();
                    ImportDataFromCsv();
                    break;
                case "2":
                    UISeparator();
                    ChosePhoneModelXml();
                    break;
                case "3":
                    UISeparator();
                    ExportToXML();
                    break;
                case "4":
                    UISeparator();
                    ReadAllContactsFromDb();
                    break;
                case "5":
                    UISeparator();
                    ChoseDatabaseOperations();
                    break;
                case "6":
                    break;
                default:
                    Console.WriteLine("Podałeś zły wybór");
                    LoopUI();
                    break;
            }
        }

        private void ChoseDatabaseOperations()
        {
            Console.WriteLine("1) Usunąć wpis po ID");
            Console.WriteLine("2) Edytować wpis po ID");
            Console.WriteLine("3) Dodać wpis ręcznie");
            var choise = Console.ReadLine();
            UISeparator();
            string id = null;
            switch (choise)
            {
                case "1":
                    Console.WriteLine("Podaj ID");
                    id = Console.ReadLine();
                    DeleteFromDbByID(id);
                    break;
                case "2":
                    Console.WriteLine("Podaj ID");
                    id = Console.ReadLine();
                    EditFromDbByID(id);
                    break;
                case "3":
                    AddNewDbEntry();
                    break;
                default:
                    Console.WriteLine("Podano niepoprawny parametr");
                    LoopUI();
                    break;
            }
        }
        private void AddNewDbEntry()
        {
            Console.WriteLine("Podaj Nazwę");
            var Name = Console.ReadLine();
            Console.WriteLine("Podaj pierwszy numer telefonu");
            var Phone1 = Console.ReadLine();
            Console.WriteLine("Podaj drugi numer telefonu");
            var Phone2 = Console.ReadLine();
            Console.WriteLine("Podaj trzeci numer telefonu");
            var Phone3 = Console.ReadLine();
            _phonebookDbContext.Add(new PhonebookInDb()
            {
                Name = Name,
                Phone1 = Phone1,
                Phone2 = Phone2,
                Phone3 = Phone3
            });
            _phonebookDbContext.SaveChanges();
            LoopUI();
        }
        private void EditFromDbByID(string? id)
        {
            var Id = int.Parse(id);
            var contactFromDb = _phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == Id);
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
                    LoopUI();
                    break;
                case "2":
                    contactFromDb.Phone1 = parameter;
                    _phonebookDbContext.SaveChanges();
                    LoopUI();
                    break;
                case "3":
                    contactFromDb.Phone2 = parameter;
                    _phonebookDbContext.SaveChanges();
                    LoopUI();
                    break;
                case "4":
                    contactFromDb.Phone3 = parameter;
                    _phonebookDbContext.SaveChanges();
                    LoopUI();
                    break;
                default:
                    Console.WriteLine("Podano niepoprawny paramert");
                    LoopUI();
                    break;
            }
        }
        private void DeleteFromDbByID(string? id)
        {
            int Id = int.Parse(id);
            var toRemove = _phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == Id);
            _phonebookDbContext.Phonebook.Remove(toRemove);
            _phonebookDbContext.SaveChanges();
            LoopUI();
        }
        private void ChosePhoneModelXml()
        {

            Console.WriteLine("Prosze wybrać model");
            UISeparator();
            Console.WriteLine("\t1)Yealink");
            Console.WriteLine("\t2)Fanvil");
            var phoneType = Console.ReadLine();
            Console.WriteLine("Podaj ścierzkę pliku");
            string pathXml = Console.ReadLine();
            switch (phoneType)
            {
                case "1":
                    ImportDataFromXml(pathXml);
                    LoopUI();
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                default:
                    Console.WriteLine("Zly wybór");
                    LoopUI();
                    break;
            }
        }
        private void ReadAllContactsFromDb()
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
            LoopUI();
        }
        private void ImportDataFromCsv()
        {

            Console.WriteLine("Podaj ścierzkę pliku lub wybierz 1 aby cofnąć do poprzedniego menu");
            string path = Console.ReadLine();
            if (path != "1")
            {
                try
                {
                    var contacts = _csvReader.CsvTypeChecker(path);
                    foreach (var contact in contacts)
                    {
                        _phonebookDbContext.Phonebook.Add(new PhonebookInDb()
                        {
                            Name = contact.Name,
                            Phone1 = contact.Phone1,
                            Phone2 = contact.Phone2,
                            Phone3 = contact.Phone3,
                        });
                    }
                    _phonebookDbContext.SaveChanges();
                    LoopUI();
                }
                catch (Exception ex)
                {
                    UISeparator();
                    Console.WriteLine("Wystąpił wyjątek: ");
                    Console.WriteLine($"\t{ex.Message}");
                    LoopUIWithError();
                }

            }
        }
        private void ImportDataFromXml(string? path)
        {
            var contacts = _xmlReader.ImportFromXml(path);
            foreach (var contact in contacts)
            {
                _phonebookDbContext.Phonebook.Add(new PhonebookInDb()
                {
                    Name = contact.Name,
                    Phone1 = contact.Phone1,
                    Phone2 = contact.Phone2,
                    Phone3 = contact.Phone3,
                });
            }
            _phonebookDbContext.SaveChanges();
            LoopUI();
        }
        private void ExportToXML()
        {
            Console.WriteLine("Wybierz folder do którego chcesz exportować");
            var pathXml = Console.ReadLine();
            var contatsFromDb = _phonebookDbContext.Phonebook.ToList();
            _xmlWriter.ExportToXml(pathXml, contatsFromDb);
            LoopUI();
        }
        public void UISeparator()
        {
            Console.WriteLine("");
            Console.WriteLine("/////////////////////");
            Console.WriteLine("");
        }
        private void LoopUI()
        {
            UISeparator();
            Console.WriteLine("\tPomyślnie wykonano operacje");
            UISeparator();
            FirstUIChoise();
        }
        private void LoopUIWithError()
        {
            FirstUIChoise();
        }
    }
}
