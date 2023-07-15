using CarddavToXML.Data.Entities;
using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;

namespace PhonebookConverter.Components.Import
{
    public class CsvReader : ICsvReader
    {
        private readonly IValidation _validation;

        public CsvReader(IValidation validation) 
        {
            _validation = validation;
        }
        public List<ContactInDb> ImportFromCsvYealinkLocal(string filePath)
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
                        Name = columns[0] + " " + columns[1],
                        Phone1 = _validation.IntParseValidation(columns[3]),
                        Phone2 = _validation.IntParseValidation(columns[4]),
                        Phone3 = _validation.IntParseValidation(columns[5])
                    };
                });
            return contactRecords.ToList();
        }
        public List<ContactInDb> ImportFromCsvFanvilLocal(string filePath)
        {
            var contactRecords =
                File.ReadAllLines(filePath)
                .Where(x => x.Length > 1)
                .Skip(1)
                .Select(x =>
                {
                    var columns = x.Split('"');
                    return new ContactInDb()
                    {
                        Name = columns[1],
                        Phone1 = _validation.IntParseValidation(columns[3]),
                        Phone2 = _validation.IntParseValidation(columns[5]),
                        Phone3 = _validation.IntParseValidation(columns[7])
                    };
                });
            return contactRecords.ToList();
        }
        private List<ContactInDb> ImportFromCsvYeastarPSeries(string filePath)
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
                        Name = columns[0] + " " + columns[1],
                        Phone1 = _validation.IntParseValidation(columns[4]),
                        Phone2 = _validation.IntParseValidation(columns[7]),
                        Phone3 = _validation.IntParseValidation(columns[9])
                    };
                });
            return contactRecords.ToList();
        }
        public List<ContactInDb> CsvTypeChecker(string filePath)
        {
            string firstLine = File.ReadLines(filePath).FirstOrDefault();
            switch (firstLine)
            {
                case "Name,Surname,Company,PhoneNumber,MobileNumber,MainNumber":
                    return ImportFromCsvYealinkLocal(filePath);
                case "\"name\",\"work\",\"mobile\",\"other\",\"ring\",\"groups\"":
                    return ImportFromCsvFanvilLocal(filePath);
                case "First Name,Last Name,Company Name,Email,Business Number,Business Number 2,Business Fax,Mobile,Mobile 2,Home,Home 2,Home Fax,Other,ZIP Code,Street,City,State,Country,Remark,Phonebook":
                    return ImportFromCsvYeastarPSeries(filePath);
                default:
                    throw new ArgumentException("Ten format pliku csv nie jest osługiwany");
            }
        }
        
    }
}

