namespace PhonebookConverter.Components.Export
{
    public interface IXmlWriter
    {
        void ExportToXmlYealinkLocal(string filepath);
        void ExportToXmlYealinkRemote(string filepath);
        void ExportToXmlFanvilRemoteAndLocal(string filepath);
    }
}
