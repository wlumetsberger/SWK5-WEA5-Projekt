using DAL.Common;
using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Dao;

namespace UltimateFestivalOrganizer.BusinessLogik
{
    public class AuthenticationService : IAuthenticationService
    {
        private IDatabase database;

        public AuthenticationService()
        {
            database = DALFactory.CreateDatabase();
        }

        public User AuthenticateAndReturnValidUser(string userName, string password)
        {
            IUserDao dao = DALFactory.CreateUserDao(database);
            HashAlgorithm algo = new SHA256Managed();
            byte[] pw = algo.ComputeHash(Encoding.Default.GetBytes(userName + "|" + password));
            string pass = System.BitConverter.ToString(pw);
            User u = dao.findByUniqueProperty(typeof(User).GetProperty("Email"), userName);
            if (u != null && u.Password.Equals(pass))
            {
                return u;
            }
            return null;
        }

        public bool AuthenticateUser(string userName, string password)
        {
            User u = this.AuthenticateAndReturnValidUser(userName, password);
            return u != null;
           
        }
    }
}
