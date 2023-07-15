using PhonebookConverter.Data.Entities;
using PhonebookConverter.UI;
namespace PhonebookConverter.Components.Export
{
    public class ExportLoopSettings : IExportLoopSettings
    {
        private readonly IDataFromUser _dataFromUser;
        private readonly IXmlWriter _xmlWriter;
        private readonly ICsvWriter _csvWriter;

        public ExportLoopSettings(IDataFromUser dataFromUser, IXmlWriter xmlWriter, ICsvWriter csvWriter)
        {
            _dataFromUser = dataFromUser;
            _xmlWriter = xmlWriter;
            _csvWriter = csvWriter;
        }
        public void CheckExportLoopSettingsExist()
        {
            var exportDataPath = "ExportData.txt";
            if (File.Exists(exportDataPath))
            {
                Console.WriteLine("Znaleziono dane z ustawionego exportu");
                string[] lines = File.ReadAllLines(exportDataPath);
                foreach (var line in lines)
                {
                    Console.WriteLine($"{line}");
                }
                var choise = _dataFromUser.CheckExportSettingsExist();
                Console.Clear();
                if (choise == "1")
                {
                    var exporData = new ExportPeriodData
                    {
                        Path = lines[2],
                        Interval = int.Parse(lines[4]),
                        Type = lines[6],
                        Format = lines[8]
                    };
                    SetPeriodicExportTypeCheck(exporData);
                }
                if (choise == "2") File.Delete(exportDataPath);
            }
        }
        public void SetPeriodicExport(ExportPeriodData exportPeriodData)
        {
            SetPeriodicExportTypeCheck(exportPeriodData);
            ShowTypeAndTimeOfExport(exportPeriodData);
            System.Timers.Timer timer = new System.Timers.Timer(30000);
            timer.Elapsed += (sender, e) =>
            {
                SetPeriodicExportTypeCheck(exportPeriodData);
                ShowTypeAndTimeOfExport(exportPeriodData);
            };
            timer.Start();
            if (!(File.Exists("ExportData.txt")))
            {
                using (var writer = File.AppendText("ExportData.txt"))
                {
                    writer.WriteLine($"Export został ustawiony: {DateTime.Now}");
                    writer.WriteLine("\tPath:");
                    writer.WriteLine($"{exportPeriodData.Path}");
                    writer.WriteLine("\tInterval w sekundach:");
                    writer.WriteLine($"{exportPeriodData.Interval / 1000}");
                    writer.WriteLine("\tType:");
                    writer.WriteLine($"{exportPeriodData.Type}");
                    writer.WriteLine("\tFormat:");
                    writer.WriteLine($"{exportPeriodData.Format}");
                    writer.WriteLine("");
                    writer.WriteLine("=========================");

                }
            }
        }
        public void SetPeriodicExportTypeCheck(ExportPeriodData exportPeriodData)
        {
            var tuple = (exportPeriodData.Type, exportPeriodData.Format);
            switch (tuple)
            {
                case ("Yealink_Local_Phonebook", "xml"):
                    _xmlWriter.ExportToXmlYealinkRemote(exportPeriodData.Path);
                    break;
                case ("Yealink_Remote_Phonebook", "xml"):
                    _xmlWriter.ExportToXmlYealinkLocal(exportPeriodData.Path);
                    break;
                case ("Fanvil_Local_and_Remote_Phonebook", "xml"):
                    _xmlWriter.ExportToXmlFanvilRemoteAndLocal(exportPeriodData.Path);
                    break;
                case ("Yealink_Local_Phonebook", "csv"):
                    _csvWriter.ExportToCsvYealinkLocal(exportPeriodData.Path);
                    break;
                case ("Fanvil_Local_Phonebook", "csv"):
                    _csvWriter.ExportToCsvFanvilLocal(exportPeriodData.Path);
                    break;
                case ("Yeastar_P_Series_Phonebook", "csv"):
                    _csvWriter.ExportToCsvYeastarPSeries(exportPeriodData.Path);
                    break;
                default:
                    break;
            }
        }
        public void ShowTypeAndTimeOfExport(ExportPeriodData exportPeriodData)
        {
            Console.WriteLine("====================");
            Console.WriteLine($"\tWykonano export pliku {exportPeriodData.Type} o godzinie: {DateTime.Now}");
            Console.WriteLine("====================");
        }
    }
}
