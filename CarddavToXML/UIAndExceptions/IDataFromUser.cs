using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.UI
{
    public interface IDataFromUser
    {
        string ExportGetFolder();
        string ExportGetType();
        bool ExportGetLoopState();
        int ExportGetLoopTime();
        string ImportGetPathCsv();
        string ImportGetPathXml();
        int? DatabaseOperationsGetID();
        string DatabaseOperationsGetType();
        string DatabaseOperationsExportToTxt();
        string DatabaseOperationsEditByIDGetChoise(ContactInDb contactFromDb);
        string FirstUIChoise();
        string DatabaseOperationsEditByIdGetParameter();
        string CheckExportSettingsExist();
    }
}
