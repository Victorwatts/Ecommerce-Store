using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCAD3___TASK_2.Forms
{
    public partial class AddItems : System.Web.UI.Page
    {
        /// <summary>
        /// refreshes the bage if the user tries clicking back after logging out
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

            /// <summary>
            /// gets a list of the current available categories
            /// </summary>
            if (!Page.IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString))
                {

                    SqlCommand cmd = new SqlCommand("SELECT CATEGORY_ID, Category_Name from CATEGORY", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    // cmd.CommandType = CommandType.Text;
                    DataTable dt = new DataTable();
                    try

                    {
                        con.Open();
                        sda.Fill(dt);
                        con.Close();
                        Category.DataSource = dt;
                        Category.DataBind();

                        Category.DataTextField = "Category_Name";
                        Category.DataValueField = "CATEGORY_ID";
                        Category.DataBind();
                        Category.Items.Insert(0, new ListItem("select", "1"));

                    }
                    catch (Exception)
                    {

                    }






                }
            }
           
        }

        protected void ItemName_TextChanged(object sender, EventArgs e)
        {
            Label3.Visible = false;
        }

        protected void ItemPrice_TextChanged(object sender, EventArgs e)
        {
            Label3.Visible = false;
        }
      
        protected void ItemDescription_TextChanged(object sender, EventArgs e)
        {
            Label3.Visible = false;
        }

        protected void SearchProduct_TextChanged(object sender, EventArgs e)
        {
            Label3.Visible = false;
        }

        protected void Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label3.Visible = false;
        }



        /// <summary>
        /// Validates the users input and prompts them to fix any errors
        /// </summary>
        public void InputValidation()
        {
            if (ItemName.Text == "")
            {
                Label3.Text = "Enter an item name!";
                Label3.ForeColor = System.Drawing.Color.Red;
                Label3.Visible = true;
                return;
            }

            else if (ItemDescription.Text == "")
            {
                Label3.Text = "Enter an item description!";
                Label3.ForeColor = System.Drawing.Color.Red;
                Label3.Visible = true;
                return;
            }

            else if (ItemPrice.Text == "")
            {
                Label3.Text = "Enter an item price!";
                Label3.ForeColor = System.Drawing.Color.Red;
                Label3.Visible = true;
                return;
            }

            else if (Category.SelectedValue == "")
            {
                Label3.Text = "Select an item category!";
                Label3.ForeColor = System.Drawing.Color.Red;
                Label3.Visible = true;
                return;
            }

            else if (!FileUpload1.HasFile)
            {
                Label3.Text = "Add an item image!";
                Label3.ForeColor = System.Drawing.Color.Red;
                Label3.Visible = true;
                return;
            }
            else
                Label3.Visible = false;
            data();

        }

        /// <summary>
        /// saves new products to the database
        /// </summary>
        public void data()
        {

            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString))
            {

                SqlCommand cmd = new SqlCommand("dbo.AddProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@cat_id    ", SqlDbType.Int).Value = Category.SelectedItem.Value;
                cmd.Parameters.Add("@product_name    ", SqlDbType.VarChar).Value = ItemName.Text;
                cmd.Parameters.Add("@product_description  ", SqlDbType.VarChar).Value = ItemDescription.Text;
                cmd.Parameters.Add("@product_price  ", SqlDbType.Decimal).Value = ItemPrice.Text;
                int Length = FileUpload1.PostedFile.ContentLength;
                byte[] pic = new byte[Length];
                FileUpload1.PostedFile.InputStream.Read(pic, 0, Length);
                cmd.Parameters.Add("@product_img  ", SqlDbType.Image).Value = pic;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);

                    if (response == "Success")
                    {
                        Label3.Text = "Item successfully added";
                        Label3.ForeColor = System.Drawing.Color.Green;
                        Label3.Visible = true;
                    }
                    else
                    {
                        Label3.Text = "Error";
                        Label3.ForeColor = System.Drawing.Color.Red;
                        Label3.Visible = true;
                    }


                }
                catch (Exception)
                {

                }
               
            }
        }



        /// <summary>
        /// validates the admins inputs before adding the tiem to the db
        /// </summary>
        protected void BtnAddItem_Click(object sender, EventArgs e)
        {

            InputValidation();

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