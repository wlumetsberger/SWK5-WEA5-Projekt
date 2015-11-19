using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Attributes;

namespace DAL.Common.Domain
{
    public class BaseEntity
    {
        [Id]
        [AutogenerateId]
        public int Id { get; set; }
    }
}
