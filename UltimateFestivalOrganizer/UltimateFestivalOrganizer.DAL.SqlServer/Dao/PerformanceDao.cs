using DAL.Common;
using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
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

        public IList<Performance> FindPerformanceForArtistsAfterDate(Artist artist, DateTime d)
        {
            StringBuilder sql = new StringBuilder(this.GetDefaultSelect());
            sql.Append(" WHERE  [Performance].[Canceld] !=@canceld AND ");
            sql.Append(" [Artist] =@artist AND [StagingTime] >=@staging ");
            DbCommand command = database.CreateCommand(sql.ToString());
            database.DefineParameter(command, "@canceld", DbType.Boolean, true);
            database.DefineParameter(command, "@artist", DbType.Object, artist.Id);
            database.DefineParameter(command, "@staging", DbType.DateTime, d);
            Console.WriteLine("EXECUTE QUERY: " +command.CommandText);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                return ConvertResultToList(reader);
            }

        }

        public IList<Performance> FindPerformanceForVenueByDay(Venue v, DateTime d)
        {
            StringBuilder sql = new StringBuilder(this.GetDefaultSelect());
            sql.Append(" WHERE [Performance].[Canceld] !=@canceld AND ");
            sql.Append(" [Venue] =@venue AND Cast([StagingTime] as Date) = @date ");
            DbCommand command = database.CreateCommand(sql.ToString());
            database.DefineParameter(command, "@canceld", DbType.Boolean, true);
            database.DefineParameter(command, "@venue", DbType.Object, v.Id);
            database.DefineParameter(command, "@date", DbType.Date, d.Date);
            Console.WriteLine("EXECUTE QUERY: " + command.CommandText);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                return ConvertResultToList(reader);
            }
        }
        public IList<Performance> FindPerormanceByDay(DateTime d)
        {
            StringBuilder sql = new StringBuilder(this.GetDefaultSelect());
            sql.Append(" WHERE [Performance].[Canceld] != @canceld AND ");
            sql.Append(" Cast([StagingTime] as Date) = @date  " );
            DbCommand command = database.CreateCommand(sql.ToString());
            database.DefineParameter(command, "@canceld", DbType.Boolean,true);
            database.DefineParameter(command, "@date", DbType.Date, d.Date);
            Console.WriteLine("EXECUTE QUERY: " + command.CommandText);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                return ConvertResultToList(reader);
            }

        }



        public bool DeletePerformancesByDay(DateTime day)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM Performance WHERE Cast([StagingTime] as Date) = @date ");
            DbCommand command = database.CreateCommand(sql.ToString());
            database.DefineParameter(command, "@date", DbType.Date, day.Date);
            Console.WriteLine("EXECUTE QUERY: " + command.CommandText);
            return database.ExecuteNonQuery(command) == 1;
        }

        public IList<Performance> FindPerformanceByCatagory(Catagory c)
        {
            StringBuilder sql = new StringBuilder("SELECT [Performance].* FROM [Performance], [Artist]");
            sql.Append(" WHERE [Performance].[Canceld] != @canceld AND ");
            sql.Append("[Artist].[Id]=[Performance].[Artist] AND [Artist].[Catagory] = @id");
            DbCommand command = database.CreateCommand(sql.ToString());
            database.DefineParameter(command, "@canceld", DbType.Boolean, true);
            database.DefineParameter(command, "@id", DbType.Object, c.Id);
      
            Console.WriteLine("EXECUTE QUERY: " + command.CommandText);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                return ConvertResultToList(reader);
            }

        }

        public new IList<Performance> findByProperty(PropertyInfo prop, object value)
        {

            StringBuilder sql = new StringBuilder(this.GetDefaultSelect());
            sql.Append(" WHERE ");
            sql.Append(prop.Name);
            sql.Append("=@Id");
            sql.Append(" AND Canceld != @canceld");
            // resolve foreign key properties correct
            object valueToSet = value;
            if (prop.PropertyType.BaseType == typeof(BaseEntity))
            {
                valueToSet = ((BaseEntity)value).Id;
            }
            DbCommand command = database.CreateCommand(sql.ToString());
            database.DefineParameter(command, "@Id", DbType.Object, valueToSet);
            database.DefineParameter(command, "@canceld", DbType.Boolean, true);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                return ConvertResultToList(reader);
            }
        }

    }
}
