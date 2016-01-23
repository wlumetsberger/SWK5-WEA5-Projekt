using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFestivalOrganizer.BusinessLogik
{
    public interface IAuthenticationService
    {
        bool AuthenticateUser(string userName, string password);
        User AuthenticateAndReturnValidUser(string userName, string password);
    }
}
