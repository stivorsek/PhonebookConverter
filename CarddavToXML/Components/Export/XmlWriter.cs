using CarddavToXML.Data;
using PhonebookConverter.Data.Entities;
using System.Xml.Linq;
using System.Timers;

namespace PhonebookConverter.Components.Export
{
    public class XmlWriter : IXmlWriter
    {
        private PhonebookDbContext _phonebookDbContext;

        public XmlWriter(PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
        }
        public void ExportToXmlFanvilRemoteAndLocal(string filePath)
        {
            filePath = filePath + "//PhonebookFanvilRemote.xml";
            var contactsFromDb = _phonebookDbContext.Phonebook.ToList();
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

        }
        public void ExportToXmlYealinkLocal(string filePath)
        {
            filePath = filePath + "//PhonebookYealinkLocal.xml";
            var contactsFromDb = _phonebookDbContext.Phonebook.ToList();
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

        }
        public void ExportToXmlYealinkRemote(string filePath)
        {
            filePath = filePath + "//PhonebookYealinkRemote.xml";
            var contactsFromDb = _phonebookDbContext.Phonebook.ToList();
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
        }
    }
}


