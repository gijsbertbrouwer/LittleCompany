using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BO
{
    public class Feedback
    {
        public object data;
        public bool ispositive;
        public new List<string> messages;

        public Feedback()
        {
            messages = new List<string>();
        }
    }
}