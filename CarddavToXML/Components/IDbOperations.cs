using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConverter.Components
{
    public interface IDbOperations
    {
        void AddNewDbEntry();
        void EditFromDbByID(string? id);
        void DeleteFromDbByID(string? id);
        void ReadAllContactsFromDb();
    }
}
