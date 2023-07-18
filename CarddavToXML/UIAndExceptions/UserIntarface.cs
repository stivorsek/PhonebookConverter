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
        private readonly IExceptions exceptions;
        private readonly ICsvReader csvReader;
        private readonly ICsvWriter csvWriter;
        private readonly PhonebookDbContext phonebookDbContext;
        private readonly IXmlWriter xmlWriter;
        private readonly IXmlReader xmlReader;
        private readonly IDataFromUser dataFromUser;
        private readonly IDbOperations dbOperations;
        private readonly IDataInFileTxt dataInFileTxt;
        private readonly IExportLoopSettings exportLoopSettings;

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
            this.exceptions = exceptions;
            this.csvReader = csvReader;
            this.csvWriter = csvWriter;
            this.phonebookDbContext = phonebookDbContext;
            this.phonebookDbContext.Database.EnsureCreated();
            this.xmlWriter = xmlWriter;
            this.xmlReader = xmlReader;
            this.dataFromUser = dataFromUser;
            this.dbOperations = dbOperations;
            this.dataInFileTxt = dataInFileTxt;
            this.exportLoopSettings = exportLoopSettings;
        }
        public void FirstUIChoise()
        {
            exportLoopSettings.CheckExportLoopSettingsExist();
            bool endProgram = false;
            do
            {
                var choise = dataFromUser.FirstUIChoise();
                this.exceptions.ExceptionsLoop(() =>
                {
                    switch (choise)
                    {
                        case "1":                            
                            ImportDataFromCsv();                            
                            break;
                        case "2":                            
                            ImportDataFromXml();
                            break;
                        case "3":                            
                            ExportToXML("xml");                                                        
                            break;
                        case "4":                            
                            ExportToCsv("csv");                                                        
                            break;
                        case "5":                            
                            ChoseDatabaseOperations("MSSQL");                            
                            break;
                        case "6":                            
                            ChoseDatabaseOperations("FILE");                                                        
                            break;
                        case "7":
                            endProgram = true;
                            break;
                        case "8":                            
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
            string path = dataFromUser.ImportGetPathCsv();
            if (path != "1")
            {
                var contacts = this.csvReader.TypeChecker(path);
                foreach (var contact in contacts)
                {
                    phonebookDbContext.Phonebook.Add(new ContactInDb()
                    {
                        Name = contact.Name,
                        Phone1 = contact.Phone1,
                        Phone2 = contact.Phone2,
                        Phone3 = contact.Phone3,
                    });
                }
                phonebookDbContext.SaveChanges();
            }
        }
        private void ImportDataFromXml()
        {
            Console.Clear();
            string path = dataFromUser.ImportGetPathXml();
            if (path != "1")
            {
                var contacts = xmlReader.TypeChecker(path);
                foreach (var contact in contacts)
                {
                    phonebookDbContext.Phonebook.Add(new ContactInDb()
                    {
                        Name = contact.Name,
                        Phone1 = contact.Phone1,
                        Phone2 = contact.Phone2,
                        Phone3 = contact.Phone3,
                    });
                }
                phonebookDbContext.SaveChanges();                
            }
        }
        private void ExportToXML(string format)
        {
            do
            {
                Console.Clear();
                string choiseType = dataFromUser.ExportGetType();
                if (choiseType == "0") break;
                string pathXml = dataFromUser.ExportGetFolder();
                if (pathXml == "0") break;
                bool loopState = dataFromUser.ExportGetLoopState();                
                var exportData = new ExportPeriodData
                {
                    Path = pathXml,
                    Type = choiseType,
                    Format = format
                };
                int loopTime = 0;
                if (loopState == true)
                {
                    loopTime = dataFromUser.ExportGetLoopTime();
                    if (loopTime == 0) break;
                    exportData.Interval = loopTime;
                }
                var tuple = (choiseType, loopState);
                Console.Clear();
                switch (tuple)
                {
                    case ("Yealink_Local_Phonebook", false):
                        xmlWriter.YealinkLocal(pathXml); 
                        break;
                    case ("Yealink_Remote_Phonebook", false):
                        xmlWriter.YealinkRemote(pathXml);
                        break;
                    case ("Fanvil_Local_and_Remote_Phonebook", false):
                        xmlWriter.FanvilRemoteAndLocal(pathXml);
                        break;
                    default:
                        exportLoopSettings.SetPeriodicExport(exportData);                        
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
                string choiseType = dataFromUser.ExportGetType();
                if (choiseType == "0") break;
                string pathXml = dataFromUser.ExportGetFolder();
                if (pathXml == "0") break;
                bool loopState = dataFromUser.ExportGetLoopState();
                var exportData = new ExportPeriodData
                {
                    Path = pathXml,
                    Type = choiseType,
                    Format = format
                };
                int loopTime = 0;
                if (loopState == true)
                {
                    loopTime = dataFromUser.ExportGetLoopTime();
                    if (loopTime == 0) break;
                    exportData.Interval = loopTime;
                }
                var tuple = (choiseType, loopState);
                Console.Clear();
                switch (tuple)
                {
                    case ("Yealink_Local_Phonebook", false):
                        csvWriter.YealinkLocal(pathXml);
                        break;
                    case ("Fanvil_Local_Phonebook", false):
                        csvWriter.FanvilLocal(pathXml);
                        break;
                    case ("Yeastar_P_Series_Phonebook", false):
                        csvWriter.YeastarPSeries(pathXml);
                        break;
                    default:
                        exportLoopSettings.SetPeriodicExport(exportData);
                        break;
                }
                break;
            }
            while (true);
        }
        private void ChoseDatabaseOperations(string dataStorage)
        {
            Console.Clear();
            string choise = dataFromUser.DataOperationsGetType();            
            if (choise != "0")
            {
                int? id = null;
                Console.Clear();
                var tuple = (choise, dataStorage);
                switch (tuple)
                {
                    case ("1","MSSQL"):
                        id = dataFromUser.DataOperationsGetID(dataStorage);
                        if (id == 0) break;
                        dbOperations.DeleteByID(id);
                        break;
                    case ("2", "MSSQL"):
                        id = dataFromUser.DataOperationsGetID(dataStorage);
                        if (id == 0) break;
                        dbOperations.EditByID(id);
                        break;
                    case ("3", "MSSQL"):
                        dbOperations.AddNewEntry();
                        break;
                    case ("4", "MSSQL"):
                        dbOperations.ShowAllContacts();
                        string choiseExportMSSQL = dataFromUser.DataOperationsExportToTxt();
                        if (choiseExportMSSQL == "2") break;
                        dbOperations.SaveDataFromDbToTxt();
                        break;
                    case ("1", "FILE"):
                        id = dataFromUser.DataOperationsGetID(dataStorage);
                        if (id == 0) break;
                        dataInFileTxt.DeleteByID(id);
                        break;
                    case ("2", "FILE"):
                        id = dataFromUser.DataOperationsGetID(dataStorage);
                        if (id == 0) break;
                        dataInFileTxt.EditByID(id);
                        break;
                    case ("3", "FILE"):
                        dataInFileTxt.AddNewEntry();
                        break;
                    case ("4", "FILE"):
                        dataInFileTxt.ShowAllContacts();
                        string choiseExportFile = dataFromUser.DataOperationsExportToTxt();
                        if (choiseExportFile == "2") break;
                        dataInFileTxt.SaveDataFromFileToTxt();
                        break;
                }
            }
        }

    }
}
