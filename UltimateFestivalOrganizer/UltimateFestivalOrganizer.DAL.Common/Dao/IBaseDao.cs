using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFestivalOrganizer.DAL.Common.Dao
{
    public interface IBaseDao<T> : IDao where T : BaseEntity
    {
        T findById(object id);
        T findByUniqueProperty(PropertyInfo prop, object value);
        IList<T> findAll();
        bool Update(T element);
        bool Insert(T element);
    }
}
