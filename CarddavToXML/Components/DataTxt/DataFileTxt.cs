using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using PhonebookConverterL.Data.Entities;
using System.Text;

namespace PhonebookConverter.Components.DataTxt
{
    public class DataFileTxt : IDataInFileTxt 
    {
        private readonly string filePath;
        private readonly IValidation _validation;        

        public DataFileTxt(IValidation validation)
        {
            filePath = "DataInCsv.csv";
            _validation = validation;
        }

        public void AddDataToCSV(ContactInDb contactInDb)
        {
            // Dodawanie danych do pliku CSV
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                // Zapisanie nowego wiersza danych
                string data = $"{contactInDb.Name},{contactInDb.Phone1},{contactInDb.Phone2},{contactInDb.Phone3}";
                sw.WriteLine(data);
            }

            Console.WriteLine("Dane zostały dodane do pliku CSV.");
        }
        public List<ContactInDb> ReadDataFromCSV()
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
                        Id = (int)_validation.IntParseValidation(columns[0]),
                        Name = columns[1],
                        Phone1 = _validation.IntParseValidation(columns[2]),
                        Phone2 = _validation.IntParseValidation(columns[3]),
                        Phone3 = _validation.IntParseValidation(columns[4])
                    };
                });
            return contactRecords.ToList();
        }
        public void UpdateDataInCSV(ContactInDb oldContactInDb, ContactInDb newContactInDb)
        {
            // Aktualizacja danych w pliku CSV
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            for (int i = 1; i < lines.Length; i++) // Rozpoczynamy od indeksu 1, pomijając nagłówek
            {
                string[] values = lines[i].Split(',');
                if (values[0] == oldContactInDb.Name 
                    && values[1] == oldContactInDb.Phone1.ToString()
                    && values[2] == oldContactInDb.Phone2.ToString() 
                    && values[3] == oldContactInDb.Phone3.ToString())
                {
                    // Aktualizacja danych dla pasującego wiersza
                    values[0] = newContactInDb.Name;
                    values[1] = newContactInDb.Phone1.ToString();
                    values[2] = newContactInDb.Phone2.ToString();
                    values[3] = newContactInDb.Phone3.ToString();
                    lines[i] = string.Join(",", values);
                    break;
                }
            }
            File.WriteAllLines(filePath, lines, Encoding.UTF8);

            Console.WriteLine("Dane zostały zaktualizowane w pliku CSV.");
        }
        public void DeleteDataInCSV(ContactInDb contactInDb)
        {
            // Usunięcie danych z pliku CSV
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            for (int i = 1; i < lines.Length; i++) // Rozpoczynamy od indeksu 1, pomijając nagłówek
            {
                string[] values = lines[i].Split(',');
                if (values[0] == contactInDb.Name 
                    && values[1] == contactInDb.Phone1.ToString() 
                    && values[1] == contactInDb.Phone1.ToString() 
                    && values[1] == contactInDb.Phone1.ToString())
                {
                    // Usunięcie pasującego wiersza
                    lines[i] = null;
                    break;
                }
            }
            File.WriteAllLines(filePath, lines, Encoding.UTF8);

            Console.WriteLine("Dane zostały usunięte z pliku CSV.");
        }
    }

}



