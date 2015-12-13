using DAL.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
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

        public IList<Performance> findPerformanceForArtistsAfterDate(Artist artist, DateTime d)
        {
            StringBuilder sql = new StringBuilder(this.GetDefaultSelect());
            sql.Append(" WHERE ");
            sql.Append(" [Artist] =@artist AND [StagingTime] >=@staging ");
            DbCommand command = database.CreateCommand(sql.ToString());
            database.DefineParameter(command, "@artist", DbType.Object, artist.Id);
            database.DefineParameter(command, "@staging", DbType.DateTime, d);
            Console.WriteLine("EXECUTE QUERY: " +command.CommandText);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                return ConvertResultToList(reader);
            }

        }
    }
}
