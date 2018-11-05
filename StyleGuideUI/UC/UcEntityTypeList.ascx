<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcEntityTypeList.ascx.cs" Inherits="StyleGuideUI.UC.UcEntityTypeList" %>
<script type="text/javascript">
    function UETL_markUpdated(targetObjectName) {
        //alert(targetObjectName);
        target = document.getElementById(targetObjectName);
        target.value = 1;
    }
</script> 
<table cellpadding="0" cellspacing="0" style="border-collapse: collapse;width:100%;  ">
    <tr>
        <td align="center">
            <div style="width:350px;">
                <div style="text-align:left;">
                    <h1 style="padding:0px;margin:0px;">Entity type</h1>
                </div>

                <div id="divGv" style="font-weight: normal; border-top: 1px solid #c3cecc;border-bottom: 1px solid #c3cecc;width:350px;
                    ">
                    <asp:GridView ID="gvEntTypeList" runat="server" BorderColor="#c3cecc" CellPadding="5"
                        GridLines="Vertical" AutoGenerateColumns="False" BorderStyle="None"
                        BorderWidth="3px" OnRowCommand="gvEntTypeList_RowCommand" OnRowDataBound="gvEntTypeList_RowDataBound">
                        <HeaderStyle BackColor="#EFEFEF" ForeColor="#3F3F3F" Height="40px" />
                        <AlternatingRowStyle BackColor="#F4F4F4" />
                        <RowStyle BackColor="White" CssClass="gvRow" Height="40px" />
                        <FooterStyle  BackColor="White" CssClass="gvRow"/>

                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btndelete" runat="server" AlternateText="Delete" ImageUrl="~/images/Trash16.png" OnClientClick="javascript:if(!confirm('Delete ?'))return false;" CssClass="btn" CommandName="DeleteRow"
                                    CommandArgument='<%# Eval("ID") %>' UseSubmitBehavior="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbID" runat="server" ReadOnly="true" Width="30px" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("ID") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entity Type" ItemStyle-Width="268px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbType" runat="server" Width="265px" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("Type") %>' onchange='<%#getMarkUpdatedCall( ((GridViewRow)Container).FindControl("hfChanged").ClientID)%>'></asp:TextBox>
                                    <asp:HiddenField ID="hfChanged" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div>
                    <asp:Label runat="server" ID="lblResult" CssClass="ErrorBox" EnableViewState="false"></asp:Label>
                </div>
                <div style="padding-top:10px;">
                    <table cellpadding="0" cellspacing="0" style="border-collapse: collapse; width: 100%;">
                        <tr>
                            <td style="text-align: left; width: 103px">
                                <asp:Button ID="btSave" CssClass="Button" runat="server" Text="Save" Width="100px"
                                    UseSubmitBehavior="false" OnClick="btSave_Click" />
                            </td>
                            <td style="text-align: right; width: 153px;">
                                <asp:Button ID="btNew" CssClass="Button" runat="server" Text="Add New Type" Width="150px"
                                    UseSubmitBehavior="false" OnClick="btNew_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

            </div>

        </td>
    </tr>
</table>
<div style="height:10px;"></div>
