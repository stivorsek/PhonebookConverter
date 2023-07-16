using PhonebookConverter.Data.Entities;

namespace PhonebookConverter.Components.Export
{
    public interface IExportLoopSettings
    {
        void CheckExportLoopSettingsExist();
        void SetPeriodicExport(ExportPeriodData exportPeriodData);
    }
}
