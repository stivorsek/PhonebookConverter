using CarddavToXML.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.UI
{
    public interface IDataFromUser
    {
        string ExportToXmlGetFolder();
        string ExportToXmlGetType();
        bool ExportToXmlGetLoopState();
        int ExportToXmlGetLoopTime();
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
