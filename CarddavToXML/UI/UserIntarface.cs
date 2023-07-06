using CarddavToXML.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarddavToXML.UI
{
    public class UserIntarface : IChoise
    {
        private readonly ICsvReader _csvReader;

        public UserIntarface(ICsvReader csvReader)
        {
            _csvReader = csvReader;
        }
        public void FirstUIChoise()
        {
            Console.WriteLine("\tWitam w programie do konwertowania plików xml na inne");
            UISeparator();
            Console.WriteLine("\tProszę wybierz co będziemy dzisiaj robić");
            Console.WriteLine("1) Załaduj Dane do bazy danych z pliku");
            Console.WriteLine("2) Wykonaj konwersje CSV");
            var choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    UISeparator();
                    ChoseFileType();
                    break;
                case "2":
                    UISeparator();
                    Console.WriteLine("Wybrałeś 2");
                    break;
                default:
                    Console.WriteLine("Podałeś zły wybór");
                    break;
            }
        }
        private void ChoseFileType()
        {

            Console.WriteLine("Prosze wybrać typ pliku");
            UISeparator();
            Console.WriteLine("\t1)Yealink XML");
            Console.WriteLine("\t2)Yealink CSV");
            Console.WriteLine("\t3)Fanvil XML");
            Console.WriteLine("\t4)Fanvil CSV");
            var fileType = Console.ReadLine();
            switch(fileType)
            {
                case "1":

                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                default:
                    Console.WriteLine("Zly wybór");
                    break;
            }
        }
        public void UISeparator()
        {
            Console.WriteLine("");
            Console.WriteLine("/////////////////////");
            Console.WriteLine("");
        }
    }
}
