using CarddavToXML.Data.Entities;

namespace PhonebookConverter.Components.Export
{
    public interface IXmlWriter
    {
        void ExportToXmlYealinkLocal(string filepath);
        void ExportToXmlYealinkRemote(string filepath);
        void ExportToXmlFanvilRemoteAndLocal(string filepath);
        void SetPeriodicExport(string pathXml, string choiseType, int loopTime);
    }
}
