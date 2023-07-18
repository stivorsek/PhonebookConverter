using PhonebookConverterL.Data;

namespace PhonebookConverter.Components.Export
{
    public class CsvWriter : ICsvWriter
    {
        private readonly PhonebookDbContext phonebookDbContext;
        public CsvWriter(PhonebookDbContext phonebookDbContext)
        {
            this.phonebookDbContext = phonebookDbContext;
        }
        public void YeastarPSeries(string filePath)
        {
            filePath = filePath + "//PhonebookYeastarPSeries.csv";
            var contactsFromDb = phonebookDbContext.Phonebook.ToList();
            using (var writer = File.AppendText(filePath))
            {
                writer.Write("First Name,Last Name,Company Name,Email,Business Number,Business Number 2,Business Fax,Mobile,Mobile 2,Home,Home 2,Home Fax,Other,ZIP Code,Street,City,State,Country,Remark,Phonebook");
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($",,{contact.Name},,,{contact.Phone1},,,{contact.Phone2},,,{contact.Phone3},,,,,,,,,");
                }
            }
            ExportToCsvSuccesfull();
        }
        public void FanvilLocal(string filePath)
        {
            filePath = filePath + "//PhonebookFanvilLocal.csv";
            var contactsFromDb = phonebookDbContext.Phonebook.ToList();
            using (var writer = File.AppendText(filePath))            
            {
                writer.Write("\"name\",\"work\",\"mobile\",\"other\",\"ring\",\"groups\"");
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($"\"{contact.Name}\",\"\",\"{contact.Phone1},\"\",\"{contact.Phone2}\",\"\",\"{contact.Phone3}\"");
                }
            }
            ExportToCsvSuccesfull();
        }
        public void YealinkLocal(string filePath)
        {
            filePath = filePath + "//PhonebookYealinkLocal.csv";
            var contactsFromDb = phonebookDbContext.Phonebook.ToList();
            using (var writer = File.AppendText(filePath))
            {
                writer.Write("display_name,office_number,mobile_number,other_number,line,ring,auto_divert,priority,group_id_name");
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($"{contact.Name},{contact.Phone1},{contact.Phone2},{contact.Phone3},,,,");                    
                }
            }
            ExportToCsvSuccesfull();
        }
        public void ExportToCsvSuccesfull()
        {
            Console.Clear();
            Console.WriteLine("Dane zostały pomyślnie wyexportowane do pliku CSV");
            Console.WriteLine("");
        }
    }
}
