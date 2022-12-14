<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReviewPollPage.aspx.cs" Inherits="VotingSystem.ReviewPollPage" %>

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
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-10">
                        <div class="card shadow mb-4">
                            <div class="card-header py-3">
                                <h6 class="m-0 font-weight-bold text-primary">Poll Details</h6>
                            </div>
                            <div class="card-body">
                                <p runat="server" id="txtCategory"></p>
                                <p runat="server" id="txtDescription"></p>
                                <p runat="server" id="txtStartDate"></p>
                                <p runat="server" id="txtEndDate"></p>
                                <p runat="server" id="txtPollVisibility"></p>
                                <p runat="server" id="txtResultsVisibility"></p>
                                <p runat="server" id="txtCreatedBy"></p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-10">
                        <div class="card shadow mb-4">
                            <div class="card-header py-3">
                                <h6 class="m-0 font-weight-bold text-primary">Question</h6>
                            </div>
                            <div class="card-body">
                                <p runat="server" id="txtQuestion"></p>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divQuestion1" runat="server" class="row">
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-10">
                        <div class="card shadow mb-4">
                            <div class="card-header py-3">
                                <h6 class="m-0 font-weight-bold text-primary">Answer Options</h6>
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

                <div class="row">
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-10">
                        <div class="card shadow mb-4">
                            <div class="card-header py-3">
                                <h6 class="m-0 font-weight-bold text-primary">Comments</h6>
                            </div>
                            <div class="card-body">
                                <div class="form-group">
                                        <textarea runat="server" type="email" class="form-control form-control-user" id="txtComments"
                                            placeholder="Comments" rows="5" />
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-3">
                    </div>
                    <div class="col-lg-3">
                        <asp:Button ID="btnApprove" OnClick="btnApprove_ServerClick" CssClass="btn btn-success btn-user btn-block" Text="Approve" runat="server" />
                    </div>
                     <div class="col-lg-3">
                        <asp:Button ID="btnReject" OnClick="btnReject_ServerClick" CssClass="btn btn-danger btn-user btn-block" Text="Reject" runat="server" />
                    </div>

                </div>

                <br />
                <br />

            </div>
        </div>
    </div>
</asp:Content>
