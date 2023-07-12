using CarddavToXML.Data.Entities;

namespace CarddavToXML.Components
{
    public interface ICsvReader
    {
        List<ContactInDb> CsvTypeChecker(string filepath);
    }
}
