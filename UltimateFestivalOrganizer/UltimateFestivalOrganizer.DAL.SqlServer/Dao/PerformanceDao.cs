using DAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.DAL.SqlServer.Dao
{
    public class PerformanceDao : BaseDao<Performance> , IPerformanceDao 
    {
        public PerformanceDao(IDatabase db) : base(db)
        {

        }
    }
}
