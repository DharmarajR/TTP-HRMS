using System;
using System.Configuration;
using System.Web.UI;
using TTP_HRMS.Models;

namespace TTP_HRMS.Views
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        public enum MessageType { Success, Error, Info, Warning };
        CommonFunctions objcom = new CommonFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserCode"] == null)
                {
                    Response.Redirect("~/Views/Login.aspx?sessionExpired=true", true);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserCode"] == null)
                    Response.Redirect("~/Views/Login.aspx?sessionExpired=true", true);
                else
                {
                    SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
                    oCompany =(SAPbobsCOM.Company) Session["SAPCompany"];                  

                     if (txtNewPwd.Text == "")
                    {
                        ShowMessage("Enter the New Password..", MessageType.Error);
                    }
                    else if (txtConfirmPed.Text == "")
                    {
                        ShowMessage("Enter the Confirm Password..", MessageType.Error);
                    }
                    else if (txtNewPwd.Text != txtConfirmPed.Text)
                    {
                        ShowMessage("Confirm password does not match..", MessageType.Error);
                    }
                    else if (ConfigurationManager.AppSettings["Login"].ToUpper() == "SAP")
                    {
                        if (oCompany.ChangePassword(txtConfirmPed.Text) != 0)
                        {
                            string StrMsg = oCompany.GetLastErrorDescription();
                            StrMsg = oCompany.GetLastErrorDescription();
                            ShowMessage(StrMsg, MessageType.Error);
                        }
                        else
                        {
                            ShowMessage("Password changed Successfully ..", MessageType.Success);
                        }
                    }
                    else
                    {
                        objcom.UpdatePassword(txtConfirmPed.Text, Session["UserCode"].ToString());                      
                        ShowMessage("Password changed Successfully ..", MessageType.Success);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                throw ex;
            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
    }
}