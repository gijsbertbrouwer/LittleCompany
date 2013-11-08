using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BL
{
    public class Person
    {
        public int CreatePerson(string name, int organisationid, int customerid)
        {
            // create organisation and return the id of it
            return new DAL.Person().CreateNewPerson(name, organisationid, customerid);

        }



    }
}