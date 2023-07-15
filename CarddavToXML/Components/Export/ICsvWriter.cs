using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.Components.Export
{
    public interface ICsvWriter
    {
        void ExportToCsvYealinkLocal(string filePath);
        void ExportToCsvYeastarPSeries(string filePath);
        void ExportToCsvFanvilLocal(string filePath);
    }
}
