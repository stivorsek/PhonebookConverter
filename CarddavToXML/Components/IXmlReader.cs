using CarddavToXML.Data.Entities;
using PhonebookConverter.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.Components
{
    public interface IXmlReader
    {
        List<PhonebookInDb> XmlTypeChecker(string filepath);
    }
}
