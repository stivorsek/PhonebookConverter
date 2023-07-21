using PhonebookConverter.Data.Entities;

namespace PhonebookConverter.Components.DataTxt
{
    public interface IDataInFile
    {                
        void ShowAllContacts();
        void AddNewEntry();
        void SaveDataFromFileToTxt();       
        void FindAndManipulatContactIn(string dataStorage);
    }
}
