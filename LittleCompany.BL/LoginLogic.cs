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


            username = BL.Cyptography.GenerateHash(username);
            password = BL.Cyptography.GenerateHash(password);

            var rand  = new Random();
            var securitytokenstring = System.Guid.NewGuid().ToString("N").ToLower();
            r.token = securitytokenstring;

            int loginid = new DAL.Security().Login(username, password, securitytokenstring);
            if (loginid < 1) { return null; } //  no user found with these credentials

            return r;
        }



        public int  Authenticate(BO.SecurityToken securitytoken)
        {
           // check if the user is logged in..
            int loginid = new DAL.Security().Authenticate(securitytoken.token);
            if (loginid < 1) { return -1; } //  no user found with these credentials
            return loginid;

        }

        
    }
}