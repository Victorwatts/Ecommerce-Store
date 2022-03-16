<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CartForm.aspx.cs" Inherits="BCAD3___TASK_2.Forms.CartForm" %>

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
   padding: 14px 16px;
    margin-left:30px;
}
.button_logout:hover {
  background: rgba(255, 0, 0, 0.7);
  text-shadow: 0 1px 3px rgba(70, 93, 41, 0.7);
}

.button_delete {
  background: rgba(255,0, 0,1);
  border: none;
  color: #fff;
  cursor: pointer;
  font-size: 17px;
  outline: none;
   padding: 14px 16px;
   
}
.button_delete:hover {
  background: rgba(139,0,0 ,1 );
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
    margin:auto;
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
        <a href="CheckoutForm.aspx">CHECKOUT</a>
  <a class="active"">CART</a>
              <a href="OrderHistoryForm.aspx">Order History</a>
             
             <asp:Button ID="Button2" runat="server" Text="LOGOUT" OnClick="Button2_Click1" CssClass="button_logout"/>
              <asp:Button ID="btn_admin" Visible="false" runat="server" Text="ADMIN CONTROLS" OnClick="admin_Click1" CssClass="button_logout"/>
</div>
        
         <div class="formc">
        <div class="center">
             <div>
                  <h2><asp:Label ID="Label2" runat="server" Text="Remove Items From Your Cart Or Go To Check Out"></asp:Label></h2>
                 <br />
                  <br />
            <asp:GridView ID="gvDetails" DataKeyNames="Cart_ID" runat="server"
AutoGenerateColumns="False" CssClass="Gridview" HeaderStyle-BackColor="#61A6F8"
ShowFooter="True" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White"
onrowdeleting="gvDetails_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="Both">
                <AlternatingRowStyle BackColor="White" />
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:Button ID="imgbtnDelete" CssClass="button_delete"  CommandName="Delete" Text="Remove Item" runat="server" ToolTip="Delete"  />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Cart_ID">
<EditItemTemplate>
<asp:TextBox ID="txtID" runat="server" Text='<%#Eval("Cart_ID") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblID" runat="server" Text='<%#Eval("Cart_ID") %>'/>
</ItemTemplate>

</asp:TemplateField>
<asp:TemplateField HeaderText="PRODUCT_NAME">
<EditItemTemplate>
<asp:TextBox ID="txtPRODUCT_NAME" runat="server" Text='<%#Eval("PRODUCT_NAME") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblPRODUCT_NAME" runat="server" Text='<%#Eval("PRODUCT_NAME") %>'/>
</ItemTemplate>

</asp:TemplateField>
<asp:TemplateField HeaderText="PRODUCT_PRICE">
<EditItemTemplate>
<asp:TextBox ID="txtPRODUCT_PRICE" runat="server" Text='<%#Eval("PRODUCT_PRICE") %>'/>
</EditItemTemplate>

<ItemTemplate>
<asp:Label ID="lblPRODUCT_PRICE" runat="server" Text='<%#Eval("PRODUCT_PRICE") %>'/>
</ItemTemplate>
</asp:TemplateField>
</Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle  BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
                 <br />
                  <asp:Label ID="lblresult" runat="server" Text=""></asp:Label>
                 <br />
                 <br />
                  <asp:Button ID="Button1" runat="server" Text="CHECKOUT" CssClass="buttons" OnClick="Button1_Click" />
</div><br />
            <br />
        </div>
             </div>
       
    </form>
</body>
</html>
