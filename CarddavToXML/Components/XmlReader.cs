using CarddavToXML.Data.Entities;
using PhonebookConverter.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PhonebookConverter.Components
{
    public class XmlReader : IXmlReader
    {
        public List<PhonebookInDb> ImportFromXmlYealinkRemote(string path)
        {
            XElement document = XElement.Load(path);
            var attributes = document
                .Descendants("Entry")
                .Select(entry =>
                {
                    return new PhonebookInDb()
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
        public List<PhonebookInDb> ImportFromXmlYealinkLocal(string path)
        {
            XElement document = XElement.Load(path);
            var attributes = document
                .Descendants("contact")
                .Select(entry =>
                {
                    return new PhonebookInDb()
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
        public List<PhonebookInDb> ImportFromXmlCustom(string path)
        {
            XElement document = XElement.Load(path);
            var attributes = document
                .Descendants("PhonebookToXml")
                .Select(entry =>
                {
                    return new PhonebookInDb()
                    {
                        Name = entry.Element("Name").Value,
                        Phone1 = entry.Element("Phone1").Value,
                        Phone2 = entry.Element("Phone2").Value,
                        Phone3 = entry.Element("Phone3").Value,
                    };
                })
                .ToList();
            return attributes;
        }
        public List<PhonebookInDb> XmlTypeChecker(string filePath)
        {
            string format = ".xml";
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Ten plik nie istnieje lub ścieżka jest niepoprawna!!!");
            }
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
                case var name when name == "Phonebook":
                    return ImportFromXmlCustom(filePath);
                default:
                    throw new ArgumentException("Ten format pliku csv nie jest osługiwany");
            }
        }
    }
}
