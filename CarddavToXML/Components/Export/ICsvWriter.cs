namespace PhonebookConverter.Components.Export
{
    public interface ICsvWriter
    {
        void ExportToCsvYealinkLocal(string filePath);
        void ExportToCsvYeastarPSeries(string filePath);
        void ExportToCsvFanvilLocal(string filePath);
    }
}
