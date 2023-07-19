using PhonebookConverter.Data;
using PhonebookConverterL.Data;
using PhonebookConverterL.Data.Entities;
using System.Xml.Linq;

namespace PhonebookConverter.Components.Export
{
    public class XmlWriter : IXmlWriter
    {        
        private readonly object fileLock;

        public void FanvilRemoteAndLocal(string filePath, List <ContactInDb> data)
        {
            filePath = filePath + "//PhonebookFanvilRemote.xml";            
            var document = new XDocument();
            var contacts = new XElement("PhoneBook", data
                    .Select(x =>
                         new XElement("DirectoryEntry",
                             new XElement("Name", x.Name),
                             new XElement("Telephone", x.Phone1.ToString()),
                             new XElement("Mobile", x.Phone2.ToString()),
                             new XElement("Othere", x.Phone3.ToString())
                                     )
                            ));
            document.Add(contacts);
            lock (fileLock)
            {
                document.Save(filePath);
            }
            ExportToCsvSuccesfull(filePath);
        }
        public void YealinkLocal(string filePath, List<ContactInDb> data)
        {
            filePath = filePath + "//PhonebookYealinkLocal.xml";            
            var document = new XDocument();
            var contacts = new XElement("vp_contact",
                new XElement("root_group", ""),
                new XElement("root_contact", data
            .Select(x =>
                new XElement("contact",
                    new XAttribute("display_name", x.Name),
                    new XAttribute("mobile_number", x.Phone1.ToString()),
                    new XAttribute("office_number", x.Phone2.ToString()),
                    new XAttribute("other_number", x.Phone3.ToString())
                    )
            )));
            document.Add(contacts);
            lock (fileLock)
            {
                document.Save(filePath);
            }
            ExportToCsvSuccesfull(filePath);
        }
        public void YealinkRemote(string filePath, List <ContactInDb> data)
        {
            filePath = filePath + "//PhonebookYealinkRemote.xml";            
            var document = new XDocument();
            var contacts = new XElement("YealinkIPPhoneBook",
                new XElement("Title", "Phonebook"),
                new XElement("Phonebook", data
            .Select(x =>
                new XElement("Entry",
                    new XAttribute("Name", x.Name),
                    new XAttribute("Phone1", x.Phone1.ToString()),
                    new XAttribute("Phone2", x.Phone2.ToString()),
                    new XAttribute("Phone3", x.Phone3.ToString())
                    )
            )));
            document.Add(contacts);
            lock (fileLock)
            {
                document.Save(filePath);
            }
            ExportToCsvSuccesfull(filePath);
        }
        public void ExportToCsvSuccesfull(string filePath)
        {
            Console.Clear();
            Console.WriteLine($"Data was successful exported to Xml : {filePath}");
            Console.WriteLine("");
        }
     
    }
}


