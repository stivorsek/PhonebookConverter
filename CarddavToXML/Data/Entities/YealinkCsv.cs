using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarddavToXML.Data.Entities
{
    public class YealinkCsv : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public int PhoneNumber { get; set; }
    }
}
