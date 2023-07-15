using CarddavToXML.Data.Entities;
using PhonebookConverter.Data.Entities;

namespace PhonebookConverter.Components.Export
{
    public interface IXmlWriter
    {
        void ExportToXmlYealinkLocal(string filepath);
        void ExportToXmlYealinkRemote(string filepath);
        void ExportToXmlFanvilRemoteAndLocal(string filepath);
        void SetPeriodicExport(ExportPeriodData exportPeriodData);        
    }
}
