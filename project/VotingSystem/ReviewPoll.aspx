<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReviewPoll.aspx.cs" Inherits="VotingSystem.ReviewPoll" %>

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
                                <h1 class="h4 text-gray-900 mb-4">Review Poll</h1>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="col-lg-11">
                        <asp:GridView ID="GridView1" CssClass="gvdatatable table table-striped table-bordered" runat="server" OnPreRender="GridView1_PreRender" AutoGenerateColumns="false" DataKeyNames="PollId" EmptyDataText="You currently have no polls to review." OnRowCommand="gvReviewInbox_Commands">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server" Text="View" CommandName="View">
                                        <%--<i class="icon-pencil bigger-120" title="Edit" data-rel="tooltip"></i>--%> Review
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
                                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
