using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace BCAD3___TASK_2.Forms
{
    public partial class ProductOrderedHistoryForm : System.Web.UI.Page
    {
        /// <summary>
        /// refreshes the page if the user tries to click back after logout
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.MinValue);

            base.OnInit(e);
        }

        /// <summary>
        /// Checks if the user has a valid session id
        /// if the session is not valid they get kicked to the login page
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Forms/default.aspx");
            }
            else
                // gets admin username
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString))
                {
                    int str = (int)Session["UserID"];
                    SqlCommand cmdUser = new SqlCommand("Select UserName from USERS where UserID = @User_ID", con);
                    cmdUser.Parameters.AddWithValue("@User_ID", str);
                    try

                    {
                        con.Open();
                        string user = (string)cmdUser.ExecuteScalar();
                        con.Close();
                        Label1.Text = user;
                    }
                    catch (Exception)
                    {

                    }
                }

        }

        /// <summary>
        /// logs the user out
        /// </summary>
        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = null;
            Response.Redirect("~/Forms/default.aspx");
        }
    }
}