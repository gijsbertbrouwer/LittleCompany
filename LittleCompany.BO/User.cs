using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BO
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Favorite> favorites { get; set; }
        public DateTime creationdate { get; set; }

        public class Favorite
        {
            public int id { get; set; }
            public int datatypeid { get; set; }
            public string datatypecaption { get; set; }
            public string name { get; set; }
        }
    }
}