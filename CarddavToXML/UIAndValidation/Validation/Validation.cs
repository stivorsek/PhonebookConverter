using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.UIAndValidation.Validation
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
                ? throw new Exception("That ID doesn't exist in database")
                : (object)contactInDb;
        }
        public object DataOperationsGetName(ContactInDb contactInDb)
        {
            return contactInDb == default
                ? throw new Exception("That Name doesn't exist in database")
                : (object)contactInDb;
        }
        public string DataOperationsEditChoseParameter(string choise)
        {
            return choise == "1" || choise == "2" || choise == "3" || choise == "4"
                ? choise
                : throw new Exception("Wrong choise!!!");
        }
        public string DataOperationsExportToTxt(string choise)
        {
            return choise == "1" || choise == "2"
                ? choise
                : throw new Exception("Wrong choise!!!");
        }
        public string DataOperationsGetType(string choise)
        {
            return choise == "0" || choise == "1" || choise == "2" || choise == "3" || choise == "4" || choise =="5"
                ? choise
                : throw new Exception("Wrong choise!!!");
        }
        public string DataOperationsEditGetChoise(string choise)
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
        public void ExceptionsLoop(Action metoda)
        {
            while (true)
            {
                try
                {
                    metoda.Invoke();
                    break;
                }
                catch (Exception ex)
                {
                    CatchError(ex);
                    continue;
                }
            }
        }
        public string GetDataType(string dataType)
        {
            if (dataType == "1") dataType = "FILE";
            if (dataType == "2") dataType = "MSSQL";
            return dataType == "FILE" || dataType == "MSSQL" || dataType == "0"
                ? dataType
                : throw new Exception("Wrong choise!!!");
        }
        public T ExceptionsLoop<T>(Func<T> method)
        {
            while (true)
            {
                try
                {
                    T result = method.Invoke();
                    return result;
                }
                catch (Exception ex)
                {
                    CatchError(ex);
                    continue;
                }
            }
        }
        public void CatchError(Exception ex)
        {
            Console.Clear();
            string separator = "///////////////////////////////////////////////////////////////////////////////";
            string exceptionHeader = "Wystąpił wyjątek: " + DateTime.Now;
            Console.WriteLine(separator);
            Console.WriteLine(exceptionHeader);
            Console.WriteLine($"\t{ex.Message}");
            Console.WriteLine(separator);
            using (var writer = File.AppendText("ErrorLog.txt"))
            {
                writer.Write(separator);
                writer.Write(exceptionHeader);
                writer.Write(ex.Message);
            }
        }
    }
}
