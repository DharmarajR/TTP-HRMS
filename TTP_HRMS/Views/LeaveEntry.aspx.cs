using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using TTP_HRMS.Entities;
using TTP_HRMS.Models;

namespace TTP_HRMS.Views
{
    public partial class LeaveEntry : System.Web.UI.Page
    {
        Leave leave = new Leave();
        ApprovalRepository objApp = new ApprovalRepository();
        CommonFunctions objcom = new CommonFunctions();
        LeaveEntities objEN = new LeaveEntities();
        string StrMsg,strCode;
        string _Return, Noofdays;
        double Holidays, Total;
        public enum MessageType { Success, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserCode"] == null)
                    Response.Redirect("~/Views/Login.aspx?sessionExpired=true", true);
                else
                {
                    strCode = Request.QueryString["DocNum"];
                    leave.BindLeaveType(Session["UserCode"].ToString(), GrdLeave);
                    leave.BindReplaceEmployee(grdRpl);
                    btnWithdraw.Visible = false;
                    btnCancel.Visible = false;
                    emp.Visible = true;
                    Owner.Visible = false;
                    if (strCode != "" && strCode != null)
                    {
                        populateLeaveRequest(strCode.Replace("'", ""));
                    }
                }
            }
        }
        private void populateLeaveRequest(string code)
        {
            bool blnValue;
            DataSet ds = new DataSet();
            try
            {               
                ds = leave.PopulateLeaveRequest(code);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtcode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                    txtfrmdate.Text = ds.Tables[0].Rows[0]["U_StartDate"].ToString();
                    txteddate.Text = ds.Tables[0].Rows[0]["U_EndDate"].ToString();
                    txtrejoin.Text = ds.Tables[0].Rows[0]["U_ReJoiNDate"].ToString();
                    txtAppTempId.Text = ds.Tables[0].Rows[0]["U_ApproveId"].ToString();
                    txtnodays.Text = ds.Tables[0].Rows[0]["U_NoofDays"].ToString();
                    txtReason.Text = ds.Tables[0].Rows[0]["U_Notes"].ToString();
                    lbllvecode.Text = ds.Tables[0].Rows[0]["U_TrnsCode"].ToString();
                    txtlveType.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    txtlveBal.Text = ds.Tables[0].Rows[0]["U_LevBal"].ToString();
                    txtConAddress.Text = ds.Tables[0].Rows[0]["U_S_PEmAdd"].ToString();
                    txtRplEmpId.Text = ds.Tables[0].Rows[0]["U_RplEmpId"].ToString();
                    txtRplempname.Text = ds.Tables[0].Rows[0]["U_RplEmpName"].ToString();
                    //ddlonbehalf.SelectedValue = ds.Tables[0].Rows[0]["U_EMPID").ToString();
                    ddlOwnerStatus.SelectedValue = ds.Tables[0].Rows[0]["U_OwnerStatus"].ToString();
                    ddlDocStatus.SelectedValue = ds.Tables[0].Rows[0]["U_DocStatus"].ToString();
                    string strCancelstatus = ds.Tables[0].Rows[0]["U_Canelled"].ToString();
                    txtPhone.Text = ds.Tables[0].Rows[0]["U_S_PEConNo"].ToString();
                    GetLeavedetails();
                    string strstatus = ds.Tables[0].Rows[0]["U_Status"].ToString();
                    blnValue = leave.WithDrawStatus("LveReq", txtcode.Text.Trim());
                    if (blnValue == true | strstatus != "P")
                    {
                        btnWithdraw.Visible = false;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                    }
                    else
                    {
                        btnWithdraw.Visible = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                    }
                    if (ddlDocStatus.SelectedValue == "D")
                        ddlDocStatus.Enabled = true;
                    else
                        ddlDocStatus.Enabled = false;
                    if (ddlOwnerStatus.SelectedValue == "P")
                        ddlOwnerStatus.Enabled = true;
                    else
                        ddlOwnerStatus.Enabled = false;

                    if (strstatus == "A" & strCancelstatus == "No")
                    {                        
                        if (txtfrmdate.Text != "")
                        {
                            DateTime FromDate = Convert.ToDateTime(txtfrmdate.Text);                           
                            blnValue = leave.CheckPayrollPostedSE(Session["UserCode"].ToString(), FromDate.Month, FromDate.Year);
                            if (blnValue == true)
                            {
                                btnWithdraw.Visible = false;
                                btnSave.Visible = false;
                                btnCancel.Visible = false;
                                ShowMessage("Payroll already posted.", MessageType.Error);
                            }
                            else
                            {
                                btnWithdraw.Visible = false;
                                btnSave.Visible = false;
                                btnCancel.Visible = true;
                            }
                        }
                    }
                    else if (ddlDocStatus.SelectedValue == "D" & strCancelstatus == "No")
                    {
                        btnSave.Visible = true;
                        emp.Visible = true;
                        Owner.Visible = false;
                    }
                    else if (ddlDocStatus.SelectedValue == "C") // & ddlonbehalf.SelectedValue == Session("UserCode").ToString() & ddlOwnerStatus.SelectedValue == "P")
                    {
                        btnSave.Visible = true;
                        emp.Visible = false;
                        Owner.Visible = true;
                    }
                    else
                    {
                        ddlDocStatus.Enabled = false;
                        ddlOwnerStatus.Enabled = false;
                        btnSave.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                ShowMessage(ex.Message, MessageType.Error);
            }
        }
        private void GetLeavedetails()
        {
            DataSet ds = new DataSet();
            ds = leave.GetLeaveDetails(lbllvecode.Text.Trim());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["U_S_PNAOM"].ToString() == "" && ds.Tables[0].Rows[0]["U_S_PNAOM"].ToString() == null)
                {
                    txtoverlap.Text = "N";
                }
                else
                {
                    txtoverlap.Text = ds.Tables[0].Rows[0]["U_S_PNAOM"].ToString();
                }

                if (ds.Tables[0].Rows[0]["U_S_PEmcon"].ToString() == "" && ds.Tables[0].Rows[0]["U_S_PEmcon"].ToString() == null)
                {
                    txtconReq.Text = "N";
                }
                else
                {
                    txtconReq.Text = ds.Tables[0].Rows[0]["U_S_PEmcon"].ToString();
                }

                if (ds.Tables[0].Rows[0]["U_S_PAttReq"].ToString() == "" && ds.Tables[0].Rows[0]["U_S_PAttReq"].ToString() == null)
                {
                    lblAttachment.Text = "N";
                }
                else
                {
                    lblAttachment.Text = ds.Tables[0].Rows[0]["U_S_PAttReq"].ToString();
                }

                if (ds.Tables[0].Rows[0]["U_S_PPayPosted"].ToString() == "" && ds.Tables[0].Rows[0]["U_S_PPayPosted"].ToString() == null)
                {
                    txtblockTrans.Text = "N";
                }
                else
                {
                    txtblockTrans.Text = ds.Tables[0].Rows[0]["U_S_PPayPosted"].ToString();
                }

                if (ds.Tables[0].Rows[0]["U_S_PBalCheck"].ToString() == "" && ds.Tables[0].Rows[0]["U_S_PBalCheck"].ToString() == null)
                {
                    lblBlockTransaction.Text = "N";
                }
                else
                {
                    lblBlockTransaction.Text = ds.Tables[0].Rows[0]["U_S_PBalCheck"].ToString();
                }
            }
        }
        protected void Btncallpop_ServerClick(object sender, EventArgs e)
        {  
            try
            {
                string str1, str2, str3;
                str1 = txtpopunique.Text.Trim();
                str2 = txtpoptno.Text.Trim();
                str3 = txttname.Text.Trim();
                if (txthidoption.Text == "Leave")
                {
                    if (txtpoptno.Text.Trim() != "")
                    {
                        lbllvecode.Text = txtpopunique.Text.Trim();
                        txtlveType.Text = txtpoptno.Text.Trim();

                        if (txtfrmdate.Text == "")
                        {
                            objEN.Year = DateTime.Now.Date.Year;
                        }
                        else
                        {
                            objEN.FromDate = Convert.ToDateTime(txtfrmdate.Text.Trim());
                            objEN.Year = objEN.FromDate.Year;
                        }

                        _Return = objApp.DocApproval("LveReq", Session["UserCode"].ToString(), lbllvecode.Text.Trim());
                        if (_Return == "P")
                        {
                            _Return = leave.GetLeaveBalance(Session["UserCode"].ToString(), lbllvecode.Text.Trim(), objEN.Year);

                            if (_Return == "" & _Return == null)
                            {
                                txtlveBal.Text = "0";
                            }
                            else
                            {
                                txtlveBal.Text = _Return;
                            }
                            GetLeavedetails();
                            if (objEN.StartDate != "" && objEN.EndDate != "" && objEN.StartDate != null && objEN.EndDate != null)
                            {
                                objEN.FromDate = Convert.ToDateTime(txtfrmdate.Text.Trim());
                                objEN.ToDate = Convert.ToDateTime(txteddate.Text.Trim());
                                string CutOff = leave.GetCutoff(lbllvecode.Text.Trim());
                                Noofdays = leave.GetNodays(txtfrmdate.Text.Trim(), txteddate.Text.Trim());
                                Holidays = leave.GetHolidayCount(Session["UserCode"].ToString(), CutOff, objEN.FromDate, objEN.ToDate, objEN.SapCompany);
                                Total = Convert.ToDouble(Noofdays) - Holidays;
                                txtnodays.Text = Total.ToString();
                            }
                        }
                        else
                        {
                            lbllvecode.Text = "";
                            txtlveType.Text = "";
                            StrMsg = "Approval Template not defined for this leave type.Please contact administrator.";
                            ShowMessage(StrMsg, MessageType.Error);
                        }
                    }
                }
                else if (txthidoption.Text == "Emp1")
                {
                    txtRplEmpId.Text = txtpopunique.Text.Trim();
                    txtRplempname.Text = txtpoptno.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                ShowMessage(ex.Message, MessageType.Error);
            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        protected void grdRpl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            txtpoptno.Text = "";
            txtpopunique.Text = "";
            txthidoption.Text = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes.Add("onclick", "popupdisplay('Emp1','" + (DataBinder.Eval(e.Row.DataItem, "empID")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "EmpName")).ToString().Replace("'", "''") + "');");
        }

        protected void GrdLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            txtpoptno.Text = "";
            txtpopunique.Text = "";
            txthidoption.Text = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes.Add("onclick", "popupdisplay('Leave','" + (DataBinder.Eval(e.Row.DataItem, "U_S_PCode")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "U_S_PName")).ToString().Trim() + "');");
        }

        protected void txtfrmdate_TextChanged(object sender, EventArgs e)
        {
            int Noofdays;
            double Holidays;
            try
            {
                if (Session["UserCode"] == null | Session["SAPCompany"] == null)
                {
                    StrMsg = "alert('Your session is Expired...')";
                    ShowMessage(StrMsg, MessageType.Error);
                    Response.Redirect("~/Views/Login.aspx?sessionExpired=true", true);
                }
                else
                {
                    if (txtfrmdate.Text != "" && txteddate.Text != "" && txtfrmdate.Text != null && txteddate.Text != null)
                    {
                        objEN.Year = (Convert.ToDateTime(txtfrmdate.Text)).Year;
                        string CutOff = leave.GetCutoff(lbllvecode.Text.Trim());
                        Noofdays = Convert.ToInt32(leave.GetNodays(txtfrmdate.Text, txteddate.Text));
                        Holidays = leave.GetHolidayCount(Session["UserCode"].ToString(), CutOff, Convert.ToDateTime(txtfrmdate.Text), Convert.ToDateTime(txteddate.Text), (SAPbobsCOM.Company)Session["SAPCompany"]);
                        txtnodays.Text = (Noofdays - Holidays).ToString();
                        leave.BindLeaveType(Session["UserCode"].ToString(), GrdLeave);
                        _Return = leave.GetLeaveBalance(Session["UserCode"].ToString(), lbllvecode.Text.Trim(), objEN.Year);
                        if (_Return == "" & _Return == null)
                            txtlveBal.Text = "0";
                        else
                            txtlveBal.Text = _Return;
                        GetLeavedetails();
                        if (txtfrmdate.Text == txteddate.Text)
                            txtnodays.Enabled = true;
                        else
                            txtnodays.Enabled = false;
                    }
                    else if (txtfrmdate.Text != "" && txtfrmdate.Text != null)
                    {
                        objEN.Year = (Convert.ToDateTime(txtfrmdate.Text)).Year;
                        leave.BindLeaveType(Session["UserCode"].ToString(), GrdLeave);
                        _Return = leave.GetLeaveBalance(Session["UserCode"].ToString(), lbllvecode.Text.Trim(), objEN.Year);
                        if (_Return == "" & _Return == null)
                            txtlveBal.Text = "0";
                        else
                            txtlveBal.Text = _Return;
                        GetLeavedetails();
                    }
                    txteddate.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                ShowMessage(ex.Message, MessageType.Error);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {              
                objApp.CancelLeave(Session["UserCode"].ToString(), txtcode.Text, (SAPbobsCOM.Company)Session["SAPCompany"], Session["SapUserName"].ToString());
                ShowMessage("Leave Request Cancelled Successfully...", MessageType.Success);
                Response.Redirect("LeaveView.aspx", false);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                ShowMessage(ex.Message, MessageType.Error);
            }
        }

        protected void txteddate_TextChanged(object sender, EventArgs e)
        {
            int Noofdays;
            double Holidays;
            try
            {
                if (Session["UserCode"] == null | Session["SAPCompany"] == null)
                {
                    StrMsg = "alert('Your session is Expired...')";
                    ShowMessage(StrMsg, MessageType.Error);
                    Response.Redirect("~/Views/Login.aspx?sessionExpired=true", true);
                }
                else
                {
                    if (txtfrmdate.Text != "" && txteddate.Text != "" && txtfrmdate.Text != null && txteddate.Text != null)
                    {
                        objEN.Year = (Convert.ToDateTime(txtfrmdate.Text)).Year;
                        string CutOff = leave.GetCutoff(lbllvecode.Text.Trim());
                        Noofdays = Convert.ToInt32(leave.GetNodays(txtfrmdate.Text, txteddate.Text));
                        Holidays = leave.GetHolidayCount(Session["UserCode"].ToString(), CutOff, Convert.ToDateTime(txtfrmdate.Text), Convert.ToDateTime(txteddate.Text), (SAPbobsCOM.Company)Session["SAPCompany"]);                       
                        txtnodays.Text = (Noofdays-Holidays).ToString();
                        if (txtfrmdate.Text == txteddate.Text)
                            txtnodays.Enabled = true;
                        else
                            txtnodays.Enabled = false;
                        txtrejoin.Text = Convert.ToDateTime(txteddate.Text).AddDays(1).ToString("yyyy/MM/dd");
                    }
                    txtReason.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                ShowMessage(ex.Message, MessageType.Error);
            }

        }

        protected void ddlDocStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool Flag = false;
            //if (ddlonbehalf.SelectedValue == Session("UserCode").ToString())
            //    Flag = true;
            //else
            //    Flag = false;
            Flag = true;
            ddlOwnerStatus.SelectedValue = "P";
            if (ddlDocStatus.SelectedValue == "C" & Flag == true)
                ddlOwnerStatus.SelectedValue = "C";
            else if (ddlDocStatus.SelectedValue == "N")
                ddlOwnerStatus.SelectedValue = "L";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {            
            bool blValue;
            bool Flag = false;
            string fileName1, fileName, strpath, Targetpath, ErrMsg, intTempID;
            try
            {
                if (Session["UserCode"] == null | Session["SAPCompany"] == null)
                    Response.Redirect("~/Views/Login.aspx?sessionExpired=true", true);
                else
                {
                   
                    //if (ddlonbehalf.SelectedValue == Session("UserCode").ToString())
                    //    Flag = true;
                    //else
                    //    Flag = false;
                    Flag = true;
                    ddlOwnerStatus.SelectedValue = "P";
                    if (ddlDocStatus.SelectedValue == "C" & Flag == true)
                        ddlOwnerStatus.SelectedValue = "C";
                    else if (ddlDocStatus.SelectedValue == "N")
                        ddlOwnerStatus.SelectedValue = "L";

                    objEN.SapCompany = (SAPbobsCOM.Company)Session["SAPCompany"];
                    objEN.Empid = Session["UserCode"].ToString(); // ddlonbehalf.SelectedValue.Trim(); // Session("UserCode").ToString()
                    objEN.EmpName = Session["UserName"].ToString(); // ddlonbehalf.SelectedItem.Text.Trim(); // Session("UserName").ToString()
                    objEN.OnBehalfId = Session["UserCode"].ToString();
                    objEN.OnBehalfName = Session["UserName"].ToString();
                    objEN.LeaveCode = lbllvecode.Text.Trim();
                    objEN.StartDate = txtfrmdate.Text.Trim().Replace("-", "/");
                    objEN.EndDate = txteddate.Text.Trim().Replace("-", "/");
                    objEN.NoofDays = txtnodays.Text.Trim();
                    objEN.Notes = txtReason.Text.Trim();
                    objEN.RejoinDate = txtrejoin.Text.Trim().Replace("-", "/");
                    objEN.strCode = txtcode.Text.Trim();
                    objEN.LeaveName = txtlveType.Text.Trim();
                    objEN.DocStatus = ddlDocStatus.SelectedValue.Trim();
                    objEN.OwnStatus = ddlOwnerStatus.SelectedValue.Trim();
                    objEN.RplEmpId = txtRplEmpId.Text.Trim();
                    objEN.RplEmpName = txtRplempname.Text.Trim();
                    if (txtotalbal.Text.Trim() != "")
                        objEN.TotalLeave = Convert.ToDouble(txtotalbal.Text.Trim());
                    if (txtlveBal.Text.Trim() != "")
                        objEN.LeaveBalance = txtlveBal.Text.Trim();
                    else
                        objEN.LeaveBalance = "0";

                    if (objEN.StartDate != "")
                        objEN.FromDate = Convert.ToDateTime(objEN.StartDate);// Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' Date.Parse(objEN.StartDate)
                    if (objEN.EndDate != "")
                        objEN.ToDate = Convert.ToDateTime(objEN.EndDate);// Date.ParseExact(objEN.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    if (objEN.RejoinDate != "")
                        objEN.RejoinDt = Convert.ToDateTime(objEN.RejoinDate);// Date.ParseExact(objEN.RejoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' 
                    if (objEN.LeaveCode == "")
                    {
                        StrMsg = "Leave type is missing...";
                        ShowMessage(StrMsg, MessageType.Error);
                    }
                    else if (objEN.StartDate == "")
                    {
                        StrMsg = "From date is missing...";
                        ShowMessage(StrMsg, MessageType.Error);
                    }
                    else if (objEN.EndDate == "")
                    {
                        StrMsg = "End date is missing...";
                        ShowMessage(StrMsg, MessageType.Error);
                    }
                    else if (objEN.Notes == "")
                    {
                        StrMsg = "Leave reason missing...";
                        ShowMessage(StrMsg, MessageType.Error);
                    }
                    else if (Convert.ToDouble(objEN.NoofDays) < 0)
                    {
                        StrMsg = "End date must greater than or equal to start date...";
                        ShowMessage(StrMsg, MessageType.Error);
                    }
                    else if (lblBalanceLeave.Text == "Y" & (Convert.ToDouble(objEN.NoofDays) > Convert.ToDouble(objEN.LeaveBalance)))
                    {
                        StrMsg = "No of leave Days exceed the available Balance...";
                        ShowMessage(StrMsg, MessageType.Error);
                    }
                    else
                    {                        
                        if (txtconReq.Text == "Y")
                        {
                            if (txtConAddress.Text == "")
                            {
                                ErrMsg = "Emergency Contct Address missing.";
                                ShowMessage(StrMsg, MessageType.Error);
                                return;
                            }
                            else if (txtPhone.Text == "")
                            {
                                ErrMsg = "Phone Number missing...";
                                ShowMessage(StrMsg, MessageType.Error);
                                return;
                            }
                        }
                        if (lblAttachment.Text == "Y")
                        {
                            if (fileattach.HasFile)
                            {
                            }
                            else
                            {
                                ErrMsg = "Attachment missing...";
                                ShowMessage(StrMsg, MessageType.Error);
                                return;
                            }
                        }
                        objEN.ContactAdd = txtConAddress.Text.Trim();
                        objEN.PhoneNo = txtPhone.Text.Trim();

                        if (fileattach.HasFile)
                        {
                            fileName = fileattach.FileName;
                            strpath = Server.MapPath(@"~\Attachments\");
                            if (fileName != "")
                            {
                                Targetpath = objcom.TargetPath();
                                fileattach.SaveAs(strpath + fileattach.FileName);
                                fileName1 = "LE" + "_" + DateTime.Now.ToString("yyyyMMddHH:mm:ss").Replace(":", "") + "_" + fileName;
                                File.Copy(strpath + fileName, strpath + fileName1);
                                File.Delete(strpath + fileName);
                                if (Targetpath != "")
                                {
                                    try
                                    {
                                        File.Copy(strpath + fileName1, Targetpath + fileName1);
                                    }
                                    catch
                                    {
                                    }
                                }
                                objEN.Attachment = strpath + fileName1;
                            }
                            else
                                objEN.Attachment = "";
                        }
                        else
                            objEN.Attachment = "";
                        objEN.Year = objEN.FromDate.Year;
                        objEN.Month = objEN.FromDate.Month;
                        string strmsg = "";
                        string StrExistLeave = "";
                        string BlockTrans = "";
                        if (txtcode.Text == "")
                        {
                            strmsg = leave.ValidateLeaveEntries(objEN.Empid, objEN.LeaveCode, objEN.FromDate, objEN.ToDate, objEN.SapCompany);
                            StrExistLeave = leave.ValidateExistsLeaveEntries(objEN.Empid, objEN.LeaveCode, objEN.FromDate, objEN.ToDate, objEN.SapCompany);
                        }
                        else
                        {
                            strmsg = "Success";
                            StrExistLeave = "Success";
                        }
                        string strLeave = leave.ValidateLeave(objEN.LeaveCode, objEN.LeaveBalance.ToString(), objEN.NoofDays, objEN.SapCompany);
                        if (lblBlockTransaction.Text == "Y")
                            BlockTrans = leave.BlockTransaction(Session["CompanyNo"].ToString(), objEN.FromDate, objEN.SapCompany, objEN.Year, objEN.Month);
                        else
                            BlockTrans = "Success";
                        if (strmsg != "Success")
                        {
                            ErrMsg = "" + strmsg + "";
                            ShowMessage(ErrMsg, MessageType.Error);
                        }
                        else if (strLeave != "Success")
                        {
                            ErrMsg = "" + strLeave + "";
                            ShowMessage(ErrMsg, MessageType.Error);
                        }
                        else if (StrExistLeave != "Success")
                        {
                            ErrMsg = "" + StrExistLeave + "";
                            ShowMessage(ErrMsg, MessageType.Error);
                        }
                        else if (BlockTrans != "Success")
                        {
                            ErrMsg = "" + BlockTrans + "";
                            ShowMessage(ErrMsg, MessageType.Error);
                        }
                        else
                        {
                            objEN.Status = objApp.DocApproval("LveReq", objEN.Empid, objEN.LeaveCode);
                            blValue = false;
                            if (objEN.Status == "P")
                            {
                            }
                            else
                            {
                                lbllvecode.Text = "";
                                txtlveType.Text = "";
                                strmsg = "Approval Template not defined for this leave type.Please contact administrator.";
                                ShowMessage(StrMsg, MessageType.Error);
                                return;
                            }
                            if (txtcode.Text == "")
                            {
                                objEN.strCode = leave.SaveLeaveRequest(objEN);
                                if (objEN.strCode == "")
                                    blValue = false;
                                else
                                    blValue = true;
                            }
                            else
                            {
                                objEN.strCode = txtcode.Text.Trim();
                                blValue = leave.UpdateLeaveRequest(objEN);
                            }

                            if (blValue == true)
                            {
                                if (ddlOwnerStatus.SelectedValue == "C")
                                {
                                     objApp.UpdateLeaveBalance_Transaction(objEN.SapCompany, objEN.Empid, objEN.LeaveCode, objEN.Year, objEN.Month);
                                    if (objEN.Status == "A")
                                        objApp.AddUDTPayroll(objEN.strCode, objEN.SapCompany, objEN.Year, objEN.Month);

                                    intTempID = objApp.GetTemplateID("LveReq", objEN.Empid, objEN.LeaveCode);
                                    if (intTempID != "0")
                                    {
                                        objApp.UpdateApprovalRequired("@TTP_ESSLT", "Code", objEN.strCode, "Y", intTempID);
                                        objApp.InitialMessage("Approval required on Leave Request", objEN.strCode, objEN.Status, intTempID, objEN.EmpName, "LveReq", objEN.SapCompany);
                                    }
                                    else
                                        objApp.UpdateApprovalRequired("@TTP_ESSLT", "Code", objEN.strCode, "N", intTempID);

                                    //if (txtRplEmpId.Text != "")
                                    //{
                                    //    if (Session("UserCode").ToString() != txtRplEmpId.Text)
                                    //    {
                                    //        if (objcom.ISHANA == "HANA")
                                    //            SqlQuery = "Select \"email\" from \"" + DBName + "\".OHEM where \"empID\"='" + txtRplEmpId.Text + "'";
                                    //        else
                                    //            SqlQuery = "Select \"email\" from \"" + DBName + "\".dbo.OHEM where \"empID\"='" + txtRplEmpId.Text + "'";
                                    //        string aMail = objcom.ReturnValue(SqlQuery);
                                    //        if (aMail != "")
                                    //        {
                                    //            string StrMailMessage = "Leave has been requested for the request number :" + System.Convert.ToString(objEN.strCode);
                                    //            objcom.SendMailforApprovalExp(aMail, StrMailMessage, StrMailMessage, objEN.SapCompany, objEN.DBName, "Leave", objEN.strCode, "Rpl");
                                    //        }
                                    //    }
                                    //}
                                }
                                ErrMsg = "Leave Request saved Successfully...";
                                ShowMessage(StrMsg, MessageType.Success);
                            }
                            else
                            {
                                ErrMsg = "Leave Request failed...";
                                ShowMessage(ErrMsg, MessageType.Error);
                            }
                            Response.Redirect("LeaveView.aspx", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                ShowMessage(ex.Message,MessageType.Error);
            }

        }

    }
}