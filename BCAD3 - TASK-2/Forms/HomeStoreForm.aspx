<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeStoreForm.aspx.cs" Inherits="BCAD3___TASK_2.Forms.HomeStoreForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" rel="stylesheet"/>
    <title></title>
    <style>
* {
  box-sizing: border-box;
}

body {
  
   background: #384047;
    font-family: sans-serif;
}

 .auto-style1 {
            position: relative;
            width: 800px;
            left: 0px;
            top: 0px;
            margin: 30px auto;
            text-align:center;
        }

.buttons {
  background: rgba(148, 186, 101, 0.7);
  box-shadow: 0 3px 0 0 rgba(123, 163, 73, 0.7);
  border-radius: 2px;
  border: none;
  color: #fff;
  cursor: pointer;
  font-size: 1em;
  width:30%;
  min-width:90px;
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

.button_search {
  background: #94af65;
  border: none;
  color: #fff;
  cursor: pointer;
  font-size: 17px;
  outline: none;
  float:right;
   padding: 14px 16px;
}
.button_search:hover {
  background:rgb(50,205,50);
  text-shadow: 0 1px 3px rgba(70, 93, 41, 0.7);
}

.filter {
  background: grey;
  border:none;
  color: #fff;
  cursor: pointer;
  font-size: 17px;
  outline: none;
  float:right;
   padding: 13.5px 16px;
 
}

.filterlabel {
  border:none;
  color: #fff;
  font-size: 17px;
  outline: none;
  float:right;
   padding: 13.5px 16px;
 
}
.img{
    height:150px;
}

/* Float four columns side by side */
.column {
  float: left;
  width: 25%;
  padding:10px;
}

/* Remove extra left and right margins, due to padding in columns */
.row {margin: 0 -5px;
}

/* Clear floats after the columns */
.row:after {
  content: "";
  display: table;
  clear: both;
}

/* Style the counter cards */
.card {
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2); /* this adds the "card" effect */
  padding: 16px;
  text-align: center;
  background-color: #f1f1f1;
}

/* Responsive columns - one column layout (vertical) on small screens */
@media screen and (max-width: 815px) {
  .column {
    width: 100%;
    display: block;
    margin-bottom: 20px;
  }
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
        <br/>
    <form id="form1" runat="server">

         <div class="topnav">
  <a>User: GUEST</a>
  <a class="active"">STORE PAGE</a>
         <a href="default.aspx">LOGIN</a>
         <asp:Button ID="Button2" runat="server" class="button_search" Text="SEARCH" OnClick="Button2_Click2" />
   <asp:DropDownList class="filter" ID="Category" runat="server"></asp:DropDownList>
   <asp:Label class="filterlabel" ID="Label4" runat="server" Text="SEARCH FILTER:"></asp:Label>
 
</div>
        
        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        
       
   <asp:Repeater id="someRep"  runat="server" >
       <ItemTemplate>
                <img src ="<%# Eval("PRODUCT_IMAGE") %> "></img> 
              <h1><%# Eval("PRODUCT_NAME") %></h1>
              <h2><%# Eval("PRODUCT_DESCRIPTION") %></h2>
              <h3><%# Eval("PRODUCT_PRICE") %></h3>
           <asp:Button ID="Ran" Text="Add To Cart"  OnCclick="btnclick_Click" CssClass="buttons" runat="server"></asp:Button>
              

          
    </ItemTemplate>
    </asp:Repeater>


         <asp:Panel ID="pnlAlertBox" Visible="false" runat="server" style="position:absolute; top:10%; left:25%;"> 
        <div class="auto-style1">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">You need to be logged in to add products to your cart</h4>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnOk" runat="server" style="float:left" CssClass="buttons" Text="Close" OnClick="btnOk_Click" />
                     <asp:Button ID="ButtonLog" runat="server" style="float:right" CssClass="buttons" Text="Login" OnClick="btnLog_Click" />
                </div>
            </div>
        </div>
</asp:Panel>
    </form>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
</body>
</html>
