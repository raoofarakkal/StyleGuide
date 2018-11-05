<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="StyleGuideUI._default" %>

<%@ Register src="UC/UcSearch.ascx" tagname="UcSearch" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Editorial Style Guide</title>
    <script src="scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.superbox.js" type="text/javascript"></script>

    <link href="styles/default.css" rel="stylesheet" />
    <link href="styles/Search.css" rel="stylesheet" />
    <link href="styles/jquery.superbox.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $.superbox();
        });

        $.superbox.settings = {
            loadTxt: "<img src=images/loading3.gif>", // Loading text
            closeTxt: "<img src=images/close.png>" // "Close" button text
        };

        function OpenInLightBox(entId) {
            var url = 'manage.aspx?E,{ID}' + entId;
            $.superbox.wait(function () {
                $.superbox.open('<iframe title="EntitiesFrame" name="iFrameEntities" frameborder="0" scrolling="none" height="100%" width="100%" src="' + url + '"></iframe>', { boxWidth: 1100, boxHeight: 600 });
            });
        }
        function OpenInLightBox2(Mtype) {
            var url = 'manage.aspx?'+Mtype;
            $.superbox.wait(function () {
                $.superbox.open('<iframe title="EntitiesFrame" name="iFrameEntities" frameborder="0" scrolling="none" height="100%" width="100%" src="' + url + '"></iframe>', { boxWidth: 1100, boxHeight: 600 });
            });
        }


    </script>
</head> 
<body style="text-align:left;margin: 0px;">
    <form id="form1" runat="server">
    <div class="divMain" >
        <div class="NavBarHolder">
            <div class="NavBar">
                &nbsp;&nbsp;<a class="NavBarElem" href="javascript:OpenInLightBox2('E');" >Entities</a>
                |&nbsp;&nbsp;<a class="NavBarElem" href="javascript:OpenInLightBox2('C');">Categories</a>
                |&nbsp;&nbsp;<a class="NavBarElem" href="javascript:OpenInLightBox2('ET');">Entitity Types</a>
                |&nbsp;&nbsp;<a class="NavBarElem" href="javascript:OpenInLightBox2('CT');">Category Types</a>
            </div>
        </div>
        <div class="DataDiv">
            <uc1:UcSearch ID="UcSearch1" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
