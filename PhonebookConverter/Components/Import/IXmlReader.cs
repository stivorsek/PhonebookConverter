using PhonebookConverter.Data.Entities;

namespace PhonebookConverter.Components.Import
{
    public interface IXmlReader
    {
        List<ContactInDb> TypeChecker(string filePath);
    }
}
