using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TTP_HRMS.Entities;
using TTP_HRMS.Models;

namespace TTP_HRMS.Views
{
    public partial class LeaveApproval : System.Web.UI.Page
    {
        Leave leave = new Leave();
        ApprovalEntities objEN = new ApprovalEntities();
        ApprovalRepository objApp = new ApprovalRepository();
        CommonFunctions objCom = new CommonFunctions();
        public enum MessageType { Success, Error, Info, Warning };
        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserCode"] == null)
                    Response.Redirect("~/Views/Login.aspx?sessionExpired=true", true);
                else
                {
                    leave.Requests(Session["SapUserName"].ToString(), grdRequestApproval);
                    leave.Summary(Session["SapUserName"].ToString(), grdSummary);
                    for (int j = 2010; j <= 2050; j++)
                        ddlYear.Items.Add(new ListItem(j.ToString(), j.ToString()));
                    for (int i = 1; i <= 12; i++)
                        ddlMonth.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
                }
            }
            RegisterPostBackControl();
        }
        private void RegisterPostBackControl()
        {
            foreach (GridViewRow row in grdRequestApproval.Rows)
            {
                LinkButton lblCode = (LinkButton)(row.FindControl("lblCode"));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lblCode);
                ImageButton ImgAppHistory = (ImageButton)(row.FindControl("ImgAppHistory"));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(ImgAppHistory);
            }
            foreach (GridViewRow row in grdSummary.Rows)
            {               
                ImageButton ImgAppSumHisotry = (ImageButton)(row.FindControl("ImgAppSumHisotry"));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(ImgAppSumHisotry);
            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        protected void lbtnHistory_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void lbtnactivity_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void grdRequestApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRequestApproval.PageIndex = e.NewPageIndex;
            leave.Requests(Session["SapUserName"].ToString(), grdRequestApproval);
        }

        protected void grdSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSummary.PageIndex = e.NewPageIndex;
            leave.Summary(Session["SapUserName"].ToString(), grdSummary);
        }

        protected void lblCode_Click(object sender, EventArgs e)
        {
            LinkButton button = (sender as LinkButton);
            GridViewRow gv = ((GridViewRow)(button.Parent.Parent));
            string commandArgument = button.CommandArgument;
            populateLeaveRequest(commandArgument);
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "ShowPopupCost();", true);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "somekey", "ShowPopupCost()", true);
        }
        private void populateLeaveRequest(string code)
        {            
            DataSet ds = new DataSet();
            try
            {
                ds = leave.PopulateLeaveRequest(code);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtcode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                    txtfrmdate.Text = ds.Tables[0].Rows[0]["U_StartDate"].ToString();
                    txteddate.Text = ds.Tables[0].Rows[0]["U_EndDate"].ToString();
                    lblempId.Text = ds.Tables[0].Rows[0]["U_EMPID"].ToString();
                    lblempName.Text = ds.Tables[0].Rows[0]["U_EMPNAME"].ToString();
                    lblNoofdays.Text = ds.Tables[0].Rows[0]["U_NoofDays"].ToString();
                    lblRemarks.Text = ds.Tables[0].Rows[0]["U_Notes"].ToString();
                    lblLveCode.Text = ds.Tables[0].Rows[0]["U_TrnsCode"].ToString();
                    lblLveName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["U_Month"].ToString();
                    ddlYear.SelectedValue = ds.Tables[0].Rows[0]["U_Year"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["U_S_PEmAdd"].ToString();
                   // txtRplEmpId.Text = ds.Tables[0].Rows[0]["U_RplEmpId"].ToString();
                    lbldelegate.Text = ds.Tables[0].Rows[0]["U_RplEmpName"].ToString();
                    txtAppRemarks.Text = ds.Tables[0].Rows[0]["U_AppRemarks"].ToString();                  

                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                ShowMessage(ex.Message, MessageType.Error);
            }
        }

        protected void btnApprove_ServerClick(object sender, EventArgs e)
        {
            AddUDO("A");
        }

        protected void btnRejected_ServerClick(object sender, EventArgs e)
        {
            AddUDO("R");
        }
        private void AddUDO(string ApprovalStatus)
        {
            string EmpName, StrMsg;
            try
            {
                if (Session["UserCode"] == null || Session["SAPCompany"] == null)
                {
                    ShowMessage("Your session is Expired...", MessageType.Error);
                    Response.Redirect("~/Views/Login.aspx?sessionExpired=true", true);
                }
                else
                {
                    objEN.EmpId = Session["UserCode"].ToString();
                    objEN.UserCode = objApp.GetUserCode(Session["UserCode"].ToString());
                    objEN.EmpUserId = Convert.ToInt32(objApp.GetEmpUserid(Session["UserCode"].ToString()));
                    if (ApprovalStatus == "R")
                    {
                        if (txtAppRemarks.Text == "")
                        {
                            ShowMessage("Remarks is missing", MessageType.Error);
                            ClientScript.RegisterStartupScript(typeof(Page), "alert", "ShowPopupCost();", true);
                            return;
                        }
                    }
                    objEN.EmpId = lblempId.Text;
                    objEN.LeaveCode = lblLveCode.Text;
                    objEN.DocEntry = txtcode.Text;
                    objEN.AppStatus = ApprovalStatus;
                    DateTime dtDate = Convert.ToDateTime(txtfrmdate.Text);
                    objEN.Year = Convert.ToInt32(ddlYear.SelectedValue);
                    objEN.Month = Convert.ToInt32(ddlMonth.SelectedValue);
                    objEN.Remarks = txtAppRemarks.Text;
                    objEN.HistoryType = "LveReq";
                    objEN.HeaderType = "LveReq";
                    EmpName = objCom.GetEmpName(objEN.EmpId);
                    objEN.DocMessage = "Approval required on Leave Request from " + EmpName;
                    objEN.SapCompany = (SAPbobsCOM.Company)Session["SAPCompany"];
                    StrMsg = objApp.AddUpdateDocument(objEN);
                    if (StrMsg == "Success" || StrMsg == "Successfully approved document...")
                    {
                        StrMsg = "Document Processed Successfully....";
                        ShowMessage(StrMsg, MessageType.Success);
                    }
                    else
                    {
                        ShowMessage(StrMsg, MessageType.Error);                        
                    }
                    leave.Requests(Session["SapUserName"].ToString(), grdRequestApproval);
                    leave.Summary(Session["SapUserName"].ToString(), grdSummary);
                }
            }
            catch(Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                ShowMessage(ex.Message, MessageType.Error);
            }
        }

        protected void ImgAppHistory_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton button = (sender as ImageButton);
            GridViewRow gv = ((GridViewRow)(button.Parent.Parent));
            string commandArgument = button.CommandArgument;
            objApp.ApprovalHistory(commandArgument, "LveReq", grdApprovalHis);
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "ShowPopupHistory();", true);
        }

        protected void ImgAppSumHisotry_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton button = (sender as ImageButton);
            GridViewRow gv = ((GridViewRow)(button.Parent.Parent));
            string commandArgument = button.CommandArgument;
            objApp.ApprovalHistory(commandArgument, "LveReq", grdApprovalHis);
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "ShowPopupHistory();", true);
        }
    }
}