using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using DAL.Common.Domain;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace DAL.Common
{
    public class DALFactory
    {
        private static string assemblyName;
        private static Assembly dalAssembly;

        static DALFactory()
        {
            assemblyName = ConfigurationManager.AppSettings["DALAssembly"];
            dalAssembly = Assembly.Load(assemblyName);
        }

        public static IDatabase CreateDatabase()
        {
            string connectionString =
              ConfigurationManager.ConnectionStrings["UltimateFestivalOrganizerDB"].ConnectionString;
            return CreateDatabase(connectionString);
        }

        public static IDatabase CreateDatabase(string connectionString)
        {
            string databaseClassName = assemblyName + ".Database";
            Type dbClass = dalAssembly.GetType(databaseClassName);

            return Activator.CreateInstance(dbClass,
              new object[] { connectionString }) as IDatabase;
        }

       
        public static IArtistDao CreateArtistDao(IDatabase db)
        {
            Type artistType = dalAssembly.GetType(assemblyName + ".Dao.ArtistDao");
            return Activator.CreateInstance(artistType, new object[] { db })
                     as IArtistDao;
        }
        public static ICatagoryDao CreateCatagoryDao(IDatabase db)
        {
            Type type = dalAssembly.GetType(assemblyName + ".Dao.CatagoryDao");
            return Activator.CreateInstance(type, new object[] { db })
                     as ICatagoryDao;
        }

        public static IPerformanceDao CreatePerformanceDao(IDatabase db)
        {
            Type type = dalAssembly.GetType(assemblyName + ".Dao.PerformanceDao");
            return Activator.CreateInstance(type, new object[] { db })
                     as IPerformanceDao;
        }
        public static IUserDao CreateUserDao(IDatabase db)
        {
            Type type = dalAssembly.GetType(assemblyName + ".Dao.UserDao");
            return Activator.CreateInstance(type, new object[] { db })
                     as IUserDao;
        }
        public static IVenueDao CreateVenueDao(IDatabase db)
        {
            Type type = dalAssembly.GetType(assemblyName + ".Dao.VenueDao");
            return Activator.CreateInstance(type, new object[] { db })
                     as IVenueDao;
        }
        /// <summary>
        /// Returns Correct Dao for any Type
        /// This is usesfully in our BaseDao to fetch Eager relation shipss
        /// </summary>
        /// <param name="db"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IDao CreateDao(IDatabase db, Type t)
        {
            if (typeof(Artist).Equals(t))
            {
                return CreateArtistDao(db);
            }
            else if (typeof(Catagory).Equals(t))
            {
                return CreateCatagoryDao(db);
            }
            else if (typeof(Performance).Equals(t))
            {
                return CreatePerformanceDao(db);
            }
            else if (typeof(Venue).Equals(t))
            {
                return CreateVenueDao(db);
            }
            else if (typeof(User).Equals(t))
            {
                return CreateUserDao(db);
            }
            return null;

        }

    }
}
