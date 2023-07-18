using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.UIAndExceptions.ExceptionsAndValidation
{
    public interface IValidation
    {
        string ExportToXmlGetFolder(string pathml);
        bool ExportToXmlGetLoopState(string choiseLoop);
        int ExportToXmlGetLoopTime(string userTime);
        string ExportToXmlGetType(string choiseType);
        string ImportGetPathXml(string path);
        string ImportGetPathCsv(string path);
        int? DataOperationsGetID(string id);
        object DataOperationsGetID(ContactInDb? contactInDb);
        string DataOperationsEditByIdChoseParameter(string choise);
        string DataOperationsExportToTxt(string choise);
        string DataOperationsGetType(string choise);
        int? IntParseValidation(string data);
        string CheckExportSettingsExist(string choise);
        string DataOperationsExportToTxtDirectoryExist(string path);
        string DataOperationsEditByIDGetChoise(string choise);
    }
}
