using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BL
{
    public class Organisation
    {
        public int CreateOrganisation(string name)
        {
            // create organisation and return the id of it
            return new DAL.Organisation().CreateNewOrganisation(name);

        }


    }
}