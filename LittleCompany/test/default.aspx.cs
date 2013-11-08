using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LittleCompany.GUI.test
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //var c = new BL.Caption().GetCaption("@General_DataTypes_File_", "NL", BO.CaptionType.Plural);



        var pid =    new BL.Person().CreatePerson("aaa gijsbert test v1", 1002, 6002);




           //// QUICK SEARCH
           // var auth = new BL.Security().Authenticate((BO.SecurityToken)Session["token"]);
           // var searchitems = new BL.Search().Search_Quick(auth, new BO.Search.Quick()
           // {
           //      query = "",
           //      searchDataTypeId = 0
                  
           // });




            //// CREATE CUSTOMER
            //var t = "v14";
            //string customername =t, username = t, password = t;
            //int mainloginid = new BL.Customer().RegisterNewCustomer(customername, username, password);


            ////LOGIN
            //var token = new BL.Security().Login(username, password);

            ////AUTHENTICATE
            //var auth = new BL.Security().Authenticate(token);

            //// test errorlog
            //new BL.LogLogic().Log("TEST", "This is a test");

            //// get user object
            //var user = new BL.User().GetUser(auth);


            //// create test favorite
            //var temporaryfav = new BL.User().SetFavorite(new BO.User.Favorite()
            //{
            //    name = "test",
            //    datatypeid = 1,
            //}, user.id, auth.customerid);


            //var favorites = new BL.User().GetUser(auth);

            //// remove favorite
            //new BL.User().DelFavorite(temporaryfav.id, user.id, auth.customerid);
        }
    }
}