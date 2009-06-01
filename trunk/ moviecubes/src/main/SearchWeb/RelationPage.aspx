<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelationPage.aspx.cs" Inherits="MovieCube.SearchWeb.RelationPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head  runat="server">
    <title>MovieCube关系图</title>
    
</head>
<body>
<form id="form1" runat="server"></form>
    <div align="center" style="height: 750px; width:100%">
        <embed id="RelationGraph" align="middle" type="application/x-shockwave-flash" pluginspage="http://www.adobe.com/go/getflashplayer" allowscriptaccess="sameDomain" name="RelationGraph" bgcolor="#869ca7" quality="high"
         src="flash/RelationGraph.swf"
         height="100%" 
         width="100%"/>
    </div>

</body>
</html>
