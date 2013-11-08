using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BL
{
   
    
    public class Caption
    {
        public BO.Caption GetCaption(string code, string languagecode, BO.CaptionType type)
        {
            return new DAL.Captions().GetCaption(code, languagecode, type);

        }

    }
}