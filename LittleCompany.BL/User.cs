using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BL
{
    public class User
    {
      
        public BO.User GetUser(BO.AuthenticationInfo auth){
           return new DAL.User().GetUser(auth.loginid, auth.customerid);
       }


       public BO.User.Favorite SetFavorite(BO.User.Favorite favorite,int userid, int customerid)
       {
          return new DAL.User().SetFavorite(favorite, userid, customerid);
       }



       public void DelFavorite(int favoriteid, int userid, int customerid)
       {

           new DAL.User().DelFavorite(favoriteid, userid, customerid);

       }

    }
}