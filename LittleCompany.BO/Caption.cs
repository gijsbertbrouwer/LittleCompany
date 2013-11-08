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

    public class Caption
    {
        public int id { get; set; }
        public string code { get; set; }
        public string caption { get; set; }
       




    }




}