using PhonebookConverter.Data;
using PhonebookConverterL.Data;
using PhonebookConverterL.Data.Entities;
using System.Xml.Linq;

namespace PhonebookConverter.Components.Export
{
    public class XmlWriter : IXmlWriter
    {
        private PhonebookDbContext phonebookDbContext;
        private readonly FileContext fileContext;

        public XmlWriter(PhonebookDbContext phonebookDbContext, FileContext fileContext)
        {
            this.phonebookDbContext = phonebookDbContext;
            this.fileContext = fileContext;
        }
        public void FanvilRemoteAndLocal(string filePath, string dataType)
        {
            filePath = filePath + "//PhonebookFanvilRemote.xml";
            var contactsFromDb = CheckDataType(dataType);
            var document = new XDocument();
            var contacts = new XElement("PhoneBook", contactsFromDb
                    .Select(x =>
                         new XElement("DirectoryEntry",
                             new XElement("Name", x.Name),
                             new XElement("Telephone", x.Phone1.ToString()),
                             new XElement("Mobile", x.Phone2.ToString()),
                             new XElement("Othere", x.Phone3.ToString())
                                     )
                            ));
            document.Add(contacts);
            document.Save(filePath);
            ExportToCsvSuccesfull(filePath);
        }
        public void YealinkLocal(string filePath, string dataType)
        {
            filePath = filePath + "//PhonebookYealinkLocal.xml";
            var contactsFromDb = CheckDataType(dataType);
            var document = new XDocument();
            var contacts = new XElement("vp_contact",
                new XElement("root_group", ""),
                new XElement("root_contact", contactsFromDb
            .Select(x =>
                new XElement("contact",
                    new XAttribute("display_name", x.Name),
                    new XAttribute("mobile_number", x.Phone1.ToString()),
                    new XAttribute("office_number", x.Phone2.ToString()),
                    new XAttribute("other_number", x.Phone3.ToString())
                    )
            )));
            document.Add(contacts);
            document.Save(filePath);
            ExportToCsvSuccesfull(filePath);
        }
        public void YealinkRemote(string filePath, string dataType)
        {
            filePath = filePath + "//PhonebookYealinkRemote.xml";
            var contactsFromDb = CheckDataType(dataType);
            var document = new XDocument();
            var contacts = new XElement("YealinkIPPhoneBook",
                new XElement("Title", "Phonebook"),
                new XElement("Phonebook", contactsFromDb
            .Select(x =>
                new XElement("Entry",
                    new XAttribute("Name", x.Name),
                    new XAttribute("Phone1", x.Phone1.ToString()),
                    new XAttribute("Phone2", x.Phone2.ToString()),
                    new XAttribute("Phone3", x.Phone3.ToString())
                    )
            )));
            document.Add(contacts);
            document.Save(filePath);
            ExportToCsvSuccesfull(filePath);
        }
        public void ExportToCsvSuccesfull(string filePath)
        {
            Console.Clear();
            Console.WriteLine($"Data was successful exported to Xml : {filePath}");
            Console.WriteLine("");
        }
        public List<ContactInDb> CheckDataType(string dataType)
        {
            List<ContactInDb> contactsFromDb = new List<ContactInDb>();
            if (dataType == "MSSQL")
            {
                return contactsFromDb = phonebookDbContext.Phonebook.ToList();
            }
            if (dataType == "FILE")
            {
                return contactsFromDb = fileContext.ReadAllContactsFromFile().ToList();
            }
            return null;
        }
    }
}


