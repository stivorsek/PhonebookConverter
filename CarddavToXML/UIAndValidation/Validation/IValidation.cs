using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.UIAndValidation.Validation
{
    public interface IValidation
    {
        string GetExportFolder(string pathml);
        bool ExportToXmlGetLoopState(string choiseLoop);
        int ExportToXmlGetLoopTime(string userTime);
        string GetExportType(string choiseType);
        string ImportGetPathXml(string path);
        string ImportGetPathCsv(string path);
        int? DataOperationsGetID(string id);
        object DataOperationsGetID(ContactInDb? contactInDb);
        string GetTypeOperationChoise(string choise);
        string DataOperationsExportToTxt(string choise);
        string DataOperationsGetType(string choise);
        int? IntParseValidation(string data);
        string CheckExportSettingsExist(string choise);
        string DataOperationsExportToTxtDirectoryExist(string path);
        string DataOperationsEditGetChoise(string choise);
        void CatchError(Exception ex);
        T ExceptionsLoop<T>(Func<T> method);
        void ExceptionsLoop(Action metoda);
        string GetDataType(string dataType);
        object DataOperationsGetName(ContactInDb contactInDb);
        string DataOperationsGetSearchType(string? choise);
        ContactInDb DataOperationsFindConctat(ContactInDb contact);
        string EditGetParameter(string? parameter, string choise);
        string GetParameterChoise(string choise);
    }
}
