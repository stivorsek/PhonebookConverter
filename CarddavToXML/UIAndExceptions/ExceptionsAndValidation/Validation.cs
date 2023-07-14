using CarddavToXML.Data.Entities;
using System;
using System.IO;

namespace PhonebookConverter.UIAndExceptions.ExceptionsAndValidation
{
    public class Validation : IValidation
    {
        private readonly IExceptions _exceptions;
        public Validation(IExceptions exceptions)
        {
            _exceptions = exceptions;
        }
        public string ExportToXmlGetFolder(string pathXml)
        {
                if (!Directory.Exists(pathXml) && pathXml != "0") throw new ArgumentException("Podany folder nie istnieje");
                return pathXml;
         
        }
        public string ExportToXmlGetType(string choiseType)
        {
                if (choiseType != "1" && choiseType != "2" && choiseType != "3" && choiseType != "4")
                {
                    throw new ArgumentException("Podano nieprawidłowy rodzaj pliku XML");
                }
                return choiseType;            
        }
        public bool ExportToXmlGetLoopState(string choiseLoop)
        {
                if (choiseLoop != "1" && choiseLoop != "2") throw new ArgumentException("Podano nieprawidłowy wybór!!!");
                bool loopState = false;
                if (choiseLoop == "1") loopState = true;
                return loopState;         
        }
        public int ExportToXmlGetLoopTime(string timer)
        {           
                int loopTime;
                var userTime = int.TryParse(timer, out loopTime);
                if (loopTime == 0) throw new Exception("Czas pętli nie może być równy 0");
                return loopTime;         
        }
        public string ImportGetPathXml(string path)
        {
                if (path == "1")
                {
                    return path;
                }
                if (!path.Contains(".xml"))
                {
                    throw new ArgumentException("To nie jest plik xml");
                }
                if (!File.Exists(path))
                {
                    throw new ArgumentException("Ten plik nie istnieje lub ścieżka jest niepoprawna!!!");
                }
                return path;         
        }
        public string ImportGetPathCsv(string path)
        {
                if (path == "1")
                {
                    Console.Clear();
                    return path;
                }
                if (!path.Contains(".csv"))
                {
                    throw new ArgumentException("To nie jest plik csv!!!");
                }
                if (!File.Exists(path))
                {
                    throw new ArgumentException("Ten plik nie istnieje lub ścieżka jest niepoprawna!!!");
                }
                return path;         
        }
        public int? DatabaseOperationsGetID(string idFromUser)
        {
                int? id = int.Parse(idFromUser);
                if (id == 0)
                {
                    return null;
                }
                return id;
         
        }
        public object DatabaseOperationsGetID(ContactInDb? contactInDb)
        {
            if (contactInDb == default)
            {
                throw new ArgumentException("Podane ID nie istnieje w bazie danych!!!");
            }
            return contactInDb;
        }
        public string DatabaseOperationsExportToTxt(string choise)
        {
            if (choise != "1" && choise != "2")
            {
                throw new ArgumentException("Podano nieprawidłowy wybór!!!");
            }
            return choise;
        }
        public string DatabaseOperationsGetType(string choise)
        {
            if (choise != "1" && choise != "2" && choise != "3" && choise != "4" && choise != "5")
            {
                throw new ArgumentException("Podano nieprawidłowy wybór!!!");
            }
            return choise;
        }
    }
}
