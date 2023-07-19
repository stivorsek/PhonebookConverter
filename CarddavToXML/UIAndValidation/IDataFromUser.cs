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
        string DataOperationsEditByIDGetChoise(ContactInDb contactFromDb);
        int? DataOperationsGetID(string dataCenter);
        string MainMenu();
        string DataOperationsEditByIdGetParameter();
        string CheckExportSettingsExist();
        ContactInDb DataOperationsAddNewEntryGetData();
        List<ContactInDb> CheckDataType(string dataType);
    }
}
