using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BL
{
    public class CustomerLogic
    {
        public int RegisterNewCustomer(string customername, string mainuseremail, string mainuserpassword)
        {
            // will check if customer or user is existing, otherwise it will create and return the mainuserloginid


            // create hashed variables
            mainuserpassword = BL.Cyptography.GenerateHash(mainuserpassword);
            mainuseremail = BL.Cyptography.GenerateHash(mainuseremail);

            // register a new customer, returns -1 if it fails down the road
            int mainloginid = new DAL.Customer().CreateNewCustomer(customername, mainuseremail, mainuserpassword);

            return mainloginid;
        }

    }
}