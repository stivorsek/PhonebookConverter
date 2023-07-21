using PhonebookConverter.UIAndValidation.Validation;
using PhonebookConverter.Data.Entities;

namespace PhonebookConverter.Components.Import
{
    public class CsvReader : ICsvReader
    {
        private readonly IValidation _validation;

        public CsvReader(IValidation validation) 
        {
            _validation = validation;
        }
        public List<ContactInDb> YealinkLocal(string filePath)
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
        public List<ContactInDb> FanvilLocal(string filePath)
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
                        Name = columns[0],
                        Phone1 = _validation.IntParseValidation(columns[1]),
                        Phone2 = _validation.IntParseValidation(columns[3]),
                        Phone3 = _validation.IntParseValidation(columns[6])
                    };
                });
            return contactRecords.ToList();
        }
        private List<ContactInDb> YeastarPSeries(string filePath)
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
        public List<ContactInDb> TypeChecker(string filePath)
        {
            string firstLine = File.ReadLines(filePath).FirstOrDefault();
            switch (firstLine)
            {
                case "Name,Surname,Company,PhoneNumber,MobileNumber,MainNumber":
                    return YealinkLocal(filePath);
                case "\"name\",\"work\",\"mobile\",\"other\",\"ring\",\"groups\"":
                    return FanvilLocal(filePath);
                case "First Name,Last Name,Company Name,Email,Business Number,Business Number 2,Business Fax,Mobile,Mobile 2,Home,Home 2,Home Fax,Other,ZIP Code,Street,City,State,Country,Remark,Phonebook":
                    return YeastarPSeries(filePath);
                default:
                    throw new ArgumentException("Ten format pliku csv nie jest osługiwany");
            }
        }
        
    }
}

