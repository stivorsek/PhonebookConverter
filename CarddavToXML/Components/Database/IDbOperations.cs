using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Components.Database
{
    public interface IDbOperations
    {
        void AddNewDbEntry();
        void EditByID(int? id);
        void DeleteFromDbByID(int? id);
        void ReadAllContactsFromDb();
        void SaveDataFromDbToTxt();
    }
}
