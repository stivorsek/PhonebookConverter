using CarddavToXML.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using PhonebookConverter.Data.Entities;
using System.Xml.Linq;
using System.IO;

namespace PhonebookConverter.Components
{
    public class XmlWriter : IXmlWriter
    {

        public void ExportToXmlFanvilLocal(string filepath, List<PhonebookInDb> contactsFromDb)
        {
            throw new NotImplementedException();
        }

        public void ExportToXmlFanvilRemote(string filepath, List<PhonebookInDb> contactsFromDb)
        {

            filepath = filepath + "//PhonebookFanvilRemote.xml";
            List<PhonebookToXml> phonebookWithoutID = new List<PhonebookToXml>();
            var document = new XDocument();
            var contacts = 
                new XElement("PhoneBook", contactsFromDb
            .Select(x =>
                new XElement("DirectoryEntry",
                    new XElement("Name", x.Name),
                    new XElement("Telephone", x.Phone1),
                    new XElement("Mobile", x.Phone2),
                    new XElement("Othere", x.Phone3)
                    )
            ));
            document.Add(contacts);
            document.Save(filepath);
        }

        public void ExportToXmlYealinkLocal(string filepath, List<PhonebookInDb> contactsFromDb)
        {
            filepath = filepath + "//PhonebookYealinkLocal.xml";
            List<PhonebookToXml> phonebookWithoutID = new List<PhonebookToXml>();
            var document = new XDocument();
            var contacts = new XElement("vp_contact",
                new XElement("root_group",""),
                new XElement("root_contact", contactsFromDb
            .Select(x =>
                new XElement("contact",
                    new XAttribute("display_name", x.Name),
                    new XAttribute("mobile_number", x.Phone1),
                    new XAttribute("office_number", x.Phone2),
                    new XAttribute("other_number", x.Phone3)
                    )
            )));
            document.Add(contacts);
            document.Save(filepath);
        }

        public void ExportToXmlYealinkRemote(string filepath, List<PhonebookInDb> contactsFromDb)
        {
            filepath = filepath + "//PhonebookYealinkRemote.xml";
            List<PhonebookToXml> phonebookWithoutID = new List<PhonebookToXml>();
            var document = new XDocument();
            var contacts = new XElement("YealinkIPPhoneBook",
                new XElement("Title", "Phonebook"),
                new XElement("Phonebook", contactsFromDb
            .Select(x =>
                new XElement("Entry",
                    new XAttribute("Name", x.Name),
                    new XAttribute("Phone1", x.Phone1),
                    new XAttribute("Phone2", x.Phone2),
                    new XAttribute("Phone3", x.Phone3)
                    )
            )));
            document.Add(contacts);
            document.Save(filepath);
        }
    }
}
