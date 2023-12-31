﻿using PhonebookConverter.UIAndValidation.Validation;
using PhonebookConverter.Data.Entities;
using System.Xml.Linq;

namespace PhonebookConverter.Components.Import
{
    public class XmlReader : IXmlReader
    {
        private readonly IValidation _validation;

        public XmlReader(IValidation validation)
        {
            _validation = validation;
        }
        public List<ContactInDb> YealinkRemote(string path)
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
        public List<ContactInDb> YealinkLocal(string path)
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
        public List<ContactInDb> FanvilRemoteAndLocal(string path)
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
        public List<ContactInDb> TypeChecker(string filePath)
        {
            XElement document = XElement.Load(filePath);
            switch (document.Name.LocalName)
            {
                case var name when name == "YealinkIPPhoneBook":
                    return YealinkRemote(filePath);
                case var name when name == "vp_contact":
                    return YealinkLocal(filePath);
                case var name when name == "PhoneBook":
                    return FanvilRemoteAndLocal(filePath);
                default:
                    throw new Exception("Podany typ pliku xml nie jest obsługiwany");
            }
        } 
    }
}
