<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcEntityList.ascx.cs" Inherits="StyleGuideUI.UC.UcEntityList" %>
<%@ Register src="UcPaging.ascx" tagname="UcPaging" tagprefix="uc1" %>
<script type="text/javascript">
    function UEL_markUpdated(targetObjectName) {
        target = document.getElementById(targetObjectName);
        target.value = 1;
    }

</script> 
<table cellpadding="0" cellspacing="0" style="border-collapse: collapse; width: 250px;">
    <tr>
        <td>
            <div class="ManageNavBar" >
                <div id="div1" style=" width: 990px;">
                    <table  cellpadding="0" cellspacing="0" style="border-collapse: collapse; width: 100%;">
                        <tr>
                           <td style="text-align:left;"><h1 style="padding:0px;margin:0px;">Entities</h1></td>
                           <td style="text-align:right;width:80px;">Entity</td>
                            <td style="text-align:right;width:200px;">
                                <asp:RadioButtonList ID="rbSearchOpt" runat="server" RepeatDirection="Horizontal"   
                                    Width="180px" TextAlign="Right" RepeatLayout="Flow" >
                                    <asp:ListItem Selected="True" Value="C"><span style="padding-right:10px;">Contains</span></asp:ListItem>
                                    <asp:ListItem Value="S"><span style="padding-right:10px;">Starts with</span></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align:right;width:265px;">
                                <asp:TextBox ID="tbSearch" runat="server"  Style="border: 1px solid gray; width: 250px;
                                    height: 27px; padding-left: 5px; padding-right: 5px;"></asp:TextBox>
                            </td>
                            <td style="text-align:right;width:168px;">
                                <asp:Button ID="btSearch" runat="server" CssClass="ButtonMedium" Text="Search" Width="80px"
                                    OnClick="btSearch_Click" />
                                <asp:Button ID="btClear" runat="server"  CssClass="ButtonMedium" Text="Reset" Width="80px"
                                    OnClick="btClear_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 990px;padding-top:5px;">
                    <table cellpadding="0" cellspacing="0" style="border-collapse: collapse; width: 100%;">
                        <tr>
                            <td style="text-align: left;"><asp:Label runat="server" ID="lblResult" CssClass="ErrorBox" EnableViewState="false"></asp:Label>&nbsp;</td>
                            <td style="text-align: left; width: 103px">
                                <asp:Button ID="btSave" CssClass="Button" runat="server" Text="Save" Width="100px"
                                    UseSubmitBehavior="false" OnClick="btSave_Click" />
                            </td>
                            <td style="text-align: right;Width:144px;">
                                <uc1:UcPaging ID="UcPaging1" runat="server" OnPageChange="on_PageChange"/>
                            </td>
                            <td style="text-align: right; width: 153px;">
                                <asp:Button ID="btNew" CssClass="Button" runat="server" Text="Add New Entity" Width="150px"
                                    UseSubmitBehavior="false" OnClick="btNew_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div style="clear:both;">
                <div id="divGv" runat="server" style="font-weight: normal; border-top: 1px solid #c3cecc;border-bottom: 1px solid #c3cecc;width:990px;margin-top:75px;">
                    <asp:GridView ID="gvEntList" runat="server" BorderColor="#c3cecc" CellPadding="5" Width="990px" Height="650px"
                        GridLines="Vertical"   AutoGenerateColumns="False" BorderStyle="none"  ShowHeader="true" ShowFooter="true"
                        BorderWidth="3px"  
                        OnRowCommand="gvEntList_RowCommand"
                        OnRowDataBound="gvEntList_RowDataBound">
                        <HeaderStyle BackColor="#EFEFEF" ForeColor="#3F3F3F" Height="40px" />
                        <AlternatingRowStyle BackColor="#F4F4F4" />
                        <RowStyle BackColor="White" CssClass="gvRow" Height="40px" />
                        <FooterStyle  BackColor="White" CssClass="gvRow"/>
                        <Columns>
                            <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-Width="20px" >
                                <ItemTemplate>
                                    <asp:ImageButton ID="btndelete" runat="server" title="Delete" ImageUrl="~/images/Trash16.png" Height="16px" OnClientClick="javascript:if(!confirm('Delete this Entity?'))return false;" CssClass="btn" CommandName="DeleteRow"
                                    CommandArgument='<%# Eval("ID") %>' UseSubmitBehavior="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" ItemStyle-Width="30px"  ItemStyle-VerticalAlign="Top"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                <ItemTemplate>
                                    <asp:TextBox ID="tbID" runat="server" ReadOnly="true" Width="30px" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("ID") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entity" ItemStyle-Width="120px"  ItemStyle-VerticalAlign="Top"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbName" runat="server"  BorderStyle="None" Rows="3" style="overflow:auto;Width:120px"  TextMode="MultiLine"  BackColor="Transparent" Text='<%#Eval("Name") %>' ToolTip='<%#getLMDTBY((GridViewRow)Container) %>' onchange='<%#getMarkUpdatedCall( ((GridViewRow)Container).FindControl("hfChanged").ClientID)%>'></asp:TextBox>
                                    <asp:HiddenField ID="hfChanged" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Suspects" ItemStyle-Width="120px"  ItemStyle-VerticalAlign="Top"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbSuspects" runat="server"  BorderStyle="None" Rows="3" style="overflow:auto;Width:120px"  TextMode="MultiLine"  BackColor="Transparent" Text='<%#Eval("Suspects") %>' onchange='<%#getMarkUpdatedCall( ((GridViewRow)Container).FindControl("hfChanged").ClientID)%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Suggestions" ItemStyle-Width="120px"  ItemStyle-VerticalAlign="Top"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbSuggestions" runat="server"  BorderStyle="None" Rows="3" style="overflow:auto;Width:120px"  TextMode="MultiLine"  BackColor="Transparent" Text='<%#Eval("Suggestions") %>' onchange='<%#getMarkUpdatedCall( ((GridViewRow)Container).FindControl("hfChanged").ClientID)%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entity Type" ItemStyle-Width="100px"  ItemStyle-VerticalAlign="Top"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlEntTypes" runat="server" width="100px" BorderStyle="None" BackColor="Transparent" onchange='<%#getMarkUpdatedCall( ((GridViewRow)Container).FindControl("hfChanged").ClientID)%>'>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Notes" ItemStyle-Width="230px" ItemStyle-VerticalAlign="Top"   HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbNotes" runat="server"   BorderStyle="None" Rows="3"  style="overflow:auto;width:230px;" BackColor="Transparent"  TextMode="MultiLine" Text='<%#Eval("Notes") %>' onchange='<%#getMarkUpdatedCall( ((GridViewRow)Container).FindControl("hfChanged").ClientID)%>' ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="150px" HeaderText="Related Categories"  HeaderStyle-HorizontalAlign="Left"  ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:GridView ID="gvEntCats" runat="server" style="overflow:hidden;width:150px;font-weight: normal"
                                        AutoGenerateColumns="false"  GridLines="None"
                                        ShowHeader="false" 
                                        BackColor="Transparent"
                                        OnRowCommand="gvEntCats_RowCommand"
                                        RowStyle-VerticalAlign="Top"
                                        >
                                        
                                        <AlternatingRowStyle />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="16px"  ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnCatDel" runat="server" title="Delete Category" ImageUrl="~/images/Trash16.png" CssClass="btn" 
                                                        OnClientClick="javascript:if(!confirm('Delete this category?'))return false;"
                                                        CommandName="DeleteCatRow"
                                                        CommandArgument='<%# Eval("EntID") +","+ Eval("CatID") %>' UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField   Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbCatID" runat="server" ReadOnly="true" Width="100px" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("CatID") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField >
                                            <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbCat" runat="server" Text='<%#Eval("CatName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                    <div style="text-align:right;">
                                        <asp:ImageButton ID="btCatNew" runat="server" title="Add Category" ImageUrl="~/images/Add16.png"  
                                            CommandName="CatNew" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' UseSubmitBehavior="false" />
                                        <div id="divAddNewCat" runat="server" visible="false">
                                            <table>
                                                <tr>
                                                    <td colspan="2" style="text-align:center;">
                                                        <asp:DropDownList ID="ddlEntCatList" DataTextField="Name" DataValueField="ID"  runat="server" width="140px" >
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:left">
                                                        <asp:Button ID="btCatAdd" CssClass="ButtonSmall" runat="server" Text="Add" Width="60px" 
                                                            CommandName="CatAdd" CommandArgument='<%# Eval("ID") %>' UseSubmitBehavior="false" />
                                                    </td>
                                                    <td style="text-align:right;">
                                                        <asp:Button ID="btCatCancel"  CssClass="ButtonSmall" runat="server" Text="Cancel" Width="60px" 
                                                            CommandName="CatAddCancel" CommandArgument='<%# Eval("ID") %>' UseSubmitBehavior="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:TemplateField>
<%--                            <asp:TemplateField HeaderText="Last modified" ItemStyle-VerticalAlign="Top"  ControlStyle-Height="100%"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="tbLmDtBy" runat="server"  Text='<%#getLMDTBY((GridViewRow)Container) %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </td>
    </tr>
</table>



