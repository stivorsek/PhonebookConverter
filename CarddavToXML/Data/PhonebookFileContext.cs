using PhonebookConverter.UIAndValidation.Validation;
using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Data
{
    public class PhonebookFileContext
    {
        private readonly string filePath;
        private readonly IValidation validation;

        public PhonebookFileContext(IValidation validation) 
        { 
            filePath = "DataInCsv.csv"; 
            this.validation = validation;
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
                        Id = (int)validation.IntParseValidation(columns[0]),
                        Name = columns[1],
                        Phone1 = validation.IntParseValidation(columns[2]),
                        Phone2 = validation.IntParseValidation(columns[3]),
                        Phone3 = validation.IntParseValidation(columns[4])
                    };
                });
            return contactRecords.ToList();
        }
    }
}
