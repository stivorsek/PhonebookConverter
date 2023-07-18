using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Components.Import
{
    public interface BaseReader
    {
        List<ContactInDb> TypeChecker(string filepath);
    }
}
