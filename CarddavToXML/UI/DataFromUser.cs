using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.UI
{
    public class DataFromUser : IDataFromUser
    {
        public string ExportToXmlGetFolder()
        {
            return ExceptionsLoop(() =>
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
            return ExceptionsLoop(() =>
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
            return ExceptionsLoop(() =>
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
            return ExceptionsLoop(() =>
            {
                Console.WriteLine("Proszę podać co jaki interwał czasu ma byc wykonywany expert w sekundach");
                int loopTime;
                var userTime = int.TryParse(Console.ReadLine(), out loopTime);
                return loopTime * 1000;
            });
        }
        public string ImportGetPath()
        {
            Console.Clear();
            return ExceptionsLoop(() =>
            {
                Console.WriteLine("Podaj ścierzkę pliku lub wybierz 1 aby cofnąć do poprzedniego menu");
                string path = Console.ReadLine();
                if (path == "1")
                {
                    Console.Clear();
                    return path;
                }
                if (!File.Exists(path))
                {
                    throw new ArgumentException("Ten plik nie istnieje lub ścieżka jest niepoprawna!!!");
                }
                if (path.Contains(".csv"))
                {
                    throw new ArgumentException("To nie jest plik csv!!!");
                }
                return path;
            });
        }
        public string DatabaseOperationsGetID()
        {
            Console.Clear();
            Console.WriteLine("Podaj ID");
            string id = Console.ReadLine();
            return id;
        }
        public string DatabaseOperationsGetType()
        {
            Console.Clear();
            return ExceptionsLoop(() =>
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
            return ExceptionsLoop(() =>
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
                    Console.Clear();
                    CatchError(ex);
                    continue;
                }
            }
        }
        public void CatchError(Exception ex)
        {

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
