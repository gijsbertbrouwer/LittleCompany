using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LittleCompany
{
    public partial class _testdb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                using (SqlCommand cmd = new SqlCommand("select * from test"))
                {

                    cmd.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}\t{1}", reader.GetInt32(0),
                            reader.GetString(1));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }


            }

        }
    }
}