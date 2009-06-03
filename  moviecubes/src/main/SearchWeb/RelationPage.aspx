<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelationPage.aspx.cs" Inherits="MovieCube.SearchWeb.RelationPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head  runat="server">
    <title>影立方 - 关系图</title>
    <style type="text/css">
        body,html{height:100%;}
    </style>
</head>
<body style="padding:0; margin:0; background-color:#000000;">
<form id="form1" runat="server"></form>
    <div align="center" style="width:100%;height:100%">
        <embed id="RelationGraph" align="middle" type="application/x-shockwave-flash" pluginspage="http://www.adobe.com/go/getflashplayer" allowscriptaccess="sameDomain" name="RelationGraph" bgcolor="#869ca7" quality="high"
         src="flash/RelationGraph.swf"
         height="100%" 
         width="100%"/>
    </div>
</body>
</html>
