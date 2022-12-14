<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateProfile.aspx.cs" Inherits="VotingSystem.UpdateProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container">
        <div class="card o-hidden border-0 shadow-lg my-5">
            <div class="card-body p-0">
                <!-- Nested Row within Card Body -->
                <%--<div class="row">
                    <div class="col-lg-12">
                        <div class="p-5">
                            <div class="text-center">
                                <h1 class="h4 text-gray-900 mb-4">Update Profile</h1>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <%--<div class="col-lg-3">--%>
                <div class="col-lg-7">
                    
                    <div class="p-5">
                        <div class="text-center">
                            <h1 class="h4 text-gray-900 mb-4">Update Profile</h1>
                        </div>
                        <div class="row">
                            <form class="user">
                                <div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-0">
                                        <input runat="server" type="text" class="form-control form-control-user" id="txtFirstName"
                                            placeholder="First Name">
                                    </div>
                                    <div class="col-sm-6">
                                        <input runat="server" type="text" class="form-control form-control-user" id="txtLastName"
                                            placeholder="Last Name">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <input runat="server" type="email" class="form-control form-control-user" id="txtEmail"
                                        placeholder="Email Address">
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-0">
                                        <input runat="server" type="password" class="form-control form-control-user"
                                            id="txtPassword" placeholder="Password">
                                    </div>
                                    <div class="col-sm-6">
                                        <input runat="server" type="password" class="form-control form-control-user"
                                            id="txtRepeatPassword" placeholder="Repeat Password">
                                    </div>
                                    <br />
                                    <br />
                                </div>

                                <h6 class="h4 text-gray-900 mb-4">Interest:</h6>
                                <div class="form-group">


                                    <asp:CheckBoxList ID="chkInterest" RepeatColumns="5" RepeatLayout="flow" runat="server" RepeatDirection="Horizontal" Width="700px" CssClass="checkboxlist">

                                        <asp:ListItem Value="1">Music</asp:ListItem>
                                        <asp:ListItem Value="2">Movies</asp:ListItem>
                                        <asp:ListItem Value="3">Language</asp:ListItem>
                                        <asp:ListItem Value="4">Humor</asp:ListItem>
                                        <asp:ListItem Value="5">Travel</asp:ListItem>
                                        <asp:ListItem Value="6">Reading</asp:ListItem>
                                        <asp:ListItem Value="7">Food</asp:ListItem>
                                        <asp:ListItem Value="8">Nature</asp:ListItem>
                                        <asp:ListItem Value="9">Pets</asp:ListItem>
                                        <asp:ListItem Value="10">Life</asp:ListItem>
                                        <asp:ListItem Value="11">Art</asp:ListItem>
                                        <asp:ListItem Value="12">Photography</asp:ListItem>
                                        <asp:ListItem Value="13">Relationships</asp:ListItem>
                                        <asp:ListItem Value="14">Culture</asp:ListItem>
                                        <asp:ListItem Value="15">Adventure</asp:ListItem>
                                        <asp:ListItem Value="16">Gaming</asp:ListItem>
                                        <asp:ListItem Value="17">Psychology</asp:ListItem>
                                        <asp:ListItem Value="18">Education</asp:ListItem>
                                        <asp:ListItem Value="19">Writing</asp:ListItem>
                                        <asp:ListItem Value="20">Technology</asp:ListItem>

                                    </asp:CheckBoxList>

                                </div>

                                <asp:Button ID="btnUpdateProfile" OnClick="btnUpdateProfile_ServerClick" CssClass="btn btn-primary btn-user btn-block" Text="Update Profile" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
