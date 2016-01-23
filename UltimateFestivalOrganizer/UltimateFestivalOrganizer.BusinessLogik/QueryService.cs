using DAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.BusinessLogik
{
    public class QueryService : IQueryService
    {
        private IDatabase database;

        public QueryService()
        {
            database = DALFactory.CreateDatabase();
        }
        public IList<Artist> QueryArtists()
        {
            IArtistDao dao = DALFactory.CreateArtistDao(database);
            return dao.findAllWithoutDeleted();
        }

        public Artist QueryArtistById(string id)
        {
            IArtistDao dao = DALFactory.CreateArtistDao(database);
            return dao.findByUniqueProperty(typeof(Artist).GetProperty("Email"), id);
        }

        public IList<Performance> QueryPerfomancesByDay(DateTime day)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            return dao.FindPerormanceByDay(day);
        }

        public IList<Performance> QueryPerfomancesByArtist(int artistId)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            IArtistDao artistDao = DALFactory.CreateArtistDao(database);
            Artist a = artistDao.findById(artistId);
            return dao.findByProperty(typeof(Performance).GetProperty("Artist"), a);
        }

        public IList<Performance> QueryPerfomancesByVenue(int venueId)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            IVenueDao venueDao = DALFactory.CreateVenueDao(database);
            Venue v = venueDao.findById(venueId);
            return dao.findByProperty(typeof(Performance).GetProperty("Venue"), v);
        }

        public IList<Performance> QueryPerfomancesByCatagory(int catagoryId)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            ICatagoryDao catagoryDao = DALFactory.CreateCatagoryDao(database);
            Catagory c = catagoryDao.findById(catagoryId);
            return dao.FindPerformanceByCatagory(c);

        }

        public IList<Catagory> QueryCatagories()
        {
            ICatagoryDao dao = DALFactory.CreateCatagoryDao(database);
            return dao.findAll();
        }

        public IList<Venue> QueryVenues()
        {
            IVenueDao dao = DALFactory.CreateVenueDao(database);
            return dao.findAll();
        }
        public IList<Performance> QueryPerformances() 
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            return dao.findAll();
        }

        public bool CheckPostponeIsPossible(int performanceId, DateTime date, int venueId)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            IVenueDao venueDao = DALFactory.CreateVenueDao(database);
            Performance toPostpone = dao.findById(performanceId);
            IList<Performance> otherPerformances = dao.FindPerormanceByDay(date);

            bool foundConflict = false;
            foreach(Performance p in otherPerformances)
            {
                if(p.Id == toPostpone.Id)
                {
                    // continue loop it is the one we want to postpone
                    continue;
                }


                DateTime postPoneDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
                DateTime currentDate = new DateTime(p.StagingTime.Year, p.StagingTime.Month, p.StagingTime.Day, p.StagingTime.Hour, 0, 0);

                // equal artist on same day
                // check if one hour is between the performance
                if (toPostpone.Artist.Id == p.Artist.Id)
                {
                   if(postPoneDate >= currentDate.AddHours(-2) && postPoneDate <= currentDate.AddHours(2))
                    {
                        foundConflict = true;
                        break;
                    }
                }
                if(p.Venue.Id == venueId && postPoneDate == currentDate)
                {
                    foundConflict = true;
                    break;
                }

              
            }
            return !foundConflict;
        }

        public bool PostponePerformance(int performanceId, DateTime date, int venueId)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            IVenueDao venueDao = DALFactory.CreateVenueDao(database);
            Performance p = dao.findById(performanceId);
            if (this.CheckPostponeIsPossible(performanceId, date, venueId))
            {
                p.StagingTime = date;
                Venue v = venueDao.findById(venueId);
                p.Venue = v;
                return dao.Update(p);
            }
            return false;
        }

        public bool CanclePerformance(int performanceId)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            Performance p = dao.findById(performanceId);
            p.Canceld = true;
            return dao.Update(p);

        }
    }
}
