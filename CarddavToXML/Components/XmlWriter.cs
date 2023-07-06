using CarddavToXML.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using PhonebookConverter.Data.Entities;

namespace PhonebookConverter.Components
{
    [XmlRoot("Phonebook")]
    public class XmlWriter : IXmlWriter
    {
        public void ExportToXml(string path, List<PhonebookInDb> contactsFromDb) 
        {
            List<PhonebookToXml> phonebookWithoutID = new List<PhonebookToXml>();
           
            foreach (var contact in contactsFromDb) 
            {
                phonebookWithoutID.Add(new PhonebookToXml()
                {
                    Name = contact.Name,
                    Phone1 = contact.Phone1,
                    Phone2 = contact.Phone2,
                    Phone3 = contact.Phone3,
                });
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<PhonebookToXml>), new XmlRootAttribute("Phonebook"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            using (StreamWriter streamWriter = new StreamWriter (path))
            {
                serializer.Serialize(streamWriter, phonebookWithoutID, namespaces);
            }

            //foreach (var contact in contactsFromDb) 
            //{
             //       
            //}
        }
    }
}
