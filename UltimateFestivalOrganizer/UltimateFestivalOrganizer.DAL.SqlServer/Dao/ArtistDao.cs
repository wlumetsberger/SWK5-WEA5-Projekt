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
    public class ArtistDao : BaseDao<Artist>, IArtistDao
    {
      public ArtistDao(IDatabase db):base(db)
      {
      }

        public IList<Artist> findAllWithoutDeleted()
        {
            StringBuilder sql = new StringBuilder(this.GetDefaultSelect());
            sql.Append(" WHERE Deleted = @Deleted");
            DbCommand command = database.CreateCommand(sql.ToString());
            database.DefineParameter(command, "@Deleted", DbType.Boolean, false);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                return ConvertResultToList(reader);
            }
        }
    }
}
