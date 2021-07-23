<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TTP_HRMS.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Chart.min.js"></script>
    <link href="../Content/Chart.min.css" rel="stylesheet" />
    <%--<script src='https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.1.4/Chart.bundle.min.js'></script>--%>
    <style>
        .DivCorner {
            border: solid 0px red;
            height: 160px;
            width: 220px;
            background-color: white;
            cursor: pointer;
            border-radius: 10px;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
        }
    </style>
    <script>
        $(document).ready(function () {
            var colors = ['#e09200', '#00a300', '#FF0000', '#c3e6cb', '#dc3545', '#6c757d'];
            var donutOptions = {
                cutoutPercentage: 65,
                responsive: true,
                legend: { position: 'right', padding: 50, right: 0, left: 0, bottom:0, labels: { pointStyle: 'circle', usePointStyle: true, radius: 3 } }
            };
            var chDonutData3 = {
                labels: ['Pending', 'Approved', 'Rejected'],
                datasets: [
                    {
                        backgroundColor: colors.slice(0, 3),
                        borderWidth: 0,
                        data: [21, 45, 55]
                    }
                ]
            };
            var chDonut3 = document.getElementById("chDonut3");
            if (chDonut3) {
                new Chart(chDonut3, {
                    type: 'doughnut',
                    data: chDonutData3,
                    options: donutOptions
                });
            }


            var chDonutData4 = {
                labels: ['Pending', 'Approved', 'Rejected'],
                datasets: [
                    {
                        backgroundColor: colors.slice(0, 3),
                        borderWidth: 0,
                        data: [15, 25, 5]
                    }
                ]
            };
            var chDonut4 = document.getElementById("chDonut4");
            if (chDonut4) {
                new Chart(chDonut4, {
                    type: 'pie',
                    data: chDonutData4,
                    options: donutOptions
                });
            }
        });
    </script>
    <br />
    <section class="container-fluid content-header" style="border-bottom: 1px dotted #8999A8;">
        <asp:Label ID="lblHeader" runat="server" Text="Document Types" Style="color: white; font-size: 18px; font-weight: bold; font-family: Helvetica,Arial,Verdana,sans-serif;"></asp:Label>
        <br />
    </section>
    <asp:Label runat="server" ID="lblMsg" Visible="false"></asp:Label>
    <section class="container-fluid" style="border: solid 1px #8999A8;">
        <br />
        <div class="col-sm-6 col-md-4 col-lg-3" id="LR" runat="server">
            <div class="DivCorner" onclick="location.href='LeaveView.aspx';">
                <br />
                <span style="font-size: 10pt; font-weight: bold; color: #000; margin-left: 5px;">Leave Request</span><br />
                <div style="border:solid 0px red;">
                    <div class="card" >
                        <div class="card-body">
                            <canvas id="chDonut3"></canvas>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </div>
        <div class="col-sm-6 col-md-4 col-lg-3" id="LA" runat="server">
            <div class="DivCorner" onclick="location.href='LeaveApproval.aspx';">
                <br />
                <span style="font-size: 10pt; font-weight: bold; color: #000; margin-left: 5px;">Leave Approval</span><br />
                <div class="card">
                    <div class="card-body">
                        <canvas id="chDonut4"></canvas>
                    </div>
                </div>
            </div>
            <br />
        </div>
    </section>
</asp:Content>
