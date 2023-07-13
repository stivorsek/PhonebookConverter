using CarddavToXML.Data;
using CarddavToXML.Data.Entities;
using PhonebookConverter.Components.Database;
using PhonebookConverter.Components.Export;
using PhonebookConverter.Components.Import;
using PhonebookConverter.UI;
using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using System;

namespace CarddavToXML.UI
{
    public class UserIntarface : IChoise
    {
        private readonly IExceptions _exceptions;
        private readonly ICsvReader _csvReader;
        private readonly PhonebookDbContext _phonebookDbContext;
        private readonly IXmlWriter _xmlWriter;
        private readonly IXmlReader _xmlReader;
        private readonly IDataFromUser _dataFromUser;
        private readonly IDbOperations _dbOperations;

        public UserIntarface(ICsvReader csvReader
            , IXmlWriter xmlWriter
            , PhonebookDbContext phonebookDbContext
            , IXmlReader xmlReader
            , IDataFromUser dataFromUser
            , IExceptions exceptions
            , IDbOperations dbOperations)
        {
            _exceptions = exceptions;
            _csvReader = csvReader;
            _phonebookDbContext = phonebookDbContext;
            _phonebookDbContext.Database.EnsureCreated();
            _xmlWriter = xmlWriter;
            _xmlReader = xmlReader;
            _dataFromUser = dataFromUser;
            _dbOperations = dbOperations;
        }
        public void FirstUIChoise()
        {

            bool endProgram = false;
            do
            {
                var choise = _dataFromUser.FirstUIChoise();
                _exceptions.ExceptionsLoop(() =>
                {
                    switch (choise)
                    {
                        case "1":
                            UISeparator();
                            ImportDataFromCsv();
                            EndOperation();
                            break;
                        case "2":
                            UISeparator();
                            ImportDataFromXml();
                            break;
                        case "3":
                            UISeparator();
                            ExportToXML();
                            Console.Clear();
                            EndOperation();
                            break;
                        case "4":
                            UISeparator();
                            ChoseDatabaseOperations();
                            Console.Clear();
                            EndOperation();
                            break;
                        case "5":
                            endProgram = true;
                            break;
                        case "6":
                            break;
                        case "7":
                            EndOperation();
                            break;
                        default:
                            throw new Exception("Podałeś nieprawidłowy wybór");
                    }
                });                
            } while (endProgram == false);
        }
        private void ImportDataFromCsv()
        {
            string path = _dataFromUser.ImportGetPathCsv();
            if (path != "1")
            {
                var contacts = _csvReader.CsvTypeChecker(path);
                foreach (var contact in contacts)
                {
                    _phonebookDbContext.Phonebook.Add(new ContactInDb()
                    {
                        Name = contact.Name,
                        Phone1 = contact.Phone1,
                        Phone2 = contact.Phone2,
                        Phone3 = contact.Phone3,
                    });
                }
                _phonebookDbContext.SaveChanges();
            }
        }
        private void ImportDataFromXml()
        {
            Console.Clear();
            string path = _dataFromUser.ImportGetPathXml();
            if (path != "1")
            {
                var contacts = _xmlReader.XmlTypeChecker(path);
                foreach (var contact in contacts)
                {
                    _phonebookDbContext.Phonebook.Add(new ContactInDb()
                    {
                        Name = contact.Name,
                        Phone1 = contact.Phone1,
                        Phone2 = contact.Phone2,
                        Phone3 = contact.Phone3,
                    });
                }
                _phonebookDbContext.SaveChanges();
                EndOperation();
            }
        }
        private void ExportToXML()
        {
            do
            {
                string choiseType = _dataFromUser.ExportToXmlGetType();
                if (choiseType == "1") break;
                string pathXml = _dataFromUser.ExportToXmlGetFolder();
                if (pathXml == "1") break;                
                bool loopState = _dataFromUser.ExportToXmlGetLoopState();
                if (loopState == false) break;                
                int loopTime = _dataFromUser.ExportToXmlGetLoopTime();
                if (loopTime == 0) break;
                var tuple = (choiseType, loopState);
                switch (tuple)
                {
                    case ("2", false):
                        _xmlWriter.ExportToXmlYealinkRemote(pathXml);
                        break;
                    case ("3", false):
                        _xmlWriter.ExportToXmlYealinkLocal(pathXml);
                        break;
                    case ("4", false):
                        _xmlWriter.ExportToXmlFanvilRemoteAndLocal(pathXml);
                        break;
                    default:
                        _xmlWriter.SetPeriodicExport(pathXml, choiseType, loopTime);
                        Console.Clear();
                        break;                        
                }
                break;
            }
            while (true);
        }
        private void ChoseDatabaseOperations()
        {
            Console.Clear();
            string choise = _dataFromUser.DatabaseOperationsGetType();
            UISeparator();
            if (choise != "1")
            {
                int? id = null;
                switch (choise)
                {
                    case "1":
                        break;
                    case "2":
                        Console.Clear();
                        id = _dataFromUser.DatabaseOperationsGetID();
                        if (id == null)
                            break;
                        _dbOperations.DeleteFromDbByID(id);
                        break;
                    case "3":
                        Console.Clear();
                        id = _dataFromUser.DatabaseOperationsGetID();
                        if (id == null)                                
                                break;                        
                        _dbOperations.EditFromDbByID(id);
                        break;
                    case "4":
                        _dbOperations.AddNewDbEntry();
                        break;
                    case "5":
                        _dbOperations.ReadAllContactsFromDb();
                        string choiseExport = _dataFromUser.DatabaseOperationsExportToTxt();
                        if (choiseExport == "2")
                        { break;}
                        _dbOperations.SaveDataFromDbToTxt();
                        break;
                }
            }
        }
        public void UISeparator()
        {
            Console.WriteLine("");
            Console.WriteLine("///////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("");
        }
        private void EndOperation()
        {
            Console.Clear();
            UISeparator();
            Console.WriteLine("\tPomyślnie wykonano operacje");
            UISeparator();
        }

    }
}
