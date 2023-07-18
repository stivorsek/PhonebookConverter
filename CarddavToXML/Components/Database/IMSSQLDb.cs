using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Components.Database
{
    public interface IMSSQLDb
    {
        void AddNewEntry();
        void EditByID(int? id);
        void DeleteByID(int? id);
        void ShowAllContacts();
        void SaveDataFromDbToTxt();
    }
}
