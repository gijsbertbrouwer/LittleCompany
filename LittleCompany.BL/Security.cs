using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BL
{
    public class Security
    {

        public BO.SecurityToken Login(string username, string password)
        {
            var r = new BO.SecurityToken();


            
            password = BL.Cyptography.GenerateHash(password, username);
            username = BL.Cyptography.GenerateHash(username, username);

            var rand  = new Random();
            var securitytokenstring = System.Guid.NewGuid().ToString("N").ToLower();
            r.token = securitytokenstring;

            var s = new DAL.Security();
            int loginid = s.Login(username, password, securitytokenstring);
           
            // remove the old tokens
            s.RemoveOldTokens();



            if (loginid < 1) { return null; } //  no user found with these credentials

            return r;
        }

        public BO.AuthenticationInfo Authenticate(BO.SecurityToken securitytoken)
        {
           // check if the user is logged in..
            return new DAL.Security().Authenticate(securitytoken.token);

        }

 
    }
}