using PhonebookConverterL.Data;
using PhonebookConverterL.Data.Entities;
using PhonebookConverter.Components.MSQSQLDb;
using PhonebookConverter.Components.Export;
using PhonebookConverter.Components.Import;
using PhonebookConverter.Data.Entities;
using PhonebookConverter.Components.DataTxt;
using PhonebookConverter.UIAndValidation.Validation;
using PhonebookConverter.Data;

namespace PhonebookConverter.UIAndValidationm
{
    public class UserInterface : IUserInterface
    {
        private readonly IValidation validation;
        private readonly ICsvReader csvReader;
        private readonly ICsvWriter csvWriter;
        private readonly PhonebookDbContext phonebookDbContext;        
        private readonly IXmlWriter xmlWriter;
        private readonly IXmlReader xmlReader;
        private readonly IDataFromUser dataFromUser;
        private readonly IMSSQLDb MSSQLDb;
        private readonly IDataInFile dataInFileTxt;
        private readonly IExportLoopSettings exportLoopSettings;
        private object fileLock = new object();

        public UserInterface(ICsvReader csvReader
            , IXmlWriter xmlWriter
            , PhonebookDbContext phonebookDbContext
            , PhonebookFileContext phonebookFileContext
            , IXmlReader xmlReader
            , IDataFromUser dataFromUser
            , IExportLoopSettings exportLoopSettings
            , ICsvWriter csvWriter
            , IValidation validation
            , IDataInFile dataInFileTxt
            , IMSSQLDb MSSQLDb)
        {
            this.validation = validation;
            this.csvReader = csvReader;
            this.csvWriter = csvWriter;
            this.phonebookDbContext = phonebookDbContext;            
            this.xmlWriter = xmlWriter;
            this.xmlReader = xmlReader;
            this.dataFromUser = dataFromUser;
            this.MSSQLDb = MSSQLDb;
            this.dataInFileTxt = dataInFileTxt;
            this.exportLoopSettings = exportLoopSettings;
        }
        public void MainMenu()
        {
            var dataType = dataFromUser.GetDataType();
            if (dataType == "MSSQL")
            {
                phonebookDbContext.Database.EnsureCreated();
            }
            exportLoopSettings.CheckExportLoopSettingsExist();
            bool endProgram = false;
            do
            {
                var choise = dataFromUser.MainMenu();
                validation.ExceptionsLoop(() =>
                {
                    switch (choise)
                    {
                        case "1":
                            ImportDataFromCsv(dataType);
                            break;
                        case "2":
                            ImportDataFromXml(dataType);
                            break;
                        case "3":
                            ExportToXML(dataType);
                            break;
                        case "4":
                            ExportToCsv(dataType);
                            break;
                        case "5":
                            DataOperations(dataType);
                            break;
                        case "6":
                            endProgram = true;
                            break;
                        case "7":                            
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
        private void ImportDataFromCsv(string dataType)
        {
            string path = dataFromUser.GetImportPathCsv();
            if (path != "0")
            {
                var contacts = csvReader.TypeChecker(path);
                var database = dataFromUser.CheckDataType(dataType);
                lock (fileLock)
                {
                    
                    foreach (var contact in contacts)
                    {
                        database.Add(new ContactInDb()
                        {
                            Name = contact.Name,
                            Phone1 = contact.Phone1,
                            Phone2 = contact.Phone2,
                            Phone3 = contact.Phone3,
                        });
                    }
                }
                dataFromUser.SaveData(database, dataType);
            }

        }
        private void ImportDataFromXml(string dataType)
        {
            Console.Clear();
            string path = dataFromUser.GetImportPathXml();
            var database = dataFromUser.CheckDataType(dataType);
            if (path != "0")
            {
                var contacts = xmlReader.TypeChecker(path);
                lock (fileLock)
                {
                    foreach (var contact in contacts)
                    {
                        database.Add(new ContactInDb()
                        {
                            Name = contact.Name,
                            Phone1 = contact.Phone1,
                            Phone2 = contact.Phone2,
                            Phone3 = contact.Phone3,
                        });
                    }
                }
                dataFromUser.SaveData(database, dataType);
            }
        }
        private void ExportToXML(string dataType)
        {
            do
            {
                Console.Clear();
                string choiseType = dataFromUser.GetExportType();
                if (choiseType == "0") break;
                string pathXml = dataFromUser.GetFolder();
                if (pathXml == "0") break;
                bool loopState = dataFromUser.GetExportLoopState();
                ExportPeriodData? exportData = new ExportPeriodData();
                int loopTime;
                if (loopState == true)
                {
                    exportData.Path = pathXml;
                    exportData.Type = choiseType;
                    exportData.Format = "xml";
                    loopTime = dataFromUser.GetExportLoopTime();
                    if (loopTime == 0) break;
                    exportData.Interval = loopTime;
                }
                Console.Clear();
                var contacts = dataFromUser.CheckDataType(dataType);
                var tuple = (choiseType, loopState);
                switch (tuple)
                {
                    case ("Yealink_Local_Phonebook", false):
                        xmlWriter.YealinkLocal(pathXml, contacts);
                        break;
                    case ("Yealink_Remote_Phonebook", false):
                        xmlWriter.YealinkRemote(pathXml, contacts);
                        break;
                    case ("Fanvil_Local_and_Remote_Phonebook", false):
                        xmlWriter.FanvilRemoteAndLocal(pathXml, contacts);
                        break;
                    default:
                        exportLoopSettings.SetPeriodicExport(exportData);
                        break;
                }
                break;
            }
            while (true);
        }
        private void ExportToCsv(string dataType)
        {
            do
            {
                Console.Clear();
                string choiseType = dataFromUser.GetExportType();
                if (choiseType == "0") break;
                string pathXml = dataFromUser.GetFolder();
                if (pathXml == "0") break;
                bool loopState = dataFromUser.GetExportLoopState();
                ExportPeriodData? exportData = new ExportPeriodData();
                int loopTime;
                if (loopState == true)
                {
                    exportData.Path = pathXml;
                    exportData.Type = choiseType;
                    exportData.Format = "csv";
                    loopTime = dataFromUser.GetExportLoopTime();
                    if (loopTime == 0) break;
                    exportData.Interval = loopTime;
                }
                Console.Clear();
                var contacts = dataFromUser.CheckDataType(dataType);
                var tuple = (choiseType, loopState);
                switch (tuple)
                {
                    case ("Yealink_Local_Phonebook", false):
                        csvWriter.YealinkLocal(pathXml, contacts);
                        break;
                    case ("Fanvil_Local_Phonebook", false):
                        csvWriter.FanvilLocal(pathXml, contacts);
                        break;
                    case ("Yeastar_P_Series_Phonebook", false):
                        csvWriter.YeastarPSeries(pathXml, contacts);
                        break;
                    default:
                        exportLoopSettings.SetPeriodicExport(exportData);
                        break;
                }
                break;
            }
            while (true);
        }        
        private void DataOperations(string dataStorage)
        {            
            Console.Clear();
            string choise = dataFromUser.GetType();
            if (choise != "0")
            {
                Console.Clear();
                var tuple = (choise, dataStorage);
                switch (tuple)
                {
                    case ("1", "MSSQL"):
                        MSSQLDb.FindAndManipulatContactIn(dataStorage);
                            break;                                  
                    case ("2", "MSSQL"):
                        MSSQLDb.AddNewEntry();
                        break;
                    case ("3", "MSSQL"):
                        MSSQLDb.ShowAllContacts();                                                
                        break;                    
                    case ("1", "FILE"):
                        dataInFileTxt.FindAndManipulatContactIn(dataStorage);
                        break;
                    case ("2", "FILE"):
                        dataInFileTxt.AddNewEntry();
                        break;
                    case ("3", "FILE"):
                        dataInFileTxt.ShowAllContacts();
                        break;
                }
            }
        }       
    }
}
