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
        string ImportGetPath();
        string DatabaseOperationsGetID();
        string DatabaseOperationsGetType();
        int ExportToXmlGetLoopTime();
        string FirstUIChoise();
        string DatabaseOperationsExportToTxt();
    }
}
