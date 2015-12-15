using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.DAL.Common.Dao
{
    public interface IPerformanceDao : IBaseDao<Performance>
    {
        IList<Performance> FindPerformanceForArtistsAfterDate(Artist artist, DateTime d);
        IList<Performance> FindPerformanceForVenueByDay(Venue v, DateTime d);
        IList<Performance> FindPerormanceByDay(DateTime d);
        bool DeletePerformancesByDay(DateTime day);
    }
}
