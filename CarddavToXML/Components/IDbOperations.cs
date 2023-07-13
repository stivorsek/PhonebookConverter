using CarddavToXML.Data.Entities;

namespace PhonebookConverter.Components
{
    public interface IDbOperations
    {
        void AddNewDbEntry();
        void EditFromDbByID(int? id);
        void DeleteFromDbByID(int? id);
        void ReadAllContactsFromDb();
        void SaveDataFromDbToTxt();
    }
}
