<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SearchWeb._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>影立方 搜索引擎</title>
    <LINK rel="stylesheet" type="text/css" href="default.css">
<LINK rel="stylesheet" type="text/css" href="WebResource.css">
</head>
<body>
    <form id="form1" runat="server">
         <div style="text-align:center"><img alt="logo" src="img/SiteLogo.png" style="width:229px; height:120px;"/></div>
       <div class="header-wrapper2">
        
        <br />
        <span class="input-panel" style="text-align:center">
            <asp:TextBox ID="TextBox1" OnTextChanged="Button1_Click" 
               onfocus="javascript:this.select()" runat="server" BorderWidth="1px" 
            CssClass="searchbar2" Width="350px"></asp:TextBox>
            <asp:Button ID="Button1"  runat="server" onclick="Button1_Click" Text="搜索"
             CssClass="searchbutton" />
        </span>
        
        <br />
        <span>
            <asp:RadioButton ID="rdbRelation" Text="关系搜索" runat="server" CssClass="rdb" GroupName="stype" 
               Checked="True" />
            
            <asp:RadioButton ID="rdbPage" Text="页面搜索" runat="server" CssClass="rdb"  GroupName="stype" />
        </span>
    </div>
    <script type= "text/javascript" language="javascript">
        var txtBox = document.getElementById("TextBox1");
        if (txtBox != null) 
            txtBox.focus();
    </script>
    <div style="height:100px">
        
    </div>
    <div class=footer-wrapper><A href="http://www.tsinghua.edu.cn/"><SPAN style="COLOR: #0000ff; TEXT-DECORATION: underline">Tsinghua University</SPAN></A> <SPAN class=foot_Label3>| </SPAN><A 
href="mailto:wangw04@gmail.com"><SPAN 
class=navlink>Feedback</SPAN></A> <BR>© 2009 Tsinghua 版权所有</div>
</form>
</body>
</html>
