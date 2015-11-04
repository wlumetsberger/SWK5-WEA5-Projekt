using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Attributes;

namespace UltimateFestivalOrganizer.DAL.Common.Domain
{
    [Table("Catagory")]
    public class Catagory : BaseEntity
    {
        [Id]
        [AutogenerateId]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
