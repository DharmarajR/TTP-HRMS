<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TTP_HRMS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />

    <title>TTP - Login Page</title>

    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
    <style>
        /*  .btn {
            height: 40px;
            margin-top: 4px;
        }

        .login-msg {
            color: #555;
            margin-top: 60px;
            text-align: center;
        }*/

        body {
            background: #eee
        }

        .card {
            border-radius: 12px
        }

        .card1 {
            background-image: url(../img/aeidEH1.jpg);
            margin-top: 20px
        }

        .first {
            background-repeat: no-repeat;
            background-size: 100% 100%;
            border-radius: 12px;
            max-width: 100%;
            display: block;
            height: auto;
        }

        .linkedin {
            position: relative;
            left: 25px;
            top: 20px;
            font-size: 19px
        }

        .in {
            border-width: 10px;
            color: #fff;
            background: blue;
            font-weight: bold
        }

        .second {
            background: #fff;
            border-radius: 12px;
            background-repeat: no-repeat;
            background-size: 100% 100%;
            max-width: 100%;
            display: block;
            height: auto;
        }

        .welcome {
            margin-top: 130px;
            font-size: 23px;
            font-weight: bold;
        }

        .form-group {
            padding-top: 8px;
            font-size: 12px
        }

        /*.form-control {
            background: #E3F2FD;
            margin-top: 10px;
            font-size: 12px;
            font-weight: bold;
            color: #fff;
            padding-top: 15px;
            padding-bottom: 15px;
            caret-color: red
        }

            .form-control:focus {
                box-shadow: none
            }*/

        .forgot {
            padding-top: 7px;
            color: #42A5F5
        }

        .space {
            padding-top: 28px
        }

        .btn {
            border-radius: 0%;
            font-size: 11px;
            font-weight: bold;
            padding-left: 40px;
            padding-right: 40px;
            padding-top: 10px;
            padding-bottom: 10px
        }

        .btn1 {
            background: #0277BD;
            padding-left: 46px;
            padding-right: 46px
        }

        .btn2 {
            background-color: #fff;
            color: #0277BD;
            margin-left: 10px
        }

        .under {
            font-size: 8px;
            color: #42A5F5;
            padding-top: 40px
        }

        .lower {
            font-size: 8px;
            color: #42A5F5;
            position: relative;
            top: 90%
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div class="card p-5 card1 container">
            <div class="row d-flex justify-content-center">
                <div class="col-md-10">
                    <div class="card">
                        <div class="row no-gutters">
                            <div class="col-md-4 first" style="background-image: url(../img/121223-1016.jpg);">
                                <div class="linkedin"><span class="no-gutters text-primary font-weight-bold"></span></div>
                            </div>
                            <div class="col-md-6 second pl-4 pr-4">
                                <h4 class="welcome text-primary">Human Resource Management</h4>
                               
                                    <div class="form-group">
                                        <label asp-for="userName" class="control-label" style="font-size: 15px;">UserName</label> &nbsp;&nbsp;
                                        <input asp-for="userName" type="text" class="form-control" id="userName" runat="server" />
                                        <span asp-validation-for="userName" class="text-danger"></span>
                                    </div>
                                    <div class="form-group">
                                        <label asp-for="password" class="control-label" style="font-size: 15px;">Password</label>&nbsp;&nbsp;
                                        <input asp-for="password" class="form-control" id="pwd" runat="server" type="password" />
                                        <span asp-validation-for="password" class="text-danger"></span>
                                    </div>
                                    <div class="space">
                                        <input type="submit" id="btnSubmit" runat="server" class="btn btn-primary btn1" value="LOGIN" onserverclick="btnSubmit_ServerClick" />
                                    </div>

                              

                                <div class="row">
                                    <div class="col-sm-4 under">
                                    </div>
                                    <div class="col-sm-3 under">
                                    </div>
                                    <div class="col-sm-4 under">
                                    </div>
                                    <div class="col-md-1	">
                                        <p></p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="~/js/site.min.js"></script>
</body>
</html>
