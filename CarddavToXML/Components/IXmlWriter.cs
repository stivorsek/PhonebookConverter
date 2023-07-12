using CarddavToXML.Data.Entities;

namespace PhonebookConverter.Components
{
    public interface IXmlWriter
    {
       void ExportToXmlYealinkLocal(string filepath, bool period);
        void ExportToXmlYealinkRemote(string filepath, bool period);       
       void ExportToXmlFanvilRemoteAndLocal(string filepath, bool period);

    }
}
