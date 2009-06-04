<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GeneralSearch._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">

.menuTd {
  background-color: #F9F7F4;
  height: 25px;
}

.menuTdhover {
  background-color: #ECE5DC;
  height: 25px;
}

.menuEntry {
  font-family: Arial, Helvetica, sans-serif;
  font-size: 12px;
  color: #000000;
  text-decoration: none;
}

.body {
  background-color: #F9F7F4;
}

.bodytext {
  font-family: Arial, Helvetica, sans-serif;
  font-size: 12px;
  color: #000000;
  text-decoration: none;
}

.title {
  font-family: Arial, Helvetica, sans-serif;
  font-size: 26px;
  color: #FF9900;
  text-decoration: none;
}

.intro {
  font-family: Arial, Helvetica, sans-serif;
  font-size: 12px;
  color: #FF9900;
  text-decoration: none;
}

.orangeTd {
  background-color: #FF9900
}

ul {
  list-style-image: url(../img/reiter/ul.gif)
}

h3 {
  font-family: Arial, Helvetica, sans-serif;
  font-size: 16px;
  color: #000000;
}

h4 {
  font-family: Arial, Helvetica, sans-serif;
  font-size: 14px;
  color: #000000;
}

.url {
  color: #996600;
}

.highlight {
  font-weight: bold;
}

.ellipsis {
  font-weight: bold;
}

</style>

<script type="text/javascript">
<!--
function queryfocus() { document.search.query.focus(); }
// -->
</script>

</head>
<body>
    
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" Width="210px"></asp:TextBox>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    
    </div>
    
    <asp:Repeater ID="Repeater1" runat="server">
    
    <HeaderTemplate>
        <table border=0 cellspacing="3" cellpadding="0">
        <tr>
        <td valign="top">
    </HeaderTemplate>
    
    <ItemTemplate>
        <b><a href="<%# Eval("Url")%>"><%# Eval("Title") %></a></b>
        <br><%# Eval("Summary")%><br>
        <span class="url"><%# Eval("Url")%></span>
        <br><br>
        </td>
    </ItemTemplate>
    
    <FooterTemplate>
        </tr>
        </table>
    </FooterTemplate>
    
    </asp:Repeater>
    
    </form>

</body>

</html>
