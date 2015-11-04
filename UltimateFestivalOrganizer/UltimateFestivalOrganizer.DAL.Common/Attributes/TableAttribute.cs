using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFestivalOrganizer.DAL.Common.Attributes
{
    public class TableAttribute : Attribute
    {
        public string TableName { get; private set; }
        
        public TableAttribute(string tablename)
        {
            TableName= tablename;
        }
    }
}
