using PhonebookConverter.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PhonebookConverter.Components
{
    public class XmlReader : IXmlReader
    {
        public List <PhonebookToXml> ImportFromXml(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PhonebookToXml>), new XmlRootAttribute ("Phonebook"));

            using (StreamReader streamReader = new StreamReader(path))
            {
                List<PhonebookToXml> phonebookData = (List <PhonebookToXml>) serializer.Deserialize(streamReader);
                return phonebookData;
            }
        }
    }
}
