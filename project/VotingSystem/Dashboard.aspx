<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="VotingSystem.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Dashboard</h1>
            <a href="CreatePoll.aspx" class="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm"><i class="fas fa-pencil-alt fa-sm text-white-50"></i>Create Poll</a>
        </div>

        <!-- Content Row -->
        <div class="row">

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                    Total Polls
                                </div>
                                <div runat="server" id="TotalPolls" class="h5 mb-0 font-weight-bold text-gray-800">10000</div>
                            </div>
                            <div class="col-auto">
                                <%--<i class="fas fa-calendar fa-2x text-gray-300"></i>--%>
                                <i class="fas fa-list-ul fa-2x text-gray-300"></i>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-success shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                    Live Polls
                                </div>
                                <div runat="server" id="LivePolls" class="h5 mb-0 font-weight-bold text-gray-800">8000</div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-caret-square-right fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Earnings (Monthly) Card Example -->
            <%--<div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-info shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-info text-uppercase mb-1">Completed Polls
                                            </div>
                                            <div class="row no-gutters align-items-center">
                                                <div class="col-auto">
                                                    <div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">2000</div>
                                                </div>
                                                <div class="col">
                                                    <div class="progress progress-sm mr-2">
                                                        <div class="progress-bar bg-info" role="progressbar" style="width: 50%" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-auto">
                                            <i class="fas fa-clipboard-list fa-2x text-gray-300"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>

            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-info shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                    Completed Polls
                                </div>
                                <div runat="server" id="CompletedPolls" class="h5 mb-0 font-weight-bold text-gray-800">2000</div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-flag fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Pending Requests Card Example -->
            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-warning shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">
                                    Total Votes
                                </div>
                                <div runat="server" id="TotalVotes" class="h5 mb-0 font-weight-bold text-gray-800">20000</div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-check-square fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="card o-hidden border-0 shadow-lg my-5">
            <div class="card-body p-0">
                <!-- Nested Row within Card Body -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="p-3">
                            <div class="text-center">
                                <h1 class="h4 text-gray-900 mb-4">Vote Poll</h1>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="col-lg-11">
                        <asp:GridView ID="GridView1" CssClass="gvdatatable table table-striped table-bordered" runat="server" OnPreRender="GridView1_PreRender" AutoGenerateColumns="false" DataKeyNames="PollId,Voted" EmptyDataText="There are currently no live polls." OnRowCommand="gvDashboardInbox_Commands" OnRowDataBound="OnRowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkVote" runat="server" Text="View" CommandName="Vote">
                                        <%--<i class="icon-pencil bigger-120" title="Edit" data-rel="tooltip"></i>--%> Vote
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="25px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                                <asp:BoundField DataField="Question" HeaderText="Question" SortExpression="Question" />
                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField DataField="EndDate" HeaderText="End Date" SortExpression="EndDate" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField DataField="PollVisibility" HeaderText="Poll Visibility" SortExpression="PollVisibility" />
                                <asp:BoundField DataField="ResultsVisibility" HeaderText="Results Visibility" SortExpression="ResultsVisibility" />
                                <asp:BoundField DataField="Voted" HeaderText="Voted" SortExpression="Voted" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
