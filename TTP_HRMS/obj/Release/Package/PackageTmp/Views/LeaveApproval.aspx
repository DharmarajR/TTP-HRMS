<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="LeaveApproval.aspx.cs" Inherits="TTP_HRMS.Views.LeaveApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
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
        function ShowPopupCost() {
            $("#btnShowPopupCost").click();
        }
        function ShowPopupHistory() {
            $("#btnShowPopupHistory").click();
        }
    </script>
    <style>
        @media (min-width: 768px) {
            .modal-xl {
                width: 90%;
                max-width: 1200px;
            }
        }
    </style>
    <section class="container-fluid content-header" style="border-bottom: 1px dotted #8999A8;">
        <asp:Label ID="lblHeader" runat="server" Text="Leave Approval" Style="color: #fff; font-size: 18px; font-weight: bold; font-family: Helvetica,Arial,Verdana,sans-serif;"></asp:Label>
        <br />
    </section>
    <div class="messagealert" id="alert_container"></div>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="~/img/preloader.gif" AlternateText="Processing" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <br />

    <asp:UpdatePanel ID="Up1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <ajaxToolkit:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme" Width="100%">
                <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="LEAVE APPROVAL">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">

                                    <asp:GridView ID="grdRequestApproval" runat="server" CellPadding="4" AllowPaging="True"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="mGrid" PageSize="10"
                                        AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="grdRequestApproval_PageIndexChanging">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Request Code">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:LinkButton ID="lblCode" runat="server" Text='<%#Bind("Code") %>' CommandArgument='<%#Bind("Code") %>' OnClick="lblCode_Click"> 
                                                        </asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp.Code" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblEmpid" runat="server" Text='<%#Bind("U_EMPID")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp.Name">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblEmpname" runat="server" Text='<%#Bind("U_EMPNAME")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Name">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lbllveName" runat="server" Text='<%#Bind("U_LeaveName")%>' Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Type" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblAgenda" runat="server" Text='<%#Bind("U_TrnsCode")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From Date">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblCouCode" runat="server" Text='<%#Bind("U_StartDate")%>' Width="80px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Date">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblCouName" runat="server" Text='<%#Bind("U_EndDate")%>' Width="80px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No.of Days">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblCouType" runat="server" Text='<%#Bind("U_NoofDays")%>' Width="60px"></asp:Label>
                                                    </div>
                                                    <div align="right" style="display: none;">
                                                        <asp:ImageButton ID="lbtnactivity" runat="server" ImageUrl="~/img/history-icon.png" ToolTip="LeaveBalance" Width="20" Height="20" OnClick="lbtnactivity_Click"></asp:ImageButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblstdate" runat="server" Text='<%#Bind("U_Notes")%>' Width="150px" Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request By" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblReqby" runat="server" Text='<%#Bind("U_RequestByName")%>' Width="150px"></asp:Label>
                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("U_RequestBy")%>' Visible="false"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date of Joining" Visible="false">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblrdoj" runat="server" Text='<%#Bind("startDate")%>' Width="150px" Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Working Company" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblrwcomp" runat="server" Text='<%#Bind("U_S_CompName")%>' Width="150px" Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved Leave History" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:ImageButton ID="lbtnHistory" runat="server" ImageUrl="~/img/history-icon.png" ToolTip="Leave Transactions" Width="20" Height="20" OnClick="lbtnHistory_Click"></asp:ImageButton>
                                                    </div>
                                                    <%----%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delegate Employee" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblRplEmpid" runat="server" Text='<%#Bind("U_RplEmpId")%>' Width="100px" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblRplEmpName" runat="server" Text='<%#Bind("U_RplEmpName")%>' Width="100px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approval Status">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblAppStatus" runat="server" Text='<%#Bind("U_Status")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:TextBox ID="txtRemarks" runat="server" Text='<%#Bind("U_AppRemarks")%>' ToolTip='<%#Bind("U_AppRemarks")%>' Width="150px" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Address">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblreason" runat="server" Text='<%#Bind("U_S_PEmAdd")%>' Width="150px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone No">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblPhone" runat="server" Text='<%#Bind("U_S_PEConNo")%>' Width="100px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachment" Visible="False">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:LinkButton ID="lnkEDownload" Text="Download" CommandArgument='<%# Eval("U_S_PAttach")%>'
                                                            runat="server" Width="150px"></asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Approval Required" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblAppreq" runat="server" Text='<%#Bind("U_AppRequired")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested Date" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblreqdt" runat="server" Text='<%#Bind("U_AppReqDate")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested Time" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblreqtime" runat="server" Text='<%#Bind("U_ReqTime")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Approver" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblcurapp" runat="server" Text='<%#Bind("U_CurApprover")%>' Width="150px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Approver">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblnxtapp" runat="server" Text='<%#Bind("U_NxtApprover")%>' Width="150px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alternative Current Approver" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblAltcurapp" runat="server" Text='<%#Bind("U_CurApprover1")%>' Width="150px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alternative Next Approver" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblAltnxtapp" runat="server" Text='<%#Bind("U_NxtApprover1")%>' Width="150px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rejected Payroll" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblRejPayroll" runat="server" Text='<%#Bind("U_RejPayroll")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:ImageButton ID="ImgAppHistory" runat="server" CommandArgument='<%#Bind("Code") %>' ImageUrl="~/img/history-icon.png" ToolTip="Approval History" Width="20" Height="20" OnClick="ImgAppHistory_Click"></asp:ImageButton>
                                                    </div>                                                  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" CssClass="GridBG" />
                                        <PagerStyle CssClass="pgr" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="LEAVE SUMMARY">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSummary" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No Records Found" CssClass="mGrid" HeaderStyle-CssClass="GridBG"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                                        Width="100%" PageSize="10" OnPageIndexChanging="grdSummary_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblSCode" runat="server" Text='<%#Bind("Code") %>'>
                                                        </asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp.Code" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsEmpid" runat="server" Text='<%#Bind("U_EMPID")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp.Name">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsEmpname" runat="server" Text='<%#Bind("U_EMPNAME")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Name">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lbllveName" runat="server" Text='<%#Bind("U_LeaveName")%>' Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Type" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsAgenda" runat="server" Text='<%#Bind("U_TrnsCode")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From Date" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblsCouCode" runat="server" Text='<%#Bind("U_StartDate")%>' Width="100px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Date" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblsCouName" runat="server" Text='<%#Bind("U_EndDate")%>' Width="100px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No.of Days">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsCouType" runat="server" Text='<%#Bind("U_NoofDays")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsstdate" runat="server" Text='<%#Bind("U_Notes")%>' ToolTip='<%#Bind("U_Notes")%>' Width="150px" Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request For" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblReqbys" runat="server" Text='<%#Bind("U_RequestByName")%>' Width="150px"></asp:Label>
                                                        <asp:Label ID="Label3s" runat="server" Text='<%#Bind("U_RequestBy")%>' Visible="false"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date of Joining" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblSdoj" runat="server" Text='<%#Bind("startDate")%>' Width="100px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Working Company" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblcanstatus" runat="server" Text='<%#Bind("U_Canelled")%>'></asp:Label>
                                                        <asp:Label ID="lblSwcomp" runat="server" Text='<%#Bind("U_S_CompName")%>' Width="150px" Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delegate Employee" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsRplEmpid" runat="server" Text='<%#Bind("U_RplEmpId")%>' Width="100px" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblsRplEmpName" runat="server" Text='<%#Bind("U_RplEmpName")%>' Width="100px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payroll Month" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblspaymonth" runat="server" Text='<%#Bind("U_Month")%>' Width="100px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payroll Year" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblspayyear" runat="server" Text='<%#Bind("U_Year")%>' Width="100px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approval Status">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsAppStatus" runat="server" Text='<%#Bind("U_Status")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Address" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsreason" runat="server" Text='<%#Bind("U_S_PEmAdd")%>' Width="150px" Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone No" Visible="False">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsPhone" runat="server" Text='<%#Bind("U_S_PEConNo")%>' Width="100px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachment" Visible="False">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("U_S_PAttach")%>'
                                                            runat="server" Width="100px"></asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Approval Required" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsAppreq" runat="server" Text='<%#Bind("U_AppRequired")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested Date" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsreqdt" runat="server" Text='<%#Bind("U_AppReqDate")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested Time" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsreqtime" runat="server" Text='<%#Bind("U_ReqTime")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Approver" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblscurapp" runat="server" Text='<%#Bind("U_CurApprover")%>' Width="150px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Approver">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsnxtapp" runat="server" Text='<%#Bind("U_NxtApprover")%>' Width="150px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alternative Current Approver" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsAltcurapp" runat="server" Text='<%#Bind("U_CurApprover1")%>' Width="150px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alternative Next Approver" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsAltnxtapp" runat="server" Text='<%#Bind("U_NxtApprover1")%>' Width="150px"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rejected Payroll" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsRejPayroll" runat="server" Text='<%#Bind("U_RejPayroll")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:ImageButton ID="ImgAppSumHisotry" runat="server" CommandArgument='<%#Bind("Code") %>' ImageUrl="~/img/history-icon.png" ToolTip="Approval History" Width="20" Height="20" OnClick="ImgAppSumHisotry_Click"></asp:ImageButton>
                                                    </div>                                                  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>


            <button type="button" style="display: none;" id="btnShowPopupCost" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#myModal">
            </button>
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Leave Approval Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="lblempId" class="col-md-4 control-label input-sm">Employee Id</label>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblempId" CssClass="form-control input-sm" runat="server"></asp:Label>
                                            <asp:Label ID="txtcode" CssClass="form-control input-sm" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="lblempName" class="col-md-4 control-label input-sm">Employee Name</label>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblempName" CssClass="form-control input-sm" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="lblLveCode" class="col-md-4 control-label input-sm">Leave Code</label>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblLveCode" CssClass="form-control input-sm" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="lblempName" class="col-md-4 control-label input-sm">Leave Name</label>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblLveName" CssClass="form-control input-sm" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="txtfrmdate" class="col-md-4 control-label input-sm">Start Date</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtfrmdate" CssClass="form-control input-sm" runat="server" TextMode="Date" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="txteddate" class="col-md-4 control-label input-sm">End Date</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txteddate" CssClass="form-control input-sm" runat="server" TextMode="Date" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="lblNoofdays" class="col-md-4 control-label input-sm">No.of Days</label>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblNoofdays" CssClass="form-control input-sm" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="lblRemarks" class="col-md-4 control-label input-sm">Remarks</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="lblRemarks" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="lbldelegate" class="col-md-4 control-label input-sm">Delegate Employee</label>
                                        <div class="col-md-6">
                                            <asp:Label ID="lbldelegate" CssClass="form-control input-sm" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="lblRemarks" class="col-md-4 control-label input-sm">Address (During Leave)</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtAddress" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="ddlYear" class="col-md-4 control-label input-sm">Year</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlYear" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="ddlMonth" class="col-md-4 control-label input-sm">Month</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlMonth" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label for="txtAppRemarks" class="col-md-4 control-label input-sm">Approvar Remarks</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtAppRemarks" CssClass="form-control input-sm" runat="server"></asp:TextBox>
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
                                            <button type="button" id="btnApprove" runat="server" class="btn btn-md btn-success" onserverclick="btnApprove_ServerClick">
                                                <span class="glyphicon glyphicon-ok"></span>&nbsp;APPROVED
                                            </button>
                                            <button type="button" id="btnRejected" runat="server" class="btn btn-md btn-danger" onserverclick="btnRejected_ServerClick">
                                                <span class="glyphicon glyphicon-remove"></span>&nbsp;REJECTED
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>


             <button type="button" style="display: none;" id="btnShowPopupHistory" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#myModalHistory">
            </button>
            <div class="modal fade" id="myModalHistory" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Leave Approval History</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                    <div class="col-lg-12 ">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdApprovalHis" runat="server"
                                                AutoGenerateColumns="False" CellPadding="4" CssClass="mGrid" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="True"
                                                Width="100%">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:BoundField DataField="U_EmpId" HeaderText="Approver Emp.ID" />
                                                    <asp:BoundField DataField="U_EmpName" HeaderText="Approver Emp.Name" />
                                                    <asp:BoundField DataField="U_ApproveBy" HeaderText="Approved By" />
                                                    <asp:BoundField DataField="CreateDate" HeaderText="Approved Date" />
                                                    <asp:BoundField DataField="CreateTime" HeaderText="Approved Time" />
                                                    <asp:BoundField DataField="UpdateDate" HeaderText="Update Date" />
                                                    <asp:BoundField DataField="UpdateTime" HeaderText="Update Time" />
                                                    <asp:BoundField DataField="U_AppStatus" HeaderText="Approved Status" />
                                                   <asp:BoundField DataField="U_Remarks" HeaderText="Remarks" />
                                                    <asp:BoundField DataField="U_AltApproveBy" HeaderText="Alternate Approved By" />                                                 
                                                   
                                                </Columns>
                                                <HeaderStyle BackColor="#CCCCCC" Height="25px" HorizontalAlign="Center" CssClass="GridBG" />
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                           
                        </div>
                        <br />
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnApprove" />
            <asp:PostBackTrigger ControlID="btnRejected" />
        </Triggers>
    </asp:UpdatePanel>



</asp:Content>
