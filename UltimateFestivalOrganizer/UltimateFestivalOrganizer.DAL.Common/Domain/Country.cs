using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Attributes;

namespace UltimateFestivalOrganizer.DAL.Common.Domain
{
    [Table("Country")]
    public class Country : BaseEntity
    {
        [Id]
        public string CountryCode{ get; set; }

        public string Name { get; set; }
        
    }
}   
