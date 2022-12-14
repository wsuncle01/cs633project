<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditPoll.aspx.cs" Inherits="VotingSystem.EditPoll" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Create Poll</title>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <script src="https://unpkg.com/gijgo@1.9.14/js/gijgo.min.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.14/css/gijgo.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div class="container">
        <div class="card o-hidden border-0 shadow-lg my-5">
            <div class="card-body p-0">
                <!-- Nested Row within Card Body -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="p-5">
                            <div class="text-center">
                                <h1 class="h4 text-gray-900 mb-4">Edit Poll</h1>
                            </div>
                            <form class="user">
                                <div class="form-group">

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
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-0">
                                        <input runat="server" type="text" class="form-control form-control-user" id="txtQuestion"
                                            placeholder="Question" />
                                    </div>
                                    <div class="col-sm-6 mb-3 mb-sm-0">
                                        <select runat="server" class="form-control" aria-label="Default select example" name="ddlPollStatus" id="ddlPollStatus">
                                            <option selected="" disabled="">Poll Status:</option>
                                            <option runat="server" value="Draft">Draft</option>
                                            <option runat="server" value="Published">Published</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <input runat="server" type="text" class="form-control form-control-user" id="txtDescription"
                                        placeholder="Description" />
                                </div>
                                <div class="form-group">
                                    <input runat="server" type="text" class="form-control form-control-user" id="txtAnswerOption1"
                                        placeholder="Answer Option 1" />
                                </div>
                                <div class="form-group">
                                    <input runat="server" type="text" class="form-control form-control-user" id="txtAnswerOption2"
                                        placeholder="Answer Option 2" />
                                </div>
                                <div class="form-group">
                                    <input runat="server" type="text" class="form-control form-control-user" id="txtAnswerOption3"
                                        placeholder="Answer Option 3" />
                                </div>
                                <div class="form-group">
                                    <input runat="server" type="text" class="form-control form-control-user" id="txtAnswerOption4"
                                        placeholder="Answer Option 4" />
                                </div>
                                <div class="form-group">
                                    <input runat="server" type="text" class="form-control form-control-user" id="txtAnswerOption5"
                                        placeholder="Answer Option 5" />
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-2">
                                        <label>Start Date: </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <input class="form-control form-control-user" runat="server" id="txtStartDate" />
                                    </div>
                                    <div class="col-sm-2">
                                        <label>End Date: </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <input class="form-control form-control-user" runat="server" id="txtEndDate" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-0">
                                        <select runat="server" class="form-control" aria-label="Default select example" name="ddlPollVisibility" id="ddlPollVisibility">
                                            <option selected="" disabled="">Poll Visibility:</option>
                                            <option runat="server" value="Everyone">Everyone</option>
                                            <option runat="server" value="Faculty">Faculty</option>
                                            <option runat="server" value="Students">Students</option>
                                            <option runat="server" value="StudentsPartTime">Students Part-Time</option>
                                            <option runat="server" value="StudentsFullTime">Students Full-Time</option>
                                        </select>
                                    </div>
                                    <div class="col-sm-6">
                                        <select runat="server" class="form-control" aria-label="Default select example" name="ddlResultsVisibility" id="ddlResultsVisibility">
                                            <option selected="" disabled="">Results Visibility:</option>
                                            <option runat="server" value="Private">Private</option>
                                            <option runat="server" value="Public">Public</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="row">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="col-lg-11">
                        <asp:GridView ID="GridView1" CssClass="gvdatatable table table-striped table-bordered" runat="server" OnPreRender="GridView1_PreRender" AutoGenerateColumns="false" DataKeyNames="PollReviewCommentId" EmptyDataText="You currently have no comments.">
                            <Columns>
                                <asp:BoundField DataField="Comment" HeaderText="Comments" SortExpression="Comment" />
                            </Columns>
                        </asp:GridView>
                    </div>
                                </div>

                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_ServerClick" CssClass="btn btn-primary btn-user btn-block" Text="Submit" runat="server" />

                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <script>
        $('#maincontent_txtStartDate').datetimepicker({
            uiLibrary: 'bootstrap4',
            modal: true,
            footer: true
        });
    </script>

    <script>
        $('#maincontent_txtEndDate').datetimepicker({
            uiLibrary: 'bootstrap4',
            modal: true,
            footer: true
        });
    </script>
</asp:Content>
