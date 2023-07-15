using PhonebookConverter.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.Components.Export
{
    public interface IExportLoopSettings
    {
        void CheckExportLoopSettingsExist();
        void SetPeriodicExport(ExportPeriodData exportPeriodData);
    }
}
