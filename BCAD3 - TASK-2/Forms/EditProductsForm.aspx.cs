using System;
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
    public partial class EditProductsForm : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString);
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

        /// <summary>
        /// checks that the user session is valid
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // checks that an admin is logged in
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

            if (!IsPostBack)
            {
                BindGridview();
            }
        }


        /// <summary>
        /// populates the gridview with available products
        /// </summary>
        protected void BindGridview()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESCRIPTION, PRODUCT_PRICE from PRODUCTS", con);
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
        //allows for editing gridview
        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindGridview();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int PRODUCT_ID = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Value.ToString());
            TextBox txtPRODUCT_NAME = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtPRODUCT_NAME");
            TextBox txtPRODUCT_DESCRIPTION = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtPRODUCT_DESCRIPTION");
            TextBox txtPRODUCT_PRICE = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtPRODUCT_PRICE");

            con.Open();
            SqlCommand cmd = new SqlCommand("update PRODUCTS set PRODUCT_NAME='" + txtPRODUCT_NAME.Text + "', PRODUCT_DESCRIPTION='" + txtPRODUCT_DESCRIPTION.Text + "', PRODUCT_PRICE='" + txtPRODUCT_PRICE.Text + "'where PRODUCT_ID=" + PRODUCT_ID, con);
            cmd.ExecuteNonQuery();
            con.Close();
            lblresult.ForeColor = Color.Green;
            lblresult.Text = ID + " Details Updated successfully";
            gvDetails.EditIndex = -1;
            BindGridview();

        }
        //allows for canceling the edit
        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            BindGridview();
        }

        //delete a product
        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int PRODUCT_ID = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["PRODUCT_ID"].ToString());

            con.Open();
            SqlCommand cmdcart = new SqlCommand("delete from CART where PRODUCT_ID=" + PRODUCT_ID, con);
            cmdcart.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand("delete from PRODUCTS where PRODUCT_ID=" + PRODUCT_ID, con);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result == 1)
            {
                BindGridview();
                lblresult.ForeColor = Color.Red;
                lblresult.Text = PRODUCT_ID + " details deleted successfully";
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