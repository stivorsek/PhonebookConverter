using CarddavToXML.Components;
using CarddavToXML.Data;
using CarddavToXML.Data.Entities;
using PhonebookConverter.Components;
using PhonebookConverter.UI;

namespace CarddavToXML.UI
{
    public class UserIntarface : IChoise
    {
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
            , IDbOperations dbOperations)
        {
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
                try
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
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    UISeparator();
                    Console.WriteLine("Wystąpił wyjątek: ");
                    Console.WriteLine($"\t{ex.Message}");
                    Console.WriteLine($"\t{ex.Source}");
                    Console.WriteLine("========================================");
                }
            } while (endProgram == false);
        }
        private void ImportDataFromCsv()
        {
            string path = _dataFromUser.ImportGetPath();
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
            string path = _dataFromUser.ImportGetPath();
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
                if (choiseType == "1") 
                { break; }
                string pathXml = _dataFromUser.ExportToXmlGetFolder();
                if (pathXml == "1")
                { break; }
                bool loopState = _dataFromUser.ExportToXmlGetLoopState();
                if (loopState == false)
                { break; }
                int loopTime = _dataFromUser.ExportToXmlGetLoopTime();
                switch (choiseType)
                {
                    case "2":
                        _xmlWriter.ExportToXmlYealinkRemote(pathXml, loopState, loopTime);
                        break;
                    case "3":
                        _xmlWriter.ExportToXmlYealinkLocal(pathXml, loopState, loopTime);
                        break;
                    case "4":
                        _xmlWriter.ExportToXmlFanvilRemoteAndLocal(pathXml, loopState, loopTime);
                        break;
                    default:
                        Console.Clear();
                        break;
                }
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
                string id = null;
                switch (choise)
                {
                    case "1":
                        break;
                    case "2":
                        id = _dataFromUser.DatabaseOperationsGetID();
                        _dbOperations.DeleteFromDbByID(id);
                        break;
                    case "3":
                        Console.Clear();
                        id = _dataFromUser.DatabaseOperationsGetID();
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
            UISeparator();
            Console.WriteLine("\tPomyślnie wykonano operacje");
            UISeparator();
        }
        public void WykonajMetodeZObslugaWyjatkow(Action metoda)
        {
            while (true)
            {
                try
                {
                    metoda.Invoke();

                    // Jeśli nie wystąpił żaden wyjątek, możemy wyjść z pętli
                    break;
                }
                catch (Exception ex)
                {
                    // Tutaj możesz umieścić kod obsługi wyjątku

                    UISeparator();
                    Console.WriteLine("Wystąpił wyjątek: ");
                    Console.WriteLine($"\t{ex.Message}");
                    Console.WriteLine("=======================================================================");
                    // Jeśli chcesz, żeby program wrócił na początek metody, kontynuuj pętlę
                    continue;
                }
            }
        }

    }
}
