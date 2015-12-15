using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFestivalOrganizer.BusinessLogik
{
    public class PersistenceException : Exception
    {
        public PersistenceException(string message) : base(message)
        {
        }
        public PersistenceException() : base()
        {

        }
    }
}
