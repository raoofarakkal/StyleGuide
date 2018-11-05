<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcPaging.ascx.cs" Inherits="StyleGuideUI.UC.UcPaging" %>
<div style="height:33px; width:140px;overflow:hidden; text-align:center;">
    <div class="ActionPanelToolBar" style="float:left; ">
        <asp:ImageButton ID="ibPrv" ToolTip="Previous Page" ImageUrl="~/Images/Left.png"
            runat="server" ImageAlign="Middle" Style="width: 32px;height:32px; vertical-align:top;"
            onclick="ibPrv_Click" />
    </div>
    <div style="float:left;padding-top:4px;width:74px; ">
        <asp:DropDownList ID="ddlPgNo" ToolTip="Select a page to navigate into" runat="server" 
            AutoPostBack="True" Width="70px" Font-Names="Calibri" Font-Size="14px"
            BackColor="#EFEFEF" onselectedindexchanged="ddlPgNo_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div class="ActionPanelToolBar" style="float:left;">
        <asp:ImageButton ID="ibNext" ToolTip="Next Page" ImageUrl="~/Images/Right.png"
            runat="server" ImageAlign="Middle" Style="width: 32px;height:32px;" 
            onclick="ibNext_Click" />
    </div>
    <div style="clear:both;height:0px"></div>
</div>
