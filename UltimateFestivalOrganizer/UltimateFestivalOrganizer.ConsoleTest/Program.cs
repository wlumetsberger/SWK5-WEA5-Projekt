using DAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using UltimateFestivalOrganizer.DAL.Common.Domain;
using UltimateFestivalOrganizer.DAL.SqlServer.Dao;

namespace UltimateFestivalOrganizer.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            IArtistDao artistDao = (IArtistDao)DALFactory.CreateArtistDao(DALFactory.CreateDatabase());
            Artist art = artistDao.findById(1);
           
          

            Artist a = new Artist();
            a.Email = "test@aon.at";
         //   a.FirstName = "test";
         //   a.LastName = "test";
            a.Country = "AUT";

            artistDao.Insert(a);

            Console.WriteLine("found: " + artistDao.findById(1).Id);
            //countryDao.Delete(c);
            Console.ReadKey();
           /* Console.WriteLine("Try to Insert Eleme
           nt");
            artistDao.Insert(new DAL.Common.Domain.Artist());
            Console.ReadKey();
            Console.WriteLine("Try to Delete Element");
            artistDao.Delete(new DAL.Common.Domain.Artist());
            Console.ReadKey();
            Console.WriteLine("Try to Update Element");
            artistDao.Update(new DAL.Common.Domain.Artist());
            Console.ReadKey();*/

        }
    }
}
