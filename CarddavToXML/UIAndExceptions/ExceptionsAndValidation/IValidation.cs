using CarddavToXML.Data.Entities;

namespace PhonebookConverter.UIAndExceptions.ExceptionsAndValidation
{
    public interface IValidation
    {
        string ExportToXmlGetFolder(string pathXml);
        bool ExportToXmlGetLoopState(string choiseLoop);
        int ExportToXmlGetLoopTime(string userTime);
        string ExportToXmlGetType(string choiseType);
        string ImportGetPathXml(string path);
        string ImportGetPathCsv(string path);
        int? DatabaseOperationsGetID(string id);
        object DatabaseOperationsGetID(ContactInDb? contactInDb);
        string DatabaseOperationsExportToTxt(string choise);
        string DatabaseOperationsGetType(string choise);
    }
}
