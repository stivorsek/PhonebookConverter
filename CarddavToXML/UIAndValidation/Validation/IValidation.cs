using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.UIAndValidation.Validation
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
        string DataOperationsEditChoseParameter(string choise);
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
    }
}
