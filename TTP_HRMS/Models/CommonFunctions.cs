using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.UI.WebControls;

namespace TTP_HRMS.Models
{
    public class CommonFunctions
    {
        DBVariables objVar = new DBVariables();
        public SAPbobsCOM.Company oCompany, objMainCompany;
        string strError, _Return;
        public string SqlQuery;
        public string ISHANA = ConfigurationManager.AppSettings["ServerType"];
        public string DBName = ConfigurationManager.AppSettings["CompanyDB"];
        public CommonFunctions()
        {
            objVar.Sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ToString());
            objVar.Odbccon = new OdbcConnection(ConfigurationManager.ConnectionStrings["ODBCConnection"].ToString());
        }
        public string ValidateLicense(string aValue)
        {
            string strProductID = "";
            string sQuery;
            try
            {
                switch (aValue)
                {
                    case "eGateESS":
                        {
                            strProductID = "E01";
                            break;
                        }

                    case "eGateWMS":
                        {
                            strProductID = "E02";
                            break;
                        }

                    case "eGateeCommerce":
                        {
                            strProductID = "E03";
                            break;
                        }
                    case "eGateSales":
                        {
                            strProductID = "E04";
                            break;
                        }
                }
                if (ISHANA == "HANA")
                    sQuery = "Select IFNULL(\"U_IsValid\",'N') AS \"U_IsValid\" From \"" + DBName + "\".\"@TTP_EPOLIM\" Where \"U_ProductID\"= '" + strProductID + "' Order By Cast(\"Code\" as Numeric) Desc";
                else
                    sQuery = "Select ISNULL(\"U_IsValid\",'N') AS \"U_IsValid\" From \"" + DBName + "\".dbo.\"@TTP_EPOLIM\" Where \"U_ProductID\"= '" + strProductID + "' Order By Cast(\"Code\" as Numeric) Desc";

                string strValid = ReturnValue(sQuery);

                if (strValid.Trim() == "Y")
                {
                    return "Success";
                }
                else
                    return "Invalid License File ...";

            }
            catch 
            {
            }
            return "Success";
        }
        public string Connection(string achoice,string SapUserName, string SapPwd)
        {
            string SID = "";
            try
            {
                oCompany = new SAPbobsCOM.Company();
                objMainCompany = new SAPbobsCOM.Company();
                oCompany.Server = ConfigurationManager.AppSettings["Server"];
                switch (ConfigurationManager.AppSettings["ServerType"])
                {
                    case "2005":
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005;
                        break;
                    case "2008":
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                        break;
                    case "2012":
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                        break;
                    case "2014":
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                        break;
                    case "2016":
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;
                        break;
                    case "2017":
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017;
                        break;
                    //case "2019":
                    //  oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019;
                    //  break;
                    case "HANA":
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB;
                        break;
                }
                switch (ConfigurationManager.AppSettings["IsCloud"])
                {
                    case "No":
                        oCompany.DbUserName = ConfigurationManager.AppSettings["SqlUserName"];
                        oCompany.DbPassword = ConfigurationManager.AppSettings["SqlPassword"];
                        break;
                }
                if (achoice == "SAP")
                {
                    oCompany.UserName = SapUserName;
                    oCompany.Password = SapPwd;
                }   
                else
                {
                    oCompany.UserName = ConfigurationManager.AppSettings["SAPUserName"];
                    oCompany.Password = ConfigurationManager.AppSettings["SAPPassword"];
                }
                oCompany.CompanyDB = ConfigurationManager.AppSettings["CompanyDB"];
                oCompany.LicenseServer = ConfigurationManager.AppSettings["License"];
                oCompany.UseTrusted = false;
                if (oCompany.Connect() != 0)
                {
                    strError = oCompany.GetLastErrorDescription();
                    WriteError(strError);
                    SID = strError;
                    return strError;
                }
                else
                {
                    objMainCompany = oCompany;
                    Guid id = Guid.NewGuid();
                    SID = id.ToString();
                    DateTime dtExpiration = DateTime.UtcNow.AddDays(2);
                    HttpContext.Current.Cache.Insert(SID, oCompany, null, dtExpiration, Cache.NoSlidingExpiration);
                    return "Success";
                }

            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                return ex.Message;
            }            
        }
        public string GetUserTemp(string EmpId)
        {
            string Status = "";
            try
            {
                if (ISHANA == "HANA")
                    SqlQuery = "SELECT \"U_DashTempID\" FROM \"" + DBName + "\".OUSR WHERE \"USER_CODE\"='" + EmpId + "'";
                else
                    SqlQuery = "SELECT U_DashTempID FROM \"" + DBName + "\".dbo.OUSR WHERE \"USER_CODE\"='" + EmpId + "'";
                Status = ReturnValue(SqlQuery);
                return Status;
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                throw ex;
            }
        }
        public string GetPosition(string PosCode)
        {
            string PosName;
            try
            {
                if (ISHANA == "HANA")
                    SqlQuery = "select \"descriptio\" from \"" + DBName + "\".OHPS WHERE \"posID\"='" + PosCode + "'";
                else
                    SqlQuery = "select \"descriptio\" from \"" + DBName + "\".dbo.OHPS WHERE \"posID\"='" + PosCode + "'";
                PosName = ReturnValue(SqlQuery);
                return PosName;
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                throw ex;
            }
        }
        public string SessionDetails(string CustCode)
        {
            try
            {
                string exists = "0";
                string sCode = Getmaxcode("TTP_SESSION", "U_SESSIONID");
                if (ISHANA == "HANA")
                    SqlQuery = "INSERT INTO \"" + DBName + "\".\"TTP_SESSION\"(\"U_SESSIONID\",\"empID\",\"U_LOGIN_DATE\") VALUES('" + sCode + "','" + CustCode + "',NOW())";
                else
                    SqlQuery = "INSERT INTO \"" + DBName + "\".dbo.\"TTP_SESSION\"(\"U_SESSIONID\",\"empID\",\"U_LOGIN_DATE\") VALUES('" + sCode + "','" + CustCode + "',getdate())";
                ExecuteNonQuery(SqlQuery);
                if (ISHANA == "HANA")
                    SqlQuery = "SELECT MAX(\"U_SESSIONID\") FROM \"" + DBName + "\".\"TTP_SESSION\"";
                else
                    SqlQuery = "SELECT MAX(\"U_SESSIONID\") FROM \"" + DBName + "\".dbo.\"TTP_SESSION\"";
                exists = ReturnValue(SqlQuery);
                return exists;
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                throw ex;
            }
        }
        public string GetUserSign(string EmpId)
        {
            string Status;
            try
            {
                if (ISHANA == "HANA")
                    SqlQuery = "SELECT \"USERID\" FROM \"" + DBName + "\".OUSR WHERE \"USER_CODE\"='" + EmpId + "'";
                else
                    SqlQuery = "SELECT USERID FROM \"" + DBName + "\".dbo.OUSR WHERE \"USER_CODE\"='" + EmpId + "'";
                Status = ReturnValue(SqlQuery);
                return Status;
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                throw ex;
            }
        }
        public string Getmaxcode(string sTable, string sColumn)
        {
            string sCode;
            int MaxCode;
            try
            {
                if (ISHANA == "HANA")
                {
                    if (objVar.Odbccon.State == ConnectionState.Open)
                        objVar.Odbccon.Close();
                }
                else if (objVar.Sqlcon.State == ConnectionState.Open)
                    objVar.Sqlcon.Close();
                if (ISHANA == "HANA")
                {
                    objVar.Odbccon.Open();
                    objVar.Odbccmd = new OdbcCommand("SELECT IFNULL(max(IFNULL(CAST(IFNULL(\"" + sColumn + "\",'0') AS Numeric),0)),0) FROM \"" + DBName + "\".\"" + sTable + "\"", objVar.Odbccon);
                    objVar.Odbccmd.CommandType = CommandType.Text;
                    MaxCode = Convert.ToInt32(objVar.Odbccmd.ExecuteScalar());
                    if (MaxCode >= 0)
                        MaxCode = MaxCode + 1;
                    else
                        MaxCode = 1;
                    sCode = String.Format(MaxCode.ToString(), "00000000");
                    objVar.Odbccon.Close();
                }
                else
                {
                    objVar.Sqlcon.Open();
                    objVar.Sqlcmd = new SqlCommand("SELECT ISNULL(max(ISNULL(CAST(ISNULL(\"" + sColumn + "\",'0') AS Numeric),0)),0) FROM \"" + DBName + "\".dbo.\"" + sTable + "\"", objVar.Sqlcon);
                    objVar.Sqlcmd.CommandType = CommandType.Text;
                    MaxCode = Convert.ToInt32(objVar.Sqlcmd.ExecuteScalar());
                    if (MaxCode >= 0)
                        MaxCode = MaxCode + 1;
                    else
                        MaxCode = 1;
                    sCode = String.Format(MaxCode.ToString(), "00000000");
                    objVar.Sqlcon.Close();
                }
                return sCode;
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                throw ex;
            }
            finally
            {
                if (ISHANA == "HANA")
                {
                    if (objVar.Odbccon.State == ConnectionState.Open)
                        objVar.Odbccon.Close();
                }
                else if (objVar.Sqlcon.State == ConnectionState.Open)
                    objVar.Sqlcon.Close();
            }
        }
        public void BindDropdown(string Query, string ValCode, string ValName, DropDownList ddl)
        {
            DataSet SqlDS = new DataSet();
            try
            {
                if (ISHANA == "HANA")
                {
                    objVar.Odbcda = new OdbcDataAdapter(Query, objVar.Odbccon);
                    objVar.Odbcda.Fill(SqlDS);
                }
                else
                {
                    objVar.Sqlda = new SqlDataAdapter(Query, objVar.Sqlcon);
                    objVar.Sqlda.Fill(SqlDS);
                }
                if (SqlDS.Tables[0].Rows.Count > 0)
                {
                    ddl.DataTextField = ValName;
                    ddl.DataValueField = ValCode;
                    ddl.DataSource = SqlDS;
                    ddl.DataBind();
                    ddl.Items.Insert(0, "");
                }
                else
                {
                    ddl.ClearSelection();
                    ddl.Items.Clear();
                    ddl.DataBind();
                    ddl.Items.Insert(0, "");
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                throw ex;
            }
        }

        public void BindDropdown1(string Query, string ValCode, string ValName, DropDownList ddl)
        {
            DataSet SqlDS = new DataSet();
            try
            {
                if (ISHANA == "HANA")
                {
                    objVar.Odbcda = new OdbcDataAdapter(Query, objVar.Odbccon);
                    objVar.Odbcda.Fill(SqlDS);
                }
                else
                {
                    objVar.Sqlda = new SqlDataAdapter(Query, objVar.Sqlcon);
                    objVar.Sqlda.Fill(SqlDS);
                }
                if (SqlDS.Tables[0].Rows.Count > 0)
                {
                    ddl.DataTextField = ValName;
                    ddl.DataValueField = ValCode;
                    ddl.DataSource = SqlDS;
                    ddl.DataBind();
                }
                else
                {
                    ddl.ClearSelection();
                    ddl.Items.Clear();
                    ddl.DataBind();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                throw ex;
            }
        }
        public void BindGrid(string Query, GridView Grdview)
        {
            DataSet SqlDS = new DataSet();
            try
            {
                if (ISHANA == "HANA")
                {
                    objVar.Odbcda = new OdbcDataAdapter(Query, objVar.Odbccon);
                    objVar.Odbcda.Fill(SqlDS);
                }
                else
                {
                    objVar.Sqlda = new SqlDataAdapter(Query, objVar.Sqlcon);
                    objVar.Sqlda.Fill(SqlDS);
                }
                if (SqlDS.Tables[0].Rows.Count > 0)
                {
                    Grdview.DataSource = SqlDS;
                    Grdview.DataBind();
                }
                else
                {
                    Grdview.DataBind();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                throw ex;
            }
        }
        public DataSet ReturnDataset(string SqlQuery)
        {
            DataSet SqlDS = new DataSet();
            try
            {
                if (ISHANA == "HANA")
                {
                    if (objVar.Odbccon.State == ConnectionState.Open)
                    {
                        objVar.Odbccon.Close();
                    }
                    // objVar.Odbcda = new OdbcDataAdapter(SqlQuery, objVar.Odbccon);
                    // objVar.Odbcda.Fill(SqlDS);
                    objVar.Odbccmd = new OdbcCommand();
                    objVar.Odbccmd.CommandText = SqlQuery;
                    objVar.Odbccmd.Connection = objVar.Odbccon;
                    objVar.Odbccmd.CommandTimeout = 900;
                    objVar.Odbccon.Open();
                    objVar.Odbcda = new OdbcDataAdapter(SqlQuery, objVar.Odbccon);
                    objVar.Odbcda.Fill(SqlDS);
                    objVar.Odbccon.Close();
                }
                else
                {
                    if (objVar.Sqlcon.State == ConnectionState.Open)
                    {
                        objVar.Sqlcon.Close();
                    }
                    objVar.Sqlcmd = new SqlCommand();
                    objVar.Sqlcmd.CommandText = SqlQuery;
                    objVar.Sqlcmd.Connection = objVar.Sqlcon;
                    objVar.Sqlcmd.CommandTimeout = 900;
                    objVar.Sqlcon.Open();
                    objVar.Sqlda = new SqlDataAdapter(SqlQuery, objVar.Sqlcon);
                    objVar.Sqlda.Fill(SqlDS);
                    objVar.Sqlcon.Close();
                }
                return SqlDS;
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                throw ex;
            }
            finally
            {
                if (ISHANA == "HANA")
                {
                    if (objVar.Odbccon.State == ConnectionState.Open)
                    {
                        objVar.Odbccon.Close();
                    }
                }
                else
                {
                    if (objVar.Sqlcon.State == ConnectionState.Open)
                    {
                        objVar.Sqlcon.Close();
                    }
                }
            }
        }

        public int ExecuteNonQuery(string SqlQuery)
        {
            int _Return = 0;
            try
            {
                if (ISHANA == "HANA")
                {
                    if (objVar.Odbccon.State == ConnectionState.Open)
                    {
                        objVar.Odbccon.Close();
                    }
                    objVar.Odbccmd = new OdbcCommand(SqlQuery, objVar.Odbccon);
                    objVar.Odbccon.Open();
                    _Return = objVar.Odbccmd.ExecuteNonQuery();
                    objVar.Odbccon.Close();
                }
                else
                {
                    if (objVar.Sqlcon.State == ConnectionState.Open)
                    {
                        objVar.Sqlcon.Close();
                    }
                    objVar.Sqlcmd = new SqlCommand(SqlQuery, objVar.Sqlcon);
                    objVar.Sqlcon.Open();
                    _Return = objVar.Sqlcmd.ExecuteNonQuery();
                    objVar.Sqlcon.Close();
                }
                return _Return;
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                return _Return;
            }
            finally
            {
                if (ISHANA == "HANA")
                {
                    if (objVar.Odbccon.State == ConnectionState.Open)
                    {
                        objVar.Odbccon.Close();
                    }
                }
                else
                {
                    if (objVar.Sqlcon.State == ConnectionState.Open)
                    {
                        objVar.Sqlcon.Close();
                    }
                }
            }
        }

        public string ReturnValue(string Query)
        {
            _Return = "";
            try
            {
                if (ISHANA == "HANA")
                {
                    if (objVar.Odbccon.State == ConnectionState.Open)
                    {
                        objVar.Odbccon.Close();
                    }
                    objVar.Odbccon.Open();
                    objVar.Odbccmd = new OdbcCommand(Query, objVar.Odbccon);
                    objVar.Odbccmd.CommandType = CommandType.Text;
                    _Return = objVar.Odbccmd.ExecuteScalar().ToString();
                    objVar.Odbccon.Close();
                }
                else
                {
                    if (objVar.Sqlcon.State == ConnectionState.Open)
                    {
                        objVar.Sqlcon.Close();
                    }
                    objVar.Sqlcon.Open();
                    objVar.Sqlcmd = new SqlCommand(Query, objVar.Sqlcon);
                    objVar.Sqlcmd.CommandType = CommandType.Text;
                    _Return = objVar.Sqlcmd.ExecuteScalar().ToString();
                    objVar.Sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                WriteError(Query);
                WriteError(ex.Message);
            }
            finally
            {

                if (ISHANA == "HANA")
                {
                    if (objVar.Odbccon.State == ConnectionState.Open)
                    {
                        objVar.Odbccon.Close();
                    }
                }
                else
                {
                    if (objVar.Sqlcon.State == ConnectionState.Open)
                    {
                        objVar.Sqlcon.Close();
                    }
                }
            }
            return _Return;
        }

        public double ReturnValueDouble(string Query)
        {
            double _Return = 0;
            try
            {
                if (ISHANA == "HANA")
                {
                    if (objVar.Odbccon.State == ConnectionState.Open)
                    {
                        objVar.Odbccon.Close();
                    }
                    objVar.Odbccon.Open();
                    objVar.Odbccmd = new OdbcCommand(Query, objVar.Odbccon);
                    objVar.Odbccmd.CommandType = CommandType.Text;
                    _Return = Convert.ToDouble(objVar.Odbccmd.ExecuteScalar());
                    objVar.Odbccon.Close();
                }
                else
                {
                    if (objVar.Sqlcon.State == ConnectionState.Open)
                    {
                        objVar.Sqlcon.Close();
                    }
                    objVar.Sqlcon.Open();
                    objVar.Sqlcmd = new SqlCommand(Query, objVar.Sqlcon);
                    objVar.Sqlcmd.CommandType = CommandType.Text;
                    _Return = Convert.ToDouble(objVar.Sqlcmd.ExecuteScalar());
                    objVar.Sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                WriteError(Query);
                WriteError(ex.Message);
            }
            finally
            {

                if (ISHANA == "HANA")
                {
                    if (objVar.Odbccon.State == ConnectionState.Open)
                    {
                        objVar.Odbccon.Close();
                    }
                }
                else
                {
                    if (objVar.Sqlcon.State == ConnectionState.Open)
                    {
                        objVar.Sqlcon.Close();
                    }
                }
            }
            return _Return;
        }
        public static void WriteError(string errorMessage)
        {

            try
            {
                string path = "~/ErrorLog/" + DateTime.Today.ToString("dd-MM-yy") + ".txt";


                if ((!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path))))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();

                }

                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {

                    //w.WriteLine(Constants.vbCrLf + "Log Entry : ");

                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));

                    //   Dim err As String = "Error in: " & System.Web.HttpContext.Current.Request.Url.ToString() & ". Error Message:" & errorMessage
                    try
                    {
                        string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() + ". Error Message:" + errorMessage;

                        w.WriteLine(err);
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine(ex.Message);
                        w.WriteLine(errorMessage);
                    }


                    //  w.WriteLine(err)

                    w.WriteLine("__________________________");

                    w.Flush();

                    w.Close();

                }

            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
        }
        public string TargetPath()
        {
            string PathName = "";
            DataSet ds1 = new DataSet();
            try
            {
                if (ISHANA == "HANA")
                {
                    SqlQuery = "select \"AttachPath\" from \"" + DBName + "\".OADP";
                }
                else
                {
                    SqlQuery = "select \"AttachPath\" from \"" + DBName + "\".dbo.OADP";
                }
                ds1 = ReturnDataset(SqlQuery);
                if (ds1.Tables[0].Rows.Count > 0)
                    PathName = ds1.Tables[0].Rows[0][0].ToString();
                return PathName;
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
            return PathName;
        }
        public void UpdatePassword(string ConfirmPwd, string EmpId)
        {
            if (ISHANA == "HANA")
                SqlQuery = "Update \"" + DBName + "\".OHEM set \"U_PWD\"='" + ConfirmPwd + "' where \"empID\"='" + EmpId + "'";
            else
                SqlQuery = "Update \"" + DBName + "\".dbo.OHEM set \"U_PWD\"='" + ConfirmPwd + "' where \"empID\"='" + EmpId + "'";
            ExecuteNonQuery(SqlQuery);
        }
        public string GetEmpName(string aEmpId)
        {
            DataSet ds = new DataSet();
            string strEmpName = "";
            try
            {
                if (ISHANA == "HANA")
                {
                    SqlQuery = "Select IFNULL(\"firstName\",'') || ' ' || IFNULL(\"middleName\",'') ||' ' || IFNULL(\"lastName\",'') AS \"EmpName\" from \"" + DBName + "\".OHEM where \"empID\"=" + aEmpId;                    
                }
                else
                {
                    SqlQuery = "Select isnull(\"firstName\",'') + ' ' + isnull(\"middleName\",'') +' ' + isnull(\"lastName\",'') AS \"EmpName\" from \"" + DBName + "\".dbo.OHEM where \"empID\"=" + aEmpId;                   
                }
                ds = ReturnDataset(SqlQuery);
                if (ds.Tables[0].Rows.Count > 0)
                    strEmpName = ds.Tables[0].Rows[0]["EmpName"].ToString();
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);               
            }
            return strEmpName;
        }
        public DataSet GetDocTemplate(string TemplateId)
        {
            DataSet ds = new DataSet();
            try
            {
                if (ISHANA == "HANA")
                {
                    SqlQuery = "SELECT T1.\"U_DocType\"  FROM \"" + DBName + "\".\"@TTP_ODASH\"  T0 JOIN \"" + DBName + "\".\"@TTP_DASH1\"  T1 ON T0.\"DocEntry\"=T1.\"DocEntry\" WHERE T0.\"U_TempId\"='" + TemplateId.Trim() + "' AND T1.\"U_Active\"='Y'";
                }
                else
                {
                    SqlQuery = "SELECT T1.\"U_DocType\"  FROM \"" + DBName + "\".dbo.\"@TTP_ODASH\"  T0 JOIN \"" + DBName + "\".dbo.\"@TTP_DASH1\"  T1 ON T0.\"DocEntry\"=T1.\"DocEntry\" WHERE T0.\"U_TempId\"='" + TemplateId.Trim() + "' AND T1.\"U_Active\"='Y'";
                }
                ds = ReturnDataset(SqlQuery);
                
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);                
            }
            return ds;
        }


    }
}