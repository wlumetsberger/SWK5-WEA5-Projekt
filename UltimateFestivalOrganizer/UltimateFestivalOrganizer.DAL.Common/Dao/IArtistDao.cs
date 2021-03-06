﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.DAL.Common.Dao
{
    public interface IArtistDao : IBaseDao<Artist>
    {
       IList<Artist>findAllWithoutDeleted();
    }
}
