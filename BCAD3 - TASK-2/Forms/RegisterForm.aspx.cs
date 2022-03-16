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
    public partial class RegisterForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// calls the validation method
        /// </summary>
        protected void Submit_Click(object sender, EventArgs e)
        {
            Validation();
        }

        /// <summary>
        /// Validates the users input
        /// If valid AddUser methode is called
        /// </summary>
        private void Validation()
        {
            string x;
            if (string.IsNullOrWhiteSpace(Username.Text))
            {

                x = "Enter username!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;

            }
            else if (string.IsNullOrWhiteSpace(password.Text))
            {

                x = "Enter password!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else if (confirm_password.Text == "")
            {

                x = "Enter confirm password!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else if (confirm_password.Text != password.Text)
            {

                x = "Passwords do not match!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else
            {
                Adduser();
            }

        }


        /// <summary>
        /// Adds user to the database
        /// the password is hashed by the procedure
        /// the password is also saved with a salt
        /// 
        ///  INSERT INTO dbo.[USERS] (UserName, Upassword, Salt)
        ///  VALUES(@username, HASHBYTES('SHA2_512', @upassword+CAST(@salt AS NVARCHAR(36))), @salt)
        ///  
        /// Full procedure can be found in the script
        /// </summary>
        private void Adduser()
        {

            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString))
            {

                string username = Username.Text.ToString(); ;
                string Password = password.Text.ToString();

                SqlCommand cmd = new SqlCommand("dbo.AddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@username    ", SqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@isadmin    ", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@upassword  ", SqlDbType.VarChar).Value = Password;
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
                        Response.Redirect("~/Forms/default.aspx");
                    }
                    else
                    {
                        LabelAlert.Text = "User sign up failed";
                        LabelAlert.Visible = true;
                    }


                }
                catch (Exception)
                {

                }



            }


        }
        /// <summary>
        /// user can navigate back to login if they don't need to register
        /// </summary>
        protected void GoToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/default.aspx");
        }
    }
}
    
