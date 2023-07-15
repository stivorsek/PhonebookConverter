using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.Data.Entities
{
    public class ExportPeriodData
    {
        public string Path { get; set; }
        public string Type { get; set; }
        public int Interval { get; set; }        
    }
}
