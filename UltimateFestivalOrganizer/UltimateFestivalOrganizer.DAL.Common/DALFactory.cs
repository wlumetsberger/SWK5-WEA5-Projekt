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

        public static IArtistDao getArtistDao(IDatabase db)
        {
            Type artistType = dalAssembly.GetType(assemblyName + ".Dao.ArtistDao");
            return Activator.CreateInstance(artistType, new object[] { db })
                     as IArtistDao;
        }

        public static IBaseDao<T> getDao<T>(IDatabase db) where T : BaseEntity
        {
            if(typeof(T) == typeof(Artist))
            {
                return (IBaseDao<T>)getArtistDao(db);
            }else if(typeof(T) == typeof(Country))
            {
                return null;
            }
            throw new NoPossibleDaoFoundException();
            
        }
    }
}
