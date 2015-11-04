using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Attributes;

namespace UltimateFestivalOrganizer.DAL.Common.Domain
{
    [Table("Venue")]
    public class Venue : BaseEntity
    {
        [Id]
        [AutogenerateId]
        public int Id { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Address { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }   
    }
}
