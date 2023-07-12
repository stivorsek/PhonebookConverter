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
            Console.Clear();
            Console.WriteLine("Wybierz folder do którego chcesz exportować");
            var pathXml = Console.ReadLine();
            if (!Directory.Exists(pathXml))
            {
                Console.Clear();
                throw new ArgumentException("Podany folder nie istnieje");
            }
            return pathXml;

        }
        public string ExportToXmlGetType()
        {
            Console.Clear();
            Console.WriteLine("Wybierz rodzaj pliku XML do którego chcesz exportować pliki");
            Console.WriteLine("\t 1)Yealink Local Phonnebook");
            Console.WriteLine("\t 2)Yealink Remote Phonebook");
            Console.WriteLine("\t 3)Fanvil Local and Remote Phonebook");
            string choiseType = Console.ReadLine();
            if (choiseType != "1" && choiseType != "2" && choiseType != "3")
            {
                Console.Clear();
                throw new ArgumentException("Podano nieprawidłowy rodzaj pliku XML");
            }
            return choiseType;

        }
        public bool ExportToXmlGetLoopState()
        {
            Console.Clear();
            Console.WriteLine("Czy chcesz wykonywać taki sam cykliczny export?");
            Console.WriteLine("1) Tak");
            Console.WriteLine("2) Nie");
            var choiseLoop = Console.ReadLine();
            if (choiseLoop != "1" && choiseLoop != "2")
            {
                Console.Clear();
                throw new ArgumentException("Podano nieprawidłowy wybór!!!");
            }
            bool loop = false;
            if (choiseLoop == "1")
            {
                loop = true;
            }
            return loop;
        }
        public string ImportGetPath()
        {
            Console.WriteLine("Podaj ścierzkę pliku lub wybierz 1 aby cofnąć do poprzedniego menu");
            string path = Console.ReadLine();
            return path;
        }
        public string DatabaseOperationsGetID()
        {
            Console.Clear();
            Console.WriteLine("Podaj ID");
            string id = Console.ReadLine();
            return id;
        }
        public string GetDatabaseOperation()
        {
            Console.Clear();
            Console.WriteLine("1) Usunąć wpis po ID");
            Console.WriteLine("2) Edytować wpis po ID");
            Console.WriteLine("3) Dodać wpis ręcznie");
            var choise = Console.ReadLine();
            if (choise != "1" && choise != "2" && choise != "3")
            {
                throw new ArgumentException("Podano nieprawidłowy wybór!!!");
            }
            return choise;
        }
    }
}
