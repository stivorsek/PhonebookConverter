using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Components.Import
{
    public interface ICsvReader
    {
        List<ContactInDb> TypeChecker(string filePath);
    }
}
