using CarddavToXML.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarddavToXML.Components
{
    public class CsvReader : ICsvReader
    {
        public List<PhonebookInDb> ImportFromCsvYealinkLocal(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<PhonebookInDb>();
            }
            var contactRecords =
                File.ReadAllLines(filePath)
                .Where(x => x.Length > 1)
                .Skip(1)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new PhonebookInDb()
                    {
                        Name = columns[0] + " " + columns[1],
                        Phone1 = columns[3],
                        Phone2 = columns[4],
                        Phone3 = columns[5]
                    };
                });
            return contactRecords.ToList();
        }
        public List<PhonebookInDb> ImportFromCsvCustom(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<PhonebookInDb>();
            }
            var contactRecords =
                File.ReadAllLines(filePath)
                .Where(x => x.Length > 1)
                .Skip(1)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new PhonebookInDb()
                    {
                        Name = columns[0],
                        Phone1 = columns[1],
                        Phone2 = columns[2],
                        Phone3 = columns[3]
                    };
                });
            return contactRecords.ToList();
        }
        public List<PhonebookInDb> CsvTypeChecker(string filePath)
        {
             
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Ten plik nie istnieje lub ścieżka jest niepoprawna!!!");
            }
            if (!filePath.Contains(".csv`"))
            {
                throw new ArgumentException("To nie jest plik csv!!!");
            }
            string firstLine = File.ReadLines(filePath).FirstOrDefault();
            switch (firstLine)
            {
                case "Name,Surname,Company,PhoneNumber,MobileNumber,MainNumber":
                    return ImportFromCsvYealinkLocal(filePath);
                case "Name,Phone1,Phone2,Phone3":
                    return ImportFromCsvCustom(filePath);
                default:
                    throw new ArgumentException("Ten format pliku csv nie jest osługiwany");
            }
        }
    }
}

