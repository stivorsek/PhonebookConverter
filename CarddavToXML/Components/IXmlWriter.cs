using CarddavToXML.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.Components
{
    public interface IXmlWriter
    {
       void ExportToXml(string filepath,List<PhonebookInDb> contactsFromDb); 
    }
}
