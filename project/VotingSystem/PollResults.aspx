<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PollResults.aspx.cs" Inherits="VotingSystem.PollResults" %>

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
                                <h1 class="h4 text-gray-900 mb-4">Poll Results</h1>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="col-lg-11">
                        <asp:GridView ID="GridView1" CssClass="gvdatatable table table-striped table-bordered" runat="server" OnPreRender="GridView1_PreRender" AutoGenerateColumns="false" DataKeyNames="PollId" EmptyDataText="You currently have no poll results.">
                            <Columns>
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
        <div class="row">
            <div class="col-lg-12">
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Answer Option Total Votes</h6>
                    </div>
                    <div class="card-body">
                        <p runat="server" id="txtAnswerOption1"></p>
                        <p runat="server" id="txtAnswerOption2"></p>
                        <p runat="server" id="txtAnswerOption3"></p>
                        <p runat="server" id="txtAnswerOption4"></p>
                        <p runat="server" id="txtAnswerOption5"></p>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <asp:Literal ID="ltrChartCases" runat="server"></asp:Literal>
            </div>
        </div>

    </div>



    <asp:ScriptManager runat="server">
        <Scripts>
            <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
            <%--Framework Scripts--%>
            <asp:ScriptReference Name="jquery" />
            <%--Site Scripts--%>
        </Scripts>
    </asp:ScriptManager>




    <script src="Scripts/HighCharts/highcharts.js"></script>
    <script src="Scripts/HighCharts/offline-exporting.js"></script>
</asp:Content>
