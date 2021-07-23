<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeProfile.aspx.cs" Inherits="TTP_HRMS.Views.EmployeeProfile" %>

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
        function ShowPopup() {
            $("#btnShowPopup").click();
        }
    </script>
    <style>
         .modalPopup {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
    </style>
    <section class="container-fluid content-header" style="border-bottom: 1px dotted #8999A8;">
        <asp:Label ID="lblHeader" runat="server" Text="Employee Profile" Style="color: #fff; font-size: 18px; font-weight: bold; font-family: Helvetica,Arial,Verdana,sans-serif;"></asp:Label>
        <br />
    </section>
    <section class="container-fluid" style="background-color: #fff; border-radius: 10px;">
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
                    <div class="col-md-10">
                        <div class="row">
                            <div class="col-md-6">
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtEmpNo" class="col-md-4 control-label input-sm">Employee No</label>
                                    <div class="col-md-6">
                                        <asp:Label ID="txtEmpNo" CssClass="form-control input-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtfirstName" class="col-md-4 control-label input-sm">First Name</label>
                                    <div class="col-md-6">
                                        <asp:Label ID="txtfirstName" CssClass="form-control input-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtMiddleName" class="col-md-4 control-label input-sm">Middle Name</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtMiddleName" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtthirdName" class="col-md-4 control-label input-sm">Third Name</label>
                                    <div class="col-md-6">
                                        <asp:Label ID="txtthirdName" CssClass="form-control input-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtlastName" class="col-md-4 control-label input-sm">Last Name</label>
                                    <div class="col-md-6">
                                        <asp:Label ID="txtlastName" CssClass="form-control input-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtPosition" class="col-md-4 control-label input-sm">Position</label>
                                    <div class="col-md-6">
                                        <asp:Label ID="txtPosition" CssClass="form-control input-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtOfficephone" class="col-md-4 control-label input-sm">Office Phone</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtOfficephone" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtdept" class="col-md-4 control-label input-sm">Department</label>
                                    <div class="col-md-6">
                                        <asp:Label ID="txtdept" CssClass="form-control input-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtmobile" class="col-md-4 control-label input-sm">Mobile Phone</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtmobile" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtManager" class="col-md-4 control-label input-sm">Manager</label>
                                    <div class="col-md-6">
                                        <asp:Label ID="txtManager" CssClass="form-control input-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txthomePhone" class="col-md-4 control-label input-sm">Home Phone</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txthomePhone" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtEmail" class="col-md-4 control-label input-sm">Email</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtEmail" CssClass="form-control input-sm" runat="server" TextMode="Email" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtfax" class="col-md-4 control-label input-sm">Fax</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtfax" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtWorkCompany" class="col-md-4 control-label input-sm">Working Company</label>
                                    <div class="col-md-6">
                                        <asp:Label ID="txtWorkCompany" CssClass="form-control input-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <br />
                        <br />
                        <asp:Image ID="Image2" runat="server" Height="150" Width="170" />&nbsp;&nbsp;
                    </div>
                </div>
                <br />
                <ajaxToolkit:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme" Width="100%">
                    <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Address">
                        <ContentTemplate>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <span class="h5" style="text-decoration: underline;">Work Address</span>
                                </div>
                                <div class="col-md-6">
                                    <span class="h5" style="text-decoration: underline;">Home Address</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtWstreet" class="col-md-3 control-label input-sm">Street</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtWstreet" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtHstreet" class="col-md-3 control-label input-sm">Street</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtHstreet" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtWblock" class="col-md-3 control-label input-sm">Block</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtWblock" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtHblock" class="col-md-3 control-label input-sm">Block</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtHblock" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtWfloor" class="col-md-3 control-label input-sm">Building/Floor/Room</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtWfloor" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtHfloor" class="col-md-3 control-label input-sm">Building/Floor/Room</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtHfloor" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtWzipcode" class="col-md-3 control-label input-sm">ZipCode</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtWzipcode" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtHzipcode" class="col-md-3 control-label input-sm">ZipCode</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtHzipcode" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtWcity" class="col-md-3 control-label input-sm">City</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtWcity" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtHcity" class="col-md-3 control-label input-sm">City</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtHcity" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtWcounty" class="col-md-3 control-label input-sm">County</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtWcounty" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtHcounty" class="col-md-3 control-label input-sm">County</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtHcounty" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="ddlWstate" class="col-md-3 control-label input-sm">State</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlWstate" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="ddlhstate" class="col-md-3 control-label input-sm">State</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlhstate" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="ddlWcountry" class="col-md-3 control-label input-sm">Country</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlWcountry" CssClass="form-control input-sm" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlWcountry_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="ddlhcountry" class="col-md-3 control-label input-sm">Country</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlhcountry" CssClass="form-control input-sm" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlhcountry_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Personal">
                        <ContentTemplate>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="ddlgender" class="col-md-3 control-label input-sm">Gender</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlgender" CssClass="form-control input-sm" runat="server" Enabled="false">
                                                <asp:ListItem Value="M">Male</asp:ListItem>
                                                <asp:ListItem Value="F">FeMale</asp:ListItem>
                                                <asp:ListItem Value="E">Not Specified</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="ddlcitizen" class="col-md-4 control-label input-sm">Citizenship</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlcitizen" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtdob" class="col-md-3 control-label input-sm">Date of Birth</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtdob" CssClass="form-control input-sm" runat="server" TextMode="Date" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtpassportno" class="col-md-4 control-label input-sm">Passport No.</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtpassportno" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="ddlcountryofbirth" class="col-md-3 control-label input-sm">Country of Birth</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlcountryofbirth" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtpaasportexpiry" class="col-md-4 control-label input-sm">Passport Expiration Date</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtpaasportexpiry" CssClass="form-control input-sm" runat="server" TextMode="Date" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="ddlmarital" class="col-md-3 control-label input-sm">Marital Status</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlmarital" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <span class="col-md-6" style="text-decoration: underline; font-size: 12px; font-weight: bold;">Emergency Contact Details</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtNoofchild" class="col-md-3 control-label input-sm">No.of Children</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtNoofchild" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtrelName" class="col-md-4 control-label input-sm">Relation Name</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtrelName" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtidno" class="col-md-3 control-label input-sm">ID No.</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtidno" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtrelType" class="col-md-4 control-label input-sm">RelationShip Type</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtrelType" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtContactno" class="col-md-4 control-label input-sm">Contact Number</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtContactno" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Education Details" Visible="false">
                        <ContentTemplate>
                            <br />
                            <div class="row">
                                <div class="col-sm-10">
                                    <button type="button" id="btnNew" runat="server" class="btn btn-md  btn-primary" onclick="ShowPopup();">
                                        <span class="glyphicon glyphicon-plus"></span>&nbsp;NEW
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 ">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdEducation" runat="server" CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr"
                                            AlternatingRowStyle-CssClass="mousecursor" AutoGenerateColumns="false" RowStyle-CssClass="mousecursor"
                                            Style="max-width: 99%">
                                            <Columns>
                                                <asp:BoundField DataField="empID" HeaderText="empid" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                <asp:BoundField DataField="line" HeaderText="lineno" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                <asp:BoundField DataField="fromDate" HeaderText="Date From" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="toDate" HeaderText="To" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="name" HeaderText="Education Type" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="institute" HeaderText="Institute" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="major" HeaderText="Major" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="diploma" HeaderText="Diploma" ItemStyle-HorizontalAlign="Left" />
                                            </Columns>
                                            <RowStyle BorderColor="#8999A8" BorderWidth="1px" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#4BAFF8" CssClass="GridBG" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="Bank Details">
                        <ContentTemplate>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="ddlbankname" class="col-md-3 control-label input-sm">Bank Name</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlbankname" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtaccno" class="col-md-3 control-label input-sm">Account No.</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtaccno" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtbranchName" class="col-md-3 control-label input-sm">Branch Name</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtbranchName" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
                <br />
                <div class="row">
                    <div class="col-sm-6 col-lg-6">
                        <div class="form-group">
                            <label for="txtprice" class="col-md-4 control-label"></label>
                            <div class="col-md-8">
                                <asp:Button ID="btnSave" class="btn btn-md btn-pill btn-primary" runat="server" Text="EDIT" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" class="btn btn-md btn-pill btn-danger" runat="server" Text="CANCEL" PostBackUrl="~/Views/Dashboard.aspx" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>

        <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
            data-toggle="modal" data-target="#myModal">
        </button>
        <div class="modal fade" id="myModal">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Education Details</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtFromDate" class="col-md-4 control-label input-sm">Date From</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtFromDate" CssClass="form-control input-sm" runat="server" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txttodate" class="col-md-4 control-label input-sm">To</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txttodate" CssClass="form-control input-sm" runat="server" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="ddledutype" class="col-md-4 control-label input-sm">Education Type</label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddledutype" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtinstitute" class="col-md-4 control-label input-sm">Institute</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtinstitute" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtmajor" class="col-md-4 control-label input-sm">Major</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtmajor" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtdiploma" class="col-md-4 control-label input-sm">Diploma</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtdiploma" CssClass="form-control input-sm" runat="server"></asp:TextBox>
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
                                        <asp:Button ID="btnEdusave" class="btn btn-md btn-pill btn-primary" runat="server" Text="SAVE" OnClick="btnEdusave_Click" />
                                    </div>
                                </div>
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
