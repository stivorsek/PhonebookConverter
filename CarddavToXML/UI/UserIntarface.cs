using CarddavToXML.Components;
using CarddavToXML.Data;
using CarddavToXML.Data.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PhonebookConverter.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarddavToXML.UI
{
    public class UserIntarface : IChoise
    {
        private readonly ICsvReader _csvReader;
        private readonly PhonebookDbContext _phonebookDbContext;
        private readonly IXmlWriter _xmlWriter;
        private readonly IXmlReader _xmlReader;
        private readonly IDbOperations _dbOperations;

        public UserIntarface(ICsvReader csvReader
            , IXmlWriter xmlWriter
            , PhonebookDbContext phonebookDbContext
            , IXmlReader xmlReader
            , IDbOperations dbOperations)
        {
            _csvReader = csvReader;
            _phonebookDbContext = phonebookDbContext;
            _phonebookDbContext.Database.EnsureCreated();
            _xmlWriter = xmlWriter;
            _xmlReader = xmlReader;
            _dbOperations = dbOperations;
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
                    ImportDataFromXml();
                    break;
                case "3":
                    UISeparator();
                    ExportToXML();
                    LoopUI();
                    break;
                case "4":
                    UISeparator();
                    _dbOperations.ReadAllContactsFromDb();
                    LoopUI();
                    break;
                case "5":
                    UISeparator();
                    ChoseDatabaseOperations();
                    LoopUI();
                    break;
                case "6":
                    break;
                case "7":
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
                    _dbOperations.DeleteFromDbByID(id);
                    break;
                case "2":
                    Console.WriteLine("Podaj ID");
                    id = Console.ReadLine();
                    _dbOperations.EditFromDbByID(id);
                    break;
                case "3":
                    _dbOperations.AddNewDbEntry();
                    break;
                default:
                    Console.WriteLine("Podano niepoprawny parametr");
                    break;
            }
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
            else
                LoopUI();
        }
        private void ImportDataFromXml()
        {

            Console.WriteLine("Podaj ścierzkę pliku");
            string path = Console.ReadLine();
            try
            {
                var contacts = _xmlReader.XmlTypeChecker(path);
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
        private void ExportToXML()
        {
            Console.WriteLine("Wybierz rodzaj pliku XML do którego chcesz exportować pliki");
            Console.WriteLine("\t 1)Yealink Local Phonnebook");
            Console.WriteLine("\t 2)Yealink Remote Phonebook");
            Console.WriteLine("\t 3)Fanvil Local Phonebook");
            Console.WriteLine("\t 4)Fanvil Remote Phonebook");
            var choise = Console.ReadLine();
            Console.WriteLine("Wybierz folder do którego chcesz exportować");
            var pathXml = Console.ReadLine();
            var contatsFromDb = _phonebookDbContext.Phonebook.ToList();
            switch (choise)
            {
                case "1":
                    _xmlWriter.ExportToXmlYealinkRemote(pathXml, contatsFromDb);                    
                    break;
                case "2":
                    _xmlWriter.ExportToXmlYealinkLocal(pathXml, contatsFromDb);                    
                    break;
                case "3":                    
                    break;
                case "4":
                    _xmlWriter.ExportToXmlFanvilRemote(pathXml, contatsFromDb);                    
                    break;
                default:
                    break;
            }

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
