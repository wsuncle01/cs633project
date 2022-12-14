<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VotePoll.aspx.cs" Inherits="VotingSystem.VotePoll" %>

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
                                <h1 class="h4 text-gray-900 mb-4">Vote Poll</h1>
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
                                <p runat="server" id="txtTimeRemaining"></p>
                                <%--<p id="demo"></p>


                                <script>
                                    // Set the date we're counting down to
                                    var countDownDate = new Date("Jan 5, 2024 15:37:25").getTime();

                                    // Update the count down every 1 second
                                    var x = setInterval(function () {

                                        // Get today's date and time
                                        var now = new Date().getTime();

                                        // Find the distance between now and the count down date
                                        var distance = countDownDate - now;

                                        // Time calculations for days, hours, minutes and seconds
                                        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
                                        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                                        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                                        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

                                        // Display the result in the element with id="demo"
                                        document.getElementById("demo").innerHTML = days + "d " + hours + "h "
                                        + minutes + "m " + seconds + "s ";

                                        // If the count down is finished, write some text
                                        if (distance < 0) {
                                            clearInterval(x);
                                            document.getElementById("demo").innerHTML = "EXPIRED";
                                        }
                                    }, 1000);
                                </script>--%>
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
                                <%--<p runat="server" id="txtAnswerOption1"></p>
                                <p runat="server" id="txtAnswerOption2"></p>
                                <p runat="server" id="txtAnswerOption3"></p>
                                <p runat="server" id="txtAnswerOption4"></p>
                                <p runat="server" id="txtAnswerOption5"></p>--%>
                                <asp:RadioButtonList ID="rblAnswerOptions" runat="server">

                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                    </div>
                    <div class="col-lg-4">
                        <asp:Button ID="btnSubmit" OnClick="btnSubmit_ServerClick" CssClass="btn btn-primary btn-user btn-block" Text="Submit" runat="server" />
                    </div>
                </div>

                <br />
                <br />

            </div>
        </div>
    </div>
</asp:Content>
