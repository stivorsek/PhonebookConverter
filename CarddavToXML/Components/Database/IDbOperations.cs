using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Components.Database
{
    public interface IDbOperations
    {
        void AddNewEntry();
        void EditByID(int? id);
        void DeleteByID(int? id);
        void ShowAllContacts();
        void SaveDataFromDbToTxt();
    }
}
