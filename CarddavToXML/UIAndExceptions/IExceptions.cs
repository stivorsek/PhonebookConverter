using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.UI
{
    public interface IExceptions
    {
        void ExceptionsLoop(Action metoda);
        T ExceptionsLoop<T>(Func<T> method);
    }
}
