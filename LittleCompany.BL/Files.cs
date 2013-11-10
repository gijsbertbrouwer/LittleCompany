using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BL
{
    public class Files
    {

        public BO.File CreateNewFile(System.IO.Stream inputstream, string name, int organisationid, int customerid, int personid, DateTime dateupload)
        {
            return new DAL.Files().CreateNewFile(inputstream, name, organisationid, customerid, personid, dateupload);
        }

        public BO.File GetFile(int fileid, int customerid)
        {

            return new DAL.Files().GetFile(fileid, customerid);
        }
    }
}