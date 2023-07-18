namespace PhonebookConverter.Components.DataTxt
{
    public interface IDataInFile
    {        
        void EditByID(int? id);
        void ShowAllContacts();
        void AddNewEntry();
        void DeleteByID(int? id);
        void SaveDataFromFileToTxt();
    }
}
