using DAL.Common;
using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Dao;

namespace UltimateFestivalOrganizer.DAL.SqlServer.Dao
{
    public class UserDao : BaseDao<User>, IUserDao
    {
        public UserDao(IDatabase db) : base(db)
        {

        }
    }
}
