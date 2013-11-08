using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BO
{
    public enum CaptionType
    {
        None,
        Singular,
        Plural 
    };

    public class Language
    {
        public string languagecode { get; set; }
        public Dictionary<string, Caption> captions{ get; set; }
    }


    public class Caption
    {
        public int id { get; set; }
        public string code { get; set; }
        public string caption { get; set; }
       

    }




}