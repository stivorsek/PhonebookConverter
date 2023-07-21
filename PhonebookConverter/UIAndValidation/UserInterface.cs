using PhonebookConverter.Data;
using PhonebookConverter.Data.Entities;
using PhonebookConverter.Components.MSQSQLDb;
using PhonebookConverter.Components.Export;
using PhonebookConverter.Components.Import;
using PhonebookConverter.Components.DataTxt;
using PhonebookConverter.UIAndValidation.Validation;

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
            var database = dataFromUser.CheckDataType(dataType);
            if (database == null)
            {
                ImportSampleData(dataType);
            }
            exportLoopSettings.CheckExportLoopSettingsExist();
            bool endProgram = false;
            do
            {
                var choise = dataFromUser.ShowMainMenu();
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
                            GetExportData(dataType, "xml");
                            break;
                        case "4":
                            GetExportData(dataType, "csv");
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
        private void ImportSampleData(string dataType)
        {
            string subfolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "ExampleData", "PhonebookYealinkLocal.xml");            
            var contacts = xmlReader.TypeChecker(subfolderPath);
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
        private void GetExportData(string dataType, string formatType)
        {
            try
            {
                Console.Clear();
                string choiseType = dataFromUser.GetExportType();
                if (choiseType == "0") throw new Exception("Back to main menu");
                string path = dataFromUser.GetExportFolder();
                if (path == "0") throw new Exception("Back to main menu");
                bool loopState = dataFromUser.GetExportLoopState();
                ExportData? exportData = new ExportData
                {
                    DataType = dataType,
                    Path = path,
                    Type = choiseType,
                    Format = formatType
                };
                int loopTime;
                if (loopState == true)
                {
                    loopTime = dataFromUser.GetExportLoopTime();
                    if (loopTime != 0)
                        exportData.Interval = loopTime;
                }
                Console.Clear();
                if (formatType == "csv") ExportToCsv(exportData, choiseType, loopState);
                if (formatType == "xml") ExportToXML(exportData, choiseType, loopState);
            } catch {  }
        }
        private void ExportToXML(ExportData exportData, string choiseType, bool loopState)
        {
            do
            {
                var contacts = dataFromUser.CheckDataType(exportData.DataType);
                var tuple = (choiseType, loopState);
                switch (tuple)
                {
                    case ("Yealink_Local_Phonebook", false):
                        xmlWriter.YealinkLocal(exportData.Path, contacts);
                        break;
                    case ("Yealink_Remote_Phonebook", false):
                        xmlWriter.YealinkRemote(exportData.Path, contacts);
                        break;
                    case ("Fanvil_Local_and_Remote_Phonebook", false):
                        xmlWriter.FanvilRemoteAndLocal(exportData.Path, contacts);
                        break;
                    default:
                        exportLoopSettings.SetPeriodicExport(exportData);
                        break;
                }
                break;
            }
            while (true);
        }
        private void ExportToCsv(ExportData exportData, string choiseType, bool loopState)
        {
            do
            {                
                var contacts = dataFromUser.CheckDataType(exportData.DataType);
                var tuple = (choiseType, loopState);
                switch (tuple)
                {
                    case ("Yealink_Local_Phonebook", false):
                        csvWriter.YealinkLocal(exportData.Path, contacts);
                        break;
                    case ("Fanvil_Local_Phonebook", false):
                        csvWriter.FanvilLocal(exportData.Path, contacts);
                        break;
                    case ("Yeastar_P_Series_Phonebook", false):
                        csvWriter.YeastarPSeries(exportData.Path, contacts);
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
                        MSSQLDb.FindAndManipulatContact(dataStorage);
                            break;                                  
                    case ("2", "MSSQL"):
                        MSSQLDb.AddContact();
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
