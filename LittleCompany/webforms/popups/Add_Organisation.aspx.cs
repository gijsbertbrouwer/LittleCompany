using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LittleCompany.GUI.webforms.popups
{
    public partial class Add_Organisation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UI_btn_add_Click(object sender, EventArgs e)
        {

            var auth = new BL.Security().Authenticate((BO.SecurityToken)Session["token"]);
            int newOrgId = new BL.Organisation().CreateOrganisation(UI_Name.Text, auth.customerid);
            if (newOrgId < 0)
            {

                UI_Respons.Text = "Er is een fout opgetreden. De organisatie kon niet gemaakt worden.";
            }

        }
    }
}