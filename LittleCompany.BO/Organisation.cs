using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BO
{
    public class Organisation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phonenumber { get; set; }
        public string emailaddress { get; set; }
        public string notes { get; set; }
    }
}