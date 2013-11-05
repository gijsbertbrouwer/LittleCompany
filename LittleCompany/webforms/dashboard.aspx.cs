using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LittleCompany.GUI.webforms
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var auth = new BL.Security().Authenticate((BO.SecurityToken)Session["token"]);

            var user = new BL.User().GetUser(auth);


            TableFiller_Favorites(UI_Favorites, user);
        }

        protected void TableFiller_Favorites(Table t, BO.User user)
        {
            //UI_Favorites

            foreach (var item in user.favorites)
            {
                var r = new TableRow();
                r.Attributes.Add("itemid", item.id.ToString());
                r.Cells.Add(new TableCell() { Text = item.id.ToString() });
                r.Cells.Add(new TableCell() { Text = item.name });
                r.Cells.Add(new TableCell() { Text = item.datatypecaption });
                t.Rows.Add(r);

            }


        }
    }
}