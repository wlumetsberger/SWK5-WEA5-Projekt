using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UltimateFestivalOrganizer.WebService.Models
{
    public class PostponePerformance
    {
        public int Id { get; set; }
        public DateTime StagingTime { get; set; }
        public int VenueId { get; set; }

    }
}