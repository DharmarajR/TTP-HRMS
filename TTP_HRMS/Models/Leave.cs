using System;
using System.Data;
using System.Web.UI.WebControls;
using TTP_HRMS.Entities;

namespace TTP_HRMS.Models
{
    public class Leave
    {
        DBVariables objVar = new DBVariables();       
        public string SqlQuery;
        CommonFunctions objCom = new CommonFunctions();
        string _Return;
        public void LeaveBindRequest(string empId, GridView Grdview)
        {
            try
            {               
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "Select T0.\"Code\" as \"Code\",T0.\"U_RequestByName\",\"U_RplEmpId\",\"U_RplEmpName\",T0.\"U_RequestBy\",\"U_TrnsCode\",T1.\"U_S_PName\" as \"Name\",CAST(\"U_StartDate\" AS varchar(10)) AS \"U_StartDate\",";
                    SqlQuery += " CAST(\"U_EndDate\" AS varchar(10)) AS \"U_EndDate\" ,cast(T0.\"U_NoofDays\" as decimal(10,2)) AS \"U_NoofDays\",\"U_Notes\",";
                    SqlQuery += " CAST(\"U_ReJoiNDate\" AS varchar(10)) AS \"U_ReJoiNDate\",case \"U_Status\" when 'P' then 'Pending' when 'R' then 'Rejected' ";
                    SqlQuery += " when 'A' then 'Approved' end as \"U_Status\",\"U_AppRemarks\",IFNULL(\"U_S_PAttach\",'') AS \"U_S_PAttach\",\"U_S_PEmAdd\",\"U_S_PEConNo\"";
                    SqlQuery += " ,IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover\" ),'') AS \"U_CurApprover\",";
                    SqlQuery += " IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover\" ),'') AS \"U_NxtApprover\", ";
                    SqlQuery += " IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover1\" ),'') AS \"U_CurApprover1\",";
                    SqlQuery += " IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover1\" ),'') AS \"U_NxtApprover1\" ";
                    SqlQuery += " ,CASE IFNULL(T0.\"U_OwnerStatus\",'C') WHEN 'P' THEN 'Pending' WHEN 'C' THEN 'Confirm' WHEN 'L' THEN 'Canceled' END AS \"U_OwnerStatus\",\"U_RequestBy\",\"U_RequestByName\",";
                    SqlQuery += " CASE IFNULL(T0.\"U_DocStatus\",'C') WHEN 'D' THEN 'Draft' WHEN 'C' THEN 'Confirm' WHEN 'L' THEN 'Canceled' END AS \"DocStatus\"";
                    SqlQuery += " ,IFNULL(\"U_RejPayroll\",'No') AS \"U_RejPayroll\",CASE IFNULL(\"U_Canelled\",'N') WHEN 'Y' THEN 'Yes' else 'No' END AS \"U_Canelled\" from \"" + objCom.DBName + "\".\"@TTP_ESSLT\" T0 inner join \"" + objCom.DBName + "\".\"@TTP_LEAVH\" T1 on T0.\"U_TrnsCode\"=T1.\"U_S_PCode\"";
                    SqlQuery += "  where (\"U_EMPID\"='" + empId + "' OR IFNULL(T0.\"U_RequestBy\",'0')='" + empId + "') and \"U_TransType\"='L' order by T0.\"Code\" Desc";
                    objCom.BindGrid(SqlQuery, Grdview);                    
                }
                else
                {
                    SqlQuery = "Select T0.\"Code\" as \"Code\",T0.\"U_RequestByName\",\"U_RplEmpId\",\"U_RplEmpName\",T0.\"U_RequestBy\",\"U_TrnsCode\",T1.\"U_S_PName\" as \"Name\",Convert(Varchar(10),\"U_StartDate\",103) AS \"U_StartDate\",";
                    SqlQuery += " Convert(Varchar(10),\"U_EndDate\",103) AS \"U_EndDate\" ,cast(T0.\"U_NoofDays\" as decimal(10,2)) AS \"U_NoofDays\",\"U_Notes\",";
                    SqlQuery += " Convert(Varchar(10),\"U_ReJoiNDate\",103) AS \"U_ReJoiNDate\",case \"U_Status\" when 'P' then 'Pending' when 'R' then 'Rejected' ";
                    SqlQuery += " when 'A' then 'Approved' end as \"U_Status\",\"U_AppRemarks\",ISNULL(\"U_S_PAttach\",'') AS \"U_S_PAttach\",\"U_S_PEmAdd\",\"U_S_PEConNo\"";
                    SqlQuery += " ,isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover\" ),'') AS \"U_CurApprover\",";
                    SqlQuery += " isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover\" ),'') AS \"U_NxtApprover\", ";
                    SqlQuery += " isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover1\" ),'') AS \"U_CurApprover1\",";
                    SqlQuery += " isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover1\" ),'') AS \"U_NxtApprover1\" ";
                    SqlQuery += " ,CASE ISNULL(T0.\"U_OwnerStatus\",'C') WHEN 'P' THEN 'Pending' WHEN 'C' THEN 'Confirm' WHEN 'L' THEN 'Canceled' END AS \"U_OwnerStatus\",\"U_RequestBy\",\"U_RequestByName\",";
                    SqlQuery += " CASE ISNULL(T0.\"U_DocStatus\",'C') WHEN 'D' THEN 'Draft' WHEN 'C' THEN 'Confirm' WHEN 'L' THEN 'Canceled' END AS \"DocStatus\"";
                    SqlQuery += " ,ISNULL(\"U_RejPayroll\",'No') AS \"U_RejPayroll\",CASE ISNULL(\"U_Canelled\",'N') WHEN 'Y' THEN 'Yes' else 'No' END AS \"U_Canelled\" from \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" T0 inner join \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\" T1 on T0.\"U_TrnsCode\"=T1.\"U_S_PCode\"";
                    SqlQuery += "  where (\"U_EMPID\"='" + empId + "' OR ISNULL(T0.\"U_RequestBy\",'0')='" + empId + "') and \"U_TransType\"='L' order by T0.\"Code\" Desc";
                    objCom.BindGrid(SqlQuery, Grdview);                    
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);               
            }
        }
        public void LeaveBindSummary(string empId, GridView Grdview)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "Select T0.\"Code\" as \"Code\",\"U_S_PTrnsCode\",\"U_RplEmpId\",\"U_RplEmpName\",T1.\"U_S_PName\" as \"Name\",CAST(\"U_S_PStartDate\" AS varchar(10)) AS \"U_S_PStartDate\",";
                    SqlQuery += " CAST(\"U_S_PEndDate\" AS varchar(10)) AS \"U_S_PEndDate\" ,cast(T0.\"U_S_PNoofDays\" as decimal(10,2)) AS \"U_S_PNoofDays\",\"U_S_PNotes\", ";
                    SqlQuery += " \"U_S_PEmAdd\",\"U_S_PEConNo\",IFNULL(\"U_S_PAttachment\",'') AS \"U_S_PAttachment\" from \"" + objCom.DBName + "\".\"@TTP_EMOLT\" T0 inner join \"" + objCom.DBName + "\".\"@TTP_LEAVH\" T1 on T0.\"U_S_PTrnsCode\"=T1.\"U_S_PCode\" where \"U_S_PEMPID\"='" + empId + "' ";
                    SqlQuery += " and \"U_S_PIsTerm\"='N' order by T0.\"Code\" Desc";
                    objCom.BindGrid(SqlQuery, Grdview);
                }
                else
                {
                    SqlQuery = "Select T0.\"Code\" as \"Code\",\"U_S_PTrnsCode\",\"U_RplEmpId\",\"U_RplEmpName\",T1.\"U_S_PName\" as \"Name\",Convert(Varchar(10),\"U_S_PStartDate\",103) AS \"U_S_PStartDate\",";
                    SqlQuery += " Convert(Varchar(10),\"U_S_PEndDate\",103) AS \"U_S_PEndDate\" ,cast(T0.\"U_S_PNoofDays\" as decimal(10,2)) AS \"U_S_PNoofDays\",\"U_S_PNotes\", ";
                    SqlQuery += " \"U_S_PEmAdd\",\"U_S_PEConNo\",ISNULL(\"U_S_PAttachment\",'') AS \"U_S_PAttachment\" from \"" + objCom.DBName + "\".dbo.\"@TTP_EMOLT\" T0 inner join \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\" T1 on T0.\"U_S_PTrnsCode\"=T1.\"U_S_PCode\" where \"U_S_PEMPID\"='" + empId + "' ";
                    SqlQuery += " and \"U_S_PIsTerm\"='N' order by T0.\"Code\" Desc";
                    objCom.BindGrid(SqlQuery, Grdview);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }
        public void BindReplaceEmployee(GridView grdRpl)
        {
            if (objCom.ISHANA == "HANA")
            {
                SqlQuery = "Select \"empID\",\"firstName\"||' '||IFNULL(\"middleName\",'')||' '||\"lastName\" AS \"EmpName\"  from \"" + objCom.DBName + "\".OHEM WHERE \"Active\"='Y'";
            }
            else
            {
                SqlQuery = "Select \"empID\",\"firstName\" +' '+ ISNULL(\"middleName\",'') +' '+ \"lastName\" AS \"EmpName\"  from \"" + objCom.DBName + "\".dbo.OHEM WHERE \"Active\"='Y'";
            }
            objCom.BindGrid(SqlQuery, grdRpl);
        }
        public void BindLeaveType(string Empid, GridView GrdLeave)
        {
            string IsTerms;
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "SELECT IFNULL(\"U_S_PTerms\",'') AS \"U_S_PTerms\" FROM \"" + objCom.DBName + "\".OHEM  WHERE \"empID\" ='" + Empid + "'";
                }
                else
                {
                    SqlQuery = "SELECT ISNULL(\"U_S_PTerms\",'') AS \"U_S_PTerms\" FROM \"" + objCom.DBName + "\".dbo.OHEM  WHERE \"empID\" ='" + Empid + "'";
                }
                IsTerms = objCom.ReturnValue(SqlQuery);
                if (IsTerms == "")
                {
                    if (objCom.ISHANA == "HANA")
                    {
                        SqlQuery = "Select T0.\"U_S_PCode\",T0.\"U_S_PName\",CAST(IFNULL(T2.\"U_S_PBalance\",0) AS Decimal(18,2)) AS \"Balance\"  from \"" + objCom.DBName + "\".\"@TTP_LEAVH\" T0 Left Outer Join  \"" + objCom.DBName + "\".\"@TTP_ELEVB\" T2 ";
                        SqlQuery += " on T2.\"U_S_PLeaveCode\"=T0.\"U_S_PCode\"   where T2.\"U_S_PEmpID\"='" + Empid + "' and T2.\"U_S_PYear\"=" + DateTime.Now.Year + " ";
                        SqlQuery += " and IFNULL(T0.\"U_S_PReqESS\",'N')='Y'  order by T0.\"DocEntry\"";
                    }
                    else
                    {
                        SqlQuery = "Select T0.\"U_S_PCode\",T0.\"U_S_PName\",CAST(ISNULL(T2.\"U_S_PBalance\",0) AS Decimal(18,2)) AS \"Balance\"  from \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\" T0 Left Outer Join  \"" + objCom.DBName + "\".dbo.\"@TTP_ELEVB\" T2 ";
                        SqlQuery += " on T2.\"U_S_PLeaveCode\"=T0.\"U_S_PCode\"   where T2.\"U_S_PEmpID\"='" + Empid + "' and T2.\"U_S_PYear\"=" + DateTime.Now.Year + " ";
                        SqlQuery += " and ISNULL(T0.\"U_S_PReqESS\",'N')='Y'  order by T0.\"DocEntry\"";
                    }
                }
                else if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "Select T0.\"U_S_PCode\",T0.\"U_S_PName\",CAST(IFNULL(T2.\"U_S_PBalance\",0) AS Decimal(18,2)) AS \"Balance\"  from \"" + objCom.DBName + "\".\"@TTP_LEAVH\" T0 Left Outer Join  \"" + objCom.DBName + "\".\"@TTP_ELEVB\" T2 ";
                    SqlQuery += " on T2.\"U_S_PLeaveCode\"=T0.\"U_S_PCode\"   where T2.\"U_S_PEmpID\"='" + Empid + "' and T2.\"U_S_PYear\"=" + DateTime.Now.Year + " ";
                    SqlQuery += " and IFNULL(T0.\"U_S_PReqESS\",'N')='Y' AND T0.\"U_S_PCode\" IN (SELECT \"U_S_PLeaveCode\" FROM \"" + objCom.DBName + "\".\"@S_PALMP\"  WHERE \"U_S_PTerms\" ='" + IsTerms.Trim() + "') order by T0.\"DocEntry\"";
                }
                else
                {
                    SqlQuery = "Select T0.\"U_S_PCode\",T0.\"U_S_PName\",CAST(ISNULL(T2.\"U_S_PBalance\",0) AS Decimal(18,2)) AS \"Balance\"  from \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\" T0 Left Outer Join  \"" + objCom.DBName + "\".dbo.\"@TTP_ELEVB\" T2 ";
                    SqlQuery += " on T2.\"U_S_PLeaveCode\"=T0.\"U_S_PCode\"    where T2.\"U_S_PEmpID\"='" + Empid + "' and T2.\"U_S_PYear\"=" + DateTime.Now.Year + " ";
                    SqlQuery += " and ISNULL(T0.\"U_S_PReqESS\",'N')='Y' AND T0.\"U_S_PCode\" IN (SELECT \"U_S_PLeaveCode\" FROM \"" + objCom.DBName + "\".dbo.\"@S_PALMP\"  WHERE \"U_S_PTerms\" ='" + IsTerms.Trim() + "') order by T0.\"DocEntry\"";
                }
                objCom.BindGrid(SqlQuery, GrdLeave);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }

        public string GetCutoff(string LveCode)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                    _Return = objCom.ReturnValue("Select T0.\"U_S_PCutoff\"  from \"" + objCom.DBName + "\".\"@TTP_LEAVH\" T0 where \"U_S_PCode\"='" + LveCode + "'");
                else
                    _Return = objCom.ReturnValue("Select T0.\"U_S_PCutoff\"  from \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\" T0 where \"U_S_PCode\"='" + LveCode + "'");

                return _Return;
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                throw ex;
            }
        }
        public string GetNodays(string FromDate,string ToDate)
        {
            DataSet ds = new DataSet();
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "SELECT DAYS_BETWEEN (TO_DATE ('" + FromDate + "'), TO_DATE('" + ToDate + "')) AS \"Column1\" FROM DUMMY";                   
                }
                else
                {
                    SqlQuery = "select datediff(D,'" + FromDate + "','" + ToDate + "')";                  
                }
                ds = objCom.ReturnDataset(SqlQuery);
                if (ds.Tables[0].Rows.Count > 0)
                    _Return = (Convert.ToInt32(ds.Tables[0].Rows[0]["Column1"].ToString()) + 1).ToString();
                return _Return;
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                throw ex;
            }
        }
        public double GetHolidayCount(string aEmpID, string aCuttoff, DateTime dtStartDate, DateTime dtEndDate, SAPbobsCOM.Company SapCompany)
        {
            double dblHolidays = 0;
            SAPbobsCOM.Recordset oRec, oRec1, otemp;
            DateTime aDate = dtStartDate;
            oRec = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRec1 = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            otemp = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            if (objCom.ISHANA == "HANA")
                oRec.DoQuery("Select * from \"" + objCom.DBName + "\".OHEM where \"empID\"=" + aEmpID);
            else
                oRec.DoQuery("Select * from \"" + objCom.DBName + "\".dbo.OHEM where \"empID\"=" + aEmpID);
            if (oRec.RecordCount > 0)
            {
                if (oRec.Fields.Item("U_S_PHldCode").Value != "")
                {
                    if (objCom.ISHANA == "HANA")
                        oRec1.DoQuery("Select * from \"" + objCom.DBName + "\".OHLD where \"HldCode\"='" + oRec.Fields.Item("U_S_PHldCode").Value + "'");
                    else
                        oRec1.DoQuery("Select * from \"" + objCom.DBName + "\".dbo.OHLD where \"HldCode\"='" + oRec.Fields.Item("U_S_PHldCode").Value + "'");
                    if (oRec1.RecordCount > 0)
                    {
                        while (dtStartDate <= dtEndDate)
                        {
                            if (aCuttoff == "B" | aCuttoff == "W")
                            {
                                string strweekname1, strweekname2;
                               // strweekname1 = WeekdayName(oRec1.Fields.Item("WndFrm").Value);
                                //strweekname2 = WeekdayName(oRec1.Fields.Item("WndTo").Value);
                               // if (DateTime.WeekdayName(DateTime.Weekday(dtStartDate)) == strweekname1 | DateTime.WeekdayName(DateTime.Weekday(dtStartDate)) == strweekname2)
                                  //  dblHolidays = dblHolidays + 1;
                            }
                            if (aCuttoff == "H" | aCuttoff == "B")
                            {
                                if (objCom.ISHANA == "HANA")
                                    otemp.DoQuery("Select * from \"" + objCom.DBName + "\".HLD1 where ('" + dtStartDate.ToString("yyyy-MM-dd") + "' between \"StrDate\" and \"EndDate\") and  \"HldCode\"='" + oRec.Fields.Item("U_S_PHldCode").Value + "'");
                                else
                                    otemp.DoQuery("Select * from \"" + objCom.DBName + "\".dbo.HLD1 where ('" + dtStartDate.ToString("yyyy-MM-dd") + "' between \"StrDate\" and \"EndDate\") and  \"HldCode\"='" + oRec.Fields.Item("U_S_PHldCode").Value + "'");

                                if (otemp.RecordCount > 0)
                                    dblHolidays = dblHolidays + 1;
                            }
                            dtStartDate = dtStartDate.AddDays(1);
                        }
                    }
                }
            }
            return dblHolidays;
        }
        public string SaveLeaveRequest(LeaveEntities objen)
        {
            string strCode;
            try
            {               
                strCode = objCom.Getmaxcode("@TTP_ESSLT", "Code");
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "Insert into \"" + objCom.DBName + "\".\"@TTP_ESSLT\" (\"Code\",\"Name\",\"U_EMPID\",\"U_EMPNAME\",\"U_TransType\",\"U_TrnsCode\",\"U_LeaveName\",\"U_StartDate\",\"U_EndDate\",\"U_NoofDays\",\"U_Notes\",\"U_ReJoiNDate\",\"U_Status\",\"U_LevBal\",\"U_Year\",\"U_Month\",\"U_TotalLeave\",\"U_S_PEmAdd\",\"U_S_PEConNo\",\"U_S_PAttach\",\"U_RejPayroll\",\"U_RequestBy\",\"U_RequestByName\",\"U_OwnerStatus\",\"U_DocStatus\",\"U_RplEmpId\",\"U_RplEmpName\") ";
                    SqlQuery += "Values ('" + strCode + "','" + strCode + "','" + objen.Empid + "','" + objen.EmpName + "','L','" + objen.LeaveCode + "','" + objen.LeaveName + "','" + objen.FromDate.ToString("yyyy/MM/dd") + "','" + objen.ToDate.ToString("yyyy/MM/dd") + "','" + objen.NoofDays + "','" + objen.Notes + "','" + objen.RejoinDt.ToString("yyyy/MM/dd") + "','" + objen.Status + "'," + objen.LeaveBalance + "," + objen.Year + "," + objen.Month + "," + objen.TotalLeave + ",'" + objen.ContactAdd + "','" + objen.PhoneNo + "','" + objen.Attachment + "','No','" + objen.OnBehalfId + "','" + objen.OnBehalfName + "','" + objen.OwnStatus + "','" + objen.DocStatus + "','" + objen.RplEmpId + "','" + objen.RplEmpName + "')";
                }
                else
                {
                    SqlQuery = "Insert into \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" (\"Code\",\"Name\",\"U_EMPID\",\"U_EMPNAME\",\"U_TransType\",\"U_TrnsCode\",\"U_LeaveName\",\"U_StartDate\",\"U_EndDate\",\"U_NoofDays\",\"U_Notes\",\"U_ReJoiNDate\",\"U_Status\",\"U_LevBal\",\"U_Year\",\"U_Month\",\"U_TotalLeave\",\"U_S_PEmAdd\",\"U_S_PEConNo\",\"U_S_PAttach\",\"U_RejPayroll\",\"U_RequestBy\",\"U_RequestByName\",\"U_OwnerStatus\",\"U_DocStatus\",\"U_RplEmpId\",\"U_RplEmpName\") ";
                    SqlQuery += "Values ('" + strCode + "','" + strCode + "','" + objen.Empid + "','" + objen.EmpName + "','L','" + objen.LeaveCode + "','" + objen.LeaveName + "','" + objen.FromDate.ToString("yyyy/MM/dd") + "','" + objen.ToDate.ToString("yyyy/MM/dd") + "','" + objen.NoofDays + "','" + objen.Notes + "','" + objen.RejoinDt.ToString("yyyy/MM/dd") + "','" + objen.Status + "'," + objen.LeaveBalance + "," + objen.Year + "," + objen.Month + "," + objen.TotalLeave + ",'" + objen.ContactAdd + "','" + objen.PhoneNo + "','" + objen.Attachment + "','No','" + objen.OnBehalfId + "','" + objen.OnBehalfName + "','" + objen.OwnStatus + "','" + objen.DocStatus + "','" + objen.RplEmpId + "','" + objen.RplEmpName + "')";
                }
                objCom.ExecuteNonQuery(SqlQuery);
                return strCode; // True
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);               
                return ex.Message;
            }          
        }
        public bool UpdateLeaveRequest(LeaveEntities objen)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                    SqlQuery = "Update \"" + objCom.DBName + "\".\"@TTP_ESSLT\" ";
                else
                    SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" ";
                SqlQuery += " SET \"U_Status\"='" + objen.Status + "', \"U_TrnsCode\"='" + objen.LeaveCode + "',\"U_DocStatus\"='" + objen.DocStatus + "', \"U_OwnerStatus\"='" + objen.OwnStatus + "',";
                SqlQuery += " \"U_StartDate\"='" + objen.FromDate.ToString("yyyy/MM/dd") + "',\"U_EndDate\"='" + objen.ToDate.ToString("yyyy/MM/dd") + "',";
                SqlQuery += " \"U_NoofDays\"='" + objen.NoofDays + "',\"U_LevBal\"=" + objen.LeaveBalance + ",\"U_Year\"=" + objen.Year + ",\"U_Month\"=" + objen.Month + ",";
                SqlQuery += " \"U_Notes\"='" + objen.Notes + "',\"U_ReJoiNDate\"='" + objen.RejoinDt.ToString("yyyy/MM/dd") + "',\"U_S_PEmAdd\"='" + objen.ContactAdd + "',\"U_S_PEConNo\"='" + objen.PhoneNo + "'";
                SqlQuery += " ,\"U_RplEmpId\"='" + objen.RplEmpId + "',\"U_RplEmpName\"='" + objen.RplEmpName + "'";
                if (objen.Attachment != "")
                    SqlQuery += ",\"U_S_PAttach\"='" + objen.Attachment + "' where \"Code\"='" + objen.strCode + "'";
                else
                    SqlQuery += " where \"Code\"='" + objen.strCode + "'";
                objCom.ExecuteNonQuery(SqlQuery);
                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);               
                return false;
            }
        }
        public string ValidateLeaveEntries(string aEmpId, string aLeveCode, DateTime dtStartDate, DateTime dtEndDate, SAPbobsCOM.Company SapCompany)
        {
            SAPbobsCOM.Recordset oTemp;          
            oTemp = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string strSQL = "", Status = "";
            try
            {
                if (objCom.ISHANA == "HANA")
                    strSQL = "Select  * from \"" + objCom.DBName + "\".\"@TTP_EMOLT\" where \"U_S_PEMPID\"='" + aEmpId + "' and '" + dtStartDate.ToString("yyyy-MM-dd") + "'  between \"U_S_PStartDate\" and \"U_S_PEndDate\"";
                else
                    strSQL = "Select  * from \"" + objCom.DBName + "\".dbo.\"@TTP_EMOLT\" where \"U_S_PEMPID\"='" + aEmpId + "' and '" + dtStartDate.ToString("yyyy-MM-dd") + "'  between \"U_S_PStartDate\" and \"U_S_PEndDate\"";
                oTemp.DoQuery(strSQL);
                if (oTemp.RecordCount > 0)
                {
                    Status = "Leave details already exists for requested period ";
                    return Status;
                }
                if (objCom.ISHANA == "HANA")
                    strSQL = "Select  * from \"" + objCom.DBName + "\".\"@TTP_EMOLT\" where \"U_S_PEMPID\"='" + aEmpId + "' and '" + dtEndDate.ToString("yyyy-MM-dd") + "'  between \"U_S_PStartDate\" and \"U_S_PEndDate\"";
                else
                    strSQL = "Select  * from \"" + objCom.DBName + "\".dbo.\"@TTP_EMOLT\" where \"U_S_PEMPID\"='" + aEmpId + "' and '" + dtEndDate.ToString("yyyy-MM-dd") + "'  between \"U_S_PStartDate\" and \"U_S_PEndDate\"";
                oTemp.DoQuery(strSQL);
                if (oTemp.RecordCount > 0)
                {
                    Status = "Leave details already exists for requested period ";
                    return Status;
                }
                CommonFunctions.WriteError("Validated Success");                
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(strSQL);
                CommonFunctions.WriteError(ex.StackTrace);
                return ex.Message;
            }
            return "Success";
        }
        public string ValidateExistsLeaveEntries(string aEmpId, string aLeveCode, DateTime dtStartDate, DateTime dtEndDate, SAPbobsCOM.Company SapCompany)
        {
            SAPbobsCOM.Recordset oTemp;           
            oTemp = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string Status;
            if (objCom.ISHANA == "HANA")
                SqlQuery = "Select  * from \"" + objCom.DBName + "\".\"@TTP_ESSLT\" where \"U_EMPID\"='" + aEmpId + "' and \"U_Status\"='P' and '" + dtStartDate.ToString("yyyy-MM-dd") + "'  between \"U_StartDate\" and \"U_EndDate\"";
            else
                SqlQuery = "Select  * from \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" where \"U_EMPID\"='" + aEmpId + "' and \"U_Status\"='P' and '" + dtStartDate.ToString("yyyy-MM-dd") + "'  between \"U_StartDate\" and \"U_EndDate\"";
            oTemp.DoQuery(SqlQuery);
            if (oTemp.RecordCount > 0)
            {
                Status = "Leave Request already exists for requested period ";
                return Status;
            }
            if (objCom.ISHANA == "HANA")
                SqlQuery = "Select  * from \"" + objCom.DBName + "\".\"@TTP_ESSLT\" where \"U_EMPID\"='" + aEmpId + "' and \"U_Status\"='P' and '" + dtEndDate.ToString("yyyy-MM-dd") + "'  between \"U_StartDate\" and \"U_EndDate\"";
            else
                SqlQuery = "Select  * from \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" where \"U_EMPID\"='" + aEmpId + "' and \"U_Status\"='P' and '" + dtEndDate.ToString("yyyy-MM-dd") + "'  between \"U_StartDate\" and \"U_EndDate\"";
            oTemp.DoQuery(SqlQuery);
            if (oTemp.RecordCount > 0)
            {
                Status = "Leave Request already exists for requested period ";
                return Status;
            }
            return "Success";
        }

        public string ValidateLeave(string aLeveCode, string aLeveBal, string aNofoDays, SAPbobsCOM.Company SapCompany)
        {
            SAPbobsCOM.Recordset oTemp;
            oTemp = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string Status;
            if (objCom.ISHANA == "HANA")
                SqlQuery = "SElect * from \"" + objCom.DBName + "\".\"@TTP_LEAVH\" where \"U_S_PCode\"='" + aLeveCode + "'";
            else
                SqlQuery = "SElect * from \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\" where \"U_S_PCode\"='" + aLeveCode + "'";
            oTemp.DoQuery(SqlQuery);
            if (oTemp.RecordCount > 0)
            {
                double MaxDays = oTemp.Fields.Item("U_S_PMaxDays").Value;
                if (oTemp.Fields.Item("U_S_PStopProces").Value == "Y")
                {
                    if (System.Convert.ToDouble(aLeveBal) < System.Convert.ToDouble(aNofoDays))
                    {
                        Status = "You can able to take maximum of " + aLeveBal + " Only ";
                        return Status;
                    }
                    if (System.Convert.ToDouble(MaxDays) < System.Convert.ToDouble(aNofoDays) & System.Convert.ToDouble(MaxDays) > 0)
                    {
                        Status = "You can able to take maximum of " + MaxDays + " Only per transaction. ";
                        return Status;
                    }
                }
            }
            return "Success";
        }
        public string BlockTransaction(string strCompany, DateTime dtStartDate, SAPbobsCOM.Company SapCompany, int intYear, int intMonth)
        {
            SAPbobsCOM.Recordset oTemp;
            oTemp = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string Status;
            if (objCom.ISHANA == "HANA")
                SqlQuery = "Select  * from \"" + objCom.DBName + "\".\"@S_PWRKST\" where ifnull(\"U_S_POffCycle\",'N')='N' and  \"U_S_PCompNo\"='" + strCompany + "' and   \"U_S_PProcess\"='Y' and \"U_S_PYEAR\"=" + intYear + " and \"U_S_PMONTH\"=" + intMonth;
            else
                SqlQuery = "Select  * from \"" + objCom.DBName + "\".dbo.\"@S_PWRKST\" where iSnull(\"U_S_POffCycle\",'N')='N' and  \"U_S_PCompNo\"='" + strCompany + "' and   \"U_S_PProcess\"='Y' and \"U_S_PYEAR\"=" + intYear + " and \"U_S_PMONTH\"=" + intMonth;
            oTemp.DoQuery(SqlQuery);
            if (oTemp.RecordCount > 0)
            {
                Status = "Payroll already generated for this selected period of Month is " + intMonth + " and Year " + intYear;
                return Status;
            }
            return "Success";
        }
        public bool WithDrawStatus(string DocType, string strCode)
        {
            DataSet ds = new DataSet();
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "select COUNT(1) from \"" + objCom.DBName + "\".\"@TTP_APHIS\" where \"U_DocEntry\"='" + strCode.Trim() + "' and \"U_DocType\"='" + DocType.Trim() + "' ";
                }
                else
                {
                    SqlQuery = "select COUNT(1) from \"" + objCom.DBName + "\".dbo.\"@TTP_APHIS\" where \"U_DocEntry\"='" + strCode.Trim() + "' and \"U_DocType\"='" + DocType.Trim() + "' ";
                }
                ds = objCom.ReturnDataset(SqlQuery);
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                throw ex;
            }
        }
        public DataSet PopulateLeaveRequest(string Code)
        {
            DataSet ds = new DataSet();
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "Select T0.\"Code\" as \"Code\",\"U_TrnsCode\",\"U_EMPID\",\"U_EMPNAME\",T1.\"U_S_PName\" as \"Name\",\"U_RplEmpId\",\"U_RplEmpName\",TO_NVARCHAR(TO_DATE(\"U_StartDate\"), 'YYYY-MM-DD') AS \"U_StartDate\",\"U_ApproveId\",";
                    SqlQuery += " TO_NVARCHAR(TO_DATE(\"U_EndDate\"), 'YYYY-MM-DD') AS \"U_EndDate\" ,cast(T0.\"U_NoofDays\" as decimal(10,2)) AS \"U_NoofDays\",\"U_Notes\",\"U_RequestBy\",\"U_RequestByName\",";
                    SqlQuery += " TO_NVARCHAR(TO_DATE(\"U_ReJoiNDate\"), 'YYYY-MM-DD') AS \"U_ReJoiNDate\",\"U_Month\",\"U_Year\",\"U_Status\",IFNULL(\"U_LevBal\",0) AS \"U_LevBal\",\"U_S_PEmAdd\",\"U_S_PEConNo\" ";
                    SqlQuery += " ,CASE IFNULL(\"U_Canelled\",'N') WHEN 'Y' THEN 'Yes' else 'No' END AS \"U_Canelled\",IFNULL(\"U_OwnerStatus\",'C') AS \"U_OwnerStatus\",IFNULL(\"U_DocStatus\",'C') AS \"U_DocStatus\",\"U_AppRemarks\" from \"" + objCom.DBName + "\".\"@TTP_ESSLT\" T0 inner join \"" + objCom.DBName + "\".\"@TTP_LEAVH\" T1 on T0.\"U_TrnsCode\"=T1.\"U_S_PCode\"  where  ";
                    SqlQuery += " T0.\"Code\"='" + Code + "'";                  
                }
                else
                {
                    SqlQuery = "Select T0.\"Code\" as \"Code\",\"U_TrnsCode\",\"U_EMPID\",\"U_EMPNAME\",T1.\"U_S_PName\" as \"Name\",\"U_RplEmpId\",\"U_RplEmpName\",Convert(Varchar(10),\"U_StartDate\",120) AS \"U_StartDate\",\"U_ApproveId\",";
                    SqlQuery += " Convert(Varchar(10),\"U_EndDate\",120) AS \"U_EndDate\" ,cast(T0.\"U_NoofDays\" as decimal(10,2)) AS \"U_NoofDays\",\"U_Notes\",\"U_RequestBy\",\"U_RequestByName\",";
                    SqlQuery += " Convert(Varchar(10),\"U_ReJoiNDate\",120) AS \"U_ReJoiNDate\",\"U_Month\",\"U_Year\",\"U_Status\",ISNULL(\"U_LevBal\",0) AS \"U_LevBal\",\"U_S_PEmAdd\",\"U_S_PEConNo\" ";
                    SqlQuery += " ,CASE ISNULL(\"U_Canelled\",'N') WHEN 'Y' THEN 'Yes' else 'No' END AS \"U_Canelled\",ISNULL(\"U_OwnerStatus\",'C') AS \"U_OwnerStatus\",ISNULL(\"U_DocStatus\",'C') AS \"U_DocStatus\",\"U_AppRemarks\" from \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" T0 inner join \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\" T1 on T0.\"U_TrnsCode\"=T1.\"U_S_PCode\"  where  ";
                    SqlQuery += " T0.\"Code\"='" + Code + "'";                   
                }
                ds = objCom.ReturnDataset(SqlQuery);                
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);               
            }
            return ds;
        }
        public bool CheckPayrollPostedSE(string EmpId, int intmonth, int intYear)
        {
            DataSet ds = new DataSet();
            try
            {               
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "select * from \"" + objCom.DBName + "\".\"@TTP_EMOLT\" where \"U_S_PPosted\"='Y' and  \"U_S_POffCycle\"='N' and \"U_S_PEMPID\" ='" + EmpId + "' and \"U_S_PMonth\"=" + intmonth + " and \"U_S_PYear\"=" + intYear;                   
                }
                else
                {
                    SqlQuery = "select * from \"" + objCom.DBName + "\".dbo.\"@TTP_EMOLT\" where \"U_S_PPosted\"='Y' and  \"U_S_POffCycle\"='N' and \"U_S_PEMPID\" ='" + EmpId + "' and \"U_S_PMonth\"=" + intmonth + " and \"U_S_PYear\"=" + intYear;                  
                }
                ds = objCom.ReturnDataset(SqlQuery);
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);                
            }
            return true;
        }

        public string GetLeaveBalance(string EmpId, string leaveCode, int intYear)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "select IFNULL(\"U_S_PBalance\",0) from \"" + objCom.DBName + "\".\"@TTP_ELEVB\" where \"U_S_PYear\"=" + intYear + " and \"U_S_PEmpID\"='" + EmpId + "' and \"U_S_PLeaveCode\"='" + leaveCode.Trim() + "'";
                }
                else
                {
                    SqlQuery = "select ISNULL(\"U_S_PBalance\",0) from \"" + objCom.DBName + "\".dbo.\"@TTP_ELEVB\" where \"U_S_PYear\"=" + intYear + " and \"U_S_PEmpID\"='" + EmpId + "' and \"U_S_PLeaveCode\"='" + leaveCode.Trim() + "'";
                }
                _Return = objCom.ReturnValue(SqlQuery);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
            return _Return;
        }
        public DataSet GetLeaveDetails(string leaveCode)
        {
            DataSet ds = new DataSet();
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "select IFNULL(\"U_S_PNAOM\",'N') AS \"U_S_PNAOM\",IFNULL(\"U_S_PEmcon\",'N') AS \"U_S_PEmcon\",";
                    SqlQuery += " IFNULL(\"U_S_PAttReq\",'N') AS \"U_S_PAttReq\",IFNULL(\"U_S_PPayPosted\",'N') AS \"U_S_PPayPosted\",";
                    SqlQuery += " IFNULL(\"U_S_PBalCheck\",'N') AS \"U_S_PBalCheck\"  from \"" + objCom.DBName + "\".\"@TTP_LEAVH\"";
                    SqlQuery += " where \"U_S_PCode\"='" + leaveCode.Trim() + "'";
                }
                else
                {
                    SqlQuery = "select ISNULL(\"U_S_PNAOM\",'N') AS \"U_S_PNAOM\",ISNULL(\"U_S_PEmcon\",'N') AS \"U_S_PEmcon\",";
                    SqlQuery += " ISNULL(\"U_S_PAttReq\",'N') AS \"U_S_PAttReq\",ISNULL(\"U_S_PPayPosted\",'N') AS \"U_S_PPayPosted\",";
                    SqlQuery += " ISNULL(\"U_S_PBalCheck\",'N') AS \"U_S_PBalCheck\"  from \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\"";
                    SqlQuery += " where \"U_S_PCode\"='" + leaveCode.Trim() + "'";
                }
                ds = objCom.ReturnDataset(SqlQuery);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
            return ds;
        }
        

        // Leave Approval Functions Start
        public string getLeaveType(string aCode)
        {
            string LeaveType = "";
            DataSet ds = new DataSet();
            if (objCom.ISHANA == "HANA")
            {
                SqlQuery = "select T0.\"U_LveType\" from \"" + objCom.DBName + "\".\"@TTP_APTMP\" T0 JOIN \"" + objCom.DBName + "\".\"@TTP_APTM2\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where (T1.\"U_AUser\" ='" + aCode + "' or T1.\"U_AUser1\" ='" + aCode + "')  and T0.\"U_DocType\"='LveReq'";
            }
            else
            {
                SqlQuery = "select T0.\"U_LveType\" from \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T0 JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where (T1.\"U_AUser\" ='" + aCode + "' or T1.\"U_AUser1\" ='" + aCode + "')  and T0.\"U_DocType\"='LveReq'";
             }
            ds = objCom.ReturnDataset(SqlQuery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int intRow = 0; intRow <= ds.Tables[0].Rows.Count - 1; intRow++)
                {
                    if (LeaveType == "")
                        LeaveType = "'" + ds.Tables[0].Rows[intRow][0].ToString() + "'";
                    else
                        LeaveType = LeaveType + " ,'" + ds.Tables[0].Rows[intRow][0].ToString() + "'";
                }
                return LeaveType;
            }
            else
                return "'99999'";
        }

        public void Requests(string UserCode,GridView grdRequestApproval)
        {            
            string strLvetype;
            try
            {
                //if (objEN.SrchLeave == "1=1")
                strLvetype = getLeaveType(UserCode);
                //else
                // strLvetype = objEN.SrchLeave;
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "Select  T0.\"Code\" as \"Code\",\"U_RplEmpId\",\"U_RplEmpName\",CAST(T6.\"startDate\" AS Varchar(10)) AS \"startDate\",IFNULL(T6.\"U_S_PWORK\",'') AS \"U_S_CompName\",T0.\"U_EMPID\",T0.\"U_EMPNAME\",\"U_TrnsCode\",IFNULL(\"U_LeaveName\",'') AS \"U_LeaveName\",CAST(\"U_StartDate\" AS Varchar(10)) AS \"U_StartDate\",";
                    SqlQuery += " CAST(\"U_EndDate\" AS Varchar(10)) AS \"U_EndDate\" ,cast(T0. \"U_NoofDays\" as Decimal(18,2)) AS \"U_NoofDays\",T0.\"U_LevBal\" AS \"U_LevBal\",CAST(\"U_Notes\" AS Varchar) as \"U_Notes\",CAST(";
                    SqlQuery += " \"U_ReJoiNDate\" AS Varchar(10)) AS \"U_ReJoiNDate\", T0.\"U_Month\",T0.\"U_Year\", case \"U_Status\" when 'P' then 'Pending' when 'R' then 'Rejected' when 'A' then 'Approved' end as \"U_Status\",IFNULL(T4.\"U_AppStatus\",'P') AS \"U_AppStatus\"";
                    SqlQuery += " ,Case \"U_AppRequired\" when 'Y' then 'Yes' else 'No' End as  \"U_AppRequired\",CAST(\"U_AppReqDate\" AS Varchar(10)) AS \"U_AppReqDate\",CAST(\"U_ReqTime\" AS Varchar) AS \"U_ReqTime\"";
                    SqlQuery += " ,IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover\" ),'') AS \"U_CurApprover\",";
                    SqlQuery += " IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover\" ),'') AS \"U_NxtApprover\", ";
                    SqlQuery += " IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover1\" ),'') AS \"U_CurApprover1\",";
                    SqlQuery += " IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover1\" ),'') AS \"U_NxtApprover1\" ";
                    SqlQuery += " , \"U_AppRemarks\",IFNULL(\"U_S_PAttach\",'') AS \"U_S_PAttach\",\"U_S_PEmAdd\",\"U_RequestBy\",\"U_RequestByName\",\"U_S_PEConNo\",IFNULL(\"U_RejPayroll\",'No') AS \"U_RejPayroll\" from \"" + objCom.DBName + "\".\"@TTP_ESSLT\" T0 JOIN  \"" + objCom.DBName + "\".\"@TTP_APTMP\" T3 ON T0.\"U_ApproveId\" = T3.\"DocEntry\" and (T0.\"U_Status\"='P' or T0.\"U_Status\"='-') ";
                    SqlQuery += " LEFT JOIN \"" + objCom.DBName + "\".\"@TTP_APHIS\" T4 ON T0.\"Code\"=T4.\"U_DocEntry\" AND (T4.\"U_ApproveBy\"='" + UserCode + "') AND T4.\"U_DocType\"='LveReq'";
                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTM2\" T2 ON T3.\"DocEntry\" = T2.\"DocEntry\" ";
                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"OHEM\" T6 ON T6.\"empID\" = T0.\"U_EMPID\" ";
                    SqlQuery += " WHERE IFNULL(T2.\"U_AMan\",'N')='Y' AND IFNULL(T3.\"U_Active\",'N')='Y' and  IFNULL(T0.\"U_AppRequired\",'N')='Y' and  (T2.\"U_AUser\" = '" + UserCode + "' OR (T2.\"U_AUser1\"='" + UserCode + "'))";
                    SqlQuery += " And (T0.\"U_NxtApprover\" = '" + UserCode + "' OR T0.\"U_NxtApprover1\" = '" + UserCode + "')";
                    SqlQuery += " And \"U_TrnsCode\" in (" + strLvetype + ") Order by T0.\"Code\" Desc";
                }
                else
                {
                    SqlQuery = "Select  T0.\"Code\" as \"Code\",\"U_RplEmpId\",\"U_RplEmpName\",Convert(varchar(10),T6.\"startDate\",103) AS \"startDate\",ISNULL(T6.\"U_S_PWORK\",'') AS \"U_S_CompName\",T0.\"U_EMPID\",T0.\"U_EMPNAME\",\"U_TrnsCode\",ISNULL(\"U_LeaveName\",'') AS \"U_LeaveName\",Convert(Varchar(10),\"U_StartDate\",103) AS \"U_StartDate\",";
                    SqlQuery += " Convert(Varchar(10),\"U_EndDate\",103) AS \"U_EndDate\" ,cast(T0. \"U_NoofDays\" as Decimal(18,2)) AS \"U_NoofDays\",T0.\"U_LevBal\" AS \"U_LevBal\",CAST(\"U_Notes\" AS Varchar) as \"U_Notes\",";
                    SqlQuery += " Convert(Varchar(10),\"U_ReJoiNDate\",103) AS \"U_ReJoiNDate\",T0.\"U_Month\",T0.\"U_Year\", case \"U_Status\" when 'P' then 'Pending' when 'R' then 'Rejected' when 'A' then 'Approved' end as \"U_Status\",ISNULL(T4.\"U_AppStatus\",'P') AS \"U_AppStatus\"";
                    SqlQuery += " ,Case \"U_AppRequired\" when 'Y' then 'Yes' else 'No' End as  \"U_AppRequired\",Convert(Varchar(10),\"U_AppReqDate\",103) AS \"U_AppReqDate\",CAST(\"U_ReqTime\" AS Varchar) AS \"U_ReqTime\"";
                    SqlQuery += " ,isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover\" ),'') AS \"U_CurApprover\",";
                    SqlQuery += " isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover\" ),'') AS \"U_NxtApprover\", ";
                    SqlQuery += " isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover1\" ),'') AS \"U_CurApprover1\",";
                    SqlQuery += " isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover1\" ),'') AS \"U_NxtApprover1\" ";
                    SqlQuery += " , U_AppRemarks,ISNULL(\"U_S_PAttach\",'') AS \"U_S_PAttach\",\"U_S_PEmAdd\",\"U_RequestBy\",\"U_RequestByName\",\"U_S_PEConNo\",ISNULL(\"U_RejPayroll\",'No') AS \"U_RejPayroll\"  from \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" T0 JOIN  \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T3 ON T0.\"U_ApproveId\" = T3.\"DocEntry\" and (T0.\"U_Status\"='P' or T0.\"U_Status\"='-') ";
                    SqlQuery += " LEFT JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APHIS\" T4 ON T0.\"Code\"=T4.\"U_DocEntry\" AND (T4.\"U_ApproveBy\"='" + UserCode + "') AND T4.\"U_DocType\"='LveReq'";
                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T2 ON T3.\"DocEntry\" = T2.\"DocEntry\" ";
                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"OHEM\" T6 ON T6.\"empID\" = T0.\"U_EMPID\" ";
                    SqlQuery += " WHERE ISNULL(T2.\"U_AMan\",'N')='Y' AND ISNULL(T3.\"U_Active\",'N')='Y' and  ISNULL(T0.\"U_AppRequired\",'N')='Y' and  (T2.\"U_AUser\" = '" + UserCode + "' OR (T2.\"U_AUser1\"='" + UserCode + "'))";
                    SqlQuery += " And (T0.\"U_NxtApprover\" = '" + UserCode + "' OR T0.\"U_NxtApprover1\" = '" + UserCode + "')";
                    SqlQuery += " And \"U_TrnsCode\" in (" + strLvetype + ") Order by T0.\"Code\" Desc";
                }
                objCom.BindGrid(SqlQuery, grdRequestApproval);              
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }          
        }

        public void Summary(string UserCode, GridView grdSummary)
        {
            string strLvetype;
            try
            {
                //if (objEN.SrchLeave == "1=1")
                strLvetype = getLeaveType(UserCode);
                //else
                // strLvetype = objEN.SrchLeave;
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "Select T0.\"Code\" as \"Code\",\"U_RplEmpId\",\"U_RplEmpName\",CAST(T6.\"startDate\" AS Varchar(10)) AS \"startDate\",IFNULL(T6.\"U_S_PWORK\",'') AS \"U_S_CompName\",T0.\"U_EMPID\",T0.\"U_EMPNAME\",\"U_TrnsCode\",IFNULL(\"U_LeaveName\",'') AS \"U_LeaveName\",CAST(\"U_StartDate\" AS varchar(10)) AS \"U_StartDate\",";
                    SqlQuery += " CAST(\"U_EndDate\" AS Varchar(10)) AS \"U_EndDate\" ,cast(T0. \"U_NoofDays\" as Decimal(18,2)) AS \"U_NoofDays\",T0.\"U_LevBal\" AS \"U_LevBal\", \"U_Notes\",CASE IFNULL(\"U_Canelled\",'N') WHEN 'Y' THEN 'Yes' else 'No' END AS \"U_Canelled\",CAST(";
                    SqlQuery += " \"U_ReJoiNDate\" AS Varchar(10)) AS \"U_ReJoiNDate\",  T0.\"U_Month\",CAST(IFNULL(T0.\"U_Year\", YEAR(NOW())) AS varchar(10)) AS \"U_Year\",case \"U_Status\" when 'P' then 'Pending' when 'R' then 'Rejected' ";
                    SqlQuery += " when 'A' then 'Approved' end as \"U_Status\",Case \"U_AppRequired\" when 'Y' then 'Yes' else 'No' End as  \"U_AppRequired\",CAST(\"U_AppReqDate\" AS Varchar) AS \"U_AppReqDate\",CAST(\"U_ReqTime\" AS Varchar) AS \"U_ReqTime\"";
                    SqlQuery += " ,IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover\" ),'') AS \"U_CurApprover\",";
                    SqlQuery += " IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover\" ),'') AS \"U_NxtApprover\", ";
                    SqlQuery += " IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover1\" ),'') AS \"U_CurApprover1\",";
                    SqlQuery += " IFNULL((select T13.\"firstName\" ||' '|| IFNULL(T13.\"middleName\",'') ||' '|| IFNULL(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".OHEM T13 JOIN \"" + objCom.DBName + "\".OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover1\" ),'') AS \"U_NxtApprover1\" ";
                    SqlQuery += " , \"U_AppRemarks\",IFNULL(\"U_S_PAttach\",'') AS \"U_S_PAttach\",\"U_S_PEmAdd\",\"U_RequestBy\",\"U_RequestByName\",\"U_S_PEConNo\",IFNULL(\"U_RejPayroll\",'No') AS \"U_RejPayroll\"   from \"" + objCom.DBName + "\".\"@TTP_ESSLT\" T0 JOIN  \"" + objCom.DBName + "\".\"@TTP_APTMP\" T3 ON T0.\"U_ApproveId\" = T3.\"DocEntry\" ";
                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTM2\" T2 ON T3.\"DocEntry\" = T2.\"DocEntry\" ";
                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"OHEM\" T6 ON T6.\"empID\" = T0.\"U_EMPID\" ";
                    SqlQuery += " WHERE IFNULL(T2.\"U_AMan\",'N')='Y' AND IFNULL(T3.\"U_Active\",'N')='Y' and  IFNULL(T0.\"U_AppRequired\",'N')='Y' and  (T2.\"U_AUser\" = '" + UserCode + "' OR T2.\"U_AUser1\" = '" + UserCode + "')";
                    SqlQuery += " And  \"U_TrnsCode\" in (" + strLvetype + ") Order by T0.\"Code\" Desc";
                }
                else
                {
                    SqlQuery = "Select T0.\"Code\" as \"Code\",\"U_RplEmpId\",\"U_RplEmpName\",Convert(varchar(10),T6.\"startDate\",103) AS \"startDate\",ISNULL(T6.\"U_S_PWORK\",'') AS \"U_S_CompName\",T0.\"U_EMPID\",T0.\"U_EMPNAME\",\"U_TrnsCode\",ISNULL(\"U_LeaveName\",'') AS \"U_LeaveName\",Convert(Varchar(10),\"U_StartDate\",103) AS \"U_StartDate\",";
                    SqlQuery += " Convert(Varchar(10),\"U_EndDate\",103) AS \"U_EndDate\" ,cast(T0. \"U_NoofDays\" as Decimal(18,2)) AS \"U_NoofDays\",T0.\"U_LevBal\" AS \"U_LevBal\", \"U_Notes\",CASE ISNULL(\"U_Canelled\",'N') WHEN 'Y' THEN 'Yes' else 'No' END AS \"U_Canelled\",";
                    SqlQuery += " Convert(Varchar(10),\"U_ReJoiNDate\",103) AS \"U_ReJoiNDate\",  T0.\"U_Month\",CAST(ISNULL(T0.\"U_Year\", YEAR(getdate())) AS varchar(10)) AS \"U_Year\",case \"U_Status\" when 'P' then 'Pending' when 'R' then 'Rejected' ";
                    SqlQuery += " when 'A' then 'Approved' end as \"U_Status\",Case \"U_AppRequired\" when 'Y' then 'Yes' else 'No' End as  \"U_AppRequired\",Convert(Varchar(10),\"U_AppReqDate\",103) AS \"U_AppReqDate\",CAST(\"U_ReqTime\" AS Varchar) AS \"U_ReqTime\"";
                    SqlQuery += " ,isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover\" ),'') AS \"U_CurApprover\",";
                    SqlQuery += " isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover\" ),'') AS \"U_NxtApprover\", ";
                    SqlQuery += " isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_CurApprover1\" ),'') AS \"U_CurApprover1\",";
                    SqlQuery += " isnull((select T13.\"firstName\" +' '+ISNULL(T13.\"middleName\",'') +' '+ isnull(T13.\"lastName\",'')  from \"" + objCom.DBName + "\".dbo.OHEM T13 JOIN \"" + objCom.DBName + "\".dbo.OUSR T14 ON T14.\"INTERNAL_K\" =T13.\"userId\" where T14.\"USER_CODE\"=T0.\"U_NxtApprover1\" ),'') AS \"U_NxtApprover1\" ";
                    SqlQuery += " , U_AppRemarks,ISNULL(\"U_S_PAttach\",'') AS \"U_S_PAttach\",\"U_S_PEmAdd\",\"U_RequestBy\",\"U_RequestByName\",\"U_S_PEConNo\",ISNULL(\"U_RejPayroll\",'No') AS \"U_RejPayroll\"  from \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" T0 JOIN  \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T3 ON T0.\"U_ApproveId\" = T3.\"DocEntry\" ";
                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T2 ON T3.\"DocEntry\" = T2.\"DocEntry\" ";
                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"OHEM\" T6 ON T6.\"empID\" = T0.\"U_EMPID\" ";
                    SqlQuery += " WHERE ISNULL(T2.\"U_AMan\",'N')='Y' AND ISNULL(T3.\"U_Active\",'N')='Y' and  ISNULL(T0.\"U_AppRequired\",'N')='Y' and  (T2.\"U_AUser\" = '" + UserCode + "' OR T2.\"U_AUser1\" = '" + UserCode + "')";
                    SqlQuery += " And  \"U_TrnsCode\" in (" + strLvetype + ") Order by T0.\"Code\" Desc";
                }
                objCom.BindGrid(SqlQuery, grdSummary);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }

     

        //Leave Approval Functions End
    }
}