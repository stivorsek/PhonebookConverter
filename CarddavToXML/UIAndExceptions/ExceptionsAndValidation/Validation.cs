using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.UIAndExceptions.ExceptionsAndValidation
{
    public class Validation : IValidation
    {        
        public string ExportToXmlGetFolder(string pathXml)
        {
            return Directory.Exists(pathXml) || pathXml == "0"
                ? pathXml
                : throw new Exception("Podany folder nie istnieje");
        }
        public string ExportToXmlGetType(string choiseType)
        {
            return choiseType == "1" || choiseType == "2" || choiseType == "3" || choiseType == "4"
                ? choiseType
                : throw new Exception("Podano nieprawidłowy rodzaj pliku XML");
        }
        public bool ExportToXmlGetLoopState(string choiseLoop)
        {
            bool loopState = false;
            if (choiseLoop == "1")
            {
                loopState = true;
            }
            return choiseLoop == "1" || choiseLoop == "2"
                ? loopState
                : throw new Exception("Podano nieprawidłowy wybór!!!");
        }
        public int ExportToXmlGetLoopTime(string timer)
        {
            int loopTime;
            var userTime = int.TryParse(timer, out loopTime);
            return loopTime == 0 
                ? throw new Exception("Incorect time format!!!") 
                : loopTime;
        }
        public string ImportGetPathXml(string path)
        {
            if (path == "0")
            {
                return path;
            }
            return path.Contains(".xml")
               ? File.Exists(path)
                   ? path
                   : throw new Exception("That file doesn't exist or path is incorect!!!")
               : throw new Exception("This is not xml file!!!");
        }
        public string ImportGetPathCsv(string path)
        {
            if (path == "0")
            {
                Console.Clear();
                return path;
            }
            return path.Contains(".csv")
                ? File.Exists(path)
                    ? path
                    : throw new Exception("That file doesn't exist or path is incorect!!!")
                : throw new Exception("This is not csv file!!!");
        }
        public int? DataOperationsGetID(string idFromUser)
        {
            int? id = int.Parse(idFromUser);
            return id == 0 
                ? null 
                : id;
        }
        public object DataOperationsGetID(ContactInDb contactInDb)
        {
            return contactInDb == default 
                ? throw new Exception("Podane ID nie istnieje w bazie danych!!!") 
                : (object)contactInDb;
        }
        public string DataOperationsEditByIdChoseParameter(string choise)
        {
            return choise == "1" || choise == "2" || choise == "3" || choise == "4" 
                ? choise 
                : throw new Exception("Wrong choise!!!");
        }
        public string DataOperationsExportToTxt(string choise)
        {
            return choise == "1" || choise== "2" 
                ? choise
                : throw new Exception("Wrong choise!!!");
        }
        public string DataOperationsGetType(string choise)
        {
            return choise == "0" || choise == "1" || choise == "2" || choise == "3" || choise == "4"
                ? choise
                : throw new Exception("Wrong choise!!!");
        }
        public string DataOperationsEditByIDGetChoise(string choise)
        {
            return choise == "0" || choise == "1" || choise == "2" || choise == "3" || choise == "4"
                ? choise
                : throw new Exception("Wrong choise!!!");
        }
        public string DataOperationsExportToTxtDirectoryExist(string path)
        {
            return Directory.Exists(path) 
                ? path 
                : throw new Exception("File path doesn't exist!!!");
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
