<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TTP_HRMS.Views.ChangePassword" %>

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
        <asp:Label ID="lblHeader" runat="server" Text="Change Password" Style="color: #fff; font-size: 18px; font-weight: bold; font-family: Helvetica,Arial,Verdana,sans-serif;"></asp:Label>
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
                    <div class="col-sm-6 col-lg-6">
                        <div class="form-group">
                            <label for="txtNewPwd" class="col-md-4 control-label input-sm">New Password</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtNewPwd" CssClass="form-control input-sm" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-lg-6">
                        <div class="form-group">
                            <label for="txtConfirmPed" class="col-md-4 control-label input-sm">Confirm Password</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtConfirmPed" CssClass="form-control input-sm" runat="server" TextMode="Password"></asp:TextBox>
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
                                <asp:Button ID="btnSave" class="btn btn-md btn-pill btn-success" runat="server" Text="SAVE" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" class="btn btn-md btn-pill btn-danger" runat="server" Text="CANCEL" PostBackUrl="~/Views/Dashboard.aspx" />
                            </div>
                        </div>
                    </div>
                </div>
                <br /><br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>
</asp:Content>
