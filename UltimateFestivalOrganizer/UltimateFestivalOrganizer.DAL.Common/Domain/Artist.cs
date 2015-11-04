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
        [Id]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }
        public Catagory Catagory { get; set; }
        public Country Country { get; set; }

    }
}
