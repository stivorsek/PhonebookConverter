using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.UIAndValidationm
{
    public interface IDataFromUser
    {
        string GetExportFolder();
        string GetExportType();
        bool GetExportLoopState();
        int GetExportLoopTime();
        string GetImportPathCsv();
        string GetImportPathXml();
        string GetType();
        string ExportToTxt();
        string GetParameterChoise(ContactInDb contactFromDb);        
        string ShowMainMenu();
        string EditGetParameter(string choise);
        string CheckExportSettingsExist();
        ContactInDb AddContactGetData();
        List<ContactInDb> CheckDataType(string dataType);
        string GetDataType();        
        void SaveData(List<ContactInDb> contacts, string dataType);
        string SearchType();
        ContactInDb FindContact(string dataCenter, string searchType);
        string GetTypeOperationChoise(ContactInDb contactFromDb);
    }
}
