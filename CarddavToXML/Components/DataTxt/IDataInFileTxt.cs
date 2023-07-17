using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Components.DataTxt
{
    public interface IDataInFileTxt
    {
        List<ContactInDb> ReadDataFromCSV();
    }
}
