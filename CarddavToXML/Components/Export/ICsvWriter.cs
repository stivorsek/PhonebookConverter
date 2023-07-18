namespace PhonebookConverter.Components.Export
{
    public interface ICsvWriter 
    {
        void YealinkLocal(string filePath, string dataType);
        void FanvilLocal(string filePath, string dataType);
        void YeastarPSeries(string filePath, string dataType);
    }
}
