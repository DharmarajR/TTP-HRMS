<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="LeaveEntry.aspx.cs" Inherits="TTP_HRMS.Views.LeaveEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">   
    <script src="../Scripts/jquery-3.4.1.min.js"></script>     
    <link rel="stylesheet" type="text/css" href="../DataTables/datatables.min.css" />
    <script type="text/javascript" src="../DataTables/datatables.min.js"></script>
    <script type="text/javascript">
        function ShowPopupCost() {
            $("#btnShowPopupCost").click();
        }
        function ShowPopupRplEmp() {
            $("#btnShowPopupRplEmp").click();
        }
        $(function () {
            $('[id*=grdRpl]').prepend($("<thead></thead>").append($('[id*=grdRpl]').find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers"
            });
        });
        $(function () {
            $('[id*=GrdLeave]').prepend($("<thead></thead>").append($('[id*=GrdLeave]').find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers"
            });
        });
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        }
        function popupdisplay(option, uniqueid, tripno) {
            if (uniqueid.length > 0) {
                var uniid = document.getElementById("<%=txtpopunique.ClientID%>").value;
                var tno = document.getElementById("<%=txtpoptno.ClientID%>").value;
                var opt = document.getElementById("<%=txthidoption.ClientID%>").value;
                uniid = ""; tno = ""; opt = "";
                if (uniid != uniqueid && tno != tripno && opt != option) {
                    document.getElementById("<%=txtpopunique.ClientID%>").value = uniqueid;
                    document.getElementById("<%=txtpoptno.ClientID%>").value = tripno;
                    document.getElementById("<%=txthidoption.ClientID%>").value = option;
                     document.getElementById("<%=Btncallpop.ClientID%>").onclick();
                 }
             }
        }
        function AllowNumbers(el) {
            var ex = /^\d*\.?\d{0,6}$/;// /^[0-9.]+$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>
    <section>
        <asp:TextBox ID="txtHEmpID" runat="server" Width="93px" Style="display: none"></asp:TextBox>
        <asp:TextBox ID="txtpopunique" runat="server" Style="display: none"></asp:TextBox>
        <asp:TextBox ID="txtpoptno" runat="server" Style="display: none"></asp:TextBox>
        <asp:TextBox ID="txthidoption" runat="server" Style="display: none"></asp:TextBox>
        <asp:TextBox ID="txttname" runat="server" Style="display: none"></asp:TextBox>
        <input id="Btncallpop" runat="server" style="display: none"
            type="button" value="button" onserverclick="Btncallpop_ServerClick" />
    </section>
    <section class="container-fluid content-header" style="border-bottom: 1px dotted #8999A8;">
        <asp:Label ID="lblHeader" runat="server" Text="Leave Request" Style="color: #fff; font-size: 18px; font-weight: bold; font-family: Helvetica,Arial,Verdana,sans-serif;"></asp:Label>
        <br />
    </section>
    <div class="messagealert" id="alert_container"></div>
    <br />
    <section class="container-fluid" style="background-color:#fff; border-radius:10px; ">
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <asp:Image ID="Image1" ImageUrl="~/img/preloader.gif" AlternateText="Processing" runat="server" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
       <br />
        <asp:UpdatePanel ID="Up1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-6 col-lg-6">
                        <div class="form-group">
                            <label for="txtlveType" class="col-md-4 control-label input-sm">Leave Type</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtcode" CssClass="form-control input-sm" runat="server" Visible="false"></asp:TextBox>
                                <asp:Label ID="lbllvecode" runat="server" CssClass="form-control input-sm" Visible="false"></asp:Label>
                                <div class="input-group add-on">
                                    <asp:TextBox ID="txtlveType" runat="server" CssClass="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <button class="btn btn-default input-sm" type="button" id="btnLveType" runat="server" onclick="ShowPopupCost();"><i class="glyphicon glyphicon-search"></i></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-6">
                        <div class="form-group">
                            <label for="txtLvebalance" class="col-md-4 control-label input-sm">Leave Balance</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtlveBal" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-lg-6">
                        <div class="form-group">
                            <label for="txtfrmdate" class="col-md-4 control-label input-sm">Start Date</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtfrmdate" CssClass="form-control input-sm" runat="server" TextMode="Date" AutoPostBack="true" OnTextChanged="txtfrmdate_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-6">
                        <div class="form-group">
                            <label for="txteddate" class="col-md-4 control-label input-sm">End Date</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txteddate" CssClass="form-control input-sm" runat="server" AutoPostBack="true" TextMode="Date" OnTextChanged="txteddate_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-lg-6">
                        <div class="form-group">
                            <label for="txtnodays" class="col-md-4 control-label input-sm">No.of Days</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtnodays" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-6">
                        <div class="form-group">
                            <label for="txtReason" class="col-md-4 control-label input-sm">Reason</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:TextBox ID="txtoverlap" CssClass="form-control input-sm" runat="server" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-lg-6">
                        <div class="form-group">
                            <label for="txtRplEmpId" class="col-md-4 control-label input-sm">Delegate Employee</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtRplEmpId" CssClass="form-control input-sm" runat="server" Visible="false"></asp:TextBox>
                                <div class="input-group add-on">
                                    <asp:TextBox ID="txtRplempname" runat="server" CssClass="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <button class="btn btn-default input-sm" type="button" id="btnrplsearch" runat="server" onclick="ShowPopupRplEmp();"><i class="glyphicon glyphicon-search"></i></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-6">
                        <div class="form-group">
                            <label for="txtAddress" class="col-md-4 control-label input-sm">Address (During Leave)</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtConAddress" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:TextBox ID="txtcutoff" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtotalbal" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtblockTrans" runat="server" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                  <div class="row">
                        <div class="col-sm-6 col-lg-6">
                            <div class="form-group">
                                <label for="txtPhone" class="col-md-4 control-label input-sm">Phone No.</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:TextBox ID="txtconReq" CssClass="form-control input-sm" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtrejoin" CssClass="form-control input-sm" runat="server" Visible="false"></asp:TextBox>
                                     <asp:Label ID="lblAttachment" CssClass="form-control input-sm" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                       <div class="col-sm-6 col-lg-6">
                            <div class="form-group">
                                <label for="txtPhone" class="col-md-4 control-label input-sm">Attachment</label>
                                <div class="col-md-6">
                                    <asp:FileUpload ID="fileattach" runat="server" />
                                    <asp:TextBox ID="txtfile" CssClass="form-control input-sm" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtAppTempId" CssClass="form-control input-sm" runat="server" Visible="false"></asp:TextBox>
                                      <asp:Label ID="lblBalanceLeave" CssClass="form-control input-sm" runat="server" Visible="false"></asp:Label>
                                     <asp:Label ID="lblBlockTransaction" CssClass="form-control input-sm" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                  <div class="row">
                        <div class="col-sm-6 col-lg-6" id="emp" runat="server">
                            <div class="form-group">
                                <label for="txtAppDate" class="col-md-4 control-label input-sm">Document Status</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlDocStatus" runat="server" CssClass="form-control input-sm">
                                        <asp:ListItem Value="D">Draft</asp:ListItem>
                                        <asp:ListItem Value="C">Confirm</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 col-lg-6" id="Owner" runat="server">
                            <div class="form-group">
                                <label for="txtAppDate" class="col-md-4 control-label input-sm">Owner Status</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlOwnerStatus" runat="server" CssClass="form-control input-sm" AutoPostBack="true">
                                        <asp:ListItem Value="P">Pending</asp:ListItem>
                                        <asp:ListItem Value="C">Confirm</asp:ListItem>
                                        <asp:ListItem Value="L">Cancel</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                   <br />
                    <div class="row">
                        <div class="col-sm-6 col-lg-6">
                            <div class="form-group">
                                <label for="txtprice" class="col-md-4 control-label"></label>
                                <div class="col-md-8">
                                    <asp:Button ID="btnSave" class="btn btn-md btn-pill btn-success" runat="server" Text="SUBMIT" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnWithdraw" class="btn btn-md btn-pill btn-info" runat="server" Text="WITHDRAW REQUEST" />
                                    <asp:Button ID="btnCancel" class="btn btn-md btn-pill btn-info" runat="server" Text="CANCEL REQUEST" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnClear" class="btn btn-md btn-pill btn-danger" runat="server" Text="BACK" PostBackUrl="~/Views/LeaveView.aspx" /><br />
                                </div>
                            </div>
                        </div>
                    </div>
                <br /><br />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>

         <button type="button" style="display: none;" id="btnShowPopupCost" class="btn btn-primary btn-lg"
                    data-toggle="modal" data-target="#myModal">
                </button>
                <div class="modal fade" id="myModal">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title">Leave Type</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:GridView ID="GrdLeave" runat="server" CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr"
                                            AlternatingRowStyle-CssClass="mousecursor" AutoGenerateColumns="false" RowStyle-CssClass="mousecursor"
                                            Style="max-width: 100%" OnRowDataBound="GrdLeave_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="U_S_PCode" HeaderText="Leave Code" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="U_S_PName" HeaderText="Leave Name" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Balance" HeaderText="Leave Balance" ItemStyle-HorizontalAlign="Right" />
                                            </Columns>

                                            <RowStyle BorderColor="#8999A8" BorderWidth="1px" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#4BAFF8" CssClass="GridBG" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>

         <button type="button" style="display: none;" id="btnShowPopupRplEmp" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#myModalRplemp">
            </button>
            <div class="modal fade" id="myModalRplemp">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">List of Employees</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:GridView ID="grdRpl" runat="server" CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="mousecursor" AutoGenerateColumns="false" RowStyle-CssClass="mousecursor"
                                        Style="max-width: 100%" OnRowDataBound="grdRpl_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="empID" HeaderText="Employee Code" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Left" />
                                        </Columns>
                                        <RowStyle BorderColor="#8999A8" BorderWidth="1px" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#4BAFF8" CssClass="GridBG" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
    </section>
</asp:Content>
