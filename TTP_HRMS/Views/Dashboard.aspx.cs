using System;
using System.Data;
using System.Web.UI;
using TTP_HRMS.Models;

namespace TTP_HRMS
{
    public partial class Dashboard : System.Web.UI.Page
    {
        CommonFunctions objcom = new CommonFunctions();
        public enum MessageType { Success, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserCode"] == null)
                {
                    Response.Redirect("~/Views/Login.aspx?sessionExpired=true", true);
                }
                else
                {
                    MenuEnableDisable();
                }
            }
        }
        private void MenuEnableDisable()
        {
            DataSet ds = new DataSet();
            LR.Visible = false;
            LA.Visible = false;
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
                    }
                }
            }
        }
    }
}