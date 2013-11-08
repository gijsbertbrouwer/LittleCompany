using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LittleCompany.GUI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UI_Login_Click(object sender, EventArgs e)
        {
            TryLogin(UI_UserName.Text, UI_Password.Text);
        }

        private void TryLogin(string username, string password)
        {

            // Login user
            var token = new BL.Security().Login(username, password);

            if (token != null)
            {
                Session["token"] = token;
                Response.Redirect("webforms/dashboard.aspx");
            }
            else
            {
                UI_Respons.Text = "Ongeldige credentials, probeer het opnieuw.";
                Session["token"] = null;
            }

        }


        protected void UI_SignUp_Click(object sender, EventArgs e)
        {


            int mainloginid = new BL.Customer().RegisterNewCustomer(
                                                        UI_SignUp_OrganisationName.Text,
                                                        UI_SignUp_EmailAdress.Text,
                                                        UI_SignUp_Password.Text);
         
        if(mainloginid > 0){
            TryLogin(UI_SignUp_EmailAdress.Text,UI_SignUp_Password.Text);
        }else{
            UI_SignUp_Respons.Text="Helaas kunt u deze gegevens niet gebruiken.";

        }




        }
    }
}