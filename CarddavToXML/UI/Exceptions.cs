using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.UI
{
    public class Exceptions : IExceptions
    {
        public void ExceptionsLoop(Action metoda)
        {
            while (true)
            {
                try
                {
                    metoda.Invoke();                    
                    break;
                }
                catch (Exception ex)
                {                    
                    CatchError(ex);
                    continue;
                }
            }
        }
        public T ExceptionsLoop<T>(Func<T> method)
        {
            while (true)
            {
                try
                {
                    T result = method.Invoke();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    CatchError(ex);
                    continue;
                }
            }
        }
        public void CatchError(Exception ex)
        {
            Console.Clear();
            string separator = "///////////////////////////////////////////////////////////////////////////////";
            string exceptionHeader = "Wystąpił wyjątek: " + DateTime.Now;
            Console.WriteLine(separator);
            Console.WriteLine(exceptionHeader);
            Console.WriteLine($"\t{ex.Message}");
            Console.WriteLine(separator);
            using (var writer = File.AppendText("ErrorLog.txt"))
            {
                writer.Write(separator);
                writer.Write(exceptionHeader);
                writer.Write(ex.Message);
            }
        }
    }
}
