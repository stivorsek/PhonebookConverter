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
        public List<YealinkCsv> ProcessYealinkCsv (string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<YealinkCsv>();
            }
            var contactRecord = 
                File.ReadAllLines(filePath)
                .Where(x => x.Length > 1)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new YealinkCsv()
                    {
                        Name = columns[0],
                        Surname = columns[1],
                        Company = columns[2],
                        PhoneNumber = int.Parse(columns[3])
                    };
                });
            return contactRecord.ToList();
        }
    }
}
