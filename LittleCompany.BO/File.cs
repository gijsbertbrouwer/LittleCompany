using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BO
{
    public class File
    {
        public int personid { get; set; }
        public int organisationid { get; set; }
        public string name { get; set; }
        public List<Version> versions { get; set; }

        public class Version
        {
            public string guid { get; set; }
            public int version { get; set; }
            public DateTime dateuploaded { get; set; }
            public int id { get; set; }
            public string path { get; set; }
            public string password { get; set; }
          
        }
    }
}