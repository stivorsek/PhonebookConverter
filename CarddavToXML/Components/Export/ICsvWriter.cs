namespace PhonebookConverter.Components.Export
{
    public interface ICsvWriter 
    {
        void YealinkLocal(string filePath);
        void YeastarPSeries(string filePath);
        void FanvilLocal(string filePath);
    }
}
