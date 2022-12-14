<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchPoll.aspx.cs" Inherits="VotingSystem.SearchPoll" MaintainScrollPositionOnPostback="true" %>

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
                                <h1 class="h4 text-gray-900 mb-4">Search Poll</h1>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <%--<div class="form-group">--%>
                    <%--<div class="col-lg-1"></div>--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="col-lg-6">
                        <select runat="server" class="form-control" aria-label="Default select example" name="ddlCategory" id="ddlCategory">
                            <option selected="" disabled="">Select Category:</option>
                            <option runat="server" value="1">Music</option>
                            <option runat="server" value="2">Movies</option>
                            <option runat="server" value="3">Language</option>
                            <option runat="server" value="4">Humor</option>
                            <option runat="server" value="5">Travel</option>
                            <option runat="server" value="6">Reading</option>
                            <option runat="server" value="7">Food</option>
                            <option runat="server" value="8">Nature</option>
                            <option runat="server" value="9">Pets</option>
                            <option runat="server" value="10">Life</option>
                            <option runat="server" value="11">Art</option>
                            <option runat="server" value="12">Photography</option>
                            <option runat="server" value="13">Relationships</option>
                            <option runat="server" value="14">Culture</option>
                            <option runat="server" value="15">Adventure</option>
                            <option runat="server" value="16">Gaming</option>
                            <option runat="server" value="17">Psychology</option>
                            <option runat="server" value="18">Education</option>
                            <option runat="server" value="19">Writing</option>
                            <option runat="server" value="20">Technology</option>
                        </select>
                        <%--</div>
                    <div class="form-group">--%>
                        <select runat="server" class="form-control" aria-label="Default select example" name="ddlPollStatus" id="ddlPollStatus">
                            <option selected="" disabled="">Poll Status:</option>
                            <option runat="server" value="Live">Live</option>
                            <option runat="server" value="Completed">Completed</option>
                        </select>
                        <%--</div>
                    <div class="form-group">--%>
                        <select runat="server" class="form-control" aria-label="Default select example" name="ddlResultsVisibility" id="ddlResultsVisibility">
                            <option selected="" disabled="">Results Visibility:</option>
                            <option runat="server" value="Private">Private</option>
                            <option runat="server" value="Public">Public</option>
                        </select>
                        <%--</div>--%>
                    </div>
                </div>
                <div class="row">
                    <br />
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="col-lg-2">
                        <asp:Button ID="btnSearch" OnClick="btnSearch_ServerClick" CssClass="btn btn-primary btn-user btn-block" Text="Search" runat="server" />
                    </div>
                    <div class="col-lg-2">
                    </div>
                    <div class="col-lg-2">
                        <asp:Button ID="btnClear" OnClick="btnClear_ServerClick" CssClass="btn btn-warning btn-user btn-block" Text="Clear" runat="server" />
                    </div>
                    <br />
                    <br />
                </div>
                <div class="row">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="col-lg-11">
                        <asp:GridView ID="GridView1" CssClass="gvdatatable table table-striped table-bordered" runat="server" OnPreRender="GridView1_PreRender" AutoGenerateColumns="false" DataKeyNames="PollId,PollStatus" EmptyDataText="No results match your search criteria." OnRowCommand="gvSearch_Commands" OnRowDataBound="OnRowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkVote" runat="server" Text="Vote" CommandName="Vote">
                                        <%--<i class="icon-pencil bigger-120" title="Edit" data-rel="tooltip"></i>--%> Vote
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
                                <asp:BoundField DataField="Voted" HeaderText="Voted" SortExpression="Voted" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
