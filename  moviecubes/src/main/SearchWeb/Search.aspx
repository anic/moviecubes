﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="MovieCube.SearchWeb.Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<LINK rel="stylesheet" type="text/css" href="default.css">
<LINK rel="stylesheet" type="text/css" href="WebResource.css">
<script type="text/javascript" src="js/mootools.js"></script>

<title>影立方 搜索</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="header-wrapper2">
        <div style="float:left;  padding-left :70px;"><a href="Default.aspx" title="主页"><img alt="logo" src="img/SiteLogo.png" style="width:229px; height:120px;"/></a></div>
        <span class="guanxi-logo">
            <a href="RelationPage.aspx?query=<%=encodeQuery %>"><img alt="查看关系图" src="img/toGuanxi.png" /></a>
        </span>
             
        <br />
        <span class="input-panel" style="float:left; padding-left:15px">
            <asp:TextBox ID="TextBox1" OnTextChanged="Button1_Click" onfocus="javascript:this.select()" runat="server" BorderWidth="1px" 
            CssClass="searchbar"></asp:TextBox>&nbsp;
            <asp:Button ID="Button1"  runat="server" onclick="Button1_Click" Text="搜索" 
             CssClass="searchbutton" />
        </span>
    </div>
    <script type= "text/javascript" language="javascript">
        var txtBox = document.getElementById("TextBox1");
        if (txtBox != null) 
            txtBox.focus();
    </script>
    <br/><br/>
  <div class="no-result-panel" id="NoResultPanel" runat="server">抱歉 我们没有找到 "<em><%=query %></em>" 相关结果</div>
  <div class="content-wrapper">
  <DIV id="LeftPanel" class="template2-left-wrapper">
  <div id="RelativeStarsPanel" runat="server">
  <div class="title-bar">演员</div>
  <div id="StarPanel" class="panel">
      <asp:Repeater ID="Repeater3" runat="server">
      <ItemTemplate>
        <div class=item>
        <div class=relationship><SPAN style="COLOR: #f60; TEXT-DECORATION: underline"><%# Eval("Area")%></SPAN></A></div>
        <div class=title><A title="点击搜索 <%# Eval("Name")%>" href="<%=queryPageUrl%>?query=<%#Eval("Name")%>"><%# Eval("Name")%></A></div>
        <div class=join><A title="点击搜索 <%# Eval("Name")%>+<%=query %>" href="<%=queryPageUrl%>?query=<%=query%>+<%#Eval("Name") %>">加入查询</A></div>
        <div class=why><A id="v_toggle_<%#Eval("ID") %>" href="#"><span>了解更多</span><img src="img/Expand_small.png" style=" vertical-align:middle" /></A></div>
        <div class=more-info-panel id="vertical_slide_<%#Eval("ID") %>">
            <div class=content>参演电影：<br><%#Eval("HtmlMovies")%></div>
            <div class=content><%#Eval("HtmlIntroduction").Equals("") ? "" : "简介："%><br><%#Eval("HtmlIntroduction")%><br><span><a style="font-weight:bolder; font-size:small" target="_blank" href="http://www.mdbchina.cn/persons/<%#Eval("ID") %>/others/?#movieIntro"><em><%#Eval("HtmlIntroduction").Equals("") ? "" : "[更多]"%></em></a></span></div>
        </div>
        </div>
        <div class=hr></div>
        <script type= "text/javascript" language="javascript">
            var myVerticalSlide<%#Eval("ID") %> = new Fx.Slide('vertical_slide_<%#Eval("ID") %>');
            myVerticalSlide<%#Eval("ID") %>.hide();
            $('v_toggle_<%#Eval("ID") %>').addEvent('click', function(e) {
                e.stop();
                myVerticalSlide<%#Eval("ID") %>.toggle();
                var node = document.getElementById("v_toggle_<%#Eval("ID") %>");
                if (node.childNodes[0].innerHTML == "了解更多") {
                    node.childNodes[0].innerHTML = "隐藏信息";
                    node.childNodes[1].src = "img/Collapse_small.png";
                    
                }
                else {
                    node.childNodes[0].innerHTML = "了解更多";
                    node.childNodes[1].src = "img/Expand_small.png";
                }              
            });
        </script>   
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
  
  </div>
  </div>
  
  
  <div id="RelativeMoviePanel" runat="server">
  <div class="title-bar">电影</div>
  <div id="MoviePanel" class="panel">
      <asp:Repeater ID="Repeater4" runat="server">
        <ItemTemplate>
        <div class=item>
        <div class=relationship><SPAN style="COLOR: #f60; TEXT-DECORATION: underline"><%# Eval("Language")%></SPAN></A></div>
        <div class=title><A title="点击搜索 <%# Eval("Name")%>" href="<%=queryPageUrl%>?query=<%#Eval("Name") %>"><%# Eval("Name")%></A></div>
        <div class=join><A title="点击搜索 <%# Eval("Name")%>+<%=query %>" href="<%=queryPageUrl%>?query=<%=query%>+<%#Eval("Name") %>">加入查询</A></div>
        <div class=why><A id="v_toggle_<%#Eval("ID") %>" href="#"><span>了解更多</span><img src="img/Expand_small.png" style=" vertical-align:middle" /></A></div>
        <div class=more-info-panel id="vertical_slide_<%#Eval("ID") %>">
            <div class=content>评分：<%#Eval("FormatRank")%></div>
            <div class=content>类型：<%#Eval("HtmlType")%></div>
            <div class=content>产地：<%#Eval("Area") %></div>
            <div class=content>出品时间：<%#Eval("Time") %></div>
            <div class=content>主创人员：</div>
            <div class=content>  <%#Eval("MainStar") %></div>
            <div class=content><%#Eval("HtmlIntroduction").Equals("") ? "" : "简介："%><br><%#Eval("HtmlIntroduction")%><br><a style="font-weight:bolder; font-size:small" target="_blank" href="http://www.mdbchina.cn/movies/<%#Eval("ID") %>/others/?#movieIntro"><em><%#Eval("HtmlIntroduction").Equals("") ? "" : "[更多]"%></em></a></div>
        </div>
        </div>
        

        <div class=hr></div>
        <script type= "text/javascript" language="javascript">
            var myVerticalSlide<%#Eval("ID") %> = new Fx.Slide('vertical_slide_<%#Eval("ID") %>');
            myVerticalSlide<%#Eval("ID") %>.hide();
            $('v_toggle_<%#Eval("ID") %>').addEvent('click', function(e) {
                e.stop();
                myVerticalSlide<%#Eval("ID") %>.toggle();
                var node = document.getElementById("v_toggle_<%#Eval("ID") %>");
                if (node.childNodes[0].innerHTML == "了解更多") {
                    node.childNodes[0].innerHTML = "隐藏信息";
                    node.childNodes[1].src = "img/Collapse_small.png";
                    
                }
                else {
                    node.childNodes[0].innerHTML = "了解更多";
                    node.childNodes[1].src = "img/Expand_small.png";
                }                
            });
        </script>   
        </ItemTemplate>
      </asp:Repeater>
    </div>

  </DIV>
  </div>

<div class="template2-right-wrapper">
<div id="RecordPanel" runat="server">
<div class="title-bar">网页 <span class="page-total">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Label1" runat="server" Text=""></asp:Label></span></div>
<div class="panel">
    <asp:Repeater ID="Repeater1" runat="server">
    
    <HeaderTemplate>
        <table border=0 cellspacing="3" cellpadding="0">
        <tr>
        <td valign="top">
    </HeaderTemplate>
    
    <ItemTemplate>
 
    <div class=newsitem>
		<DIV>
        	<SPAN class=title><A href="<%# Eval("Url")%>" target=_blank><%# Eval("Title") %></A></SPAN> 
        </div>
    	<div class=content><%# Eval("Summary")%></div>
    	<div>
    	    <SPAN class=url><%# Eval("Url")%></SPAN>
    	</div>
        <DIV>
            <SPAN class=info><A href=<%# Eval("Cache")%>>网页快照</A></SPAN>
        </div>
    </div>
    </ItemTemplate>
    
    <FooterTemplate>
    </FooterTemplate>
    
    </asp:Repeater>
    </div>
    <div class="p">
        <asp:Repeater ID="Repeater2" runat="server">
            <HeaderTemplate>
                <a href="<%=(prevPage.Id).Equals("") ? "" : prevPage.Url %>"><%=(prevPage.Id).Equals("") ? "" : "前一页" %></a>
            </HeaderTemplate>
            <ItemTemplate>
                <a href = "<%# Eval("Url")%>"><%# Eval("IsCurrent").ToString().Equals("True") ? "" : "["%><%# Eval("Id")%><%# Eval("IsCurrent").ToString().Equals("True") ? "" : "]"%></a>&nbsp;
            </ItemTemplate> 
            <FooterTemplate>
                <a href="<%=(nextPage.Id).Equals("") ? "" : nextPage.Url %>"><%=(nextPage.Id).Equals("") ? "" : "后一页"%></a>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </div>

    </div>
    </div>

    <div class=footer-wrapper><A href="http://www.tsinghua.edu.cn/"><SPAN 
style="COLOR: #0000ff; TEXT-DECORATION: underline">Tsinghua University</SPAN></A> <SPAN class=foot_Label3>| </SPAN><A 
href="mailto:wangw04@gmail.com"><SPAN 
class=navlink>Feedback</SPAN></A> <BR>© 2009 Tsinghua 版权所有</div>
 </form>
 
 	

</body>
</html>
