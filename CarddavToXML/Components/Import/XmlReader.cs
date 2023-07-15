using CarddavToXML.Data.Entities;
using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using System.Xml.Linq;
using static Grpc.Core.Metadata;

namespace PhonebookConverter.Components.Import
{
    public class XmlReader : IXmlReader
    {
        private readonly IValidation _validation;

        public XmlReader(IValidation validation)
        {
            _validation = validation;
        }
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
                        Phone1 = _validation.IntParseValidation(entry.Attribute("Phone1").Value),
                        Phone2 = _validation.IntParseValidation(entry.Attribute("Phone2").Value),
                        Phone3 = _validation.IntParseValidation(entry.Attribute("Phone3").Value)
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
                        Phone1 = _validation.IntParseValidation(entry.Attribute("mobile_number").Value),
                        Phone2 = _validation.IntParseValidation(entry.Attribute("office_number").Value),
                        Phone3 = _validation.IntParseValidation(entry.Attribute("other_number").Value),
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
                        Phone1 = _validation.IntParseValidation(entry.Element("Telephone").Value),
                        Phone2 = _validation.IntParseValidation(entry.Element("Mobile").Value),
                        Phone3 = _validation.IntParseValidation(entry.Element("Other").Value),
                    };
                })
                .ToList();
            return attributes;
        }
        public List<ContactInDb> XmlTypeChecker(string filePath)
        {
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
                    throw new Exception("Podany typ pliku xml nie jest obsługiwany");
            }
        } 
    }
}
