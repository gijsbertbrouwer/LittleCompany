using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace LittleCompany.GUI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            LittleCompany.DAL.Connection.connectionstring = LittleCompany.GUI.Properties.Settings.Default.DBConnection;

            // check if database is there
            if (!LittleCompany.DAL.Connection.databaseIsAccesible())
            {
                new BL.LogLogic().Log("GUI.START", string.Format("(Application_Start) - The database with connectionstring {0} could not be reached. it might be down or the conectionstring is incorrect.", LittleCompany.DAL.Connection.connectionstring));
              
                
                return;
            }



            LittleCompany.BL.Cyptography.salt = LittleCompany.GUI.Properties.Settings.Default.Salt;

            // Load all captions to memory
            library.Globals.captions.Load();

            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}