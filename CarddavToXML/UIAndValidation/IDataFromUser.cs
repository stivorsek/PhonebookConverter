using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.UIAndValidationm
{
    public interface IDataFromUser
    {
        string GetFolder();
        string GetExportType();
        bool GetExportLoopState();
        int GetExportLoopTime();
        string GetImportPathCsv();
        string GetImportPathXml();
        string GetType();
        string ExportToTxt();
        string GetParameterChoise(ContactInDb contactFromDb);        
        string MainMenu();
        string EditGetParameter();
        string CheckExportSettingsExist();
        ContactInDb AddNewEntryGetData();
        List<ContactInDb> CheckDataType(string dataType);
        string GetDataType();        
        void SaveData(List<ContactInDb> contacts, string dataType);
        string SearchType();
        ContactInDb FindContact(string dataCenter, string searchType);
        string GetTypeOperationChoise(ContactInDb contactFromDb);
    }
}
