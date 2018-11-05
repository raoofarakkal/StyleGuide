<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="StyleGuideUI._Manage" %>

<%@ Register src="UC/UcCategoryTypeList.ascx" tagname="UcCategoryTypeList" tagprefix="uc1" %>

<%@ Register src="UC/UcCategoryList.ascx" tagname="UcCategoryList" tagprefix="uc2" %>


<%@ Register src="UC/UcEntityTypeList.ascx" tagname="UcEntityTypeList" tagprefix="uc3" %>


<%@ Register src="UC/UcEntityList.ascx" tagname="UcEntityList" tagprefix="uc4" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Editorial Style Guide</title>
    <script src="scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <link href="styles/default.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var isInIframe = (window.location != window.parent.location) ? true : false;
            if (isInIframe) {
                $('.home').hide();
            }
        });
     
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div class="divMain">

<%--        <div class="NavBarHolder"  style="visibility:hidden display:none;" >
            <div class="NavBar">
                &nbsp;&nbsp;<a class="NavBarElem" href="default.aspx">Home</a>
                |&nbsp;&nbsp;<a class="NavBarElem" href="Manage.aspx?E">Entities</a>
                |&nbsp;&nbsp;<a class="NavBarElem" href="Manage.aspx?C">Categories</a>
                |&nbsp;&nbsp;<a class="NavBarElem" href="Manage.aspx?ET">Entitity Types</a>
                |&nbsp;&nbsp;<a class="NavBarElem" href="Manage.aspx?CT">Category Types</a>
            </div>
        </div>
--%>
        <div class="DataDivManage"  >
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="E" runat="server">
                    <uc4:UcEntityList ID="UcEntityList1" runat="server" />
                </asp:View>
                <asp:View ID="ET" runat="server">
                    <uc3:UcEntityTypeList ID="UcEntityTypeList1" runat="server" />
                </asp:View>
                <asp:View ID="C" runat="server">
                    <uc2:UcCategoryList ID="UcCategoryList1" runat="server" />
                </asp:View>
                <asp:View ID="CT" runat="server">
                    <uc1:UcCategoryTypeList ID="UcCategoryTypeList1" runat="server" />
                </asp:View>

            </asp:MultiView>
        </div>

    </div>
    
    </form>
</body>
</html>
