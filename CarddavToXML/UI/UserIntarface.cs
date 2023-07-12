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
            bool endProgram=false;
            do
            {
                Console.WriteLine("\tWitam w programie do zarządzania plikami książek telefonicznych");
                UISeparator();
                Console.WriteLine("\tProszę wybierz co będziemy dzisiaj robić");
                Console.WriteLine("1) Załaduj Dane do bazy danych z pliku CSV");
                Console.WriteLine("2) Załaduj Dane do bazy danych z pliku XML");
                Console.WriteLine("3) Exportuj dane do XML");
                Console.WriteLine("4) Wyświetl liste kontaktow w bazie danych");
                Console.WriteLine("5) Aby wybrać operacje na bazie danych");
                Console.WriteLine("6) Aby zakończyć program");
                var choise = Console.ReadLine();
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
                            _dbOperations.ReadAllContactsFromDb();
                            Console.Clear();
                            EndOperation();
                            break;
                        case "5":
                            UISeparator();
                            ChoseDatabaseOperations();
                            Console.Clear();
                            EndOperation();
                            break;
                        case "6":
                            endProgram = true;
                            break;
                        case "7":
                            EndOperation();
                            break;
                        default:
                            Console.WriteLine("Podałeś zły wybór");
                            EndOperation();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    UISeparator();
                    Console.WriteLine("Wystąpił wyjątek: ");
                    Console.WriteLine($"\t{ex.Message}");
                    Console.WriteLine("========================================");                    
                }
            }while (endProgram = false);
        }                
        private void ChoseDatabaseOperations()
        {
            Console.Clear();
            string choise = _dataFromUser.GetDatabaseOperation();
            UISeparator();
            string id = null;
            switch (choise)
            {
                case "1":
                    id = _dataFromUser.DatabaseOperationsGetID();
                    _dbOperations.DeleteFromDbByID(id);
                    break;
                case "2":
                    Console.Clear();
                    id = _dataFromUser.DatabaseOperationsGetID();
                    _dbOperations.EditFromDbByID(id);
                    break;
                case "3":
                    _dbOperations.AddNewDbEntry();
                    break;
            }
        }
        private void ImportDataFromCsv()
        {            
            string path = _dataFromUser.ImportGetPath();
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
        private void ImportDataFromXml()
        {            
            string path = _dataFromUser.ImportGetPath();
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
        private void ExportToXML()
        {
            string choiseType = _dataFromUser.ExportToXmlGetType();
            string pathXml = _dataFromUser.ExportToXmlGetFolder();
            bool period = _dataFromUser.ExportToXmlGetLoopState();
            switch (choiseType)
            {
                case "1":
                    _xmlWriter.ExportToXmlYealinkRemote(pathXml, period);
                    break;
                case "2":
                    _xmlWriter.ExportToXmlYealinkLocal(pathXml, period);
                    break;
                case "3":
                    _xmlWriter.ExportToXmlFanvilRemoteAndLocal(pathXml, period);
                    break;
                default:
                    Console.Clear();
                    break;
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
        
    }
}
