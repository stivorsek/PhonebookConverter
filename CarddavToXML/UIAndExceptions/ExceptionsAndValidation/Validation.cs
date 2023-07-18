using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.UIAndExceptions.ExceptionsAndValidation
{
    public class Validation : IValidation
    {        
        public string ExportToXmlGetFolder(string pathXml)
        {
            if (!Directory.Exists(pathXml) && pathXml != "0") throw new Exception("Podany folder nie istnieje");
            return pathXml;

        }
        public string ExportToXmlGetType(string choiseType)
        {
            if (choiseType != "1" && choiseType != "2" && choiseType != "3" && choiseType != "4")
            {
                throw new Exception("Podano nieprawidłowy rodzaj pliku XML");
            }
            return choiseType;
        }
        public bool ExportToXmlGetLoopState(string choiseLoop)
        {
            if (choiseLoop != "1" && choiseLoop != "2") throw new Exception("Podano nieprawidłowy wybór!!!");
            bool loopState = false;
            if (choiseLoop == "1") loopState = true;
            return loopState;
        }
        public int ExportToXmlGetLoopTime(string timer)
        {
            int loopTime;
            var userTime = int.TryParse(timer, out loopTime);
            if (loopTime == 0) throw new Exception("Został podany nieprawidłowy czas pętli");
            return loopTime;
        }
        public string ImportGetPathXml(string path)
        {
            if (path == "0")
            {
                return path;
            }
            if (!path.Contains(".xml"))
            {
                throw new Exception("To nie jest plik xml");
            }
            if (!File.Exists(path))
            {
                throw new Exception("Ten plik nie istnieje lub ścieżka jest niepoprawna!!!");
            }
            return path;
        }
        public string ImportGetPathCsv(string path)
        {
            if (path == "0")
            {
                Console.Clear();
                return path;
            }
            if (!path.Contains(".csv"))
            {
                throw new Exception("To nie jest plik csv!!!");
            }
            if (!File.Exists(path))
            {
                throw new Exception("Ten plik nie istnieje lub ścieżka jest niepoprawna!!!");
            }
            return path;
        }
        public int? DataOperationsGetID(string idFromUser)
        {
            int? id = int.Parse(idFromUser);
            if (id == 0)
            {
                return null;
            }
            return id;

        }
        public object DataOperationsGetID(ContactInDb contactInDb)
        {
            return contactInDb == default ? throw new Exception("Podane ID nie istnieje w bazie danych!!!") : (object)contactInDb;
        }
        public string DataOperationsEditByIdChoseParameter(string choise)
        {
            if (choise != "1" && choise != "2" && choise != "3" && choise != "4")
            {
                throw new Exception("Nie ma takiego parametru!!!");
                
            }
            return choise;
        }
        public string DataOperationsExportToTxt(string choise)
        {
            if (choise != "1" && choise != "2")
            {
                throw new Exception("Podano nieprawidłowy wybór!!!");
            }
            return choise;
        }
        public string DataOperationsGetType(string choise)
        {
            if (choise != "0" && choise != "1" && choise != "2" && choise != "3" && choise != "4")
            {
                throw new Exception("Podano nieprawidłowy wybór!!!");
            }
            return choise;
        }
        public string DataOperationsEditByIDGetChoise(string choise)
        {
            if (choise != "0" && choise != "1" && choise != "2" && choise != "3" && choise != "4") 
            throw new Exception("Podano nieprawidłowy wybór!!!");
            return choise;
        }
        public string DataOperationsExportToTxtDirectoryExist(string path)
        {
            if (Directory.Exists(path)) return path;
            throw new Exception("Podana ścierzka pliku nie istnieje!!!");
        }
        public string CheckExportSettingsExist(string choise)
        {
            if (choise != "1" && choise != "2")
            {
                throw new Exception("Podano nieprawidłowy wybór!!!");
            }
            return choise;
        }
        public int? IntParseValidation(string data)
        {
            int? result = string.IsNullOrEmpty(data) ? null : int.Parse(data);
            return result;
        }
    }
}
