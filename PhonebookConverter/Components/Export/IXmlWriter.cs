using PhonebookConverter.Data.Entities;

namespace PhonebookConverter.Components.Export
{
    public interface IXmlWriter 
    {
        void YealinkLocal(string pathXml, List<ContactInDb> data);
        void YealinkRemote(string pathXml, List<ContactInDb> data);
        void FanvilRemoteAndLocal(string pathXml, List<ContactInDb> data);
    }
}
