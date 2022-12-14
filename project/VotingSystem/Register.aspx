<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="VotingSystem.Register" EnableEventValidation="false" %>

<!DOCTYPE html>
<html lang="en">

<head>



    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>BU Voting System - Register</title>

    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">

    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.min.css" rel="stylesheet">

    <%--<style type="text/css">
        .checkboxlist input {
            font: inherit;
            font-size: 0.875em; /* 14px / 16px */
            color: #494949;
            margin-bottom: 12px;
            margin-top: 5px;
            margin-right: 10px !important;
        }
    </style>--%>

</head>

<body class="bg-gradient-primary">
    <form id="frmValid" class="user" runat="server">
        <div class="container">

            <div class="card o-hidden border-0 shadow-lg my-5">
                <div class="card-body p-0">
                    <!-- Nested Row within Card Body -->
                    <div class="row">
                        <%--<div class="col-lg-5 d-none d-lg-block bg-register-image">--%>
                        <div class="col-lg-5 d-none d-lg-block">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <center><img src="img/LogoVote.jpg" /></center>
                        </div>
                        <div class="col-lg-7">
                            <div class="p-5">
                                <div class="text-center">
                                    <h1 class="h4 text-gray-900 mb-4">Create an Account!</h1>
                                </div>
                                <form class="user">
                                    <div class="form-group">
                                        <select runat="server" class="form-control" aria-label="Default select example" name="ddlUserType" id="ddlUserType">
                                            <option selected="" disabled="">Select User Type:</option>
                                            <option runat="server" value="2">Student Part-Time</option>
                                            <option runat="server" value="3">Student Full-Time</option>
                                            <option runat="server" value="4">Faculty</option>
                                        </select>
                                    </div>
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

                                    <%--<a href="login.html" class="btn btn-primary btn-user btn-block">
                                    Register Account
                                </a>--%>

                                    <%--<button id="btnRegister" runat="server" class="btn btn-primary btn-user btn-block" onserverclick="btnRegister_ServerClick">Register Account</button>--%>
                                    <asp:Button ID="btnRegister" OnClick="btnRegister_ServerClick" CssClass="btn btn-primary btn-user btn-block" Text="Register Account" runat="server" />

                                    <%-- <hr>
                                <a href="index.html" class="btn btn-google btn-user btn-block">
                                    <i class="fab fa-google fa-fw"></i> Register with Google
                                </a>
                                <a href="index.html" class="btn btn-facebook btn-user btn-block">
                                    <i class="fab fa-facebook-f fa-fw"></i> Register with Facebook
                                </a>--%>
                                </form>
                                <hr>
                                <div class="text-center">
                                    <a class="small" href="ForgotPassword.aspx">Forgot Password?</a>
                                </div>
                                <div class="text-center">
                                    <a class="small" href="Default.aspx">Already have an account? Login!</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
    <!-- Bootstrap core JavaScript-->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom scripts for all pages-->
    <script src="js/sb-admin-2.min.js"></script>

</body>
