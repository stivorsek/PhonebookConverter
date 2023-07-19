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
    }
}
