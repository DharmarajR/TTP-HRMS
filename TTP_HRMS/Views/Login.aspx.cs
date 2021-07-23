using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using TTP_HRMS.Models;

namespace TTP_HRMS
{
    public partial class Login : System.Web.UI.Page
    {
        CommonFunctions objCom = new CommonFunctions();
        string strError, SqlQuery;
        DataSet SqlDS = new DataSet();
        DataSet SqlDS2 = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["SessionExpired"] != null || Request.QueryString["SessionExpired"] == "ture")
                {
                    string strmsg = "Session expired. You will be redirected to Login page";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('" + strmsg + "')</script>");
                }
                //lblCustomer.Text = ConfigurationManager.AppSettings["CustomerName"].ToString();
                Session.Clear();
                Session.Abandon();
            }
        }       

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            try
            {
                bool blnError = false;
                Session.Clear();              
               
                Session["SapUserName"] = userName.Value.Trim();               
                if (userName.Value == "")
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Enter the UserName')</script>");
                else if (pwd.Value == "")
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Enter the Password')</script>");
                else
                {
                    // strError = DBCon.Connection(objEN.Uid, objEN.Pwd)
                    Session["LoginFlag"] = ConfigurationManager.AppSettings["Login"].ToUpper();
                    if (ConfigurationManager.AppSettings["Login"].ToUpper() == "SAP")
                        strError = objCom.Connection(Session["LoginFlag"].ToString(), userName.Value.Trim(), pwd.Value.Trim());
                    else
                    {
                        if (objCom.ISHANA == "HANA")
                        {
                            SqlQuery = "Select \"empID\"";
                            SqlQuery += "  from \"" + objCom.DBName + "\".OHEM T0 INNER JOIN \"" + objCom.DBName + "\".OUSR T1 ON T1.\"USERID\" = T0.\"userId\" where T0.\"Active\"='Y'";
                            SqlQuery += "  and T0.\"U_PWD\"='" + pwd.Value.Trim() + "' AND T1.\"USER_CODE\"='" + Session["Sapusername"].ToString() + "'";
                        }
                        else
                        {
                            SqlQuery = "Select \"empID\"";
                            SqlQuery += " from \"" + objCom.DBName + "\".dbo.OHEM T0 INNER JOIN \"" + objCom.DBName + "\".dbo.OUSR T1 ON T1.\"USERID\" = T0.\"userId\" where T0.\"Active\"='Y'";
                            SqlQuery += "  and T0.\"U_PWD\"='" + pwd.Value.Trim() + "' AND T1.\"USER_CODE\"='" + Session["Sapusername"].ToString() + "'";
                        }
                        SqlDS = objCom.ReturnDataset(SqlQuery);
                        if (SqlDS.Tables[0].Rows.Count > 0)
                            strError = objCom.Connection(Session["LoginFlag"].ToString(), Session["Sapusername"].ToString().Trim(), Session["SapPwd"].ToString().Trim());
                        else
                        {
                            strError = "Invalid login.Check Userid and Password.";
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Invalid login.Check Userid and Password.')</script>");
                        }
                    }

                    if (strError == "Success")
                    {
                        Session["SAPCompany"] = objCom.objMainCompany;
                        // Session("SAPCompany") = Nothing
                        //if (objCom.ISHANA == "HANA")
                        //    objCom.HANAchecktable(DBCon.DBName);
                        //else
                        //    objCom.checktable(DBCon.DBName);
                        Session["UserSign"] = objCom.GetUserSign(Session["SapUserName"].ToString());
                        Session["UserTemp"] = objCom.GetUserTemp(Session["SapUserName"].ToString());
                        Session["PayRoll"] = ConfigurationManager.AppSettings["LeaveRequest"];
                       
                        // License Validation
                        if (blnError == false)
                        {
                            if (objCom.ISHANA == "HANA")
                            {
                                SqlQuery = "Select \"empID\",IFNULL(\"firstName\",'') ||' '|| IFNULL(\"middleName\",'') ||' '|| IFNULL(\"lastName\",'') AS \"EmpName\",IFNULL(\"position\",'') AS \"position\",";
                                SqlQuery += "  \"dept\",\"U_S_PCompNo\"   from \"" + objCom.DBName + "\".OHEM where \"Active\"='Y' and \"userId\"=" + Session["UserSign"].ToString();
                            }
                            else
                            {
                                SqlQuery = "Select \"empID\",ISNULL(\"firstName\",'') +' '+ ISNULL(\"middleName\",'') +' '+ ISNULL(\"lastName\",'') AS \"EmpName\",ISNULL(\"position\",'') AS \"position\",";
                                SqlQuery += "  \"dept\",\"U_S_PCompNo\" from \"" + objCom.DBName + "\".dbo.OHEM where \"Active\"='Y' and \"userId\"=" + Session["UserSign"].ToString();
                            }
                            SqlDS2 = objCom.ReturnDataset(SqlQuery);
                            if (SqlDS2.Tables[0].Rows.Count > 0)
                            {                               
                                Session["UserCode"] = SqlDS2.Tables[0].Rows[0]["empID"].ToString();
                                Session["UserName"] = SqlDS2.Tables[0].Rows[0]["EmpName"].ToString();
                                Session["PosId"] = SqlDS2.Tables[0].Rows[0]["position"].ToString();
                                Session["Dept"] = SqlDS2.Tables[0].Rows[0]["dept"].ToString();
                                Session["CompanyNo"] = SqlDS2.Tables[0].Rows[0]["U_S_PCompNo"].ToString();
                                Session["PosName"] = null;
                                if (SqlDS2.Tables[0].Rows[0]["position"].ToString() != "" && SqlDS2.Tables[0].Rows[0]["position"].ToString() != "0")
                                    Session["PosName"] = objCom.GetPosition(SqlDS2.Tables[0].Rows[0]["position"].ToString());
                               // Session["SessionId"] = objCom.SessionDetails(Session["UserCode"].ToString());
                                Guid ID = Guid.NewGuid();
                                string SID = ID.ToString();
                                Session["Session"] = Session["SessionId"];
                                Response.Redirect("~/Views/Dashboard.aspx", false);
                            }
                            else
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('Please check Employee is active and User is mapped in Employee master')</script>");
                        }
                        else
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('" + strError + "')</script>");
                    }
                    else
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('" + strError + "')</script>");
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('" + ex.Message + "')</script>");
            }

        }
    }
}