using CarddavToXML.Data.Entities;

namespace PhonebookConverter.Components
{
    public interface IXmlReader
    {
        List<ContactInDb> XmlTypeChecker(string filepath);
    }
}
