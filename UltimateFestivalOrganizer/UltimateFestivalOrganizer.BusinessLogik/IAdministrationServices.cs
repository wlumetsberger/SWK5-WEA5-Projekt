using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.BusinessLogik
{
    public interface IAdministrationServices
    {
     

        bool DeleteArtist(Artist artist);
        Artist SaveArtist(Artist artist);
        IList<Artist> GetArtists();
        Task<bool> CheckEmailIsAvailable(string email, Artist artist);

        IList<Catagory> GetCatagories();
        Catagory SaveCatagory(Catagory catagory);
        bool DelteCatagory(Catagory catagory);
        bool CheckIsCatagoryInUse(Catagory catagory);

        IList<Venue> GetVenues();
        Venue SaveVenue(Venue venue);
        bool DeleteVenue(Venue venue);
        User CheckUser(string userName, string password);


        IList<Performance> GetPerformancesByVenueAndDay(Venue venue, DateTime day);
        IList<Performance> GetPerformancesByDay(DateTime day);
        bool DeletePerformancesByDay(DateTime day);
        bool SavePerformance(Performance p);

        void SendMail(IList<Performance> performances, IList<Performance> dayPerformances);

    }
}
