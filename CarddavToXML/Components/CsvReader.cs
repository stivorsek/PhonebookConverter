using CarddavToXML.Data.Entities;

namespace CarddavToXML.Components
{
    public class CsvReader : ICsvReader
    {
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
                        Phone1 = columns[3],
                        Phone2 = columns[4],
                        Phone3 = columns[5]
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
                        Phone1 = columns[3],
                        Phone2 = columns[5],
                        Phone3 = columns[7]
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
                        Name = columns[0] +" "+ columns[1],
                        Phone1 = columns[4],
                        Phone2 = columns[7],
                        Phone3 = columns[9]
                    };
                });
            return contactRecords.ToList();
        }
        public List<ContactInDb> CsvTypeChecker(string filePath)
        {

            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Ten plik nie istnieje lub ścieżka jest niepoprawna!!!");
            }
            if (!filePath.Contains(".csv"))
            {
                throw new ArgumentException("To nie jest plik csv!!!");
            }
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

