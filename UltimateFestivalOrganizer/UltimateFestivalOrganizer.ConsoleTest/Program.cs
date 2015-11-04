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

            IArtistDao artistDao = (IArtistDao)DALFactory.getDao<Artist>(DALFactory.CreateDatabase());
                //(IArtistDao) DALFactory.getDao<Artist>(DALFactory.CreateDatabase());
            artistDao.findAll().ToList().ForEach(x => Console.WriteLine(x.Email));
            Console.ReadKey();
           /* Console.WriteLine("Try to Insert Element");
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
