using CarddavToXML.Data;
using System.Xml.Linq;

namespace PhonebookConverter.Components
{
    public class XmlWriter : IXmlWriter
    {
        private PhonebookDbContext _phonebookDbContext;

        public  XmlWriter ( PhonebookDbContext phonebookDbContext)
        {
            _phonebookDbContext = phonebookDbContext;
        }
        public void ExportToXmlFanvilRemoteAndLocal(string filePath, bool period)
        { 
            if (period = false )
            {
                filePath = filePath + "//PhonebookFanvilRemote.xml";
                var contactsFromDb = _phonebookDbContext.Phonebook.ToList();
                var document = new XDocument();
                var contacts = new XElement("PhoneBook", contactsFromDb
                        .Select(x =>
                             new XElement("DirectoryEntry",
                                 new XElement("Name", x.Name),
                                 new XElement("Telephone", x.Phone1),
                                 new XElement("Mobile", x.Phone2),
                                 new XElement("Othere", x.Phone3)
                                         )
                                ));
                document.Add(contacts);
                document.Save(filePath);
            }            
            else
            {
                string type = "ExportYealinkLocal";
                SetPeriodicExport(filePath, type);
            }
        }
        public void ExportToXmlYealinkLocal(string filePath, bool period)
        {
            if (period == false)
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
                        new XAttribute("mobile_number", x.Phone1),
                        new XAttribute("office_number", x.Phone2),
                        new XAttribute("other_number", x.Phone3)
                        )
                )));
                document.Add(contacts);
                document.Save(filePath);
            }
            else
            {
                string type = "ExportYealinkLocal";
                SetPeriodicExport(filePath, type);
            }
        }
        public void ExportToXmlYealinkRemote(string filePath, bool period)
        {
            if (period = false)
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
                        new XAttribute("Phone1", x.Phone1),
                        new XAttribute("Phone2", x.Phone2),
                        new XAttribute("Phone3", x.Phone3)
                        )
                )));
                document.Add(contacts);
                document.Save(filePath);
            }
            else
            {
                string type = "ExportYealinkRemote";
                SetPeriodicExport(filePath, type );
            }
        }
        private void SetPeriodicExport(string filePath,string type )
        {
            System.Timers.Timer timer = new System.Timers.Timer(30000);
            timer.Elapsed += async (sender, e) =>
            {
                var period = false;
                switch (type)
                {
                   case "ExportYealinkRemote":
                        ExportToXmlYealinkRemote(filePath, period);
                            break;
                    case "ExportYealinkLocal":
                        ExportToXmlYealinkLocal(filePath, period);
                        break;
                    case "ExportFanvilRemoteAndLocal":
                        ExportToXmlFanvilRemoteAndLocal(filePath, period);
                        break;
                    default:
                        ExportToXmlYealinkRemote(filePath, period);
                        break;
                }
                Console.WriteLine("====================");
                Console.WriteLine("Wykonano export pliku o godzinie: " + DateTime.Now);
                Console.WriteLine("====================");
            };
            timer.Start();
        }

    }
}
