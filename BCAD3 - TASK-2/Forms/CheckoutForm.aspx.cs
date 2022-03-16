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
     
    public partial class CheckoutForm : System.Web.UI.Page
    {
        public int p_ID;

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
            //checks a user is logged in
            if (Session["Admin"].Equals(true))
            {
                btn_admin.Visible = true;
            }

            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Forms/default.aspx");
            }
            else
                //gets user name
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

            /// <summary>
            /// calculates total cost of all products
            /// </summary>
            using (SqlConnection con1 = new SqlConnection(Properties.Settings.Default.ConnectoinString))
            {
                int str = (int)Session["UserID"];
                SqlCommand cmd = new SqlCommand("dbo.GetCartTotal", con1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@User_ID    ", SqlDbType.Int).Value = str;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try

                {
                    con1.Open();
                    // cmd.ExecuteNonQuery();
                    // int SUM = (int)cmd.ExecuteScalar();
                    object result = cmd.ExecuteScalar();
                    con1.Close();
                    Label1.Text = result.ToString();//SUM.ToString();

                }
                catch (Exception)
                {
                    Label2.Text = "Add Items To Cart First";
                    Label2.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        /// <summary>
        /// acts as the payment portal
        /// </summary>
        protected void Button1_Click(object sender, EventArgs e)
        {
            GetCart();


            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString))
            {


                int str = (int)Session["UserID"];
                SqlCommand cmd = new SqlCommand("delete from CART where UserID = @User_ID", con);
                cmd.Parameters.AddWithValue("@User_ID", str);

                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    Label1.Text = "0";
                    Label2.Text = "PAYMENT SUCCESSFUL";
                    Label2.ForeColor = System.Drawing.Color.Green;
                    con.Close();
                }
                catch (Exception)
                {
                    Label2.Text = "PAYMENT UNSUCCESSFUL";
                    Label2.ForeColor = System.Drawing.Color.Red;
                }

            }


        }

        /// <summary>
        /// methods used for adding all the needed data to the different tables for order history and category orders
        /// </summary>
        public void GetCart()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString))
            {
                
                int str = (int)Session["UserID"];
                SqlCommand cmdCart = new SqlCommand("SELECT PRODUCT_ID FROM CART WHERE UserID = @User_ID ", con);
                cmdCart.Parameters.AddWithValue("@User_ID", str);
                try
                {

                    con.Open();
                    cmdCart.ExecuteNonQuery();
                    p_ID = (int)cmdCart.ExecuteScalar();
                    con.Close();

                    SqlCommand cmdOrder = new SqlCommand("INSERT INTO OrderHistory (UserID, PRODUCT_ID, ORDER_DATE) VALUES (@User_ID, @Product_ID, @Order_Date)", con);
                    cmdOrder.Parameters.AddWithValue("@User_ID", str);
                    cmdOrder.Parameters.AddWithValue("@Product_ID", p_ID);
                    cmdOrder.Parameters.AddWithValue("@Order_Date", DateTime.Now);
                    cmdOrder.CommandType = CommandType.Text;
                    try
                    {

                        con.Open();
                        cmdOrder.ExecuteNonQuery();
                        con.Close();
                        AddCat_History();
                    }
                    catch (Exception)
                    {
                        System.Diagnostics.Debug.WriteLine("order UNSUCCESSFUL");
                    }
                   
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("cart UNSUCCESSFUL");
                }
               


            }
        }


        public void AddCat_History()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString))
            {

                int str = (int)Session["UserID"];
                SqlCommand cmd = new SqlCommand("SELECT CATEGORY_ID FROM PRODUCT_CATEGORY WHERE PRODUCT_ID = @product_ID ", con);
                cmd.Parameters.AddWithValue("@product_ID", p_ID);
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    int category_ID = (int)cmd.ExecuteScalar();
                    con.Close();

                    SqlCommand cmdCart = new SqlCommand("SELECT Order_ID FROM ORDERHISTORY WHERE USERID = @userid AND PRODUCT_ID = @productid ", con);
                    cmdCart.Parameters.AddWithValue("@userid", str);
                    cmdCart.Parameters.AddWithValue("@productid", p_ID);
                    try
                    {

                        con.Open();
                        cmdCart.ExecuteNonQuery();
                        int orderID = (int)cmdCart.ExecuteScalar();
                        con.Close();

                        SqlCommand cmdOrder = new SqlCommand("INSERT INTO CATEGORY_HISTORY (CATEGORY_ID, Order_ID, Date_Order) VALUES (@cat_ID, @order_ID, @Order_Date)", con);
                        cmdOrder.Parameters.AddWithValue("@cat_ID", category_ID);
                        cmdOrder.Parameters.AddWithValue("@order_ID", orderID);
                        cmdOrder.Parameters.AddWithValue("@Order_Date", DateTime.Now);
                        cmdOrder.CommandType = CommandType.Text;
                        try
                        {

                            con.Open();
                            cmdOrder.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception)
                        {
                            System.Diagnostics.Debug.WriteLine("cat UNSUCCESSFUL");
                        }
                    }
                    catch (Exception) { 
                    }


                      

                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("cart UNSUCCESSFUL");
                }



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

        /// <summary>
        /// is only available to admins
        /// </summary>

        protected void admin_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/AddItems.aspx");
        }
    }
}
