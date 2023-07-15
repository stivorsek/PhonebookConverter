﻿using CarddavToXML.Data;
using CarddavToXML.Data.Entities;
using PhonebookConverter.Data.Entities;
using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.UI
{
    public class DataFromUser : IDataFromUser
    {
        private readonly IExceptions _exceptions;
        private readonly PhonebookDbContext _phonebookDbContext;
        private readonly IValidation _validation;

        public DataFromUser(IExceptions exceptions, PhonebookDbContext phonebookDbContext, IValidation validation)
        {
            _exceptions = exceptions;
            _phonebookDbContext = phonebookDbContext;
            _validation = validation;
        }
        public string ExportToXmlGetFolder()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Wybierz folder do którego chcesz exportować lub 1 aby wrócić do główneg menu");
                var pathXml = _validation.ExportToXmlGetFolder(Console.ReadLine());
                Console.Clear();
                return pathXml;
            });
        }
        public string ExportToXmlGetType()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Wybierz rodzaj pliku XML do którego chcesz exportować pliki");
                Console.WriteLine("\t 0)Aby wrócić do poprzedniego menu");
                Console.WriteLine("\t 1)Yealink Local Phonnebook");
                Console.WriteLine("\t 2)Yealink Remote Phonebook");
                Console.WriteLine("\t 3)Fanvil Local and Remote Phonebook");
                string choiseType = _validation.ExportToXmlGetType(Console.ReadLine());
                if(choiseType == "1") choiseType = "Yealink_Local_Phonebook";
                if(choiseType == "2") choiseType = "Yealink_Remote_Phonebook";
                if(choiseType == "3") choiseType = "Fanvil_Local_and_Remote_Phonebook";
                Console.Clear();
                return choiseType;
                });
        }
        public bool ExportToXmlGetLoopState()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Czy chcesz wykonywać taki sam cykliczny export?");
                Console.WriteLine("1) Tak");
                Console.WriteLine("2) Nie");
                bool loopState = _validation.ExportToXmlGetLoopState(Console.ReadLine());
                Console.Clear();
                return loopState;
            });
        }
        public int ExportToXmlGetLoopTime()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.Clear();
                Console.WriteLine("Proszę podać co jaki interwał czasu ma byc wykonywany expert w sekundach");
                var loopTime = _validation.ExportToXmlGetLoopTime(Console.ReadLine());
                return loopTime * 1000;
            });
        }
        public string ImportGetPathCsv()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.Clear();
                Console.WriteLine("Podaj ścierzkę pliku lub wybierz 1 aby cofnąć do poprzedniego menu");
                string path = _validation.ImportGetPathCsv(Console.ReadLine());
                Console.Clear();
                return path;
            });
        }
        public string ImportGetPathXml()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Podaj ścierzkę pliku lub wybierz 1 aby cofnąć do poprzedniego menu");
                string path = _validation.ImportGetPathXml(Console.ReadLine());
                Console.Clear();
                return path;
            });
        }
        public int? DatabaseOperationsGetID()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("Podaj ID lub 0 aby wrócić do poprzedniego menu");
                int? id = _validation.DatabaseOperationsGetID(Console.ReadLine());
                var contactFromDb = _validation.DatabaseOperationsGetID(_phonebookDbContext.Phonebook.FirstOrDefault(c => c.Id == id));
                Console.Clear();
                return id;
            });
        }
        public string DatabaseOperationsGetType()
        {
            return _exceptions.ExceptionsLoop(() =>
            {
                Console.WriteLine("0) Aby cofnąć do poprzedniego menu");
                Console.WriteLine("1) Usunąć wpis po ID");
                Console.WriteLine("2) Edytować wpis po ID");
                Console.WriteLine("3) Dodać wpis ręcznie");
                Console.WriteLine("4) Wyświetlić wszystkie dane");
                var choise = _validation.DatabaseOperationsGetType(Console.ReadLine());                
                Console.Clear();
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
                var choise = _validation.DatabaseOperationsExportToTxt(Console.ReadLine());                
                Console.Clear();
                return choise;
            });
        }
        public string DatabaseOperationsEditByIDGetChoise(ContactInDb contactFromDb)
        {
            Console.WriteLine($"\t1) Name : {contactFromDb.Name}");
            Console.WriteLine($"\t2) Phone1 : {contactFromDb.Phone1}");
            Console.WriteLine($"\t3) Phone2 : {contactFromDb.Phone2}");
            Console.WriteLine($"\t4) Phone3 : {contactFromDb.Phone3}");
            Console.WriteLine("");
            Console.WriteLine("Który parametr chcesz zmienić lub wybierz 0 aby cofnąć?");
            var choise = Console.ReadLine();
            return choise;
        }
        public string DatabaseOperationsEditByIdGetParameter()
        {
            Console.WriteLine("Podaj na co chcesz zmienić parametr");
            var parameter = Console.ReadLine();
            return parameter;
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
        public int? IntParseValidation(string data)
        {
            int? result = string.IsNullOrEmpty(data) ? null : int.Parse(data);
            return result;
        }
        public string CheckExportSettingsExist()
        {
            Console.WriteLine("Czy chcesz przywrócić ten export?");
            Console.WriteLine("1) Tak");
            Console.WriteLine("2) Nie (Dane exportu zostaną usunięte!!!)");
            var choise = _validation.CheckExportSettingsExist(Console.ReadLine());
            Console.Clear();
            return choise;
        }

    }
}