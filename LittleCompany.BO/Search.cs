using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BO
{
    public class Search
    {

        public class Quick
        {
            public string query { get; set; }
            public int customerid { get; set; }
            public int searchDataTypeId { get; set; }


            public List<item> searchresults { get; set; }

            public class item
            {
                public int id { get; set; }
                public string name { get; set; }
                public int datatypeid { get; set; }

            }

        }

    }
}