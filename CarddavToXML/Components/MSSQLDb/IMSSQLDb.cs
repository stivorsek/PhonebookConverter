using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Components.MSQSQLDb
{
    public interface IMSSQLDb
    {
        void AddNewEntry();
        void EditByID(int? id);
        void DeleteByID(int? id);
        void ShowAllContacts();
        void SaveDataFromDbToTxt();
        void EditByName(string name);
        void Delete(ContactInDb contact);
        void Edit(ContactInDb? contactFromDb);
        void FindAndManipulatContactIn(string dataStorage);
    }
}
