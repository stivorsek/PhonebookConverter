using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using PhonebookConverterL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.Data
{
    public class FileContext
    {
        private readonly string filePath;
        private readonly IValidation _validation;

        public FileContext(IValidation validation) 
        { 
            filePath = "DataInCsv.csv"; 
            _validation = validation;
        }
        public List<ContactInDb> ReadAllContactsFromFile()
        {
            var contactRecords =
                File.ReadAllLines(filePath)
                .Where(x => x.Length > 1)
                .Skip(1)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new ContactInDb()
                    {
                        Id = (int)_validation.IntParseValidation(columns[0]),
                        Name = columns[1],
                        Phone1 = _validation.IntParseValidation(columns[2]),
                        Phone2 = _validation.IntParseValidation(columns[3]),
                        Phone3 = _validation.IntParseValidation(columns[4])
                    };
                });
            return contactRecords.ToList();
        }
    }
}
