using PhonebookConverter.Data;
using PhonebookConverter.Data.Entities;
using PhonebookConverter.UIAndValidation.Validation;

namespace PhonebookConverter.UIAndValidationm
{
    public class DataFromUser : IDataFromUser
    {
        private readonly PhonebookDbContext phonebookDbContext;
        private readonly IValidation validation;
        private readonly PhonebookFileContext phonebookFileContext;
        public DataFromUser(PhonebookDbContext phonebookDbContext, IValidation validation, PhonebookFileContext phonebookFileContext)
        {
            this.phonebookDbContext = phonebookDbContext;
            this.validation = validation;
            this.phonebookFileContext = phonebookFileContext;
        }
        public string ShowMainMenu()
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
        public string GetExportFolder()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter the folder path or 0 to back to the main menu");
                var pathXml = validation.GetExportFolder(Console.ReadLine());
                Console.Clear();
                return pathXml;
            });
        }
        public string GetExportType()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Chosse Type of XML File");
                Console.WriteLine("\t 0)Back to the main Menu");
                Console.WriteLine("\t 1)Yealink Local Phonnebook");
                Console.WriteLine("\t 2)Yealink Remote Phonebook");
                Console.WriteLine("\t 3)Fanvil Local and Remote Phonebook");
                string choiseType = validation.GetExportType(Console.ReadLine());
                if (choiseType == "1") choiseType = "Yealink_Local_Phonebook";
                if (choiseType == "2") choiseType = "Yealink_Remote_Phonebook";
                if (choiseType == "3") choiseType = "Fanvil_Local_and_Remote_Phonebook";
                Console.Clear();
                return choiseType;
            });
        }
        public bool GetExportLoopState()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Do you want make cyclical export");
                Console.WriteLine("1) Yes");
                Console.WriteLine("2) No");
                bool loopState = validation.GetExportLoopState(Console.ReadLine());
                Console.Clear();
                return loopState;
            });
        }
        public int GetExportLoopTime()
        {
            Console.Clear();
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter an interval time in seconds");
                var loopTime = validation.GetExportLoopTime(Console.ReadLine());
                return loopTime * 1000;
            });
        }
        public string GetImportPathCsv()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.Clear();
                Console.WriteLine("Please enter path or chosee 0 to go back to the main menu");
                string path = validation.GetImportPathCsv(Console.ReadLine());
                Console.Clear();
                return path;
            });
        }
        public string GetImportPathXml()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter path or chosee 0 to go back to the main menu");
                string path = validation.GetImportPathXml(Console.ReadLine());
                Console.Clear();
                return path;
            });
        }
        public string GetType()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter the choise");
                Console.WriteLine("0) Back to the Main Menu");                
                Console.WriteLine("1) Find record (Show/Delete/Edit)");
                Console.WriteLine("2) Manual add record");
                Console.WriteLine("3) Show all records");
                var choise = validation.GetType(Console.ReadLine());
                Console.Clear();
                return choise;
            });
        }
        public string ExportToTxt()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Do you wanna save data in txt file?");
                Console.WriteLine("\t 1)Yes");
                Console.WriteLine("\t 2)No");
                var choise = validation.ExportToTxt(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("\t Date wasn't saved in txt file");
                return choise;
            });
        }
        public string SearchType()
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter the type of parameted you wanna search by or enter 0 to back to the last menu");
                Console.WriteLine("\t1) ID ");
                Console.WriteLine("\t2) Name");
                Console.WriteLine("\t3) Phonenumber");
                var choise = Console.ReadLine();
                if (choise == "0") return choise;
                validation.DataOperationsGetSearchType(choise);
                return choise;
            });
        }
        public ContactInDb FindContact(string dataCenter, string searchType)
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter the ID, Name or Phonenumber u wanna search by or enter 0 to back to the main menu");
                string searchParameter = Console.ReadLine();
                if (searchParameter == "0")
                {
                    Console.Clear();
                    return null;
                }
                List<ContactInDb>? contacts = CheckDataType(dataCenter);
                ContactInDb contact = new ContactInDb();
                switch (searchType)
                {
                    case "1":
                        contact = contacts.Find(c => c.Id == int.Parse(searchParameter));
                        break;
                    case "2":
                        contact = contacts.Find(c => c.Name == searchParameter);
                        break;
                    case "3":
                        contact = contacts.Find(c => c.Phone1 == int.Parse(searchParameter) 
                            && c.Phone2 == int.Parse(searchParameter)
                            && c.Phone3 == int.Parse(searchParameter));
                        break;                    
                }
                validation.DataOperationsFindConctat(contact);
                Console.Clear();
                return contact;
            });
        }
        public string EditGetParameter(string choise)
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Please enter parameter");
                var parameter = Console.ReadLine();
                validation.EditGetParameter(parameter, choise);                
                return parameter;
            });
        }
        public string GetParameterChoise(ContactInDb contactFromDb)
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Data searched contact");
                Console.WriteLine($"\t1) Name : {contactFromDb.Name}");
                Console.WriteLine($"\t2) Phone1 : {contactFromDb.Phone1}");
                Console.WriteLine($"\t3) Phone2 : {contactFromDb.Phone2}");
                Console.WriteLine($"\t4) Phone3 : {contactFromDb.Phone3}");
                Console.WriteLine("");
                Console.WriteLine("Chose parameter you wanna edit or 0 to go back to the operations Menu");
                var choise = Console.ReadLine();
                if (choise == "0") return choise;
                validation.GetParameterChoise(choise);
                return choise;
            });
        }
        public string GetTypeOperationChoise(ContactInDb contactFromDb)
        {
            return validation.ExceptionsLoop(() =>
            {
                Console.WriteLine("Data searched contact");
                Console.WriteLine($"\t Name : {contactFromDb.Name}");
                Console.WriteLine($"\t Phone1 : {contactFromDb.Phone1}");
                Console.WriteLine($"\t Phone2 : {contactFromDb.Phone2}");
                Console.WriteLine($"\t Phone3 : {contactFromDb.Phone3}");
                Console.WriteLine("");
                Console.WriteLine("Please enter the operation?");
                Console.WriteLine("0) Back to the last menu");
                Console.WriteLine("1) Delete");
                Console.WriteLine("2) Edit");
                var choise = Console.ReadLine();
                validation.GetTypeOperationChoise(choise);
                Console.Clear();
                return choise;
            });
        }
        public ContactInDb AddContactGetData()
        {
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
        public List<ContactInDb> CheckDataType(string dataType)
        {
            List<ContactInDb> contactsFromDb = new List<ContactInDb>();
            if (dataType == "MSSQL")
            {
                return contactsFromDb = phonebookDbContext.Phonebook.ToList();
            }
            if (dataType == "FILE")
            {
                return contactsFromDb = phonebookFileContext.ReadAllContactsFromFile().ToList();
            }
            return null;
        }
        public void SaveData(List<ContactInDb> contacts, string dataType)
        {
            if (dataType == "MSSQL")
            {
                phonebookDbContext.SaveChanges();
            }
            if (dataType == "FILE")
            {
                phonebookFileContext.SaveChanges(contacts);
            }
        }
    }
}
