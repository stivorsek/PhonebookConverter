using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.UIAndValidationm
{
    public interface IDataFromUser
    {
        string ExportGetFolder();
        string ExportGetType();
        bool ExportGetLoopState();
        int ExportGetLoopTime();
        string ImportGetPathCsv();
        string ImportGetPathXml();
        string DataOperationsGetType();
        string DataOperationsExportToTxt();
        string DataOperationsEditGetChoise(ContactInDb contactFromDb);
        int? DataOperationsGetID(string dataCenter);
        string MainMenu();
        string DataOperationsEditGetParameter();
        string CheckExportSettingsExist();
        ContactInDb DataOperationsAddNewEntryGetData();
        List<ContactInDb> CheckDataType(string dataType);
        string GetDataType();
        string DataOperationsGetName(string dataCenter);
        void SaveDataToDatabase(List<ContactInDb> contacts, string dataType);
    }
}
