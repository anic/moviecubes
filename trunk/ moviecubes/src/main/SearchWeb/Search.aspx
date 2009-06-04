<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="MovieCube.SearchWeb.Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<LINK rel="stylesheet" type="text/css" href="default.css">
<LINK rel="stylesheet" type="text/css" href="WebResource.css">
<title>影立方 搜索</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="header-wrapper2">
        <span class="guanxi-logo">
            <a href="RelationPage.aspx?query=<%=encodeQuery %>"><img src="img/toGuanxi.bmp" /></a>
        </span>
             <span class="yinglifang-logo">
            <a href=""><img src="img/SiteLogo.png" style="width:130px;height:75px;"/></a>
            </span>
        <br />
        <span class="input-panel">
            <asp:TextBox ID="TextBox1" runat="server" Width="210px" BorderWidth="1px"></asp:TextBox>&nbsp;
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="搜索" 
                Width="80px" Height="19px" />
        </span>
    </div>
  <div class="no-result-panel" id="NoResultPanel" runat=server>抱歉 我们没有找到 "<em><%=query %></em>" 相关结果</div>
  <div class=content-wrapper>
  
  <DIV id=LeftPanel class=template2-left-wrapper>
  <div id="RelativeStarsPanel" runat=server>
  <DIV class=title-bar>演员</DIV>
  <DIV class=panel>
      <asp:Repeater ID="Repeater3" runat="server">
      <ItemTemplate>
        <DIV class=item>
        <DIV class=relationship><SPAN style="COLOR: #f60; TEXT-DECORATION: underline"><%# Eval("Area")%></SPAN></A></DIV>
        <DIV class=title><A title="点击搜索 <%# Eval("Name")%>>" href=""><%# Eval("Name")%></A></DIV>
        <DIV class=join><A title="点击搜索 <%# Eval("Name")%>+</asp:Label>" href="">加入查询</A></DIV>
        <DIV class=why><A onClick="" href="javascript:">了解更多</A></DIV></DIV>
        <DIV class=hr></DIV>
        </ItemTemplate>
      </asp:Repeater>
  <DIV class=more><IMG class=small-icon src="img/arrow.png" width=16 height=16><A href="">更多</A></DIV>
  </DIV>
  </div>
  
  <div id="RelativeMoviePanel" runat=server>
  <DIV class=title-bar>电影</DIV>
  <DIV class=panel>
      <asp:Repeater ID="Repeater4" runat="server">
        <ItemTemplate>
        <DIV class=item>
        <DIV class=relationship><SPAN style="COLOR: #f60; TEXT-DECORATION: underline"><%# Eval("Language")%></SPAN></A></DIV>
        <DIV class=title><A title="点击搜索 <%# Eval("Name")%>>" href=""><%# Eval("Name")%></A></DIV>
        <DIV class=join><A title="点击搜索 <%# Eval("Name")%>+</asp:Label>" href="">加入查询</A></DIV>
        <DIV class=why><A onClick="" href="javascript:">了解更多</A></DIV></DIV>
        <DIV class=hr></DIV>
        </ItemTemplate>
      </asp:Repeater>
  <DIV class=more><IMG class=small-icon src="img/arrow.png" 
width=16 height=16><A href="">更多</A></DIV>
    </DIV>
  </DIV>
  </div>

<div class="template2-right-wrapper">
<div id="RecordPanel" runat=server>
<div class=title-bar>网页 <span class="page-total">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Label1" runat="server" Text=""></asp:Label></span></div>
<div class="panel">
    <asp:Repeater ID="Repeater1" runat="server">
    
    <HeaderTemplate>
        <table border=0 cellspacing="3" cellpadding="0">
        <tr>
        <td valign="top">
    </HeaderTemplate>
    
    <ItemTemplate>
 
    <DIV class=newsitem>
		<DIV>
        	<SPAN class=title><A href="<%# Eval("Url")%>" target=_blank><%# Eval("Title") %></A></SPAN> 
        </DIV>
    	<DIV class=content><%# Eval("Summary")%></DIV>
    	<div>
    	    <SPAN class=url><%# Eval("Url")%></SPAN>
    	</div>
        <DIV>
            <SPAN class=info><A href=<%# Eval("Cache")%>>网页快照</A> - <A href=<%# Eval("More")%>>更多</A></SPAN>
        </DIV>
    </DIV>
    </ItemTemplate>
    
    <FooterTemplate>
    
    </FooterTemplate>
    
    </asp:Repeater>
    </div>
    <div class="p">
        <asp:Repeater ID="Repeater2" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <a href = "<%# Eval("Url")%>"><%# Eval("IsCurrent").ToString().Equals("True") ? "" : "["%><%# Eval("Id")%><%# Eval("IsCurrent").ToString().Equals("True") ? "" : "]"%></a>&nbsp;
            </ItemTemplate> 
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </div>

    </div>
    </div>

    <DIV class=footer-wrapper><A href="http://www.msra.cn/"><SPAN 
style="COLOR: #0000ff; TEXT-DECORATION: underline">Tsinghua University</SPAN></A> <SPAN class=foot_Label3>| </SPAN><A 
href="http://www.tsinghua.edu.cn"><SPAN 
class=navlink>Feedback</SPAN></A> <BR>© 2009 Tsinghua 版权所有 | <A 
href="http://www.thss.tsinghua.edu.cn" target=_blank>联系我们</A> | <A 
href="http://www.thss.tsinghua.edu.cn" target=_blank>隐私声明</A> 
| <A href="http://www.thss.tsinghua.edu.cn" 
target=_blank>商标</A> | <A id=statmentLInk 
href="http://www.thss.tsinghua.edu.cn" 
target=_blank>使用条款</A> </DIV>
 </form>

</body>
</html>
