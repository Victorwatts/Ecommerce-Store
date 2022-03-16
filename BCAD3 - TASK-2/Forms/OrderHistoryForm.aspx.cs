﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCAD3___TASK_2.Forms
{
    public partial class OrderHistoryForm : System.Web.UI.Page
    {


        /// <summary>
        /// refreshes the page if the user clicks back after logging out
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.MinValue);

            base.OnInit(e);
        }

        SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString);

        /// <summary>
        /// checks that the user session is valid
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //checks that a user is logged in
            if (Session["Admin"].Equals(true))
            {
                btn_admin.Visible = true;
            }

            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Forms/UserLogin.aspx");
            }
            else
                // gets the username
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
                        Label3.Text = user;
                    }
                    catch (Exception)
                    {

                    }
                }

            if (!IsPostBack)
            {
                BindGridview();
            }
        }


        /// <summary>
        /// populates the order history gridview
        /// </summary>
        protected void BindGridview()
        {
            int str = (int)Session["UserID"];
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Order_ID, PRODUCT_NAME, PRODUCT_PRICE, ORDER_DATE from PRODUCTS A, OrderHistory B where A.product_id = B.product_id AND UserID = @User_ID", con);
            cmd.Parameters.AddWithValue("@User_ID", str);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        /// <summary>
        /// logs the user out
        /// </summary>
        protected void Button2_Click1(object sender, EventArgs e)
        {
            Session["UserID"] = null;
            Response.Redirect("~/Forms/default.aspx");
        }

        protected void admin_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/AddItems.aspx");
        }
    }
}