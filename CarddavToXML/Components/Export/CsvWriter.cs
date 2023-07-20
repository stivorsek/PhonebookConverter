using PhonebookConverter.Data;
using PhonebookConverterL.Data;
using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Components.Export
{
    public class CsvWriter : ICsvWriter
    {
        public void YeastarPSeries(string filePath, List<ContactInDb> contacts)
        {
            filePath = filePath + "//PhonebookYeastarPSeries.csv";
            using (var writer = File.AppendText(filePath))
            {
                writer.Write("First Name,Last Name,Company Name,Email,Business Number,Business Number 2,Business Fax,Mobile,Mobile 2,Home,Home 2,Home Fax,Other,ZIP Code,Street,City,State,Country,Remark,Phonebook");
                foreach (var contact in contacts)
                {
                    writer.Write($",,{contact.Name},,,{contact.Phone1},,,{contact.Phone2},,,{contact.Phone3},,,,,,,,,");
                }
            }
            ExportToCsvSuccesfull(filePath);
        }
        public void FanvilLocal(string filePath, List<ContactInDb> contacts)
        {
            filePath = filePath + "//PhonebookFanvilLocal.csv";
            using (var writer = File.AppendText(filePath))
            {
                writer.Write("\"name\",\"work\",\"mobile\",\"other\",\"ring\",\"groups\"");
                foreach (var contact in contacts)
                {
                    writer.Write($"\"{contact.Name}\",\"\",\"{contact.Phone1},\"\",\"{contact.Phone2}\",\"\",\"{contact.Phone3}\"");
                }
            }
            ExportToCsvSuccesfull(filePath);
        }
        public void YealinkLocal(string filePath, List<ContactInDb> contacts)
        {
            filePath = filePath + "//PhonebookYealinkLocal.csv";
            using (var writer = File.AppendText(filePath))
            {
                writer.Write("display_name,office_number,mobile_number,other_number,line,ring,auto_divert,priority,group_id_name");
                foreach (var contact in contacts)
                {
                    writer.Write($"{contact.Name},{contact.Phone1},{contact.Phone2},{contact.Phone3},,,,");
                }
            }
            ExportToCsvSuccesfull(filePath);
        }
        public void ExportToCsvSuccesfull(string filePath)
        {
            Console.Clear();
            Console.WriteLine($"Data was successful exported to Csv : {filePath}");
            Console.WriteLine("");
        }
    }
}
