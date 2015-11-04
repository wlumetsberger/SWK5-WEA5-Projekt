using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Attributes;

namespace UltimateFestivalOrganizer.DAL.Common.Domain
{
    [Table("Performance")]
    public class Performance : BaseEntity
    {
        [Id]
        [AutogenerateId]
        public int Id { get; set; }
        public DateTime StagingTime { get; set; }
        public Artist Artist { get; set; }
        public Venue Venue { get; set; }
    }
}
