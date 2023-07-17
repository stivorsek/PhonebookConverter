using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.UIAndExceptions
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
        string FirstUIChoise();
        string DataOperationsEditByIdGetParameter();
        string CheckExportSettingsExist();
        ContactInDb DataOperationsAddNewEntryGetData();
        
    }
}
