using CarddavToXML.Data.Entities;

namespace PhonebookConverter.Components
{
    public interface IXmlWriter
    {
       void ExportToXmlYealinkLocal(string filepath, bool period, int loopTime);
        void ExportToXmlYealinkRemote(string filepath, bool period, int loopTime);       
       void ExportToXmlFanvilRemoteAndLocal(string filepath, bool period, int loopTime);

    }
}
