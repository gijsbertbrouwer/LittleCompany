using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BL
{
    public class LogLogic
    {
        public void Log(string code, string message)
        {
            code = code.ToUpper();

            new DAL.Logger().Log(code, message);

            return;
        }
    }
}