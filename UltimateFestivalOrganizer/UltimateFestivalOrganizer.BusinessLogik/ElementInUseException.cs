using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFestivalOrganizer.BusinessLogik
{
    public class ElementInUseException : Exception
    {
        public ElementInUseException(String message) : base(message)
        {

        }
    }
}
