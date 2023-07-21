using PhonebookConverter.Data.Entities;

namespace PhonebookConverter.Components.MSQSQLDb
{
    public interface IMSSQLDb
    {
        void AddContact();
        void ShowAllContacts();
        void SaveDataFromDbToTxt();
        void Delete(ContactInDb contact);
        void Edit(ContactInDb? contactFromDb);
        void FindAndManipulatContact(string dataStorage);
    }
}
