using System;
using System.Data;
using System.Web.UI;
using TTP_HRMS.Models;

namespace TTP_HRMS
{
    public partial class SiteMaster : MasterPage
    {
        CommonFunctions objcom = new CommonFunctions();
        public enum MessageType { Success, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["UserCode"] == null)
                        Response.Redirect("Login.aspx?sessionExpired=true", true);
                    else
                    {
                        lblname.Text = Session["UserName"].ToString();
                        MenuEnableDisable();
                    }
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
        private void MenuEnableDisable()
        {
            DataSet ds = new DataSet();
            LR.Visible = false;
            LA.Visible = false;
            TR.Visible = false;
            TA.Visible = false;
            if (Session["UserTemp"].ToString() != "")
            {
                ds = objcom.GetDocTemplate(Session["UserTemp"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int introw = 0; introw <= ds.Tables[0].Rows.Count - 1; introw++)
                    {
                        if (ds.Tables[0].Rows[introw][0].ToString() == "LR")
                        {
                            LR.Visible = true;
                        }
                        else if (ds.Tables[0].Rows[introw][0].ToString() == "LA")
                        {
                            LA.Visible = true;
                        }
                        else if (ds.Tables[0].Rows[introw][0].ToString() == "TR")
                        {
                            TR.Visible = true;
                        }
                        else if (ds.Tables[0].Rows[introw][0].ToString() == "TA")
                        {
                            TA.Visible = true;
                        }
                    }
                }
            }
        }
    }
}