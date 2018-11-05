<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleFr.aspx.cs" Inherits="StyleGuideUI.ArticleFr" %>

<%@ Register src="UC/UcFindReplaceArticleEntities.ascx" tagname="UcFindReplaceArticleEntities" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/FindReplace.css" rel="stylesheet" />
    <script src="scripts/jquery-1.8.0.min.js"></script>
    <script src="scripts/common.js"></script>
    <script src="scripts/ArticleFr.js"></script>
</head>
<body style="margin:0 auto;">
    <form id="form1" runat="server">
    <div>
        
        <uc1:UcFindReplaceArticleEntities ID="UcFindReplaceArticleEntities1" runat="server" />
    
    </div>
    </form>
</body>
</html>
