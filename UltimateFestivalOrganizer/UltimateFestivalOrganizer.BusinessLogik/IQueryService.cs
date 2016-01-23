using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.BusinessLogik
{
    public interface IQueryService
    {
        IList<Artist> QueryArtists();
        Artist QueryArtistById(string id);

        IList<Performance> QueryPerformances();
        IList<Performance> QueryPerfomancesByDay(DateTime day);
        IList<Performance> QueryPerfomancesByArtist(int artistId);
        IList<Performance> QueryPerfomancesByVenue(int venueId);
        IList<Performance> QueryPerfomancesByCatagory(int catagoryId);

        IList<Catagory> QueryCatagories();
        IList<Venue> QueryVenues();
        bool CheckPostponeIsPossible(int performanceId, DateTime date, int venueId);
        bool PostponePerformance(int performanceId, DateTime date, int venueId);
        bool CanclePerformance(int performanceId);
    }
}
