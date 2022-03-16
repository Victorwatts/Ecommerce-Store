<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckoutForm.aspx.cs" Inherits="BCAD3___TASK_2.Forms.CheckoutForm" %>

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
  max-width: 400px;
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
   padding: 14px 16px;
    margin-left:30px;
}
.button_logout:hover {
  background: rgba(255, 0, 0, 0.7);
  text-shadow: 0 1px 3px rgba(70, 93, 41, 0.7);
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
  margin: auto;
  outline: none;
  padding: .8em 0;
  text-shadow: 0 1px #68B25B;
  
}
.buttons:hover {
  background: #94af65;
  text-shadow: 0 1px 3px rgba(70, 93, 41, 0.7);
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

.center {
  margin: auto;
  text-align:center;
}
         
    </style>

</head>

<body style="height: 640px">
    

     <br/>
    <form id="form1" runat="server">
        <div class="topnav">
        <a>User: <asp:Label ID="Label3" runat="server" Text=""></asp:Label></a>
  <a href="HomeStoreLoggedForm.aspx">STORE PAGE</a>
  <a class="active"">CHECKOUT</a>
  <a href="CartForm.aspx">CART</a>
             <a href="OrderHistoryForm.aspx">Order History</a>
            
<asp:Button ID="Button2" runat="server" Text="LOGOUT" OnClick="Button2_Click1" CssClass="button_logout"/>
             <asp:Button ID="btn_admin" Visible="false" runat="server" Text="ADMIN CONTROLS" OnClick="admin_Click1" CssClass="button_logout"/>
            </div>
        
        <div class="formc">
        <div class="center">
            <h1>Checkout</h1>
            <h3>Total R:<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </h3><br />
            <asp:Button ID="Button1" runat="server" Text="MAKE PAYMENT" CssClass="buttons" OnClick="Button1_Click" />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        </div>
            </div>
        
    </form>
</body>
</html>
