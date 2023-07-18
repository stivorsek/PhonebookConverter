using PhonebookConverter.Data;
using PhonebookConverterL.Data;
using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Components.Export
{
    public class CsvWriter : ICsvWriter
    {
        private readonly PhonebookDbContext phonebookDbContext;
        private readonly FileContext fileContext;

        public CsvWriter(PhonebookDbContext phonebookDbContext, FileContext fileContext)
        {
            this.phonebookDbContext = phonebookDbContext;
            this.fileContext = fileContext;
        }
        public void YeastarPSeries(string filePath, string dataType)
        {
            filePath = filePath + "//PhonebookYeastarPSeries.csv";
            var contactsFromDb = CheckDataType(dataType);
            using (var writer = File.AppendText(filePath))
            {
                writer.Write("First Name,Last Name,Company Name,Email,Business Number,Business Number 2,Business Fax,Mobile,Mobile 2,Home,Home 2,Home Fax,Other,ZIP Code,Street,City,State,Country,Remark,Phonebook");
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($",,{contact.Name},,,{contact.Phone1},,,{contact.Phone2},,,{contact.Phone3},,,,,,,,,");
                }
            }
            ExportToCsvSuccesfull(filePath);
        }
        public void FanvilLocal(string filePath, string dataType)
        {
            filePath = filePath + "//PhonebookFanvilLocal.csv";
            var contactsFromDb = CheckDataType(dataType);
            using (var writer = File.AppendText(filePath))            
            {
                writer.Write("\"name\",\"work\",\"mobile\",\"other\",\"ring\",\"groups\"");
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($"\"{contact.Name}\",\"\",\"{contact.Phone1},\"\",\"{contact.Phone2}\",\"\",\"{contact.Phone3}\"");
                }
            }
            ExportToCsvSuccesfull(filePath);
        }
        public void YealinkLocal(string filePath, string dataType)
        {
            filePath = filePath + "//PhonebookYealinkLocal.csv";
            var contactsFromDb = CheckDataType(dataType);
            using (var writer = File.AppendText(filePath))
            {
                writer.Write("display_name,office_number,mobile_number,other_number,line,ring,auto_divert,priority,group_id_name");
                foreach (var contact in contactsFromDb)
                {
                    writer.Write($"{contact.Name},{contact.Phone1},{contact.Phone2},{contact.Phone3},,,,");                    
                }
            }
            ExportToCsvSuccesfull(filePath);
        }
        public List<ContactInDb> CheckDataType(string dataType)
        {
            List<ContactInDb> contactsFromDb = new List<ContactInDb>();
            if (dataType == "MSSQL")
            {
                return contactsFromDb = phonebookDbContext.Phonebook.ToList();
            }
            if (dataType == "FILE")
            {
                return contactsFromDb = fileContext.ReadAllContactsFromFile().ToList();
            }
            return null;
        }
        public void ExportToCsvSuccesfull(string filePath)
        {
            Console.Clear();
            Console.WriteLine($"Data was successful exported to Csv : {filePath}");
            Console.WriteLine("");
        }
    }
}
