using PhonebookConverter.Data.Entities;

namespace PhonebookConverter.UIAndValidation.Validation
{
    public interface IValidation
    {
        string GetExportFolder(string pathml);
        bool GetExportLoopState(string choiseLoop);
        int GetExportLoopTime(string userTime);
        string GetExportType(string choiseType);
        string GetImportPathXml(string path);
        string GetImportPathCsv(string path);
        int? GetID(string id);
        object GetID(ContactInDb? contactInDb);
        string GetTypeOperationChoise(string choise);
        string ExportToTxt(string choise);
        string GetType(string choise);
        int? IntParseValidation(string data);
        string CheckExportSettingsExist(string choise);
        string ExportToTxtDirectoryExist(string path);
        string EditGetChoise(string choise);
        void CatchError(Exception ex);
        T ExceptionsLoop<T>(Func<T> method);
        void ExceptionsLoop(Action metoda);
        string GetDataType(string dataType);
        object DataOperationsGetName(ContactInDb contactInDb);
        string DataOperationsGetSearchType(string? choise);
        ContactInDb DataOperationsFindConctat(ContactInDb contact);
        string EditGetParameter(string? parameter, string choise);
        string GetParameterChoise(string choise);
        void CheckFileDbExist();
    }
}
