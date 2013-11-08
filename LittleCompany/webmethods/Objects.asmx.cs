using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LittleCompany.GUI.webmethods
{
    /// <summary>
    /// Summary description for Objects
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Objects : System.Web.Services.WebService
    {

        [WebMethod]
        public BO.Feedback AddOrganisation(BO.SecurityToken securityToken, string organisationName)
        {
            var fb = new BO.Feedback();

            var auth = new BL.Security().Authenticate(securityToken);
            
            int newOrgId = new BL.Organisation().CreateOrganisation(organisationName, auth.customerid);
            if (newOrgId < 0)
            {
               fb.messages.Add("Er is een fout opgetreden. De organisatie kon niet worden toegevoegd.");
            }
            else
            {
                fb.ispositive = true;
            }

            return fb;
        }

        [WebMethod]
        public BO.Feedback AddPerson(BO.SecurityToken securityToken, string personName)
        {
            var fb = new BO.Feedback();

            var auth = new BL.Security().Authenticate(securityToken);

            int newOrgId = 1;   // = new BL.Organisation().CreatePerson(personName, auto.customerid);
            if (newOrgId < 0)
            {
                fb.messages.Add("Er is een fout opgetreden. De contactpersoon kon niet worden toegevoegd.");
            }
            else
            {
                fb.ispositive = true;
            }

            return fb;
        }

        [WebMethod]
        public BO.Feedback QuickSearch(BO.SecurityToken securityToken, BO.Search.Quick quicksearch)
        {
            var fb = new BO.Feedback();

            var auth = new BL.Security().Authenticate(securityToken);
            var result = new BL.Search().Search_Quick(auth, quicksearch);
            if (result != null)
            {
                fb.data = result;
                fb.ispositive = true;
            }

            return fb;

        }
    }
}
