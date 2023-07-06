using CarddavToXML.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarddavToXML.Components
{
    public interface ICsvReader
    {
        List<YealinkCsv> ProcessYealinkCsv(string filepath);
    }
}
