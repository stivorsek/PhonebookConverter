using PhonebookConverterL.Data.Entities;

namespace PhonebookConverter.Components.Export
{
    public interface ICsvWriter 
    {
        void YeastarPSeries(string pathXml, List<ContactInDb> data);
        void FanvilLocal(string pathXml, List<ContactInDb> data);
        void YealinkLocal(string pathXml, List<ContactInDb> data);
    }
}
