using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BL
{
    public class Search
    {
        public BO.Search.Quick Search_Quick(BO.AuthenticationInfo auth, BO.Search.Quick search)
        {

            search.customerid = auth.customerid;


            return new DAL.Search().Search_Quick(search);

        }
    }
}