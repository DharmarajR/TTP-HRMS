using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TTP_HRMS.Models;

namespace TTP_HRMS.Views
{
    public partial class LeaveView : System.Web.UI.Page
    {
        Leave leave = new Leave();
        public enum MessageType { Success, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserCode"] == null)
                    Response.Redirect("~/Views/Login.aspx?sessionExpired=true", true);
                else
                {
                    leave.LeaveBindRequest(Session["UserCode"].ToString(), grdLeaveRequest);
                    leave.LeaveBindSummary(Session["UserCode"].ToString(), grdSummary);
                }
            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {

        }

        protected void lbtAppHistory_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void lnkEDownload_Click(object sender, EventArgs e)
        {

        }

        protected void lbtndocnum_Click(object sender, EventArgs e)
        {
            LinkButton button = (sender as LinkButton);
            GridViewRow gv = ((GridViewRow)(button.Parent.Parent));
            string commandArgument = button.CommandArgument;
            Response.Redirect("~/Views/LeaveEntry.aspx?DocNum='" + commandArgument + "'", false);
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/LeaveEntry.aspx", false);
        }

        protected void grdLeaveRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLeaveRequest.PageIndex = e.NewPageIndex;
            leave.LeaveBindRequest(Session["UserCode"].ToString(), grdLeaveRequest);
        }

        protected void grdSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSummary.PageIndex = e.NewPageIndex;
            leave.LeaveBindSummary(Session["UserCode"].ToString(), grdSummary);
        }
    }
}