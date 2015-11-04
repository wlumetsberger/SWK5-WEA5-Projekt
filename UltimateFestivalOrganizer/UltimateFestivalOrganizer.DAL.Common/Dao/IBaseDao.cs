using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFestivalOrganizer.DAL.Common.Dao
{
    public interface IBaseDao<T> where T : BaseEntity
    {
        T findById(object id);
        IList<T> findAll();
        bool Update(T element);
        bool Insert(T element);
        bool Delete(T element);
    }
}
