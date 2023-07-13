using CarddavToXML.Data.Entities;

namespace PhonebookConverter.Components.Import
{
    public interface IXmlReader
    {
        List<ContactInDb> XmlTypeChecker(string filepath);
    }
}
