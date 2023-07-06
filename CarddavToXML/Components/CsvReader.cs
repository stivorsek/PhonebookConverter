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
        public List<PhonebookInDb> ProcessYealinkCsv (string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<PhonebookInDb>();
            }
            if (filePath.Contains(".csv`"))
            {
                throw new ArgumentException("To nie jest plik csv");
            }
            var contactRecord = 
                File.ReadAllLines(filePath)
                .Where(x => x.Length > 1)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new PhonebookInDb()
                    {
                        Name = columns[0]+" " + columns[1],
                        Phone1 = columns[3],
                        Phone2 = columns[4],
                        Phone3 = columns[5]
                    };
                });
            return contactRecord.ToList();
        }
    }
}
