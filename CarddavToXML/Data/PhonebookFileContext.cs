using PhonebookConverter.UIAndValidation.Validation;
using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Data
{
    public class PhonebookFileContext
    {
        private readonly string filePath;
        private readonly IValidation validation;
        private object fileLock = new object();

        public PhonebookFileContext(IValidation validation)
        {
            filePath = "DataInCsv.csv";
            this.validation = validation;
        }
        public List<ContactInDb> ReadAllContactsFromFile()
        {
            lock (fileLock)
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
        public void SaveChanges(List<ContactInDb> contactsInDb)
        {
            lock (fileLock)
            {
                using (var writer = File.AppendText(filePath))
                {
                    foreach (var contact in contactsInDb)
                    {
                        string data = $"{contact.Id},{contact.Name},{contact.Phone1},{contact.Phone2},{contact.Phone3}";
                        writer.WriteLine(data);
                    }
                }
            }
        }
    }
}
