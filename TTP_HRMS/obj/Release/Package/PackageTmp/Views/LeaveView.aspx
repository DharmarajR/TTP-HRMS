<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="LeaveView.aspx.cs" Inherits="TTP_HRMS.Views.LeaveView" %>

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
    </script>
    <section class="container-fluid content-header" style="border-bottom: 1px dotted #8999A8;">
        <asp:Label ID="lblHeader" runat="server" Text="Leave Request" Style="color: #fff; font-size: 18px; font-weight: bold; font-family: Helvetica,Arial,Verdana,sans-serif;"></asp:Label>
        <br />
    </section>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="~/img/preloader.gif" AlternateText="Processing" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
     <div class="messagealert" id="alert_container"></div>
    <br />
    <asp:UpdatePanel ID="Up1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-10">
                    <button type="button" id="btnNew" runat="server" class="btn btn-md  btn-primary" onserverclick="btnNew_ServerClick">
                        <span class="glyphicon glyphicon-plus"></span>&nbsp;NEW
                    </button>
                </div>
            </div>
            <br />
            <ajaxToolkit:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme" Width="100%">
                <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="LEAVE REQUEST">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">

                                    <asp:GridView ID="grdLeaveRequest" runat="server" AllowPaging="True" ShowHeaderWhenEmpty="True"
                                        CssClass="mGrid" AutoGenerateColumns="False" Width="100%" PageSize="10" OnPageIndexChanging="grdLeaveRequest_PageIndexChanging">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Request Code">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:LinkButton ID="lbtndocnum" runat="server" Text='<%#Bind("Code") %>' CommandArgument='<%#Bind("Code") %>' OnClick="lbtndocnum_Click"></asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Code" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lbllvecode" runat="server" Text='<%#Bind("U_TrnsCode")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Type">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lbllvetype" runat="server" Text='<%#Bind("Name") %>' Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblstdate" runat="server" Text='<%#Bind("U_StartDate")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lbledDate" runat="server" Text='<%#Bind("U_EndDate")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No.Of Days">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lbldays" runat="server" Text='<%#Bind("U_NoofDays")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ReJoin Date" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblRejoin" runat="server" Text='<%#Bind("U_ReJoiNDate")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblreason" runat="server" Text='<%#Bind("U_Notes")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delegate Employee" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblRplEmpid" runat="server" Text='<%#Bind("U_RplEmpId")%>' Width="100px" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblRplEmpName" runat="server" Text='<%#Bind("U_RplEmpName")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone No" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblPhone" runat="server" Text='<%#Bind("U_S_PEConNo")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachment" Visible="false">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:LinkButton ID="lnkEDownload" Text="Download" CommandArgument='<%# Eval("U_S_PAttach")%>'
                                                            runat="server" Width="100px" OnClick="lnkEDownload_Click"></asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request By" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblReqby" runat="server" Text='<%#Bind("U_RequestByName")%>'></asp:Label>
                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("U_RequestBy")%>' Visible="false"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document Status" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lbldocStat" runat="server" Text='<%#Bind("DocStatus")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Owner Status" Visible="true">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblOwnerStatus" runat="server" Text='<%#Bind("U_OwnerStatus")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cancelled Status" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblcanstatus" runat="server" Text='<%#Bind("U_Canelled")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" Visible="true">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Bind("U_Status")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approver Remarks">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblAppRemark" runat="server" Text='<%#Bind("U_AppRemarks")%>' ToolTip='<%#Bind("U_AppRemarks")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Approver" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblCurApp" runat="server" Text='<%#Bind("U_CurApprover")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Approver" Visible="true">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblNxtApp" runat="server" Text='<%#Bind("U_NxtApprover")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rejected Payroll" Visible="false">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblRejPayroll" runat="server" Text='<%#Bind("U_RejPayroll")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="true">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:ImageButton ID="lbtAppHistory" ImageUrl="~/img/history-icon.png" ToolTip="Approval History" runat="server" Width="20" Height="20" OnClick="lbtAppHistory_Click" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" CssClass="GridBG" />
                                        <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="LEAVE APPROVED HISTORY">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSummary" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true"
                                        CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="false" Width="100%" PageSize="10" OnPageIndexChanging="grdSummary_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Request Code">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lbtsdocnum" runat="server" Text='<%#Bind("Code") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Code">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblslvecode" runat="server" Text='<%#Bind("U_S_PTrnsCode")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Name">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblslvetype" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblsstdate" runat="server" Text='<%#Bind("U_S_PStartDate")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblsedDate" runat="server" Text='<%#Bind("U_S_PEndDate")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No.Of Days">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsdays" runat="server" Text='<%#Bind("U_S_PNoofDays")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsreason" runat="server" Text='<%#Bind("U_S_PNotes")%>' ToolTip='<%#Bind("U_S_PNotes")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delegate Employee">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsRplEmpid" runat="server" Text='<%#Bind("U_RplEmpId")%>' Width="100px" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblsRplEmpName" runat="server" Text='<%#Bind("U_RplEmpName")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone No">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        <asp:Label ID="lblsPhone" runat="server" Text='<%#Bind("U_S_PEConNo")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachment">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("U_S_PAttachment")%>'
                                                        runat="server" Width="100px" OnClick="lnkDownload_Click"></asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
