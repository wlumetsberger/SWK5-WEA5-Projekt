﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Attributes;

namespace DAL.Common.Domain
{
    [Table("User")]
    public class User : BaseEntity
    {
        [Id]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
