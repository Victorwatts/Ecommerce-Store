<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddItems.aspx.cs" Inherits="BCAD3___TASK_2.Forms.AddItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>

         body {
  background: #384047;
  font-family: sans-serif;
  font-size: 10px;
}

.formc {
  background: #fff;
  padding: 4em 4em 2em;
  max-width: 600px;
  margin: 50px auto 0;
  box-shadow: 0 0 1em #222;
  border-radius: 2px;
}
form h2 {
  margin: 0 0 50px 0;
  padding: 10px;
  text-align: center;
  font-size: 30px;
  color: #666666;
  border-bottom: solid 1px #e5e5e5;
}

.button_logout {
  background: rgba(255, 90, 0,0.7);
  border: none;
  color: #fff;
  cursor: pointer;
  font-size: 17px;
  outline: none;
  float:right;
   width: 10%;
   padding: 14px 16px;
    margin-left:30px;
}
.button_logout:hover {
  background: rgba(255, 0, 0, 0.7);
  text-shadow: 0 1px 3px rgba(70, 93, 41, 0.7);
}
form p {
  margin: 0 0 3em 0;
  position: relative;
}
form input {
  display: block;
  box-sizing: border-box;
  width: 100%;
  outline: none;
  margin: 0;
}
form input[type="text"]{
  background: #fff;
  border: 1px solid #dbdbdb;
  font-size: 1.6em;
  padding: .8em .5em;
  border-radius: 2px;
}
form input[type="text"]:focus,
form input[type="password"]:focus {
  background: #fff;
}
formc span {
  display: block;
  background: #F9A5A5;
  padding: 2px 5px;
  color: #666;
}
.buttons {
  background: rgba(148, 186, 101, 0.7);
  box-shadow: 0 3px 0 0 rgba(123, 163, 73, 0.7);
  border-radius: 2px;
  border: none;
  color: #fff;
  cursor: pointer;
  display: block;
  font-size: 2em;
  line-height: 1.6em;
  margin: 2em 0 0;
  outline: none;
  padding: .8em 0;
  text-shadow: 0 1px #68B25B;
}
.buttons:hover {
  background: #94af65;
  text-shadow: 0 1px 3px rgba(70, 93, 41, 0.7);
}
form label {
  position: absolute;
  left: 8px;
  top: 12px;
  color: #999;
  font-size: 16px;
  display: inline-block;
  padding: 4px 10px;
  font-weight: 400;
  background-color: rgba(255, 255, 255, 0);
  -moz-transition: color 0.3s, top 0.3s, background-color 0.8s;
  -o-transition: color 0.3s, top 0.3s, background-color 0.8s;
  -webkit-transition: color 0.3s, top 0.3s, background-color 0.8s;
  transition: color 0.3s, top 0.3s, background-color 0.8s;
}
form label.floatLabel {
  top: -11px;
  background-color: rgba(255, 255, 255, 0.8);
  font-size: 14px;
}


/* Add a right margin to each icon */
.fa {
  margin-left: -12px;
  margin-right: 8px;
}
.topnav {
  overflow: hidden;
  background-color: #333;
}

.topnav a {
  float: left;
  color: #f2f2f2;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  font-size: 17px;
}

.topnav a:hover {
  background-color: #ddd;
  color: black;
}

.topnav a.active {
  background-color: #3368FF;
  color: white;
}
.Gridview
{
font-family:Verdana;
font-size:10pt;
font-weight:normal;
color:black;
}


        </style>

</head>
<body style="height: 640px">
   
     
        <br/>

    <form id="form1" runat="server">
        <div class="topnav">
         <a>User: <asp:Label ID="Label1" runat="server" Text=""></asp:Label></a>
         <a class="active">ADD PRODUCTS</a>
         <a href="EditProductsForm.aspx">EDIT PRODUCTS</a>
         <a href="ProductOrderedHistoryForm.aspx">PRODUCT ORDERD HISTORY</a>
         <a href="HomeStoreLoggedForm.aspx">STORE PAGE</a>
         <asp:Button ID="Button1" runat="server" Text="LOGOUT" OnClick="Button1_Click" CssClass="button_logout" />
</div>
         
        <br/>
       <div class="formc">
        <div >
            <h2>Add Product</h2>
         <p>   
        <label for="lblItemName" class="floatLabel">Item Name</label>
        <asp:TextBox ID="ItemName" runat="server" name="lblItemName" OnTextChanged="ItemName_TextChanged" type="text"></asp:TextBox>
        </p>
        <p>
        <label for="lblItemDesc" class="floatLabel">Item Description</label>
        <asp:TextBox ID="ItemDescription" runat="server" OnTextChanged="ItemDescription_TextChanged" type="text"></asp:TextBox>
        </p>
         <p>
        <label for="lblItemPrice" class="floatLabel">Item Price</label>
        <asp:TextBox ID="ItemPrice" runat="server" OnTextChanged="ItemPrice_TextChanged" type="text"></asp:TextBox>
        </p>
        <p>
        <label for="lblCategory" class="floatLabel">Item Category</label>
             <br />
             <br />
            <asp:DropDownList ID="Category" runat="server" OnSelectedIndexChanged="Category_SelectedIndexChanged">
            </asp:DropDownList>
        </p>
        <p>
        <label for="lblupload" class="floatLabel">Choose Item Picture</label>
            <br />
             <br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        </p>
        <asp:Button ID="btnAddItem" runat="server" Text="Add Item" class="buttons" OnClick="BtnAddItem_Click" />
       
         <asp:Label ID="Label3" runat="server" Text="" Visible="false"></asp:Label>

        <br />
            </div>
           </div>
          
    </form>
</body>
</html>

