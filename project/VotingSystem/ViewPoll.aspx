<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewPoll.aspx.cs" Inherits="VotingSystem.ViewPoll" EnableEventValidation="false" %>

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
                                <h1 class="h4 text-gray-900 mb-4">View Poll</h1>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="col-lg-11">
                        <asp:GridView ID="GridView1" CssClass="gvdatatable table table-striped table-bordered" runat="server" OnPreRender="GridView1_PreRender" AutoGenerateColumns="false" DataKeyNames="PollId,PollStatus" EmptyDataText="You currently have no polls." OnRowCommand="gvViewInbox_Commands" OnRowDataBound="gv_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EditRow">
                                        <%--<i class="icon-pencil bigger-120" title="Edit" data-rel="tooltip"></i>--%> Edit
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
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkViewResults" runat="server" Text="ViewResults" CommandName="ViewResults">
                                        <%--<i class="icon-pencil bigger-120" title="Edit" data-rel="tooltip"></i>--%> View Results
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="25px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="PollStatus" HeaderText="Status" SortExpression="PollStatus" />
                                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                                <asp:BoundField DataField="Question" HeaderText="Question" SortExpression="Question" />
                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField DataField="EndDate" HeaderText="End Date" SortExpression="EndDate" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField DataField="PollVisibility" HeaderText="Poll Visibility" SortExpression="PollVisibility" />
                                <asp:BoundField DataField="ResultsVisibility" HeaderText="Results Visibility" SortExpression="ResultsVisibility" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
