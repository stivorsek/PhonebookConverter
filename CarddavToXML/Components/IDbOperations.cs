using CarddavToXML.Data.Entities;

namespace PhonebookConverter.Components
{
    public interface IDbOperations
    {
        void AddNewDbEntry();
        void EditFromDbByID(string? id);
        void DeleteFromDbByID(string? id);
        void ReadAllContactsFromDb();
        void SaveDataFromDbToTxt();
    }
}
