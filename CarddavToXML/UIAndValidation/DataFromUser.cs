using PhonebookConverterL.Data;
using PhonebookConverterL.Data.Entities;
using PhonebookConverter.Data;
using PhonebookConverter.UIAndValidation.Validation;

namespace PhonebookConverter.UIAndValidationm
{
    public class DataFromUser : IDataFromUser
    {
        private readonly PhonebookDbContext phonebookDbContext;
        private readonly IValidation validation;
        private readonly PhonebookFileContext fileContext;
        public DataFromUser(PhonebookDbContext phonebookDbContext, IValidation validation, PhonebookFileContext fileContext)
        {
            this.phonebookDbContext = phonebookDbContext;
            this.validation = validation;
            this.fileContext = fileContext;
        }
        public string ExportGetFolder()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter the folder path or 1 to back to the main menu");
                var pathXml = validation.ExportToXmlGetFolder(Console.ReadLine());
                Console.Clear();
                return pathXml;
            });
        }
        public string ExportGetType()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Chosse Type of XML File");
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
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Do you want make cyclical export");
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
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter an interval time in seconds");
                var loopTime = validation.ExportToXmlGetLoopTime(Console.ReadLine());
                return loopTime * 1000;
            });
        }
        public string ImportGetPathCsv()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.Clear();
                Console.WriteLine("Please enter path or chosee 0 to go back to the main menu");
                string path = validation.ImportGetPathCsv(Console.ReadLine());
                Console.Clear();
                return path;
            });
        }
        public string ImportGetPathXml()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter path or chosee 0 to go back to the main menu");
                string path = validation.ImportGetPathXml(Console.ReadLine());
                Console.Clear();
                return path;
            });
        }
        public int? DataOperationsGetID(string dataCenter)
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter ID or chosee 0 to go back to the main menu");
                string choise = Console.ReadLine();
                if (choise == "0")
                {
                    Console.Clear();
                    return int.Parse(choise);
                }
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
        public string DataOperationsGetName(string dataCenter)
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter Name or chosee 0 to go back to the main menu");
                string name = Console.ReadLine();
                if (name == "0")
                {
                    Console.Clear();
                    return name;
                }
                if (dataCenter == "MSSQL")
                {
                    var contactFromDb = validation.DataOperationsGetName(phonebookDbContext.Phonebook.FirstOrDefault(c => c.Name == name));
                }
                if (dataCenter == "FILE")
                {
                    var contacts = validation.DataOperationsGetName(fileContext.ReadAllContactsFromFile().FirstOrDefault(c => c.Name == name));
                }
                Console.Clear();
                return name;
            });
        }
        public string DataOperationsGetType()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("0) Back to the Main Menu");
                Console.WriteLine("1) Delete record by ID");
                Console.WriteLine("2) Edit record by ID");
                Console.WriteLine("3) Edit record by Name");
                Console.WriteLine("4) Manual add record");
                Console.WriteLine("5) Show all records");
                var choise = validation.DataOperationsGetType(Console.ReadLine());
                Console.Clear();
                return choise;
            });
        }
        public string DataOperationsExportToTxt()
        {
            return validation.ExceptionsLoop(() =>
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
        public string DataOperationsEditGetChoise(ContactInDb contactFromDb)
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine($"\t1) Name : {contactFromDb.Name}");
                Console.WriteLine($"\t2) Phone1 : {contactFromDb.Phone1}");
                Console.WriteLine($"\t3) Phone2 : {contactFromDb.Phone2}");
                Console.WriteLine($"\t4) Phone3 : {contactFromDb.Phone3}");
                Console.WriteLine("");
                Console.WriteLine("Chose parameter you wanna edit or 0 to go back to the Main Menu");
                var choise = Console.ReadLine();
                if (choise == "0") return choise;
                validation.DataOperationsEditChoseParameter(choise);
                return choise;
            });
        }
        public string DataOperationsEditGetParameter()
        {
            Console.WriteLine("Please enter parameter");
            var parameter = Console.ReadLine();
            return parameter;
        }
        public ContactInDb DataOperationsAddNewEntryGetData()
        {
            Console.Clear();
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter Name");
                var Name = Console.ReadLine();
                Console.WriteLine("Please enter First Phone Number");
                var Phone1 = validation.IntParseValidation(Console.ReadLine());
                Console.WriteLine("Please enter Second Phone Number");
                var Phone2 = validation.IntParseValidation(Console.ReadLine());
                Console.WriteLine("Please enter Third Phone Number");
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
        public string MainMenu()
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
            Console.WriteLine("3) Export data to XML");
            Console.WriteLine("4) Export data to CSV");
            Console.WriteLine("5) Operations on data");
            Console.WriteLine("6) To close the program");
            var choise = Console.ReadLine();
            return choise;
        }
        public List<ContactInDb> CheckDataType(string dataType)
        {
            List<ContactInDb> contactsFromDb = new List<ContactInDb>();
            if (dataType == "MSSQL")
            {
                return contactsFromDb = phonebookDbContext.Phonebook.ToList();
            }
            if (dataType == "FILE")
            {
                return contactsFromDb = fileContext.ReadAllContactsFromFile().ToList();
            }
            return null;
        }
        public string GetDataType()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("\tPleach chosee data you wanna use");
                Console.WriteLine("0) End program");
                Console.WriteLine("1) Files");
                Console.WriteLine("2) MSSQL");
                var dataType = validation.GetDataType(Console.ReadLine());
                Console.Clear();
                return dataType;
            });
        }
        public string CheckExportSettingsExist()
        {
            return validation.ExceptionsLoop(() =>
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
