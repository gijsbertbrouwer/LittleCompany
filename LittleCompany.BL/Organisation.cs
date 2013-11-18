using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BL
{
    public class Organisation
    {
        public int CreateOrganisation(string name, int customerid)
        {
            // create organisation and return the id of it
            return new DAL.Organisation().CreateNewOrganisation(name, customerid);
        }

        public BO.Organisation GetOrganisationById(int organisationid, int customerid)
        {
            // get the organisation
            return new DAL.Organisation().GetOrganisation(organisationid, customerid);
        }



    }
}