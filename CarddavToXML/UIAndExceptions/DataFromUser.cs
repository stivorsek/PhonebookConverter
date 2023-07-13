using CarddavToXML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.UI
{
    public class DataFromUser : IDataFromUser
    {
        private IExceptions _exceptions;
        private PhonebookDbContext _phonebookDbContext;

        public DataFromUser(IExceptions exceptions,PhonebookDbContext phonebookDbContext) 
        {
            _exceptions= exceptions;
            _phonebookDbContext= phonebookDbContext;
        }
        public string ExportToXmlGetFolder()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Wybierz folder do którego chcesz exportować lub 1 aby wrócić do główneg menu");
                var pathXml = Console.ReadLine();
                if (!Directory.Exists(pathXml) && pathXml != "1")
                {
                    throw new ArgumentException("Podany folder nie istnieje");
                }
                Console.Clear();
                return pathXml;
            });
        }
        public string ExportToXmlGetType()
        {
            Console.Clear();
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Wybierz rodzaj pliku XML do którego chcesz exportować pliki");
                Console.WriteLine("\t 1)Aby wrócić do poprzedniego menu");
                Console.WriteLine("\t 2)Yealink Local Phonnebook");
                Console.WriteLine("\t 3)Yealink Remote Phonebook");
                Console.WriteLine("\t 4)Fanvil Local and Remote Phonebook");
                string choiseType = Console.ReadLine();
                if (choiseType != "1" && choiseType != "2" && choiseType != "3" && choiseType != "4")
                {
                    throw new ArgumentException("Podano nieprawidłowy rodzaj pliku XML");
                }
                Console.Clear();
                return choiseType;
            });

        }
        public bool ExportToXmlGetLoopState()
        {
            Console.Clear();
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Czy chcesz wykonywać taki sam cykliczny export?");
                Console.WriteLine("1) Tak");
                Console.WriteLine("2) Nie");
                var choiseLoop = Console.ReadLine();
                if (choiseLoop != "1" && choiseLoop != "2")
                {
                    throw new ArgumentException("Podano nieprawidłowy wybór!!!");
                }
                bool loopState = false;
                if (choiseLoop == "1")
                {
                    loopState = true;
                }
                Console.Clear();
                return loopState;
            });
        }
        public int ExportToXmlGetLoopTime()
        {
            Console.Clear();
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Proszę podać co jaki interwał czasu ma byc wykonywany expert w sekundach");
                int loopTime;
                var userTime = int.TryParse(Console.ReadLine(), out loopTime);
                return loopTime * 1000;
            });
        }
        public string ImportGetPathCsv()
        {
            Console.Clear();
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Podaj ścierzkę pliku lub wybierz 1 aby cofnąć do poprzedniego menu");
                string path = Console.ReadLine();
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
            });
        }
        public string ImportGetPathXml()            
        {
            
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Podaj ścierzkę pliku lub wybierz 1 aby cofnąć do poprzedniego menu");
                string path = Console.ReadLine();
                if (path == "1")
                {
                    Console.Clear();
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
            });
        }
        public int? DatabaseOperationsGetID()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Podaj ID lub 0 aby wrócić do poprzedniego menu");               
                int? id = int.Parse(Console.ReadLine());
                if (id == 0)
                {
                    return null;
                }    
                var contactFromDb = _phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == id);
                if (contactFromDb == null)
                {
                    throw new ArgumentException("Podane ID nie istnieje w bazie danych!!!");
                }
                Console.Clear();
                return id;
            });
        }
        public string DatabaseOperationsGetType()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("1) Aby cofnąć do poprzedniego menu");
                Console.WriteLine("2) Usunąć wpis po ID");
                Console.WriteLine("3) Edytować wpis po ID");
                Console.WriteLine("4) Dodać wpis ręcznie");
                Console.WriteLine("5) Wyświetlić wszystkie dane");
                var choise = Console.ReadLine();
                if (choise != "1" && choise != "2" && choise != "3" && choise != "4" && choise != "5")
                {
                    throw new ArgumentException("Podano nieprawidłowy wybór!!!");
                }
                return choise;
            });
        }
        public string DatabaseOperationsExportToTxt()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Czy chcesz zapisać plik do pliku tekstowego?");
                Console.WriteLine("\t 1)Tak");
                Console.WriteLine("\t 2)Nie");
                var choise = Console.ReadLine();
                if (choise != "1" && choise != "2")
                {
                    throw new ArgumentException("Podano nieprawidłowy wybór!!!");
                }
                return choise;
            });
        }
        public string FirstUIChoise()
        {
            Console.WriteLine("\tWitam w programie do zarządzania plikami książek telefonicznych");
            Console.WriteLine("");
            Console.WriteLine("///////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("");
            Console.WriteLine("\tProszę wybierz co będziemy dzisiaj robić");
            Console.WriteLine("1) Załaduj Dane do bazy danych z pliku CSV");
            Console.WriteLine("2) Załaduj Dane do bazy danych z pliku XML");
            Console.WriteLine("3) Exportuj dane do XML");
            Console.WriteLine("4) Aby wybrać operacje na bazie danych");
            Console.WriteLine("5) Aby zakończyć program");
            var choise = Console.ReadLine();
            return choise;
        }
    }
}
