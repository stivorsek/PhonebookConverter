using PhonebookConverter.Data.Entities;
using PhonebookConverter.UIAndExceptions;

namespace PhonebookConverter.Components.Export
{
    public class ExportLoopSettings : IExportLoopSettings
    {
        private readonly IDataFromUser dataFromUser;
        private readonly IXmlWriter xmlWriter;
        private readonly ICsvWriter _csvWriter;

        public ExportLoopSettings(IDataFromUser dataFromUser, IXmlWriter xmlWriter, ICsvWriter csvWriter)
        {
            this.dataFromUser = dataFromUser;
            this.xmlWriter = xmlWriter;
            _csvWriter = csvWriter;
        }
        public void CheckExportLoopSettingsExist()
        {
            var exportDataPath = "ExportData.txt";
            if (File.Exists(exportDataPath))
            {
                Console.WriteLine("Data form seted export found!!!");
                string[] lines = File.ReadAllLines(exportDataPath);
                foreach (var line in lines)
                {
                    Console.WriteLine($"{line}");
                }
                var choise = dataFromUser.CheckExportSettingsExist();
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
                    writer.WriteLine($"Export was seted : {DateTime.Now}");
                    writer.WriteLine("\tPath:");
                    writer.WriteLine($"{exportPeriodData.Path}");
                    writer.WriteLine("\tInterval in seconds:");
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
                    xmlWriter.YealinkRemote(exportPeriodData.Path, exportPeriodData.DataType);
                    break;
                case ("Yealink_Remote_Phonebook", "xml"):
                    xmlWriter.YealinkLocal(exportPeriodData.Path, exportPeriodData.DataType);
                    break;
                case ("Fanvil_Local_and_Remote_Phonebook", "xml"):
                    xmlWriter.FanvilRemoteAndLocal(exportPeriodData.Path, exportPeriodData.DataType);
                    break;
                case ("Yealink_Local_Phonebook", "csv"):
                    _csvWriter.YealinkLocal(exportPeriodData.Path, exportPeriodData.DataType);
                    break;
                case ("Fanvil_Local_Phonebook", "csv"):
                    _csvWriter.FanvilLocal(exportPeriodData.Path, exportPeriodData.DataType);
                    break;
                case ("Yeastar_P_Series_Phonebook", "csv"):
                    _csvWriter.YeastarPSeries(exportPeriodData.Path, exportPeriodData.DataType);
                    break;                
            }
        }
        public void ShowTypeAndTimeOfExport(ExportPeriodData exportPeriodData)
        {
            Console.WriteLine("====================");
            Console.WriteLine($"\tThe file {exportPeriodData.Type} has been expoted: {DateTime.Now}");
            Console.WriteLine("====================");
        }
    }
}
