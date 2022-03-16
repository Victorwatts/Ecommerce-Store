<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductOrderedHistoryForm.aspx.cs" Inherits="BCAD3___TASK_2.Forms.ProductOrderedHistoryForm" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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

  body {
  background: #384047;
  font-family: sans-serif;
  font-size: 10px;
}

.formc {
  background: #fff;
  padding: 4em 4em 2em;
  max-width: 900px;
  min-width: 848px;
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

.button_edit {
  background: rgba(148, 186, 101, 0.7);
  border: none;
  color: #fff;
  cursor: pointer;
  font-size: 17px;
  outline: none;
   padding: 14px 16px;
   
}
.button_edit:hover {
  background: #94af65;
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
         <a>User: <asp:Label ID="Label1" runat="server" Text=""></asp:Label></a>
  <a href="AddItems.aspx">ADD PRODUCTS</a>
  <a href="EditProductsForm.aspx">EDIT PRODUCTS</a>
   <a class="active">PRODUCT ORDERD HISTORY</a>
   <a href="HomeStoreLoggedForm.aspx">STORE PAGE</a>
               
  <asp:Button ID="Button1" runat="server" CssClass="button_logout" Text="LOGOUT" OnClick="Button1_Click" />
         
</div>
         
        <br/>
        <div class="formc">
         <div class="center">

             <h2>Category order history</h2>
             <asp:Chart ID="Chart1" runat="server" Height="350px" Palette="None"   
        PaletteCustomColors="192, 0, 0" Width="350px" DataSourceID="SqlDataSource1">  
        <Series>  
            <asp:Series Name="Series1" ChartType="Bar" XValueMember="Category" YValueMembers="AMOUNT ORDERED" YValuesPerPoint="2">  
            </asp:Series>  
        </Series>  
        <ChartAreas>  
            <asp:ChartArea Name="ChartArea1" >  
            </asp:ChartArea>  
        </ChartAreas>  
        <BorderSkin BackColor="" PageColor="192, 64, 0" />  
    </asp:Chart>  
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_a76fcd_bcad3progConnectionString %>" SelectCommand="  SELECT CONCAT(Category_name, ' ', Date_Order) AS Category, COUNT(*) as 'AMOUNT ORDERED'
FROM [CATEGORY_ORDER_TOTAL]
GROUP BY Category_name, Date_Order
HAVING COUNT(*) &gt;= 1
ORDER BY Date_Order"></asp:SqlDataSource>
</div>
            </div><br />
            <br />
        <asp:Label ID="lblresult" runat="server" Text=""></asp:Label>
          
    </form>
</body>
</html>
