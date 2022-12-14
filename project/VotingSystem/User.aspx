<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="VotingSystem.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
        <div class="container">
        <div class="card o-hidden border-0 shadow-lg my-5">
            <div class="card-body p-0">
                <!-- Nested Row within Card Body -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="p-3">
                            <div class="text-center">
                                <h1 class="h4 text-gray-900 mb-4">Users</h1>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="col-lg-11">
                        <asp:GridView ID="GridView1" CssClass="gvdatatable table table-striped table-bordered" runat="server" OnPreRender="GridView1_PreRender" AutoGenerateColumns="false" DataKeyNames="UserId, Email" EmptyDataText="There are currently no users." OnRowCommand="gvUser_Commands" OnRowDataBound="gv_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkActivate" runat="server" Text="Activate" CommandName="Activate">
                                        <%--<i class="icon-pencil bigger-120" title="Edit" data-rel="tooltip"></i>--%> Activate
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="25px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="DeleteRow">
                                        <%--<i class="icon-pencil bigger-120" title="Edit" data-rel="tooltip"></i>--%> Delete
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="25px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName" />
                                <asp:BoundField DataField="UserStatus" HeaderText="User Status" SortExpression="UserStatus" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
