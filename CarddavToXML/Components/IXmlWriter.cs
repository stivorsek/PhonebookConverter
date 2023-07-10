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
       void ExportToXmlYealinkLocal(string filepath,List<PhonebookInDb> contactsFromDb);
       void ExportToXmlYealinkRemote(string filepath, List<PhonebookInDb> contactsFromDb);
       void ExportToXmlFanvilLocal(string filepath, List<PhonebookInDb> contactsFromDb);
       void ExportToXmlFanvilRemote(string filepath, List<PhonebookInDb> contactsFromDb);
        
    }
}
