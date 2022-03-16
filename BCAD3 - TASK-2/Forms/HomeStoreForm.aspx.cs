
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BCAD3___TASK_2.Forms
{
    public partial class HomeStoreForm : System.Web.UI.Page
    {
        public System.Web.UI.HtmlControls.HtmlGenericControl createRow = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        public System.Web.UI.HtmlControls.HtmlGenericControl createRowX = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");


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


            /// <summary>
            /// fetches and displays all the products
            /// </summary>
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT PRODUCT_ID, Product_Name, PRODUCT_DESCRIPTION, PRODUCT_PRICE, PRODUCT_IMAGE from PRODUCTS", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                sda.Fill(dt);

                int count = 0;
                //dynamically add the products and the needed controls to the website using a repeater
                //System.Web.UI.HtmlControls.HtmlGenericControl createRow = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                createRow.Attributes.Add("class", "row");
                Form.Controls.Add(createRow);
                foreach (DataRow row in dt.Rows)
                {
                    byte[] bytes = (byte[])row["PRODUCT_IMAGE"];
                    string strBase64 = Convert.ToBase64String(bytes);
                    ProductDetails.PRODUCT_IMAGE = "data:Image/png;base64," + strBase64;
                    ProductDetails.PRODUCT_NAME = row["PRODUCT_NAME"].ToString();
                    ProductDetails.PRODUCT_DESCRIPTION = row["PRODUCT_DESCRIPTION"].ToString();
                    ProductDetails.PRODUCT_PRICE = Convert.ToInt16(row["PRODUCT_PRICE"]);
                    ProductDetails.ID = Int32.Parse(row["PRODUCT_ID"].ToString());
                    Button Slct = new Button();
                    Slct.Text = "Add To Cart";
                    Slct.ID = row["PRODUCT_ID"].ToString();
                    Slct.Click += new EventHandler(btnclick_Click);
                    Label ITEM = new Label();
                    ITEM.Font.Underline = true;
                    ITEM.Text = "ITEM NAME";
                    Label DESC = new Label();
                    DESC.Font.Underline = true;
                    DESC.Text = "DESCRIPTION";
                    Label Price = new Label();
                    Price.Font.Underline = true;
                    Price.Text = "PRICE: ";
                    Label UNIT = new Label();
                    UNIT.Text = "R";
                    Label lb1 = new Label();
                    lb1.Text = row["Product_Name"].ToString();
                    Label lb2 = new Label();
                    lb2.Text = row["PRODUCT_DESCRIPTION"].ToString();
                    Label lb3 = new Label();
                    lb3.Text = row["PRODUCT_PRICE"].ToString();
                    Image tb = new Image();
                    Label ItemID = new Label();
                    ItemID.Text = ID.ToString();
                    tb.ImageUrl = "data:Image/png;base64," + strBase64;
                    tb.Attributes.Add("class", "img");
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    System.Web.UI.HtmlControls.HtmlGenericControl cardDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    createDiv.Attributes.Add("class", "column");
                    cardDiv.Attributes.Add("class", "card");
                    createRow.Controls.Add(createDiv);
                    createDiv.Controls.Add(cardDiv);
                    Slct.Attributes.Add("class", "buttons");
                    cardDiv.Controls.Add(tb);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(ITEM);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(lb1);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(DESC);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(lb2);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(Price);
                    cardDiv.Controls.Add(new LiteralControl("&nbsp;"));
                    cardDiv.Controls.Add(UNIT);
                    cardDiv.Controls.Add(lb3);
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(new LiteralControl("<br />"));
                    cardDiv.Controls.Add(Slct);
                    count++;

                }



                con.Close();





            }

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            pnlAlertBox.Visible = false;
        }
        /// <summary>
        /// Calls the required methods
        /// </summary>

        protected void btnclick_Click(object sender, EventArgs e)
        {
            pnlAlertBox.Visible = true;
        }
        //logout
        protected void Button1_Click1(object sender, EventArgs e)
        {
            Session["USER"] = null;
            Response.Redirect("~/Forms/UserLogin.aspx");
        }

        /// <summary>
        ///fetches and displays products according to the category
        /// </summary>
        protected void Button2_Click2(object sender, EventArgs e)
        {
            Form.Controls.Remove(createRow);
            Form.Controls.Remove(createRowX);

            // gets and displays all the saved products
            // gets and displays all the saved products
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.ConnectoinString))
            {

                SqlCommand cmd = new SqlCommand("dbo.GetProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@cat_id    ", SqlDbType.Int).Value = Category.SelectedItem.Value;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                int count = 0;

                try
                {
                    con.Open();
                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                    if (response == "Success")
                    {
                        //dynamically add the products and the needed controls to the website using a repeater

                        //System.Web.UI.HtmlControls.HtmlGenericControl createRow = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                        createRowX.Attributes.Add("class", "row");
                        Form.Controls.Add(createRowX);
                        foreach (DataRow row in dt.Rows)
                        {
                            byte[] bytes = (byte[])row["PRODUCT_IMAGE"];
                            string strBase64 = Convert.ToBase64String(bytes);
                            ProductDetails.PRODUCT_IMAGE = "data:Image/png;base64," + strBase64;
                            ProductDetails.PRODUCT_NAME = row["PRODUCT_NAME"].ToString();
                            ProductDetails.PRODUCT_DESCRIPTION = row["PRODUCT_DESCRIPTION"].ToString();
                            ProductDetails.PRODUCT_PRICE = Convert.ToInt16(row["PRODUCT_PRICE"]);
                            ProductDetails.ID = Int32.Parse(row["PRODUCT_ID"].ToString());
                            Button Slct = new Button();
                            Slct.Text = "Add To Cart";
                            Slct.ID = row["PRODUCT_ID"].ToString();
                            Slct.Click += new EventHandler(btnclick_Click);
                            Label ITEM = new Label();
                            ITEM.Font.Underline = true;
                            ITEM.Text = "ITEM NAME";
                            Label DESC = new Label();
                            DESC.Font.Underline = true;
                            DESC.Text = "DESCRIPTION";
                            Label Price = new Label();
                            Price.Font.Underline = true;
                            Price.Text = "PRICE: ";
                            Label UNIT = new Label();
                            UNIT.Text = "R";
                            Label lb1 = new Label();
                            lb1.Text = row["Product_Name"].ToString();
                            Label lb2 = new Label();
                            lb2.Text = row["PRODUCT_DESCRIPTION"].ToString();
                            Label lb3 = new Label();
                            lb3.Text = row["PRODUCT_PRICE"].ToString();
                            Image tb = new Image();
                            Label ItemID = new Label();
                            ItemID.Text = ID.ToString();
                            tb.ImageUrl = "data:Image/png;base64," + strBase64;
                            tb.Attributes.Add("class", "img");
                            System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                            System.Web.UI.HtmlControls.HtmlGenericControl cardDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                            createDiv.Attributes.Add("class", "column");
                            cardDiv.Attributes.Add("class", "card");
                            Slct.Attributes.Add("class", "buttons");
                            createRowX.Controls.Add(createDiv);
                            createDiv.Controls.Add(cardDiv);
                            cardDiv.Controls.Add(tb);
                            cardDiv.Controls.Add(new LiteralControl("<br />"));
                            cardDiv.Controls.Add(new LiteralControl("<br />"));
                            cardDiv.Controls.Add(ITEM);
                            cardDiv.Controls.Add(new LiteralControl("<br />"));
                            cardDiv.Controls.Add(lb1);
                            cardDiv.Controls.Add(new LiteralControl("<br />"));
                            cardDiv.Controls.Add(new LiteralControl("<br />"));
                            cardDiv.Controls.Add(DESC);
                            cardDiv.Controls.Add(new LiteralControl("<br />"));
                            cardDiv.Controls.Add(lb2);
                            cardDiv.Controls.Add(new LiteralControl("<br />"));
                            cardDiv.Controls.Add(new LiteralControl("<br />"));
                            cardDiv.Controls.Add(Price);
                            cardDiv.Controls.Add(new LiteralControl("&nbsp;"));
                            cardDiv.Controls.Add(UNIT);
                            cardDiv.Controls.Add(lb3);
                            cardDiv.Controls.Add(new LiteralControl("<br />"));
                            cardDiv.Controls.Add(new LiteralControl("<br />"));
                            cardDiv.Controls.Add(Slct);
                            count++;
                        }
                        con.Close();
                    }
                    else
                    {

                    }
                }
                catch (Exception)
                {

                }



            }
        }

        /// <summary>
        /// is only available to admins
        /// </summary>
        protected void btnLog_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/default.aspx");
        }
    }
}
