using PhonebookConverterL.Data;
using PhonebookConverterL.Data.Entities;
using PhonebookConverter.Components.Database;
using PhonebookConverter.Components.Export;
using PhonebookConverter.Components.Import;
using PhonebookConverter.Data.Entities;
using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using PhonebookConverter.Components.DataTxt;
using PhonebookConverter.UIAndExceptions;

namespace PhonebookConverterL.UI
{
    public class UserIntarface : IUserIntarface
    {
        private readonly IExceptions _exceptions;
        private readonly ICsvReader _csvReader;
        private readonly ICsvWriter _csvWriter;
        private readonly PhonebookDbContext _phonebookDbContext;
        private readonly IXmlWriter _xmlWriter;
        private readonly IXmlReader _xmlReader;
        private readonly IDataFromUser _dataFromUser;
        private readonly IDbOperations _dbOperations;
        private readonly IDataInFileTxt _dataInFileTxt;
        private readonly IExportLoopSettings _exportLoopSettings;

        public UserIntarface(ICsvReader csvReader
            , IXmlWriter xmlWriter
            , PhonebookDbContext phonebookDbContext
            , IXmlReader xmlReader
            , IDataFromUser dataFromUser
            , IExportLoopSettings exportLoopSettings
            , ICsvWriter csvWriter
            , IExceptions exceptions
            , IDataInFileTxt dataInFileTxt
            , IDbOperations dbOperations)
        {
            _exceptions = exceptions;
            _csvReader = csvReader;
            _csvWriter = csvWriter;
            _phonebookDbContext = phonebookDbContext;
            _phonebookDbContext.Database.EnsureCreated();
            _xmlWriter = xmlWriter;
            _xmlReader = xmlReader;
            _dataFromUser = dataFromUser;
            _dbOperations = dbOperations;
            _dataInFileTxt = dataInFileTxt;
            _exportLoopSettings = exportLoopSettings;
        }
        public void FirstUIChoise()
        {
            _exportLoopSettings.CheckExportLoopSettingsExist();
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
                            ExportToXML("xml");
                            Console.Clear();
                            EndOperation();
                            break;
                        case "4":
                            UISeparator();
                            ExportToCsv("csv");
                            Console.Clear();
                            EndOperation();
                            break;
                        case "5":
                            UISeparator();
                            ChoseDatabaseOperations("MSSQL");                            
                            break;
                        case "6":
                            UISeparator();
                            ChoseDatabaseOperations("FILE");                            
                            EndOperation();
                            break;
                        case "7":
                            endProgram = true;
                            break;
                        case "8":
                            EndOperation();
                            break;
                        default:
                            Console.Clear();
                            break;
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
        private void ExportToXML(string format)
        {
            do
            {
                Console.Clear();
                string choiseType = _dataFromUser.ExportGetType();
                if (choiseType == "0") break;
                string pathXml = _dataFromUser.ExportGetFolder();
                if (pathXml == "0") break;
                bool loopState = _dataFromUser.ExportGetLoopState();                
                var exportData = new ExportPeriodData
                {
                    Path = pathXml,
                    Type = choiseType,
                    Format = format
                };
                int loopTime = 0;
                if (loopState == true)
                {
                    loopTime = _dataFromUser.ExportGetLoopTime();
                    if (loopTime == 0) break;
                    exportData.Interval = loopTime;
                }
                var tuple = (choiseType, loopState);
                Console.Clear();
                switch (tuple)
                {
                    case ("Yealink_Local_Phonebook", false):
                        _xmlWriter.ExportToXmlYealinkLocal(pathXml); 
                        break;
                    case ("Yealink_Remote_Phonebook", false):
                        _xmlWriter.ExportToXmlYealinkRemote(pathXml);
                        break;
                    case ("Fanvil_Local_and_Remote_Phonebook", false):
                        _xmlWriter.ExportToXmlFanvilRemoteAndLocal(pathXml);
                        break;
                    default:
                        _exportLoopSettings.SetPeriodicExport(exportData);                        
                        break;                        
                }
                break;
            }
            while (true);
        }
        private void ExportToCsv(string format)
        {
            do
            {
                Console.Clear();
                string choiseType = _dataFromUser.ExportGetType();
                if (choiseType == "0") break;
                string pathXml = _dataFromUser.ExportGetFolder();
                if (pathXml == "0") break;
                bool loopState = _dataFromUser.ExportGetLoopState();
                var exportData = new ExportPeriodData
                {
                    Path = pathXml,
                    Type = choiseType,
                    Format = format
                };
                int loopTime = 0;
                if (loopState == true)
                {
                    loopTime = _dataFromUser.ExportGetLoopTime();
                    if (loopTime == 0) break;
                    exportData.Interval = loopTime;
                }
                var tuple = (choiseType, loopState);
                Console.Clear();
                switch (tuple)
                {
                    case ("Yealink_Local_Phonebook", false):
                        _csvWriter.ExportToCsvYealinkLocal(pathXml);
                        break;
                    case ("Fanvil_Local_Phonebook", false):
                        _csvWriter.ExportToCsvFanvilLocal(pathXml);
                        break;
                    case ("Yeastar_P_Series_Phonebook", false):
                        _csvWriter.ExportToCsvYeastarPSeries(pathXml);
                        break;
                    default:
                        _exportLoopSettings.SetPeriodicExport(exportData);
                        break;
                }
                break;
            }
            while (true);
        }
        private void ChoseDatabaseOperations(string dataStorage)
        {
            Console.Clear();
            string choise = _dataFromUser.DataOperationsGetType();
            UISeparator();
            if (choise != "0")
            {
                int? id = null;
                Console.Clear();
                var tuple = (choise, dataStorage);
                switch (tuple)
                {
                    case ("1","MSSQL"):
                        id = _dataFromUser.DataOperationsGetID(dataStorage);
                        if (id == 0) break;
                        _dbOperations.DeleteByID(id);
                        break;
                    case ("2", "MSSQL"):
                        id = _dataFromUser.DataOperationsGetID(dataStorage);
                        if (id == 0) break;
                        _dbOperations.EditByID(id);
                        break;
                    case ("3", "MSSQL"):
                        _dbOperations.AddNewEntry();
                        break;
                    case ("4", "MSSQL"):
                        _dbOperations.ShowAllContacts();
                        string choiseExportMSSQL = _dataFromUser.DataOperationsExportToTxt();
                        if (choiseExportMSSQL == "2") break;
                        _dbOperations.SaveDataFromDbToTxt();
                        break;
                    case ("1", "FILE"):
                        id = _dataFromUser.DataOperationsGetID(dataStorage);
                        if (id == 0) break;
                        _dataInFileTxt.DeleteByID(id);
                        break;
                    case ("2", "FILE"):
                        id = _dataFromUser.DataOperationsGetID(dataStorage);
                        if (id == 0) break;
                        _dataInFileTxt.EditByID(id);
                        break;
                    case ("3", "FILE"):
                        _dataInFileTxt.AddNewEntry();
                        break;
                    case ("4", "FILE"):
                        _dataInFileTxt.ShowAllContacts();
                        string choiseExportFile = _dataFromUser.DataOperationsExportToTxt();
                        if (choiseExportFile == "2") break;
                        _dataInFileTxt.SaveDataFromFileToTxt();
                        break;
                }
            }
        }
        private void UISeparator()
        {
            Console.WriteLine("");
            Console.WriteLine("///////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("");
        }
        private void EndOperation()
        {
            
            UISeparator();
            
        }

    }
}
