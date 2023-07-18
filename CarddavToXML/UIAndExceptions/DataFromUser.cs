using PhonebookConverterL.Data;
using PhonebookConverterL.Data.Entities;
using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using PhonebookConverter.Data;

namespace PhonebookConverter.UIAndExceptions
{
    public class DataFromUser : IDataFromUser
    {
        private readonly IExceptions exceptions;
        private readonly PhonebookDbContext phonebookDbContext;        
        private readonly IValidation validation;
        private readonly FileContext fileContext;

        public DataFromUser(IExceptions exceptions, PhonebookDbContext phonebookDbContext, IValidation validation, FileContext fileContext)
        {
            this.exceptions = exceptions;
            this.phonebookDbContext = phonebookDbContext;            
            this.validation = validation;
            this.fileContext = fileContext;
        }
        public string ExportGetFolder()
        {
            return exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Chosse Folder to export or 1 to back to the main menu");
                var pathXml = validation.ExportToXmlGetFolder(Console.ReadLine());
                Console.Clear();
                return pathXml;
            });
        }
        public string ExportGetType()
        {
            return exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Chosse Type of XML Type");
                Console.WriteLine("\t 0)Back to the main Menu");
                Console.WriteLine("\t 1)Yealink Local Phonnebook");
                Console.WriteLine("\t 2)Yealink Remote Phonebook");
                Console.WriteLine("\t 3)Fanvil Local and Remote Phonebook");
                string choiseType = validation.ExportToXmlGetType(Console.ReadLine());
                if (choiseType == "1") choiseType = "Yealink_Local_Phonebook";
                if (choiseType == "2") choiseType = "Yealink_Remote_Phonebook";
                if (choiseType == "3") choiseType = "Fanvil_Local_and_Remote_Phonebook";
                Console.Clear();
                return choiseType;
            });
        }
        public bool ExportGetLoopState()
        {
            return exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Do you wanna make loop export");
                Console.WriteLine("1) Yes");
                Console.WriteLine("2) No");
                bool loopState = validation.ExportToXmlGetLoopState(Console.ReadLine());
                Console.Clear();
                return loopState;
            });
        }
        public int ExportGetLoopTime()
        {
            Console.Clear();
            return exceptions.ExceptionsLoop(() =>
            {                
                Console.WriteLine("Please give us interval time in seconds");
                var loopTime = validation.ExportToXmlGetLoopTime(Console.ReadLine());
                return loopTime * 1000;
            });
        }
        public string ImportGetPathCsv()
        {
            return exceptions.ExceptionsLoop(() =>
            {
                Console.Clear();
                Console.WriteLine("Please give us directory or chosee 0 to go back to the main menu");
                string path = validation.ImportGetPathCsv(Console.ReadLine());
                Console.Clear();
                return path;
            });
        }
        public string ImportGetPathXml()
        {
            return exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please give us directory or chosee 0 to go back to the main menu");
                string path = validation.ImportGetPathXml(Console.ReadLine());
                Console.Clear();
                return path;
            });
        }
        public int? DataOperationsGetID(string dataCenter)
        {
            return exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Give us ID or chosee 0 to go back to the main menu");
                string choise = Console.ReadLine();
                if (choise == "0") return int.Parse(choise);
                int? id = validation.DataOperationsGetID(choise);
                if (dataCenter == "MSSQL")
                {
                    var contactFromDb = validation.DataOperationsGetID(phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == id));
                }
                if (dataCenter == "FILE")
                {
                    var contacts = validation.DataOperationsGetID(fileContext.ReadAllContactsFromFile().FirstOrDefault(c => c.Id == id));
                }
                Console.Clear();
                return id;
            });
        }
        public string DataOperationsGetType()
        {
            return exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("0) To go back to the Main Menu");
                Console.WriteLine("1) Delete record by ID");
                Console.WriteLine("2) Edit record by ID");
                Console.WriteLine("3) Manual add record");
                Console.WriteLine("4) Show all records");
                var choise = validation.DataOperationsGetType(Console.ReadLine());
                Console.Clear();
                return choise;
            });
        }
        public string DataOperationsExportToTxt()
        {
            return exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Do you wanna save data in txt file?");
                Console.WriteLine("\t 1)Yes");
                Console.WriteLine("\t 2)No");
                var choise = validation.DataOperationsExportToTxt(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("\t Date wasn't saved in txt file");
                return choise;
            });
        }
        public string DataOperationsEditByIDGetChoise(ContactInDb contactFromDb)
        {
            return exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine($"\t1) Name : {contactFromDb.Name}");
                Console.WriteLine($"\t2) Phone1 : {contactFromDb.Phone1}");
                Console.WriteLine($"\t3) Phone2 : {contactFromDb.Phone2}");
                Console.WriteLine($"\t4) Phone3 : {contactFromDb.Phone3}");
                Console.WriteLine("");
                Console.WriteLine("Chose parameter to edit or 0 to go back to the Main Menu");
                var choise = validation.DataOperationsEditByIdChoseParameter(Console.ReadLine());
                return choise;
            });
        }
        public string DataOperationsEditByIdGetParameter()
        {
            Console.WriteLine("Give us parameter");
            var parameter = Console.ReadLine();
            return parameter;
        }
        public ContactInDb DataOperationsAddNewEntryGetData()
        {
            Console.Clear();
            return exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Give us Name");
                var Name = Console.ReadLine();
                Console.WriteLine("Give us First Phone Number");
                var Phone1 = validation.IntParseValidation(Console.ReadLine());
                Console.WriteLine("Give us Second Phone Number");
                var Phone2 = validation.IntParseValidation(Console.ReadLine());
                Console.WriteLine("Give us Third Phone Number");
                var Phone3 = validation.IntParseValidation(Console.ReadLine());
                var contact = new ContactInDb()
                {
                    Name = Name,
                    Phone1 = Phone1,
                    Phone2 = Phone2,
                    Phone3 = Phone3
                };
                Console.Clear();
                return contact;
            });
        }
        public string FirstUIChoise()
        {            
            Console.WriteLine("///////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("");
            Console.WriteLine("\tWellcome in program to menage phonebook files");
            Console.WriteLine("");
            Console.WriteLine("///////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("");
            Console.WriteLine("\tPlease choose the operation");
            Console.WriteLine("1) Import data from CSV");
            Console.WriteLine("2) Import data from XML");
            Console.WriteLine("3) Exportuj data to XML");
            Console.WriteLine("4) Exportuj data to CSV");
            Console.WriteLine("5) Operations on database");
            Console.WriteLine("6) Operations on data in file");
            Console.WriteLine("7) To close the program");
            var choise = Console.ReadLine();
            return choise;
        }
        public int? IntParseValidation(string data)
        {
            int? result = string.IsNullOrEmpty(data) ? null : int.Parse(data);
            return result;
        }
        public string CheckExportSettingsExist()
        {
            return exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Do you wanna set back that export loop?");
                Console.WriteLine("1) Yes");
                Console.WriteLine("2) No (Export loop data will be deleted!!!)");
                var choise = validation.CheckExportSettingsExist(Console.ReadLine());
                Console.Clear();
                return choise;
            });
        }

    }
}
