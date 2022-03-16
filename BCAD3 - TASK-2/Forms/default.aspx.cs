using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCAD3___TASK_2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            Validation();
        }
        /// <summary>
        /// Validates the users input
        /// If valid LoginUser method is called
        /// </summary>
        private void Validation()
        {
            string x;
            if (string.IsNullOrWhiteSpace(Username.Text))
            {
                //MessageBox.Show("Enter username!");
                x = "Enter username!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;

            }
            else if (string.IsNullOrWhiteSpace(password.Text))
            {
                //MessageBox.Show("Enter password!");
                x = "Enter password!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else
            {
                LoginUser();
            }

        }
        /// <summary>
        /// The password and login are sent to the sql db where the are authenticated
        /// the db sends a response back to say if the user can login or not
        /// </summary>
        private void LoginUser()
        {

            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString))
            {

                string username = Username.Text.ToString(); ;
                string Password = password.Text.ToString();

                SqlCommand cmd = new SqlCommand("dbo.userLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@loginname    ", SqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@upassword  ", SqlDbType.NVarChar).Value = Password;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                    if (response == "User successfully logged in")
                    {
                        ///gets the user id once they have logged in, the id is stored in the class library for later reference.
                        SqlCommand cmdUid = new SqlCommand("dbo.GetUserID", con);
                        cmdUid.CommandType = CommandType.StoredProcedure;
                        cmdUid.Parameters.Add("@loginname  ", SqlDbType.VarChar).Value = username;
                        cmdUid.Parameters.Add("@upassword  ", SqlDbType.NVarChar).Value = Password;
                        var resultParamId = cmdUid.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                        resultParamId.Direction = ParameterDirection.Output;
                        try
                        {
                            con.Open();
                            int I = (int)cmdUid.ExecuteScalar();
                            con.Close();
                            Session["UserID"] = I;
                            System.Diagnostics.Debug.WriteLine(I);
                            Session["User"] = username;
                            response = resultParamId.Value.ToString();


                            int str = (int)Session["UserID"];
                            SqlCommand cmdAdmin = new SqlCommand("dbo.GetUserPrivilage", con);
                            cmdAdmin.CommandType = CommandType.StoredProcedure;
                            cmdAdmin.Parameters.Add("@user_id  ", SqlDbType.Int).Value = str;
                            var resultParamIdA = cmdAdmin.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                            resultParamIdA.Direction = ParameterDirection.Output;
                            try
                            {
                                con.Open();
                               var x = cmdAdmin.ExecuteScalar();
                                con.Close();
                                Session["Admin"] = x;
                                System.Diagnostics.Debug.WriteLine(x);

                            }
                            catch (Exception)
                            {


                            }


                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Fail");
                        }

                        


                        if (Session["Admin"].Equals(true))
                        {
                            Response.Redirect("~/Forms/AddItems.aspx");
                        }
                        else
                        {
                            Response.Redirect("~/Forms/HomeStoreLoggedForm.aspx");
                        }


                    }
                    else if (response == "Invalid login")
                    {
                        LabelAlert.Text = "Username or password is incorrect";
                        LabelAlert.Visible = true;
                    }
                    else if (response == "Incorrect password")
                    {
                        LabelAlert.Text = "Password is incorrect";
                        LabelAlert.Visible = true;
                    }

                }
                catch (Exception)
                {

                }



            }


        }

      

        /// <summary>
        /// will navigate the user to the register page 
        /// </summary>
        protected void GoToRegister_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
     "err_msg",
     "alert('Dispatch assignment saved, but you forgot to click Confirm or Cancel!)');",
     true);
            Response.Redirect("~/Forms/RegisterForm.aspx");
        }
    }
}

