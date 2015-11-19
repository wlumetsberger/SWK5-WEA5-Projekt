using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Attributes;

namespace UltimateFestivalOrganizer.DAL.Common.Domain
{
    [Table("Artist")]
    public class Artist : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Picture { get; set; }
        public Catagory Catagory { get; set; }
        public string Country { get; set; }

        public bool Deleted { get; set; }

    }
}
