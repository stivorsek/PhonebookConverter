using PhonebookConverterL.Data;

namespace PhonebookConverter.Components.Export
{
    public class CsvWriter : ICsvWriter
    {
        private readonly PhonebookDbContext _phonebookDbContext;

        public CsvWriter(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
        }
        public void ExportToCsvYeastarPSeries(string filePath)
        {
            filePath = filePath + "//PhonebookYeastarPSeries.csv";
            var contactsFromDb = _phonebookDbContext.Phonebook.ToList();
            using (var writer = File.AppendText(filePath))
            {
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($",,{contact.Name},,,{contact.Phone1},,,{contact.Phone2},,,{contact.Phone3}");
                }
            }
        }
        public void ExportToCsvFanvilLocal(string filePath)
        {
            filePath = filePath + "//PhonebookFanvilLocal.csv";
            var contactsFromDb = _phonebookDbContext.Phonebook.ToList();
            using (var writer = File.AppendText(filePath))            
            {
                writer.Write("");
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($",{contact.Name},,{contact.Phone1},,{contact.Phone2},,{contact.Phone3}");
                }
            }
        }
        public void ExportToCsvYealinkLocal(string filePath)
        {
            filePath = filePath + "//PhonebookYealinkLocal.csv";
            var contactsFromDb = _phonebookDbContext.Phonebook.ToList();
            using (var writer = File.AppendText(filePath))
            {
                writer.Write("");
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($"{contact.Name},,{contact.Phone1},{contact.Phone2},{contact.Phone3}");                    
                }
            }
        }
    }
}
