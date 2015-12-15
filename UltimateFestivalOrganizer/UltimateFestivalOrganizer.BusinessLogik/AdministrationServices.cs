using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common.Domain;
using UltimateFestivalOrganizer.DAL.Common.Domain;
using DAL.Common;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using System.Security.Cryptography;
using System.Net.Mail;

namespace UltimateFestivalOrganizer.BusinessLogik
{
    public class AdministrationServices : IAdministrationServices
    {
        private IDatabase database;

        public AdministrationServices()
        {
            database = DALFactory.CreateDatabase();
        }

        public bool CheckIsCatagoryInUse(Catagory catagory)
        {
            IArtistDao dao = DALFactory.CreateArtistDao(database);
            if(dao.findByProperty(typeof(Artist).GetProperty("Catagory"), catagory).Count() > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// disable an artist and set performances after now 
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        public bool DeleteArtist(Artist artist)
        {
            IArtistDao dao = DALFactory.CreateArtistDao(database);
            Artist toDelete = dao.findById(artist.Id);
            if(toDelete == null)
            {
                return false;
            }
            toDelete.Deleted = true;
            bool success = dao.Update(toDelete);
            if (success)
            {
                IPerformanceDao performanceDao = DALFactory.CreatePerformanceDao(database);
                IList<Performance> performances = performanceDao.FindPerformanceForArtistsAfterDate(toDelete, DateTime.Now);
                foreach(Performance p in performances)
                {
                    Console.WriteLine("found performances " + p.Id + "artist " + p.Artist.Name);
                    p.Canceld = true;
                    success = performanceDao.Update(p);
                }
            }
            return success;

        }

        public bool DelteCatagory(Catagory catagory)
        {
            ICatagoryDao dao = DALFactory.CreateCatagoryDao(database);
            if (this.CheckIsCatagoryInUse(catagory))
            {
                throw new ElementInUseException($"Can't delete catagory {catagory.Name}. Catagory is assigned");
            }
            return dao.Delete(catagory);

        }

        public IList<Artist> GetArtists()
        {
            IArtistDao dao = DALFactory.CreateArtistDao(database);
            return dao.findAllWithoutDeleted();
        }

        public IList<Catagory> GetCatagories()
        {
            ICatagoryDao dao = DALFactory.CreateCatagoryDao(database);
            return dao.findAll();
        }

        public Artist SaveArtist(Artist artist)
        {
            IArtistDao dao = DALFactory.CreateArtistDao(database);
            if(artist.Id != null && artist.Id > 0)
            {
                dao.Update(artist);
                return artist;
            }
            artist = dao.Insert(artist);
            return artist;
        }

        public Catagory SaveCatagory(Catagory catagory)
        {
            ICatagoryDao dao = DALFactory.CreateCatagoryDao(database);
            if(catagory.Id != null && catagory.Id > 0)
            {
                dao.Update(catagory);
                return catagory; 
            }
            catagory = dao.Insert(catagory);
            return catagory;
        }
        public Venue SaveVenue(Venue venue)
        {
            IVenueDao dao = DALFactory.CreateVenueDao(database);
            if(venue.Id != null && venue.Id > 0)
            {
                dao.Update(venue);
                return venue;
            }
            venue = dao.Insert(venue);
            return venue;
        }

        public IList<Venue> GetVenues()
        {
            IVenueDao dao = DALFactory.CreateVenueDao(database);
            return dao.findAll();
        }

        private bool canDeleteVenue(Venue venue)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            return dao.findByProperty(typeof(Performance).GetProperty("Venue"), venue).Count <= 0;
        }
        public bool DeleteVenue(Venue venue)
        {
            if (!canDeleteVenue(venue))
            {
                throw new ElementInUseException($"Can't delete Venue {venue.Description}. Venue is assigend in any Performance");
            }
            IVenueDao dao = DALFactory.CreateVenueDao(database);
            return dao.Delete(venue);
        }

        public Task<bool> CheckEmailIsAvailable(string email, Artist artist)
        {
            return Task<bool>.Run(() => 
            {
                IArtistDao dao = DALFactory.CreateArtistDao(database);
                IList < Artist > artists = dao.findByProperty(typeof(Artist).GetProperty("Email"), email);
                if (artists != null && artists.Count > 0)
                {
                    if(artists.Count == 1 && artists.First().Id == artist.Id)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            });

        }

        public User CheckUser(string userName, string password)
        {
            IUserDao dao = DALFactory.CreateUserDao(database);
            HashAlgorithm algo = new SHA256Managed();
            byte[] pw = algo.ComputeHash( Encoding.Default.GetBytes(userName + "|" + password));
            string pass = System.BitConverter.ToString(pw);
            User u = dao.findByUniqueProperty(typeof(User).GetProperty("Email"), userName);
            if(u != null && u.Password.Equals(pass))
            {
                return u;
            }
            return null;
        }

        public IList<Performance> GetPerformancesByVenueAndDay(Venue venue, DateTime day)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            return dao.FindPerformanceForVenueByDay(venue, day);
        }
        public IList<Performance> GetPerformancesByDay(DateTime day)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            return dao.FindPerormanceByDay(day);
        }
        public bool DeletePerformancesByDay(DateTime day)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            return dao.DeletePerformancesByDay(day);
        }
        public bool SavePerformance(Performance p)
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(database);
            dao.Insert(p);
            return true;            
        }

        public void SendMail(IList<Performance> performances, IList<Performance> dayPerformances)
        {
            StringBuilder program = new StringBuilder();
            string format = "yyyy-MM-d HH";
            program.Append("Programm: \n");

            foreach(Performance p in dayPerformances)
            {
                program.Append("Artist: \t ").Append(p.Artist.Name).Append("\n");
                program.Append("Venue: \t ").Append(p.Venue.Description).Append("(").Append(p.Venue.Longitude).Append("/").Append(p.Venue.Latitude).Append(") \n");
                program.Append("Time: \t ").Append(p.StagingTime.ToString(format));
                program.Append("\n \n ");

            }
            foreach(Performance performance in performances)
            {
                
                SmtpClient client = new SmtpClient();
                MailMessage mail = new MailMessage
                {
                    To = { new MailAddress(performance.Artist.Email) },
                    Subject = $"Programm für den {performance.StagingTime.Date}",
                    Body = program.ToString()
                };
                client.Send(mail);
            }
           
        }
    }
}
