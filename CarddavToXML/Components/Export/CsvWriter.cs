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
                writer.Write("First Name,Last Name,Company Name,Email,Business Number,Business Number 2,Business Fax,Mobile,Mobile 2,Home,Home 2,Home Fax,Other,ZIP Code,Street,City,State,Country,Remark,Phonebook");
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($",,{contact.Name},,,{contact.Phone1},,,{contact.Phone2},,,{contact.Phone3},,,,,,,,,");
                }
            }
        }
        public void ExportToCsvFanvilLocal(string filePath)
        {
            filePath = filePath + "//PhonebookFanvilLocal.csv";
            var contactsFromDb = _phonebookDbContext.Phonebook.ToList();
            using (var writer = File.AppendText(filePath))            
            {
                writer.Write("\"name\",\"work\",\"mobile\",\"other\",\"ring\",\"groups\"\r\n");
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($"\"{contact.Name}\",,\"{contact.Phone1},,\"{contact.Phone2}\",,\"{contact.Phone3}\"");
                }
            }
        }
        public void ExportToCsvYealinkLocal(string filePath)
        {
            filePath = filePath + "//PhonebookYealinkLocal.csv";
            var contactsFromDb = _phonebookDbContext.Phonebook.ToList();
            using (var writer = File.AppendText(filePath))
            {
                writer.Write("display_name,office_number,mobile_number,other_number,line,ring,auto_divert,priority,group_id_name");
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($"{contact.Name},{contact.Phone1},{contact.Phone2},{contact.Phone3},,,,");                    
                }
            }
        }
    }
}
