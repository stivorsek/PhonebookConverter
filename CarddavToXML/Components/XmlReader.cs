using CarddavToXML.Data.Entities;
using System.Xml.Linq;

namespace PhonebookConverter.Components
{
    public class XmlReader : IXmlReader
    {
        public List<ContactInDb> ImportFromXmlYealinkRemote(string path)
        {
            XElement document = XElement.Load(path);
            var attributes = document
                .Descendants("Entry")
                .Select(entry =>
                {
                    return new ContactInDb()
                    {
                        Name = entry.Attribute("Name").Value,
                        Phone1 = entry.Attribute("Phone1").Value,
                        Phone2 = entry.Attribute("Phone2").Value,
                        Phone3 = entry.Attribute("Phone3").Value,
                    };
                })
                .ToList();
            return attributes;
        }
        public List<ContactInDb> ImportFromXmlYealinkLocal(string path)
        {
            XElement document = XElement.Load(path);
            var attributes = document
                .Descendants("contact")
                .Select(entry =>
                {
                    return new ContactInDb()
                    {
                        Name = entry.Attribute("display_name").Value,
                        Phone1 = entry.Attribute("mobile_number").Value,
                        Phone2 = entry.Attribute("office_number").Value,
                        Phone3 = entry.Attribute("other_number").Value,
                    };
                })
                .ToList();
            return attributes;
        }
        public List<ContactInDb> ImportFromXmlFanvilRemoteAndLocal(string path)
        {
            XElement document = XElement.Load(path);
            var attributes = document
                .Descendants("DirectoryEntry")                
                .Select(entry =>
                {
                    return new ContactInDb()
                    {
                        Name = entry.Element("Name").Value,
                        Phone1 = entry.Element("Telephone").Value,
                        Phone2 = entry.Element("Mobile").Value,
                        Phone3 = entry.Element("Other").Value,
                    };
                })
                .ToList();
            return attributes;
        }
        public List<ContactInDb> XmlTypeChecker(string filePath)
        {
            string format = ".xml";
            if (!filePath.Contains(format))
            {
                throw new ArgumentException("To nie jest plik xml!!!");
            }
            XElement document = XElement.Load(filePath);
            
            switch (document.Name.LocalName)
            {
                case var name when name == "YealinkIPPhoneBook":
                    return ImportFromXmlYealinkRemote(filePath);
                case var name when name == "vp_contact":
                    return ImportFromXmlYealinkLocal(filePath);
                case var name when name == "PhoneBook":
                    return ImportFromXmlFanvilRemoteAndLocal(filePath);
                default:
                    throw new ArgumentException("Ten format pliku csv nie jest osługiwany");
            }
        }
    }
}
