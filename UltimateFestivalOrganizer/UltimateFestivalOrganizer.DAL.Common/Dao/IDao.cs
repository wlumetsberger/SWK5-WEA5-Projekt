using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFestivalOrganizer.DAL.Common.Dao
{
    /// <summary>
    /// Interface without Generic
    /// With this Interface it is Possible to use DALFactory and Generate a Generic Type
    /// </summary>
    public interface IDao
    {
        object findById(object id);
        object findByUniqueProperty(PropertyInfo prop, object value);
        object findAll();
        bool Update(object element);

        bool Insert(object element);
    }
}
