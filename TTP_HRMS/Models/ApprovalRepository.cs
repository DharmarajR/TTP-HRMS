using System;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI.WebControls;
using TTP_HRMS.Entities;

namespace TTP_HRMS.Models
{
    public class ApprovalRepository
    {
        public string SqlQuery;
        CommonFunctions objCom = new CommonFunctions();
        Leave leave = new Leave();
        string _return;
        SmtpClient SmtpServer = new SmtpClient();
        MailMessage mail = new MailMessage();
        public string GetUserCode(string EmpId)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                    SqlQuery = "select T1.\"USER_CODE\" from \"" + objCom.DBName + "\".OHEM T0 JOIN \"" + objCom.DBName + "\".OUSR T1 on T0.\"userId\"=T1.\"USERID\" where T0.\"empID\"='" + EmpId + "'";
                else
                    SqlQuery = "select T1.\"USER_CODE\" from \"" + objCom.DBName + "\".dbo.OHEM T0 JOIN \"" + objCom.DBName + "\".dbo.OUSR T1 on T0.\"userId\"=T1.\"USERID\" where T0.\"empID\"='" + EmpId + "'";
                _return = objCom.ReturnValue(SqlQuery);                
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
            return _return;
        }
        public string GetEmpUserid(string EmpId)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "select T0.\"userId\" from \"" + objCom.DBName + "\".OHEM T0 JOIN \"" + objCom.DBName + "\".OUSR T1 on T0.\"userId\"=T1.\"USERID\" where T0.\"empID\"='" + EmpId + "'";
                }
                else
                {
                    SqlQuery = "select T0.\"userId\" from \"" + objCom.DBName + "\".dbo.OHEM T0 JOIN \"" + objCom.DBName + "\".dbo.OUSR T1 on T0.\"userId\"=T1.\"USERID\" where T0.\"empID\"='" + EmpId + "'";
                }
               _return = objCom.ReturnValue(SqlQuery);                
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                throw ex;
            }
            return _return;
        }

        public string DocApproval(string DocType, string Empid, string LeaveType = "")
        {
            DataSet ds = new DataSet();
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    switch (DocType)
                    {
                        case "Exp":
                        case "Train":
                        case "Travel":
                        case "LveReq":
                        case "LoanReq":
                        case "Air":
                        case "Time":
                            {
                                if (DocType == "LveReq")
                                {
                                    if (LeaveType != "")
                                        SqlQuery = "Select * from \"" + objCom.DBName + "\".\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where T0.\"U_LveType\"='" + LeaveType + "' and  T0.\"U_Active\"='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                    else
                                        SqlQuery = "Select * from \"" + objCom.DBName + "\".\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where T0.\"U_Active\"='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                }
                                else
                                    SqlQuery = "Select * from \"" + objCom.DBName + "\".\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where T0.\"U_Active\"='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                break;
                            }
                    }
                }
                else
                {
                    switch (DocType)
                    {
                        case "Exp":
                        case "Train":
                        case "Travel":
                        case "LveReq":
                        case "LoanReq":
                        case "Air":
                        case "Time":
                            {
                                if (DocType == "LveReq")
                                {
                                    if (LeaveType != "")
                                        SqlQuery = "Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where T0.\"U_LveType\"='" + LeaveType + "' and  T0.\"U_Active\"='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                    else
                                        SqlQuery = "Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where T0.\"U_Active\"='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                }
                                else
                                    SqlQuery = "Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where T0.\"U_Active\"='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                break;
                            }
                    }
                }
                ds = objCom.ReturnDataset(SqlQuery);
                if (ds.Tables[0].Rows.Count > 0)
                    _return = "P";
                else
                    _return = "A";
                return _return;
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                return "P";
            }
        }
        public string GetTemplateID(string DocType, string Empid, string LeaveType = "")
        {
            DataSet ds = new DataSet();
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    switch (DocType)
                    {
                        case "Exp":
                        case "Train":
                        case "Travel":
                        case "LveReq":
                        case "LoanReq":
                        case "Air":
                        case "Time":
                        case "PerApr":
                        case "RBAT":
                        case "BGOD":
                        case "Appr":
                            {
                                if (DocType == "LveReq")
                                {
                                    if (LeaveType != "")
                                        SqlQuery = "Select * from \"" + objCom.DBName + "\".\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where T0.\"U_LveType\"='" + LeaveType + "' and  IFNULL(T0.\"U_Active\",'N')='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                    else
                                        SqlQuery = "Select * from \"" + objCom.DBName + "\".\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where IFNULL(T0.\"U_Active\",'N')='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                }
                                else
                                    SqlQuery = "Select * from \"" + objCom.DBName + "\".\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where IFNULL(T0.\"U_Active\",'N')='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                break;
                            }
                    }
                }
                else
                {
                    switch (DocType)
                    {
                        case "Exp":
                        case "Train":
                        case "Travel":
                        case "LveReq":
                        case "LoanReq":
                        case "Air":
                        case "Time":
                        case "PerApr":
                        case "RBAT":
                        case "BGOD":
                        case "Appr":
                            {
                                if (DocType == "LveReq")
                                {
                                    if (LeaveType != "")
                                        SqlQuery = "Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where T0.\"U_LveType\"='" + LeaveType + "' and  isnull(T0.\"U_Active\",'N')='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                    else
                                        SqlQuery = "Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where isnull(T0.\"U_Active\",'N')='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                }
                                else
                                    SqlQuery = "Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T0 left join \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\" where isnull(T0.\"U_Active\",'N')='Y' and T0.\"U_DocType\"='" + DocType.ToString() + "' and T1.\"U_OUser\"='" + Empid + "' ";
                                break;
                            }
                    }
                }
                ds = objCom.ReturnDataset(SqlQuery);
                if (ds.Tables[0].Rows.Count > 0)
                    _return = ds.Tables[0].Rows[0]["DocEntry"].ToString();
                else
                    _return = "0";
                return _return;
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                throw ex;
            }
        }

        public void UpdateApprovalRequired(string strTable, string sColumn, string StrCode, string ReqValue, string AppTempId)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "Update \"" + objCom.DBName + "\".\"" + strTable + "\" set \"U_AppRequired\"='" + ReqValue + "',\"U_AppReqDate\"=NOW(),\"U_ApproveId\"='" + AppTempId + "',";
                    SqlQuery += " \"U_ReqTime\"='" + DateTime.Now.TimeOfDay.ToString() + "' where \"" + sColumn + "\" IN (" + StrCode + ")";
                }
                else
                {
                    SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"" + strTable + "\" set \"U_AppRequired\"='" + ReqValue + "',\"U_AppReqDate\"=getdate(),\"U_ApproveId\"='" + AppTempId + "',";
                    SqlQuery += " \"U_ReqTime\"='" + DateTime.Now.TimeOfDay.ToString() + "' where \"" + sColumn + "\" IN (" + StrCode + ")";
                }
                objCom.ExecuteNonQuery(SqlQuery);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }

        public void InitialMessage(string strReqType, string strReqNo, string strAppStatus
          , string strTemplateNo, string strOrginator, string enDocType, SAPbobsCOM.Company objMainCompany, string strExpNo = "", string ESSlink = "")
        {
            try
            {
                //SEUpdateApprover(strReqType, strReqNo, strAppStatus, strTemplateNo, strOrginator, enDocType, objMainCompany, DBName, strExpNo, ESSlink);
                // If ConnectSAP() = True Then
                string strQuery, mailType = "";
                string strEmailMessage = "";
                string strMessageUser, strMessageUser1, strExpReqNo1;
                SAPbobsCOM.Recordset oRecordSet, oTemp;
                SAPbobsCOM.CompanyService oCmpSrv;
                SAPbobsCOM.MessagesService oMessageService;
                SAPbobsCOM.Message oMessage;
                SAPbobsCOM.MessageDataColumns pMessageDataColumns;
                SAPbobsCOM.MessageDataColumn pMessageDataColumn;
                SAPbobsCOM.MessageDataLines oLines;
                SAPbobsCOM.MessageDataLine oLine;
                SAPbobsCOM.RecipientCollection oRecipientCollection;
                oCmpSrv = objMainCompany.GetCompanyService();
                oMessageService = oCmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.MessagesService);
                oMessage = oMessageService.GetDataInterface(SAPbobsCOM.MessagesServiceDataInterfaces.msdiMessage);
                oRecordSet = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oTemp = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (objCom.ISHANA == "HANA")
                    strQuery = "Select Top 1 \"U_AUser\",\"U_AUser1\" From \"" + objCom.DBName + "\".\"@TTP_APTM2\" Where \"DocEntry\" = '" + strTemplateNo + "'  and IFNULL(\"U_AMan\",'')='Y' Order By \"LineId\" Asc ";
                else
                    strQuery = "Select Top 1 \"U_AUser\",\"U_AUser1\" From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" Where \"DocEntry\" = '" + strTemplateNo + "'  and isnull(\"U_AMan\",'')='Y' Order By \"LineId\" Asc ";

                oRecordSet.DoQuery(strQuery);
                if (!oRecordSet.EoF)
                {
                    strMessageUser = oRecordSet.Fields.Item(0).Value;
                    strMessageUser1 = oRecordSet.Fields.Item(1).Value;
                    oMessage.Subject = strReqType + " " + "Need Your Approval ";
                    string strMessage = "";
                    switch (enDocType)
                    {
                        case "LveReq":
                            {
                                if (objCom.ISHANA == "HANA")
                                    strQuery = "Update \"" + objCom.DBName + "\".\"@TTP_ESSLT\" set \"U_CurApprover\"='" + strMessageUser + "',\"U_NxtApprover\"='" + strMessageUser + "',\"U_CurApprover1\"='" + strMessageUser1 + "',\"U_NxtApprover1\"='" + strMessageUser1 + "' where \"Code\"='" + strReqNo + "'";
                                else
                                    strQuery = "Update \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" set \"U_CurApprover\"='" + strMessageUser + "',\"U_NxtApprover\"='" + strMessageUser + "',\"U_CurApprover1\"='" + strMessageUser1 + "',\"U_NxtApprover1\"='" + strMessageUser1 + "' where \"Code\"='" + strReqNo + "'";
                                oTemp.DoQuery(strQuery);

                                if (objCom.ISHANA == "HANA")
                                    strQuery = "Select * from  \"" + objCom.DBName + "\".\"@TTP_ESSLT\" where \"Code\"='" + strReqNo + "'";
                                else
                                    strQuery = "Select * from  \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" where \"Code\"='" + strReqNo + "'";
                                oTemp.DoQuery(strQuery);
                                strMessage = " Requested by  " + oTemp.Fields.Item("U_EMPNAME").Value + ": Leave Name : " + oTemp.Fields.Item("U_LeaveName").Value;
                                strOrginator = strMessage;
                                mailType = "Approval required on Leave Request from  " + oTemp.Fields.Item("U_EMPNAME").Value;
                                break;
                            }
                    }

                    // Dim IntReqNo As Integer = Integer.Parse(strReqNo)
                    string IntReqNo = strReqNo;
                    oMessage.Text = strReqType + "  " + IntReqNo + " " + strOrginator + " is awaiting your approval ";
                    oRecipientCollection = oMessage.RecipientCollection;
                    oRecipientCollection.Add();
                    oRecipientCollection.Item(0).SendInternal = SAPbobsCOM.BoYesNoEnum.tYES;
                    oRecipientCollection.Item(0).UserCode = strMessageUser;
                    pMessageDataColumns = oMessage.MessageDataColumns;

                    pMessageDataColumn = pMessageDataColumns.Add();
                    pMessageDataColumn.ColumnName = "Request No";
                    oLines = pMessageDataColumn.MessageDataLines;
                    oLine = oLines.Add();
                    oLine.Value = IntReqNo;
                    oMessageService.SendMessage(oMessage);
                    if (enDocType == "LveReq")
                    {
                        SendMail_Approval(strEmailMessage, strMessageUser1, strMessageUser, objMainCompany, enDocType, oTemp.Fields.Item("U_EMPID").Value, strReqNo);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError("Initial Message :" + ex.Message);
                throw ex;
            }
        }
        public string AddUpdateDocument(ApprovalEntities objEN)
        {
            string strCurApprover, strAlterApprover, _Return = "";
            try
            {
                SAPbobsCOM.GeneralService oGeneralService;
                SAPbobsCOM.GeneralData oGeneralData;
                SAPbobsCOM.GeneralDataParams oGeneralParams;
                SAPbobsCOM.CompanyService oCompanyService;
                SAPbobsCOM.Recordset oRecordSet, otestRs, oTemp, oTest;
                oCompanyService = objEN.SapCompany.GetCompanyService();
                oGeneralService = oCompanyService.GetGeneralService("TTP_APHIS");
                oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);
                oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
                oRecordSet = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                otestRs = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oTemp = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oTest = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                switch (objEN.HeaderType)
                {
                    case "Exp":
                    case "Train":
                    case "Travel":
                    case "Air":
                    case "Time":
                    case "PerApr":
                    case "RBAT":
                    case "BGOD":
                    case "Appr":
                        {
                            if (objCom.ISHANA == "HANA")
                            {
                                SqlQuery = "select T0.\"DocEntry\",T1.\"LineId\",T1.\"U_AUser\",T1.\"U_AUser1\" from \"" + objCom.DBName + "\".\"@TTP_APTMP\" T0 JOIN \"" + objCom.DBName + "\".\"@TTP_APTM2\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\"";
                                SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTM1\" T2 on T1.\"DocEntry\"=T2.\"DocEntry\"";
                                SqlQuery += " where T0.\"U_DocType\"='" + objEN.HeaderType + "' AND T2.\"U_OUser\"='" + objEN.EmpId + "' AND (T1.\"U_AUser\"='" + objEN.UserCode + "' OR T1.\"U_AUser1\"='" + objEN.UserCode + "')";
                            }
                            else
                            {
                                SqlQuery = "select T0.\"DocEntry\",T1.\"LineId\",T1.\"U_AUser\",T1.\"U_AUser1\" from \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T0 JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\"";
                                SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T2 on T1.\"DocEntry\"=T2.\"DocEntry\"";
                                SqlQuery += " where T0.\"U_DocType\"='" + objEN.HeaderType + "' AND T2.\"U_OUser\"='" + objEN.EmpId + "' AND (T1.\"U_AUser\"='" + objEN.UserCode + "' OR T1.\"U_AUser1\"='" + objEN.UserCode + "')";
                            }
                            break;
                        }

                    case "LveReq":
                        {
                            if (objCom.ISHANA == "HANA")
                            {
                                SqlQuery = "select T0.\"DocEntry\",T1.\"LineId\",T1.\"U_AUser\",T1.\"U_AUser1\" from \"" + objCom.DBName + "\".\"@TTP_APTMP\" T0";
                                SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTM2\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\"";
                                SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTM1\" T2 on T1.\"DocEntry\"=T2.\"DocEntry\"";
                                SqlQuery += " where T0.\"U_LveType\"='" + objEN.LeaveCode + "' AND T0.\"U_DocType\"='" + objEN.HeaderType + "'";
                                SqlQuery += " AND T2.\"U_OUser\"='" + objEN.EmpId + "' AND (T1.\"U_AUser\"='" + objEN.UserCode + "' OR T1.\"U_AUser1\"='" + objEN.UserCode + "')";
                            }
                            else
                            {
                                SqlQuery = "select T0.\"DocEntry\",T1.\"LineId\",T1.\"U_AUser\",T1.\"U_AUser1\" from \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T0";
                                SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T1 on T0.\"DocEntry\"=T1.\"DocEntry\"";
                                SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T2 on T1.\"DocEntry\"=T2.\"DocEntry\"";
                                SqlQuery += " where T0.\"U_LveType\"='" + objEN.LeaveCode + "' AND T0.\"U_DocType\"='" + objEN.HeaderType + "'";
                                SqlQuery += " AND T2.\"U_OUser\"='" + objEN.EmpId + "' AND (T1.\"U_AUser\"='" + objEN.UserCode + "' OR T1.\"U_AUser1\"='" + objEN.UserCode + "')";
                            }
                            break;
                        }
                }
                otestRs.DoQuery(SqlQuery);
                if (otestRs.RecordCount > 0)
                {
                    objEN.HeadDocEntry = otestRs.Fields.Item(0).Value.ToString();
                    objEN.HeadLineId = otestRs.Fields.Item(1).Value.ToString();
                    strCurApprover = otestRs.Fields.Item(2).Value.ToString();
                    strAlterApprover = otestRs.Fields.Item(3).Value.ToString();
                    if (strAlterApprover == null || strAlterApprover == "")
                        strAlterApprover = strCurApprover;
                }
                else
                {
                    strCurApprover = objEN.UserCode;
                    strAlterApprover = objEN.UserCode;
                }
                if (objCom.ISHANA == "HANA")
                    SqlQuery = "Select * from \"" + objCom.DBName + "\".\"@TTP_APHIS\" where \"U_DocEntry\"='" + objEN.DocEntry + "' and \"U_DocType\"='" + objEN.HistoryType + "' and (\"U_ApproveBy\"='" + objEN.UserCode + "' OR \"U_AltApproveBy\"='" + strAlterApprover.Trim() + "')";
                else
                    SqlQuery = "Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_APHIS\" where \"U_DocEntry\"='" + objEN.DocEntry + "' and \"U_DocType\"='" + objEN.HistoryType + "' and (\"U_ApproveBy\"='" + objEN.UserCode + "' OR \"U_AltApproveBy\"='" + strAlterApprover.Trim() + "')";
                oRecordSet.DoQuery(SqlQuery);
                if (oRecordSet.RecordCount > 0)
                {
                    oGeneralParams.SetProperty("DocEntry", oRecordSet.Fields.Item("DocEntry").Value);
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);
                    oGeneralData.SetProperty("U_AppStatus", objEN.AppStatus);
                    oGeneralData.SetProperty("U_Remarks", objEN.Remarks);
                    oGeneralData.SetProperty("U_ADocEntry", objEN.HeadDocEntry);
                    oGeneralData.SetProperty("U_ALineId", objEN.HeadLineId);
                    oGeneralData.SetProperty("U_ApproveBy", strCurApprover);
                    oGeneralData.SetProperty("U_AltApproveBy", strAlterApprover);
                    if (objEN.HistoryType == "LveReq" | objEN.HistoryType == "Air")
                    {
                        oGeneralData.SetProperty("U_Month", objEN.Month);
                        oGeneralData.SetProperty("U_Year", objEN.Year);
                    }
                    if (objCom.ISHANA == "HANA")
                        oTemp.DoQuery("Select * ,IFNULL(\"firstName\",'') || ' '|| IFNULL(\"middleName\",'') ||' '|| IFNULL(\"lastName\",'') AS \"EmpName\" from \"" + objCom.DBName + "\".OHEM where \"userId\"=" + objEN.EmpUserId + "");
                    else
                        oTemp.DoQuery("Select * ,isnull(\"firstName\",'') +  ' ' + isnull(\"middleName\",'') +  ' ' + isnull(\"lastName\",'') AS \"EmpName\" from \"" + objCom.DBName + "\".dbo.OHEM where \"userId\"=" + objEN.EmpUserId + "");
                    if (oTemp.RecordCount > 0)
                    {
                        oGeneralData.SetProperty("U_EmpId", oTemp.Fields.Item("empID").Value.ToString());
                        oGeneralData.SetProperty("U_EmpName", oTemp.Fields.Item("EmpName").Value);
                    }
                    else
                    {
                        oGeneralData.SetProperty("U_EmpId", "");
                        oGeneralData.SetProperty("U_EmpName", "");
                    }
                    oGeneralService.Update(oGeneralData);
                    _Return = "Successfully approved document...";
                }
                else if ((objEN.DocEntry != "" & objEN.DocEntry != "0"))
                {
                    if (objCom.ISHANA == "HANA")
                        oTemp.DoQuery("Select * ,IFNULL(\"firstName\",'') || ' '|| IFNULL(\"middleName\",'') ||' '|| IFNULL(\"lastName\",'') AS \"EmpName\" from \"" + objCom.DBName + "\".OHEM where \"userId\"=" + objEN.EmpUserId + "");
                    else
                        oTemp.DoQuery("Select * ,isnull(\"firstName\",'') +  ' ' + isnull(\"middleName\",'') +  ' ' + isnull(\"lastName\",'') AS \"EmpName\" from \"" + objCom.DBName + "\".dbo.OHEM where \"userId\"=" + objEN.EmpUserId + "");
                    if (oTemp.RecordCount > 0)
                    {
                        oGeneralData.SetProperty("U_EmpId", oTemp.Fields.Item("empID").Value.ToString());
                        oGeneralData.SetProperty("U_EmpName", oTemp.Fields.Item("EmpName").Value);
                    }
                    else
                    {
                        oGeneralData.SetProperty("U_EmpId", "");
                        oGeneralData.SetProperty("U_EmpName", "");
                    }
                    oGeneralData.SetProperty("U_DocEntry", objEN.DocEntry);
                    oGeneralData.SetProperty("U_DocType", objEN.HistoryType);
                    oGeneralData.SetProperty("U_AppStatus", objEN.AppStatus);
                    oGeneralData.SetProperty("U_Remarks", objEN.Remarks);
                    oGeneralData.SetProperty("U_ApproveBy", strCurApprover);
                    oGeneralData.SetProperty("U_AltApproveBy", strAlterApprover);
                    oGeneralData.SetProperty("U_Approvedt", System.DateTime.Now);
                    oGeneralData.SetProperty("U_ADocEntry", objEN.HeadDocEntry);
                    oGeneralData.SetProperty("U_ALineId", objEN.HeadLineId);
                    if (objEN.HistoryType == "LveReq" | objEN.HistoryType == "Air")
                    {
                        oGeneralData.SetProperty("U_Month", objEN.Month);
                        oGeneralData.SetProperty("U_Year", objEN.Year);
                    }
                    oGeneralService.Add(oGeneralData);
                    _Return = "Successfully approved document...";
                }
                if (objEN.HeaderType != "Time")
                {
                    _Return = updateFinalStatus(objEN);
                    if (_Return == "Success" | _Return == "Successfully approved document...")
                    {
                        if (GetFinalStatus(objEN) == false)
                        {
                            if (objEN.AppStatus == "A")
                            {
                                SendMessage(objEN);
                            }
                        }
                    }
                }
                return _Return;
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                _Return = ex.Message;
                return _Return;
            }
        }
        public string updateFinalStatus(ApprovalEntities objEN)
        {
            string _Return = "";
            try
            {                
                SAPbobsCOM.Recordset oRecordSet, oTemp, oRec2;
                string StrMailMessage, strErrorMsg="", TANo;
                oRec2 = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecordSet = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oTemp = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (objEN.AppStatus == "A")
                {
                    switch (objEN.HeaderType)
                    {
                        case "Rec":
                        case "Pos":
                        case "Pro":
                            {
                                if (objCom.ISHANA == "HANA")
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@S_APPT3\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T3.\"DocEntry\"='" + objEN.ApproveId + "' and  \"U_AFinal\" = 'Y'";
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }
                                else
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@S_APPT3\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T3.\"DocEntry\"='" + objEN.ApproveId + "' and  \"U_AFinal\" = 'Y'";
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }

                                break;
                            }

                        case "ExpCli":
                        case "Train":
                        case "TraReq":
                        case "Air":
                            {
                                if (objCom.ISHANA == "HANA")
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "' and  \"U_AFinal\" = 'Y'";
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }
                                else
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "' and  \"U_AFinal\" = 'Y'";
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }

                                break;
                            }

                        case "LveReq":
                            {
                                if (objCom.ISHANA == "HANA")
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "' and  \"U_AFinal\" = 'Y' and T3.\"U_LveType\"='" + objEN.LeaveCode + "'";
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }
                                else
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "' and  \"U_AFinal\" = 'Y' and T3.\"U_LveType\"='" + objEN.LeaveCode + "'";
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }

                                break;
                            }
                    }
                    oRecordSet.DoQuery(SqlQuery);
                    if (!oRecordSet.EoF)
                    {
                        switch (objEN.HistoryType)
                        {

                            case "LveReq":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@TTP_ESSLT\" Set \"U_Year\"=" + objEN.Year + ",\"U_Month\"=" + objEN.Month + ",\"U_Status\" = 'A',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" Set \"U_Year\"=" + objEN.Year + ",\"U_Month\"=" + objEN.Month + ",\"U_Status\" = 'A',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";

                                    oRecordSet.DoQuery(SqlQuery);
                                    _Return = AddUDTPayroll(objEN.DocEntry, objEN.SapCompany, objEN.Year, objEN.Month);
                                    if (_Return == "Success")
                                    {
                                        StrMailMessage = "Leave request has been approved for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                        SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                        if (objCom.ISHANA == "HANA")
                                            oRec2.DoQuery("Select \"U_S_PEmpID\",IFNULL(\"U_GRCode\",'') AS \"U_S_GRCode\" from \"" + objCom.DBName + "\".OHEM where \"empID\"=" + objEN.EmpId);
                                        else
                                            oRec2.DoQuery("Select \"U_S_PEmpID\",ISNULL(\"U_GRCode\",'') AS \"U_S_GRCode\" from \"" + objCom.DBName + "\".dbo.OHEM where \"empID\"=" + objEN.EmpId);
                                        TANo = oRec2.Fields.Item("U_S_PEmpID").Value;
                                        if (oRec2.Fields.Item("U_S_GRCode").Value == "14")
                                            //_Return = objCom.AddUDTLeaveAirTicket(objEN.Airticket, objEN.EmpId, TANo, objEN.StartDate, objEN.SapCompany, objCom.DBName, objEN.Year, objEN.Month);
                                            return _Return;
                                    }
                                    else
                                    {
                                        if (objCom.ISHANA == "HANA")
                                            SqlQuery = "Update \"" + objCom.DBName + "\".\"@TTP_ESSLT\" Set  \"U_Status\" = 'P',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                        else
                                            SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" Set  \"U_Status\" = 'P',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        return _Return;
                                    }
                                    break;
                                }

                            case "RetLve":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_RLETRANS1\" Set \"U_Status\" = 'A',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_RLETRANS1\" Set \"U_Status\" = 'A',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);

                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "UPDATE \"" + objCom.DBName + "\".\"@S_PELVETRANS\" SET \"U_S_PReJoiNDate\"='" + objEN.DisDate.ToString("yyyyMMdd") + "' WHERE \"Code\"='" + objEN.SrchLeave.Trim() + "'";
                                    else
                                        SqlQuery = "UPDATE \"" + objCom.DBName + "\".dbo.\"@S_PELVETRANS\" SET \"U_S_PReJoiNDate\"='" + objEN.DisDate.ToString("yyyyMMdd") + "' WHERE \"Code\"='" + objEN.SrchLeave.Trim() + "' ";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Return from Leave request has been approved for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    break;
                                }

                            case "ExpCli":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@Z_HR_EXPCL\" Set \"U_Year\"=" + objEN.Year + ",\"U_Month\"=" + objEN.Month + ", \"U_AppStatus\" = 'A' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@Z_HR_EXPCL\" Set \"U_Year\"=" + objEN.Year + ",\"U_Month\"=" + objEN.Month + ", \"U_AppStatus\" = 'A' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    // _Return = AddtoUDT1_PayrollTrans(objEN.DocEntry, objEN.SapCompany)
                                    StrMailMessage = "Expense claim request has been approved for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "TraReq":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_OTRAREQ\" Set \"U_AppStatus\" = 'A',\"U_SComme\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_OTRAREQ\" Set \"U_AppStatus\" = 'A',\"U_SComme\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Travel request has been approved for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "RegTra":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_TRIN1\" Set \"U_Status\"='A' , U_AppStatus = 'A',\"U_MgrRegRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_TRIN1\" Set \"U_Status\"='A' , U_AppStatus = 'A',\"U_MgrRegRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Your Registered Training request number :" + System.Convert.ToString(objEN.DocEntry) + " has been approved.";
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "NewTra":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_ONTREQ\" Set \"U_AppStatus\" = 'A',\"U_MgrRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_ONTREQ\" Set \"U_AppStatus\" = 'A',\"U_MgrRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "New Training request number :" + System.Convert.ToString(objEN.DocEntry) + " has been approved.";
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    // strErrorMsg = objCom.CreateCourse(objEN.DocEntry, objEN.SapCompany, objCom.DBName);
                                    if (strErrorMsg == "Success")
                                    {
                                        strErrorMsg = "";
                                        //   strErrorMsg = objCom.CreateAgenda(objEN.DocEntry, objEN.SapCompany, objCom.DBName);
                                        if (strErrorMsg != "Success")
                                        {
                                            if (objCom.ISHANA == "HANA")
                                                SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_ONTREQ\" Set \"U_AppStatus\" = 'P' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                            else
                                                SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_ONTREQ\" Set \"U_AppStatus\" = 'P' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                            oRecordSet.DoQuery(SqlQuery);
                                        }
                                    }
                                    else
                                    {
                                        if (objCom.ISHANA == "HANA")
                                            SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_ONTREQ\" Set \"U_AppStatus\" = 'P' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        else
                                            SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_ONTREQ\" Set \"U_AppStatus\" = 'P' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                    }
                                    _Return = strErrorMsg;
                                    break;
                                }

                            case "Rec":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_ORMPREQ\" Set \"U_AppStatus\" = 'A',\"U_SRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_ORMPREQ\" Set \"U_AppStatus\" = 'A',\"U_SRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Recruitment request has been approved for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "AppShort":
                                {
                                    if (objCom.ISHANA == "HANA")
                                    {
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_OHEM1\" Set \"U_AppStatus\" = 'A',\"U_MgrRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        SqlQuery = "Select \"U_SAppID\" from \"" + objCom.DBName + "\".\"@S_OHEM1\" where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        if (oRecordSet.RecordCount > 0)
                                        {
                                            SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_OCRAPP\" Set \"U_Status\" = 'N' Where \"DocEntry\" = '" + oRecordSet.Fields.Item(0).Value + "'";
                                            oTemp.DoQuery(SqlQuery);
                                        }
                                    }
                                    else
                                    {
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_OHEM1\" Set \"U_AppStatus\" = 'A',\"U_MgrRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        SqlQuery = "Select \"U_SAppID\" from \"" + objCom.DBName + "\".dbo.\"@S_OHEM1\" where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        if (oRecordSet.RecordCount > 0)
                                        {
                                            SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_OCRAPP\" Set \"U_Status\" = 'N' Where \"DocEntry\" = '" + oRecordSet.Fields.Item(0).Value + "'";
                                            oTemp.DoQuery(SqlQuery);
                                        }
                                    }
                                    _Return = "Success";
                                    break;
                                }

                            case "EmpPro":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_HEM2\" Set \"U_AppStatus\" = 'A' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_HEM2\" Set \"U_AppStatus\" = 'A' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Employee promotion request has been approved for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "EmpPos":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_HEM4\" Set \"U_AppStatus\" = 'A' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_HEM4\" Set \"U_AppStatus\" = 'A' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Employee position change request has been approved for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "Final":
                                {
                                    if (objCom.ISHANA == "HANA")
                                    {
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_OHEM1\" set  \"U_APPlStatus\"='A', \"U_IntervStatus\" = 'A',\"U_IPHODSta\" = 'S', \"U_Finished\" = 'Y' where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        oRecordSet.DoQuery("Select \"U_SAppID\" from \"" + objCom.DBName + "\".\"@S_OHEM1\" where \"DocEntry\"='" + objEN.DocEntry + "'");
                                        if (oRecordSet.RecordCount > 0)
                                        {
                                            SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_OCRAPP\" set \"U_Status\" = 'M' where \"DocEntry\" = '" + oRecordSet.Fields.Item(0).Value + "'";
                                            oTemp.DoQuery(SqlQuery);
                                        }
                                    }
                                    else
                                    {
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_OHEM1\" set  \"U_APPlStatus\"='A', \"U_IntervStatus\" = 'A',\"U_IPHODSta\" = 'S', \"U_Finished\" = 'Y' where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        oRecordSet.DoQuery("Select \"U_SAppID\" from \"" + objCom.DBName + "\".dbo.\"@S_OHEM1\" where \"DocEntry\"='" + objEN.DocEntry + "'");
                                        if (oRecordSet.RecordCount > 0)
                                        {
                                            SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_OCRAPP\" set \"U_Status\" = 'M' where \"DocEntry\" = '" + oRecordSet.Fields.Item(0).Value + "'";
                                            oTemp.DoQuery(SqlQuery);
                                        }
                                    }
                                    _Return = "Success";
                                    break;
                                }

                            case "IntAppReq":
                                {
                                    string blnvalue = "";
                                    // blnvalue = CreateApplicants(objEN)
                                    if (blnvalue == "Success")
                                        _Return = "Success";
                                    else
                                        _Return = blnvalue;
                                    break;
                                }

                            case "PerObj":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"U_PEOPLEOBJ\" Set \"U_AppStatus\" = 'A',\"U_Remarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"U_DocEntry\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"U_PEOPLEOBJ\" Set \"U_AppStatus\" = 'A',\"U_Remarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"U_DocEntry\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    // _Return = PersonalObjectiveApproval(objEN)
                                    StrMailMessage = "Personel objective request has been approved for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "Air":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"S_AirTicket\" Set \"S_PYear\"=" + objEN.Year + ",\"S_PMonth\"=" + objEN.Month + ", \"S_AppStatus\" = 'A',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "',\"S_PPaid\"='" + objEN.IntReqNo + "' Where \"S_DocEntry\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"S_AirTicket\" Set \"S_PYear\"=" + objEN.Year + ",\"S_PMonth\"=" + objEN.Month + ", \"S_AppStatus\" = 'A',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "',\"S_PPaid\"='" + objEN.IntReqNo + "' Where \"S_DocEntry\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    //_Return = objCom.AddUDTAirTicket(objEN.DocEntry, objEN.SapCompany, objCom.DBName, objEN.CreateBy);
                                    StrMailMessage = "AirTicket request has been approved for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }
                        }
                    }
                }
                else if (objEN.AppStatus == "R")
                {
                    switch (objEN.HeaderType)
                    {
                        case "Rec":
                        case "Pos":
                        case "Pro":
                            {
                                if (objCom.ISHANA == "HANA")
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@S_APPT3\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T3.\"DocEntry\"='" + objEN.ApproveId + "'"; // and  U_Z_AFinal = 'Y'"
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }
                                else
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@S_APPT3\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T3.\"DocEntry\"='" + objEN.ApproveId + "'"; // and  U_Z_AFinal = 'Y'"
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }

                                break;
                            }

                        case "ExpCli":
                        case "Train":
                        case "TraReq":
                        case "Air":
                            {
                                if (objCom.ISHANA == "HANA")
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "'"; // and  U_Z_AFinal = 'Y'"
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }
                                else
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "'"; // and  U_Z_AFinal = 'Y'"
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }

                                break;
                            }

                        case "LveReq":
                            {
                                if (objCom.ISHANA == "HANA")
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "' and T3.\"U_LveType\"='" + objEN.LeaveCode + "'"; // and  U_Z_AFinal = 'Y' 
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }
                                else
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "' and T3.\"U_LveType\"='" + objEN.LeaveCode + "'"; // and  U_Z_AFinal = 'Y' 
                                    SqlQuery += " And (T2.\"U_AUser\"='" + objEN.UserCode + "' OR T2.\"U_AUser1\"='" + objEN.UserCode + "') And T3.\"U_DocType\" = '" + objEN.HeaderType + "'";
                                }

                                break;
                            }
                    }
                    oRecordSet.DoQuery(SqlQuery);
                    if (!oRecordSet.EoF)
                    {
                        switch (objEN.HistoryType)
                        {
                            case "BankTime":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@Z_PAY_OLADJTRANS1\" Set \"U_AppStatus\" = 'R',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@Z_PAY_OLADJTRANS1\" Set \"U_AppStatus\" = 'R',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Bank time request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "Air":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"S_AirTicket\" Set \"S_PYear\"=" + objEN.Year + ",\"S_PMonth\"=" + objEN.Month + ", \"S_AppStatus\" = 'R',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"S_DocEntry\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"S_AirTicket\" Set \"S_PYear\"=" + objEN.Year + ",\"S_PMonth\"=" + objEN.Month + ", \"S_AppStatus\" = 'R',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"S_DocEntry\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "AirTicket request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "PerHour":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@TTP_ESSLT\" Set  \"U_Status\" = 'R',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" Set  \"U_Status\" = 'R',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    _Return = "Success";
                                    break;
                                }

                            case "RetLve":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_RLETRANS1\" Set \"U_Status\" = 'R',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_RLETRANS1\" Set \"U_Status\" = 'R',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Return from Leave request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry) + " And Remarks:" + objEN.Remarks.Trim().Replace("'", "''");
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "LveReq":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@TTP_ESSLT\" Set \"U_Year\"=" + objEN.Year + ",\"U_Month\"=" + objEN.Month + ", \"U_Status\" = 'R',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" Set \"U_Year\"=" + objEN.Year + ",\"U_Month\"=" + objEN.Month + ", \"U_Status\" = 'R',\"U_AppRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Select * from \"" + objCom.DBName + "\".\"@TTP_ESSLT\"  Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\"  Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Leave request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "ExpCli":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_EXPCL\" Set \"U_Year\"=" + objEN.Year + ",\"U_Month\"=" + objEN.Month + ", \"U_AppStatus\" = 'R',\"U_RejRemark\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_EXPCL\" Set \"U_Year\"=" + objEN.Year + ",\"U_Month\"=" + objEN.Month + ", \"U_AppStatus\" = 'R',\"U_RejRemark\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Expense Claim request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "TraReq":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_OTRAREQ\" Set \"U_AppStatus\" = 'R',\"U_HRComme\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_OTRAREQ\" Set \"U_AppStatus\" = 'R',\"U_HRComme\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Travel request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "RegTra":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_TRIN1\" Set \"U_Status\"='R' , \"U_AppStatus\" = 'R',\"U_ApproveRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_TRIN1\" Set \"U_Status\"='R' , \"U_AppStatus\" = 'R',\"U_ApproveRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Your Registered training request number :" + System.Convert.ToString(objEN.DocEntry) + " has been rejected.";
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "NewTra":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_ONTREQ\" Set \"U_AppStatus\" = 'R',\"U_MgrRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_ONTREQ\" Set \"U_AppStatus\" = 'R',\"U_MgrRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "New Training request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "Rec":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_ORMPREQ\" Set \"U_AppStatus\" = 'R',\"U_SRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_ORMPREQ\" Set \"U_AppStatus\" = 'R',\"U_SRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Recruitment request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "AppShort":
                                {
                                    if (objCom.ISHANA == "HANA")
                                    {
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_OHEM1\" Set \"U_AppStatus\" = 'R',\"U_MgrRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        SqlQuery = "Select \"U_SAppID\" from \"" + objCom.DBName + "\".\"@S_OHEM1\" where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        if (oRecordSet.RecordCount > 0)
                                        {
                                            SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_OCRAPP\" Set \"U_Status\" = 'N' Where \"DocEntry\" = '" + oRecordSet.Fields.Item(0).Value + "'";
                                            oTemp.DoQuery(SqlQuery);
                                        }
                                    }
                                    else
                                    {
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_OHEM1\" Set \"U_AppStatus\" = 'R',\"U_MgrRemarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        SqlQuery = "Select \"U_SAppID\" from \"" + objCom.DBName + "\".dbo.\"@S_OHEM1\" where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        if (oRecordSet.RecordCount > 0)
                                        {
                                            SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_OCRAPP\" Set \"U_Status\" = 'N' Where \"DocEntry\" = '" + oRecordSet.Fields.Item(0).Value + "'";
                                            oTemp.DoQuery(SqlQuery);
                                        }
                                    }
                                    _Return = "Success";
                                    break;
                                }

                            case "EmpPro":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_HEM2\" Set \"U_AppStatus\" = 'R' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_HEM2\" Set \"U_AppStatus\" = 'R' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Employee promotion request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "EmpPos":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_HEM4\" Set \"U_AppStatus\" = 'R' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_HEM4\" Set \"U_AppStatus\" = 'R' Where \"Code\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Employee position change request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "Final":
                                {
                                    if (objCom.ISHANA == "HANA")
                                    {
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"@Z_HR_OHEM1\" set  \"U_APPlStatus\"='R', \"U_IntervStatus\" = 'R',\"U_IPHODSta\" = 'R', \"U_Finished\" = 'Y' where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        oRecordSet.DoQuery("Select \"U_SAppID\" from \"" + objCom.DBName + "\".\"@S_OHEM1\" where \"DocEntry\"='" + objEN.DocEntry + "'");
                                        if (oRecordSet.RecordCount > 0)
                                        {
                                            SqlQuery = "Update \"" + objCom.DBName + "\".\"@S_OCRAPP\" set \"U_Status\" = 'R',\"U_RejResn\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' where \"DocEntry\" = '" + oRecordSet.Fields.Item(0).Value + "'";
                                            oTemp.DoQuery(SqlQuery);
                                        }
                                    }
                                    else
                                    {
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@Z_HR_OHEM1\" set  \"U_APPlStatus\"='R', \"U_IntervStatus\" = 'R',\"U_IPHODSta\" = 'R', \"U_Finished\" = 'Y' where \"DocEntry\" = '" + objEN.DocEntry + "'";
                                        oRecordSet.DoQuery(SqlQuery);
                                        oRecordSet.DoQuery("Select \"U_SAppID\" from \"" + objCom.DBName + "\".dbo.\"@S_OHEM1\" where \"DocEntry\"='" + objEN.DocEntry + "'");
                                        if (oRecordSet.RecordCount > 0)
                                        {
                                            SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@S_OCRAPP\" set \"U_Status\" = 'R',\"U_RejResn\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' where \"DocEntry\" = '" + oRecordSet.Fields.Item(0).Value + "'";
                                            oTemp.DoQuery(SqlQuery);
                                        }
                                    }
                                    _Return = "Success";
                                    break;
                                }

                            case "IntAppReq":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"U_VACPOSITION\" Set \"U_AppStatus\" = 'R',\"U_Remarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"U_DocEntry\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"U_VACPOSITION\" Set \"U_AppStatus\" = 'R',\"U_Remarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "'  Where \"U_DocEntry\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Internel applicatns request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }

                            case "PerObj":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        SqlQuery = "Update \"" + objCom.DBName + "\".\"U_PEOPLEOBJ\" Set \"U_AppStatus\" = 'R',\"U_Remarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"U_DocEntry\" = '" + objEN.DocEntry + "'";
                                    else
                                        SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"U_PEOPLEOBJ\" Set \"U_AppStatus\" = 'R',\"U_Remarks\"='" + objEN.Remarks.Trim().Replace("'", "''") + "' Where \"U_DocEntry\" = '" + objEN.DocEntry + "'";
                                    oRecordSet.DoQuery(SqlQuery);
                                    StrMailMessage = "Personel objective request has been rejected for the request number :" + System.Convert.ToString(objEN.DocEntry);
                                    SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany, objEN.HistoryType, objEN.DocEntry);
                                    _Return = "Success";
                                    break;
                                }
                        }
                    }
                }
                // End If

            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                _Return = ex.Message;
                return _Return;
            }
            return _Return;
        }
        public string AddUDTPayroll(string strHeadcode, SAPbobsCOM.Company SapCompany, int intYear, int intMonth)
        {
            string strTable, strCode, strQuery, PayCompany, PayPosted;
            string _Return = "";
            SAPbobsCOM.UserTable oUserTable;
            SAPbobsCOM.Recordset oRecSet, oRec2, oTemp;
            string stStartdt, StEnddt, stRejoindt, TANo;
            DateTime Startdt = DateTime.Now, Enddt = DateTime.Now, Rejoindt = DateTime.Now;
            try
            {
                oRecSet = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRec2 = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oTemp = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oUserTable = SapCompany.UserTables.Item("TTP_EMOLT");
                strTable = "@TTP_EMOLT";
                try
                {
                    if (objCom.ISHANA == "HANA")
                    {
                        strQuery = "Select \"U_EMPID\",\"U_EMPNAME\",\"U_TrnsCode\",\"U_LeaveName\",\"U_NoofDays\",\"U_Year\", \"U_ReJoiNDate\",";
                        strQuery += " \"U_RplEmpId\",\"U_RplEmpName\", \"U_StartDate\", \"U_EndDate\",\"U_Notes\",\"U_Month\",\"U_S_PEmAdd\",";
                        strQuery += " \"U_S_PEConNo\",\"U_S_PAttach\" from \"" + objCom.DBName + "\".\"@TTP_ESSLT\" ";
                        strQuery += " where \"U_Status\"='A' and  \"Code\"='" + strHeadcode + "'";
                    }
                    else
                    {
                        strQuery = "Select \"U_EMPID\",\"U_EMPNAME\",\"U_TrnsCode\",\"U_LeaveName\",\"U_NoofDays\",\"U_Year\", \"U_ReJoiNDate\",";
                        strQuery += " \"U_RplEmpId\",\"U_RplEmpName\",\"U_StartDate\" AS \"U_StartDate\",\"U_EndDate\" AS \"U_EndDate\",\"U_Notes\",";
                        strQuery += "  \"U_Month\",\"U_S_PEmAdd\",\"U_S_PEConNo\",\"U_S_PAttach\" from \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" ";
                        strQuery += " where \"U_Status\"='A' and  \"Code\"='" + strHeadcode + "'";
                    }
                    CommonFunctions.WriteError(strQuery);
                    oRecSet.DoQuery(strQuery);
                    if (oRecSet.RecordCount > 0)
                    {
                        CommonFunctions.WriteError("Validated");

                        _Return = leave.ValidateLeaveEntries(oRecSet.Fields.Item("U_EMPID").Value, oRecSet.Fields.Item("U_TrnsCode").Value, oRecSet.Fields.Item("U_StartDate").Value, oRecSet.Fields.Item("U_EndDate").Value, SapCompany);
                        if (_Return != "Success")
                        {
                            CommonFunctions.WriteError(_Return);
                            return _Return;
                        }
                        if (objCom.ISHANA == "HANA")
                            oRec2.DoQuery("Select * from \"" + objCom.DBName + "\".OHEM where \"empID\"=" + oRecSet.Fields.Item("U_EMPID").Value);
                        else
                            oRec2.DoQuery("Select * from \"" + objCom.DBName + "\".dbo.OHEM where \"empID\"=" + oRecSet.Fields.Item("U_EMPID").Value);
                        PayCompany = oRec2.Fields.Item("U_S_PCompNo").Value;
                        TANo = oRec2.Fields.Item("U_S_PEmpID").Value;

                        if (objCom.ISHANA == "HANA")
                            SqlQuery = "select IFNULL(\"U_S_PPayPosted\",'N') from \"" + objCom.DBName + "\".\"@TTP_LEAVH\" where \"U_S_PCode\"='" + oRecSet.Fields.Item("U_TrnsCode").Value + "'";
                        else
                            SqlQuery = "select ISNULL(\"U_S_PPayPosted\",'N') from \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\" where \"U_S_PCode\"='" + oRecSet.Fields.Item("U_TrnsCode").Value + "'";
                        oRec2.DoQuery(SqlQuery);
                        PayPosted = oRec2.Fields.Item(0).Value;

                        string CutOff = leave.GetCutoff(oRecSet.Fields.Item("U_TrnsCode").Value);
                        Startdt = oRecSet.Fields.Item("U_StartDate").Value;// Date.ParseExact(stStartdt.Trim().Replace(".", "/").Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) ' strClim
                        Enddt = oRecSet.Fields.Item("U_EndDate").Value;// Date.ParseExact(StEnddt.Trim().Replace("-", "/").Replace(".", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) ' strClim
                        Rejoindt = oRecSet.Fields.Item("U_ReJoiNDate").Value;// Date.ParseExact(stRejoindt.Trim().Replace(".", "/").Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) ' strClim
                        if (PayPosted == "Y")
                        {
                            _Return = leave.BlockTransaction(PayCompany, Startdt, SapCompany, intYear, intMonth);
                            if (_Return != "Success")
                            {
                                CommonFunctions.WriteError(_Return);
                                return _Return;
                            }
                        }
                        if (objCom.ISHANA == "HANA")
                            SqlQuery = "select IFNULL(\"U_S_PNAOM\",'N') from \"" + objCom.DBName + "\".\"@TTP_LEAVH\" where \"U_S_PCode\"='" + oRecSet.Fields.Item("U_TrnsCode").Value + "'";
                        else
                            SqlQuery = "select ISNULL(\"U_S_PNAOM\",'N') from \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\" where \"U_S_PCode\"='" + oRecSet.Fields.Item("U_TrnsCode").Value + "'";
                        _Return = objCom.ReturnValue(SqlQuery);

                        DateTime tempstdt, tempeddt;
                        tempeddt = Enddt; // 2016-10-17
                        tempstdt = Startdt; // 2016-12-17
                        DateTime dtTimeSpan;
                        bool blnSameMonth = false;
                        double intNoofdays;
                        double Holidays;
                        if (_Return == "Y")
                        {
                            do
                            {
                                if (blnSameMonth == true)
                                    break;
                                else if (tempstdt.Month != tempeddt.Month)
                                {
                                    Startdt = tempstdt;
                                    dtTimeSpan = new DateTime(Startdt.Year, Startdt.Month, DateTime.DaysInMonth(Startdt.Year, Startdt.Month));
                                    Enddt = dtTimeSpan;
                                    tempstdt = Enddt.AddDays(1);
                                    intNoofdays = (Enddt - Startdt).TotalDays + 1; // Startdt.Subtract(Enddt);
                                    //intNoofdays = DateTime.DateDiff(DateInterval.Day, Startdt, Enddt) + 1;
                                    intMonth = Startdt.Month;
                                    intYear = Startdt.Year;
                                }
                                else
                                {
                                    Startdt = tempstdt;
                                    Enddt = tempeddt;
                                    blnSameMonth = true;
                                    tempstdt = Enddt.AddDays(1);
                                    if (Startdt == Enddt)
                                    {
                                        if (oRecSet.Fields.Item("U_NoofDays").Value < 1)
                                        {
                                            intNoofdays = Convert.ToDouble(oRecSet.Fields.Item("U_NoofDays").Value);
                                        }
                                        else
                                        {
                                            intNoofdays = 1;
                                        }
                                    }
                                    else
                                    {
                                        intNoofdays = (Enddt - Startdt).TotalDays + 1;
                                        //intNoofdays = DateTime.DateDiff(DateInterval.Day, Startdt, Enddt) + 1;
                                    }
                                    intMonth = Startdt.Month;
                                    intYear = Startdt.Year;
                                }
                                Holidays = leave.GetHolidayCount(oRecSet.Fields.Item("U_EMPID").Value, CutOff, Startdt, Enddt, SapCompany);

                                intNoofdays = intNoofdays - Holidays;
                                strCode = objCom.Getmaxcode(strTable, "Code");
                                if (objCom.ISHANA == "HANA")
                                {
                                    try
                                    {
                                        SqlQuery = "Insert into \"" + objCom.DBName + "\".\"@TTP_EMOLT\"(\"U_RplEmpId\",\"U_RplEmpName\",\"Code\",\"Name\",\"U_S_PEmpId1\",\"U_S_PEMPID\",\"U_S_PEMPNAME\",\"U_S_PTrnsCode\",\"U_S_PLeaveName\",\"U_S_PStartDate\",\"U_S_PEndDate\",\"U_S_PNoofDays\",\"U_S_PNotes\",\"U_S_PMonth\",\"U_S_PYear\",\"U_S_PReJoiNDate\",\"U_S_PCutoff\",\"U_S_PEmAdd\",\"U_S_PEConNo\",\"U_S_PAttachment\",\"U_S_ESS_Ref\")";
                                        SqlQuery += " Values ('" + oRecSet.Fields.Item("U_RplEmpId").Value + "','" + oRecSet.Fields.Item("U_RplEmpName").Value + "','" + strCode + "','" + strCode + "','" + TANo + "','" + oRecSet.Fields.Item("U_EMPID").Value + "','" + oRecSet.Fields.Item("U_EMPNAME").Value + "','" + oRecSet.Fields.Item("U_TrnsCode").Value + "',";
                                        SqlQuery += " '" + oRecSet.Fields.Item("U_LeaveName").Value + "','" + Startdt.ToString("yyyy-MM-dd") + "','" + Enddt.ToString("yyyy-MM-dd") + "','" + intNoofdays + "','" + oRecSet.Fields.Item("U_Notes").Value + "'," + intMonth + ",";
                                        SqlQuery += " " + intYear + ",'" + Rejoindt.ToString("yyyy-MM-dd") + "','" + CutOff + "','" + oRecSet.Fields.Item("U_S_PEmAdd").Value + "','" + oRecSet.Fields.Item("U_S_PEConNo").Value + "','" + oRecSet.Fields.Item("U_S_PAttach").Value + "','" + strHeadcode + "')";
                                        oTemp.DoQuery(SqlQuery);
                                    }
                                    catch (Exception ex)
                                    {
                                        CommonFunctions.WriteError(SqlQuery);
                                        SqlQuery = "Insert into \"" + objCom.DBName + "\".\"@TTP_EMOLT\"(\"U_RplEmpId\",\"U_RplEmpName\",\"Code\",\"Name\",\"U_S_PEMPID\",\"U_S_PEMPNAME\",\"U_S_PTrnsCode\",\"U_S_PLeaveName\",\"U_S_PStartDate\",\"U_S_PEndDate\",\"U_S_PNoofDays\",\"U_S_PNotes\",\"U_S_PMonth\",\"U_S_PYear\",\"U_S_PReJoiNDate\",\"U_S_PCutoff\",\"U_S_PEmAdd\",\"U_S_PEConNo\",\"U_S_PAttachment\",\"U_S_ESS_Ref\")";
                                        SqlQuery += " Values ('" + oRecSet.Fields.Item("U_RplEmpId").Value + "','" + oRecSet.Fields.Item("U_RplEmpName").Value + "','" + strCode + "','" + strCode + "','" + oRecSet.Fields.Item("U_EMPID").Value + "','" + oRecSet.Fields.Item("U_EMPNAME").Value + "','" + oRecSet.Fields.Item("U_TrnsCode").Value + "',";
                                        SqlQuery += " '" + oRecSet.Fields.Item("U_LeaveName").Value + "','" + Startdt.ToString("yyyy-MM-dd") + "','" + Enddt.ToString("yyyy-MM-dd") + "','" + intNoofdays + "','" + oRecSet.Fields.Item("U_Notes").Value + "','" + intMonth + "',";
                                        SqlQuery += " '" + intYear + "','" + Rejoindt.ToString("yyyy-MM-dd") + "','" + CutOff + "','" + oRecSet.Fields.Item("U_S_PEmAdd").Value + "','" + oRecSet.Fields.Item("U_S_PEConNo").Value + "','" + oRecSet.Fields.Item("U_S_PAttach").Value + "','" + strHeadcode + "')";
                                        oTemp.DoQuery(SqlQuery);
                                    }
                                }
                                else
                                    try
                                    {
                                        SqlQuery = "Insert into \"" + objCom.DBName + "\".dbo.\"@TTP_EMOLT\"(\"U_RplEmpId\",\"U_RplEmpName\",\"Code\",\"Name\",\"U_S_PEmpId1\",\"U_S_PEMPID\",\"U_S_PEMPNAME\",\"U_S_PTrnsCode\",\"U_S_PLeaveName\",\"U_S_PStartDate\",\"U_S_PEndDate\",\"U_S_PNoofDays\",\"U_S_PNotes\",\"U_S_PMonth\",\"U_S_PYear\",\"U_S_PReJoiNDate\",\"U_S_PCutoff\",\"U_S_PEmAdd\",\"U_S_PEConNo\",\"U_S_PAttachment\",\"U_S_ESS_Ref\")";
                                        SqlQuery += " Values ('" + oRecSet.Fields.Item("U_RplEmpId").Value + "','" + oRecSet.Fields.Item("U_RplEmpName").Value + "','" + strCode + "','" + strCode + "','" + TANo + "','" + oRecSet.Fields.Item("U_EMPID").Value + "','" + oRecSet.Fields.Item("U_EMPNAME").Value + "','" + oRecSet.Fields.Item("U_TrnsCode").Value + "',";
                                        SqlQuery += " '" + oRecSet.Fields.Item("U_LeaveName").Value + "','" + Startdt.ToString("yyyy-MM-dd") + "','" + Enddt.ToString("yyyy-MM-dd") + "','" + intNoofdays + "','" + oRecSet.Fields.Item("U_Notes").Value + "'," + intMonth + ",";
                                        SqlQuery += " " + intYear + ",'" + Rejoindt.ToString("yyyy-MM-dd") + "','" + CutOff + "','" + oRecSet.Fields.Item("U_S_PEmAdd").Value + "','" + oRecSet.Fields.Item("U_S_PEConNo").Value + "','" + oRecSet.Fields.Item("U_S_PAttach").Value + "','" + strHeadcode + "')";
                                        oTemp.DoQuery(SqlQuery);
                                    }
                                    catch (Exception ex)
                                    {
                                        CommonFunctions.WriteError(SqlQuery);
                                        SqlQuery = "Insert into \"" + objCom.DBName + "\".dbo.\"@TTP_EMOLT\"(\"U_RplEmpId\",\"U_RplEmpName\",\"Code\",\"Name\",\"U_S_PEMPID\",\"U_S_PEMPNAME\",\"U_S_PTrnsCode\",\"U_S_PLeaveName\",\"U_S_PStartDate\",\"U_S_PEndDate\",\"U_S_PNoofDays\",\"U_S_PNotes\",\"U_S_PMonth\",\"U_S_PYear\",\"U_S_PReJoiNDate\",\"U_S_PCutoff\",\"U_S_PEmAdd\",\"U_S_PEConNo\",\"U_S_PAttachment\",\"U_S_ESS_Ref\")";
                                        SqlQuery += " Values ('" + oRecSet.Fields.Item("U_RplEmpId").Value + "','" + oRecSet.Fields.Item("U_RplEmpName").Value + "','" + strCode + "','" + strCode + "','" + oRecSet.Fields.Item("U_EMPID").Value + "','" + oRecSet.Fields.Item("U_EMPNAME").Value + "','" + oRecSet.Fields.Item("U_TrnsCode").Value + "',";
                                        SqlQuery += " '" + oRecSet.Fields.Item("U_LeaveName").Value + "','" + Startdt.ToString("yyyy-MM-dd") + "','" + Enddt.ToString("yyyy-MM-dd") + "','" + intNoofdays + "','" + oRecSet.Fields.Item("U_Notes").Value + "','" + intMonth + "',";
                                        SqlQuery += " " + intYear + ",'" + Rejoindt.ToString("yyyy-MM-dd") + "','" + CutOff + "','" + oRecSet.Fields.Item("U_S_PEmAdd").Value + "','" + oRecSet.Fields.Item("U_S_PEConNo").Value + "','" + oRecSet.Fields.Item("U_S_PAttach").Value + "','" + strHeadcode + "')";
                                        oTemp.DoQuery(SqlQuery);
                                    }
                            }
                            while (true);
                        }
                        else
                        {
                            strCode = objCom.Getmaxcode(strTable, "Code");
                            if (objCom.ISHANA == "HANA")
                            {
                                try
                                {
                                    SqlQuery = "Insert into \"" + objCom.DBName + "\".\"@TTP_EMOLT\"(\"U_RplEmpId\",\"U_RplEmpName\",\"Code\",\"Name\",\"U_S_PEmpId1\",\"U_S_PEMPID\",\"U_S_PEMPNAME\",\"U_S_PTrnsCode\",\"U_S_PLeaveName\",\"U_S_PStartDate\",\"U_S_PEndDate\",\"U_S_PNoofDays\",\"U_S_PNotes\",\"U_S_PMonth\",\"U_S_PYear\",\"U_S_PReJoiNDate\",\"U_S_PCutoff\",\"U_S_PEmAdd\",\"U_S_PEConNo\",\"U_S_PAttachment\",\"U_S_ESS_Ref\")";
                                    SqlQuery += " Values ('" + oRecSet.Fields.Item("U_RplEmpId").Value + "','" + oRecSet.Fields.Item("U_RplEmpName").Value + "','" + strCode + "','" + strCode + "','" + TANo + "','" + oRecSet.Fields.Item("U_EMPID").Value + "','" + oRecSet.Fields.Item("U_EMPNAME").Value + "','" + oRecSet.Fields.Item("U_TrnsCode").Value + "',";
                                    SqlQuery += " '" + oRecSet.Fields.Item("U_LeaveName").Value + "','" + Startdt.ToString("yyyy-MM-dd") + "','" + Enddt.ToString("yyyy-MM-dd") + "','" + oRecSet.Fields.Item("U_NoofDays").Value + "','" + oRecSet.Fields.Item("U_Notes").Value + "'," + intMonth + ",";
                                    SqlQuery += " " + intYear + ",'" + Rejoindt.ToString("yyyy-MM-dd") + "','" + CutOff + "','" + oRecSet.Fields.Item("U_S_PEmAdd").Value + "','" + oRecSet.Fields.Item("U_S_PEConNo").Value + "','" + oRecSet.Fields.Item("U_S_PAttach").Value + "','" + strHeadcode + "')";
                                    oTemp.DoQuery(SqlQuery);
                                }
                                catch (Exception ex)
                                {
                                    CommonFunctions.WriteError(SqlQuery);
                                    SqlQuery = "Insert into \"" + objCom.DBName + "\".\"@TTP_EMOLT\"(\"U_RplEmpId\",\"U_RplEmpName\",\"Code\",\"Name\",\"U_S_PEMPID\",\"U_S_PEMPNAME\",\"U_S_PTrnsCode\",\"U_S_PLeaveName\",\"U_S_PStartDate\",\"U_S_PEndDate\",\"U_S_PNoofDays\",\"U_S_PNotes\",\"U_S_PMonth\",\"U_S_PYear\",\"U_S_PReJoiNDate\",\"U_S_PCutoff\",\"U_S_PEmAdd\",\"U_S_PEConNo\",\"U_S_PAttachment\",\"U_S_ESS_Ref\")";
                                    SqlQuery += " Values ('" + oRecSet.Fields.Item("U_RplEmpId").Value + "','" + oRecSet.Fields.Item("U_RplEmpName").Value + "','" + strCode + "','" + strCode + "','" + oRecSet.Fields.Item("U_EMPID").Value + "','" + oRecSet.Fields.Item("U_EMPNAME").Value + "','" + oRecSet.Fields.Item("U_TrnsCode").Value + "',";
                                    SqlQuery += " '" + oRecSet.Fields.Item("U_LeaveName").Value + "','" + Startdt.ToString("yyyy-MM-dd") + "','" + Enddt.ToString("yyyy-MM-dd") + "','" + oRecSet.Fields.Item("U_NoofDays").Value + "','" + oRecSet.Fields.Item("U_Notes").Value + "'," + intMonth + ",";
                                    SqlQuery += " " + intYear + ",'" + Rejoindt.ToString("yyyy-MM-dd") + "','" + CutOff + "','" + oRecSet.Fields.Item("U_S_PEmAdd").Value + "','" + oRecSet.Fields.Item("U_S_PEConNo").Value + "','" + oRecSet.Fields.Item("U_S_PAttach").Value + "','" + strHeadcode + "')";
                                    oTemp.DoQuery(SqlQuery);
                                }
                            }
                            else
                                try
                                {
                                    SqlQuery = "Insert into \"" + objCom.DBName + "\".dbo.\"@TTP_EMOLT\"(\"U_RplEmpId\",\"U_RplEmpName\",\"Code\",\"Name\",\"U_S_PEmpId1\",\"U_S_PEMPID\",\"U_S_PEMPNAME\",\"U_S_PTrnsCode\",\"U_S_PLeaveName\",\"U_S_PStartDate\",\"U_S_PEndDate\",\"U_S_PNoofDays\",\"U_S_PNotes\",\"U_S_PMonth\",\"U_S_PYear\",\"U_S_PReJoiNDate\",\"U_S_PCutoff\",\"U_S_PEmAdd\",\"U_S_PEConNo\",\"U_S_PAttachment\",\"U_S_ESS_Ref\")";
                                    SqlQuery += " Values ('" + oRecSet.Fields.Item("U_RplEmpId").Value + "','" + oRecSet.Fields.Item("U_RplEmpName").Value + "','" + strCode + "','" + strCode + "','" + TANo + "','" + oRecSet.Fields.Item("U_EMPID").Value + "','" + oRecSet.Fields.Item("U_EMPNAME").Value + "','" + oRecSet.Fields.Item("U_TrnsCode").Value + "',";
                                    SqlQuery += " '" + oRecSet.Fields.Item("U_LeaveName").Value + "','" + Startdt.ToString("yyyy-MM-dd") + "','" + Enddt.ToString("yyyy-MM-dd") + "','" + oRecSet.Fields.Item("U_NoofDays").Value + "','" + oRecSet.Fields.Item("U_Notes").Value + "'," + intMonth + ",";
                                    SqlQuery += " " + intYear + ",'" + Rejoindt.ToString("yyyy-MM-dd") + "','" + CutOff + "','" + oRecSet.Fields.Item("U_S_PEmAdd").Value + "','" + oRecSet.Fields.Item("U_S_PEConNo").Value + "','" + oRecSet.Fields.Item("U_S_PAttach").Value + "','" + strHeadcode + "')";
                                    oTemp.DoQuery(SqlQuery);
                                }
                                catch (Exception ex)
                                {
                                    CommonFunctions.WriteError(SqlQuery);
                                    SqlQuery = "Insert into \"" + objCom.DBName + "\".dbo.\"@TTP_EMOLT\"(\"U_RplEmpId\",\"U_RplEmpName\",\"Code\",\"Name\",\"U_S_PEMPID\",\"U_S_PEMPNAME\",\"U_S_PTrnsCode\",\"U_S_PLeaveName\",\"U_S_PStartDate\",\"U_S_PEndDate\",\"U_S_PNoofDays\",\"U_S_PNotes\",\"U_S_PMonth\",\"U_S_PYear\",\"U_S_PReJoiNDate\",\"U_S_PCutoff\",\"U_S_PEmAdd\",\"U_S_PEConNo\",\"U_S_PAttachment\",\"U_S_ESS_Ref\")";
                                    SqlQuery += " Values ('" + oRecSet.Fields.Item("U_RplEmpId").Value + "','" + oRecSet.Fields.Item("U_RplEmpName").Value + "','" + strCode + "','" + strCode + "','" + oRecSet.Fields.Item("U_EMPID").Value + "','" + oRecSet.Fields.Item("U_EMPNAME").Value + "','" + oRecSet.Fields.Item("U_TrnsCode").Value + "',";
                                    SqlQuery += " '" + oRecSet.Fields.Item("U_LeaveName").Value + "','" + Startdt.ToString("yyyy-MM-dd") + "','" + Enddt.ToString("yyyy-MM-dd") + "','" + oRecSet.Fields.Item("U_NoofDays").Value + "','" + oRecSet.Fields.Item("U_Notes").Value + "'," + intMonth + ",";
                                    SqlQuery += " " + intYear + ",'" + Rejoindt.ToString("yyyy-MM-dd") + "','" + CutOff + "','" + oRecSet.Fields.Item("U_S_PEmAdd").Value + "','" + oRecSet.Fields.Item("U_S_PEConNo").Value + "','" + oRecSet.Fields.Item("U_S_PAttach").Value + "','" + strHeadcode + "')";
                                    oTemp.DoQuery(SqlQuery);
                                }
                        }
                        UpdateLeaveBalance_Transaction(SapCompany, oRecSet.Fields.Item("U_EMPID").Value, oRecSet.Fields.Item("U_TrnsCode").Value, intYear, intMonth);
                        _Return = "Success";
                    }
                }
                catch (Exception ex)
                {
                    CommonFunctions.WriteError(ex.StackTrace);
                    CommonFunctions.WriteError(SqlQuery);
                    _Return = ex.Message;
                    return _Return;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.StackTrace);
                CommonFunctions.WriteError(SqlQuery);
                _Return = ex.Message;
                return _Return;
            }
            return _Return;
        }
        public void UpdateLeaveBalance_Transaction(SAPbobsCOM.Company objMainCompany, string aEmpID, string aCode, int ayear, int amonth)
        {
            SAPbobsCOM.Recordset otemp1;
            string strRefCode, strEmpRefcode;
            otemp1 = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            double dblCarriedForward, dblYearly, dblTransaction, dblAdjustment, dblAccurred, dblPendingApproval;
            SAPbobsCOM.Recordset oTst, oTerms;
            string stString, strQuery, strLeaveName, st1;
            oTst = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oTerms = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            int intyear, intMont;
            for (int intLoop = 0; intLoop <= 0; intLoop++)
            {
                intyear = ayear;
                intMont = amonth;
                strRefCode = aEmpID;
                if (objCom.ISHANA == "HANA")
                    st1 = "Select * from \"" + objCom.DBName + "\".\"@TTP_ELEVM\" where \"U_S_PEmpID\"='" + strRefCode + "' and \"U_S_PLeaveCode\" ='" + aCode + "'";
                else
                    st1 = "Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_ELEVM\" where \"U_S_PEmpID\"='" + strRefCode + "' and \"U_S_PLeaveCode\" ='" + aCode + "'";

                otemp1.DoQuery(st1);
                for (int intRow = 0; intRow <= otemp1.RecordCount - 1; intRow++)
                {
                    SAPbobsCOM.Recordset oEar;
                    oEar = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    if (objCom.ISHANA == "HANA")
                        oEar.DoQuery("Select * from \"" + objCom.DBName + "\".\"@TTP_LEAVH\"  where \"U_S_PCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "'");
                    else
                        oEar.DoQuery("Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\"  where \"U_S_PCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "'");
                    dblYearly = oEar.Fields.Item("U_S_PDaysYear").Value;
                    strEmpRefcode = otemp1.Fields.Item("U_S_PLeaveCode").Value;
                    strLeaveName = otemp1.Fields.Item("U_S_PLeaveName").Value;
                    double dblUnPostedTrns1;
                    if (objCom.ISHANA == "HANA")
                        stString = "select IFNULL(sum(\"U_S_PNoofDays\"),0)  from \"" + objCom.DBName + "\".\"@TTP_EMOLT\" where \"Code\"=\"Name\" and \"U_S_PTrnsCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and  \"U_S_PYear\"= " + intyear + " and \"U_S_PEMPID\"='" + strRefCode + "'"; // group by U_Z_EmpID"
                    else
                        stString = "select ISNULL(sum(\"U_S_PNoofDays\"),0)  from \"" + objCom.DBName + "\".dbo.\"@TTP_EMOLT\" where \"Code\"=\"Name\" and \"U_S_PTrnsCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and  \"U_S_PYear\"= " + intyear + " and \"U_S_PEMPID\"='" + strRefCode + "'";// group by U_Z_EmpID"
                    oTst.DoQuery(stString);
                    dblUnPostedTrns1 = oTst.Fields.Item(0).Value;

                    if (objCom.ISHANA == "HANA")
                        stString = "select IFNULL(sum(\"U_S_PNoofDays\"),0),sum(\"U_S_PRedim\") AS \"Transaction\",sum(\"U_S_PAdjustment\") AS \"Adjustment\",\"U_S_PEmpID\"  from \"" + objCom.DBName + "\".\"@TTP_LEAVD\" where \"U_S_PLeaveCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and \"U_S_PYear\"=" + intyear + " and \"U_S_PEmpID\"='" + strRefCode + "' group by \"U_S_PEmpID\"";
                    else
                        stString = "select ISNULL(sum(\"U_S_PNoofDays\"),0),sum(\"U_S_PRedim\") AS \"Transaction\",sum(\"U_S_PAdjustment\") AS \"Adjustment\",\"U_S_PEmpID\"  from \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVD\" where \"U_S_PLeaveCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and \"U_S_PYear\"=" + intyear + " and \"U_S_PEmpID\"='" + strRefCode + "' group by \"U_S_PEmpID\"";
                    oTst.DoQuery(stString);

                    dblAccurred = oTst.Fields.Item(0).Value;
                    dblTransaction = dblUnPostedTrns1; // oTst.Fields.Item(1).Value
                    dblAdjustment = oTst.Fields.Item(2).Value;

                    if (objCom.ISHANA == "HANA")
                        stString = "select IFNULL(sum(\"U_NoofDays\"),0) from \"" + objCom.DBName + "\".\"@TTP_ESSLT\" where \"U_TrnsCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and \"U_Year\"=" + intyear + " and \"U_EMPID\"='" + strRefCode + "' and \"U_Status\"='P' group by \"U_EMPID\"";
                    else
                        stString = "select ISNULL(sum(\"U_NoofDays\"),0) from \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" where \"U_TrnsCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and \"U_Year\"=" + intyear + " and \"U_EMPID\"='" + strRefCode + "' and \"U_Status\"='P' group by \"U_EMPID\"";
                    oTst.DoQuery(stString);
                    dblPendingApproval = oTst.Fields.Item(0).Value;

                    if (objCom.ISHANA == "HANA")
                        oTst.DoQuery("Select IFNULL(\"U_S_PAccured\",'N') from \"" + objCom.DBName + "\".\"@TTP_LEAVH\" where \"U_S_PCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "'");
                    else
                        oTst.DoQuery("Select ISNULL(\"U_S_PAccured\",'N') from \"" + objCom.DBName + "\".dbo.\"@TTP_LEAVH\" where \"U_S_PCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "'");
                    bool blnCAFW = false;
                    if (oTst.Fields.Item(0).Value == "Y")
                        blnCAFW = true;
                    if (objCom.ISHANA == "HANA")
                        strQuery = "Select * from \"" + objCom.DBName + "\".\"@TTP_ELEVB\" where \"U_S_PLeaveCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and \"U_S_PEmpID\"='" + strRefCode + "'  and \"U_S_PYear\"=" + intyear;
                    else
                        strQuery = "Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_ELEVB\" where \"U_S_PLeaveCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and \"U_S_PEmpID\"='" + strRefCode + "'  and \"U_S_PYear\"=" + intyear;
                    oTst.DoQuery(strQuery);
                    double dblFinalBalance, dblOB, dblClosing;

                    if (oTst.RecordCount > 0)
                    {
                        if (objCom.ISHANA == "HANA")
                            strQuery = "Select IFNULL(\"U_S_PCAFWD\",0) \"U_S_PCAFWD\",IFNULL(\"U_S_PEntile\",0) \"Yearly\",\"Code\",IFNULL(\"U_S_POB\",0) \"OB\" from \"" + objCom.DBName + "\".\"@TTP_ELEVB\" where \"U_S_PLeaveCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and \"U_S_PEmpID\"='" + strRefCode + "'  and \"U_S_PYear\"=" + intyear;
                        else
                            strQuery = "Select ISNULL(\"U_S_PCAFWD\",0) \"U_S_PCAFWD\",ISNULL(\"U_S_PEntile\",0) \"Yearly\",\"Code\",ISNULL(\"U_S_POB\",0) \"OB\" from \"" + objCom.DBName + "\".dbo.\"@TTP_ELEVB\" where \"U_S_PLeaveCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and \"U_S_PEmpID\"='" + strRefCode + "'  and \"U_S_PYear\"=" + intyear;
                        oTst.DoQuery(strQuery);
                        string strcode1 = oTst.Fields.Item("Code").Value;
                        dblYearly = oTst.Fields.Item("Yearly").Value;
                        dblOB = oTst.Fields.Item("OB").Value;
                        // new addition 2014-01-16
                        if (blnCAFW == false)
                            dblClosing = dblYearly;
                        else
                            dblClosing = 0;
                        // end
                        dblCarriedForward = oTst.Fields.Item("U_S_PCAFWD").Value;
                        dblFinalBalance = dblClosing + dblOB + dblCarriedForward + dblAccurred - dblTransaction + dblAdjustment - dblPendingApproval; // - dblUnPostedTrns1
                        if (objCom.ISHANA == "HANA")
                            strQuery = "Update \"" + objCom.DBName + "\".\"@TTP_ELEVB\" set \"U_S_PendingApp\"='" + dblPendingApproval + "', \"U_S_POB\"='" + dblOB + "', \"U_S_PLeaveName\"='" + strLeaveName + "', \"U_S_PCAFWD\"='" + dblCarriedForward + "',  \"U_S_PACCR\"='" + dblAccurred + "', \"U_S_PAdjustment\"='" + dblAdjustment + "',\"U_S_PTrans\"='" + dblTransaction + "',\"U_S_PBalance\"='" + dblFinalBalance + "' where \"Code\"='" + strcode1 + "'"; // U_Z_LeaveCode='" & otemp2.Fields.Item("U_Z_LeaveCode").Value & "' and U_Z_Year=" & ayear
                        else
                            strQuery = "Update \"" + objCom.DBName + "\".dbo.\"@TTP_ELEVB\" set \"U_S_PendingApp\"='" + dblPendingApproval + "', \"U_S_POB\"='" + dblOB + "', \"U_S_PLeaveName\"='" + strLeaveName + "', \"U_S_PCAFWD\"='" + dblCarriedForward + "',  \"U_S_PACCR\"='" + dblAccurred + "', \"U_S_PAdjustment\"='" + dblAdjustment + "',\"U_S_PTrans\"='" + dblTransaction + "',\"U_S_PBalance\"='" + dblFinalBalance + "' where \"Code\"='" + strcode1 + "'";// U_Z_LeaveCode='" & otemp2.Fields.Item("U_Z_LeaveCode").Value & "' and U_Z_Year=" & ayear
                        oTst.DoQuery(strQuery);
                    }
                    else
                    {
                        if (objCom.ISHANA == "HANA")
                            strQuery = "Select IFNULL(\"U_S_POB\",0) \"OB\", IFNULL(\"U_S_PBalance\",0) \"U_S_PCAFWD\",IFNULL(\"U_S_PEntile\",0) \"Yearly\" from \"" + objCom.DBName + "\".\"@TTP_ELEVB\" where \"U_S_PLeaveCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and \"U_S_PEmpID\"='" + strRefCode + "'  and \"U_S_PYear\"=" + intyear - 1;
                        else
                            strQuery = "Select ISNULL(\"U_S_POB\",0) \"OB\", ISNULL(\"U_S_PBalance\",0) \"U_S_PCAFWD\",ISNULL(\"U_S_PEntile\",0) \"Yearly\" from \"" + objCom.DBName + "\".dbo.\"@TTP_ELEVB\" where \"U_S_PLeaveCode\"='" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "' and \"U_S_PEmpID\"='" + strRefCode + "'  and \"U_S_PYear\"=" + intyear - 1;
                        oTst.DoQuery(strQuery);
                        dblOB = oTst.Fields.Item("OB").Value;
                        dblCarriedForward = oTst.Fields.Item("U_S_PCAFWD").Value;
                        // new addition 2014-01-16
                        if (blnCAFW == false)
                            dblClosing = dblYearly;
                        else
                            dblClosing = 0;
                        // end
                        dblFinalBalance = dblClosing + dblOB + dblCarriedForward + dblAccurred - dblTransaction + dblAdjustment - dblPendingApproval; // - dblUnPostedTrns1
                        string strCode1 = objCom.Getmaxcode("@TTP_ELEVB", "Code");
                        if (objCom.ISHANA == "HANA")
                            strQuery = "Insert into \"" + objCom.DBName + "\".\"@TTP_ELEVB\" (\"Code\",\"Name\",\"U_S_PEmpID\",\"U_S_PYear\",\"U_S_PCAFWD\",\"U_S_PLeaveCode\",\"U_S_PLeaveName\") values('" + strCode1 + "','" + strCode1 + "','" + strRefCode + "'," + intyear + ",'" + dblCarriedForward + "','" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "','" + strLeaveName + "')";
                        else
                            strQuery = "Insert into \"" + objCom.DBName + "\".dbo.\"@TTP_ELEVB\" (\"Code\",\"Name\",\"U_S_PEmpID\",\"U_S_PYear\",\"U_S_PCAFWD\",\"U_S_PLeaveCode\",\"U_S_PLeaveName\") values('" + strCode1 + "','" + strCode1 + "','" + strRefCode + "'," + intyear + ",'" + dblCarriedForward + "','" + otemp1.Fields.Item("U_S_PLeaveCode").Value + "','" + strLeaveName + "')";
                        oTst.DoQuery(strQuery);
                        if (objCom.ISHANA == "HANA")
                            strQuery = "Update \"" + objCom.DBName + "\".\"@TTP_ELEVB\" set \"U_S_PendingApp\"='" + dblPendingApproval + "', \"U_S_POB\"='" + dblOB + "', \"U_S_PEntile\"='" + dblYearly + "', \"U_S_PCAFWD\"='" + dblCarriedForward + "',  \"U_S_PACCR\"='" + dblAccurred + "', \"U_S_PAdjustment\"='" + dblAdjustment + "',\"U_S_PTrans\"='" + dblTransaction + "',\"U_S_PBalance\"='" + dblFinalBalance + "' where  \"Code\"='" + strCode1 + "'";
                        else
                            strQuery = "Update \"" + objCom.DBName + "\".dbo.\"@TTP_ELEVB\" set \"U_S_PendingApp\"='" + dblPendingApproval + "', \"U_S_POB\"='" + dblOB + "', \"U_S_PEntile\"='" + dblYearly + "', \"U_S_PCAFWD\"='" + dblCarriedForward + "',  \"U_S_PACCR\"='" + dblAccurred + "', \"U_S_PAdjustment\"='" + dblAdjustment + "',\"U_S_PTrans\"='" + dblTransaction + "',\"U_S_PBalance\"='" + dblFinalBalance + "' where  \"Code\"='" + strCode1 + "'";
                        oTst.DoQuery(strQuery);
                    }
                    otemp1.MoveNext();
                }
            }
        }
        public bool GetFinalStatus(ApprovalEntities objEN)
        {
            SAPbobsCOM.Recordset oRecordSet, oTemp;
            oRecordSet = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oTemp = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            try
            {
                if (objEN.AppStatus == "A")
                {
                    switch (objEN.HeaderType)
                    {
                        case "LveReq":
                            {
                                if (objCom.ISHANA == "HANA")
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "' and  \"U_AFinal\" = 'Y' and T3.\"U_LveType\"='" + objEN.LeaveCode + "' and T3.\"U_DocType\"='" + objEN.HeaderType + "'";
                                    SqlQuery += " And (T2.\"U_AUser\" = '" + objEN.UserCode + "' OR T2.\"U_AUser1\" = '" + objEN.UserCode + "') ";
                                }
                                else
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "' and  \"U_AFinal\" = 'Y' and T3.\"U_LveType\"='" + objEN.LeaveCode + "' and T3.\"U_DocType\"='" + objEN.HeaderType + "'";
                                    SqlQuery += " And (T2.\"U_AUser\" = '" + objEN.UserCode + "' OR T2.\"U_AUser1\" = '" + objEN.UserCode + "') ";
                                }

                                break;
                            }

                        case "Exp":
                        case "Train":
                        case "Travel":
                        case "Air":
                        case "Time":
                        case "RBAT":
                        case "PerApr":
                        case "BGOD":
                        case "Appr":
                            {
                                if (objCom.ISHANA == "HANA")
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "' and  \"U_AFinal\" = 'Y' and T3.\"U_DocType\"='" + objEN.HeaderType + "'";
                                    SqlQuery += " And (T2.\"U_AUser\" = '" + objEN.UserCode + "' OR T2.\"U_AUser1\" = '" + objEN.UserCode + "') ";
                                }
                                else
                                {
                                    SqlQuery = " Select T2.\"DocEntry\" ";
                                    SqlQuery += " From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" T2 ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTMP\" T3 ON T2.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " JOIN \"" + objCom.DBName + "\".dbo.\"@TTP_APTM1\" T4 ON T4.\"DocEntry\" = T3.\"DocEntry\"  ";
                                    SqlQuery += " Where T4.\"U_OUser\"='" + objEN.EmpId + "' and  \"U_AFinal\" = 'Y' and T3.\"U_DocType\"='" + objEN.HeaderType + "'";
                                    SqlQuery += " And (T2.\"U_AUser\" = '" + objEN.UserCode + "' OR T2.\"U_AUser1\" = '" + objEN.UserCode + "') ";
                                }

                                break;
                            }
                    }
                }
                oRecordSet.DoQuery(SqlQuery);
                if (oRecordSet.RecordCount > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
                return false;
            }
        }
        public void SendMessage(ApprovalEntities objEN)
        {
            try
            {
                string strQuery;
                string strEmailMessage, mailType;
                string strMessageUser, strMessageUser1, CurApprover1, CurApprover;
                int intLineID;
                SAPbobsCOM.Recordset oRecordSet, oTemp, oRec;
                oRecordSet = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRec = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oTemp = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (objCom.ISHANA == "HANA")
                    strQuery = "Select \"LineId\",\"U_AUser1\",\"U_AUser\" From \"" + objCom.DBName + "\".\"@TTP_APTM2\" Where \"DocEntry\" = '" + objEN.HeadDocEntry + "' And (\"U_AUser\" = '" + objEN.UserCode + "' OR (\"U_AUser1\"='" + objEN.UserCode + "' AND NOW() BETWEEN \"U_ValidFrom\" AND \"U_ValidTo\"))";
                else
                    strQuery = "Select \"LineId\",\"U_AUser1\",\"U_AUser\" From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" Where \"DocEntry\" = '" + objEN.HeadDocEntry + "' And (\"U_AUser\" = '" + objEN.UserCode + "' OR (\"U_AUser1\"='" + objEN.UserCode + "' AND GETDATE() BETWEEN \"U_ValidFrom\" AND \"U_ValidTo\"))";
                oRec.DoQuery(strQuery);
                if (!oRec.EoF)
                {
                    intLineID = System.Convert.ToInt32(oRec.Fields.Item(0).Value);
                    CurApprover1 = oRec.Fields.Item(1).Value;
                    CurApprover = oRec.Fields.Item(2).Value;
                    if (objCom.ISHANA == "HANA")
                        strQuery = "Select Top 1 \"U_AUser\",\"U_AUser1\" From \"" + objCom.DBName + "\".\"@TTP_APTM2\" Where  \"DocEntry\" = '" + objEN.HeadDocEntry + "' And \"LineId\" > '" + intLineID.ToString() + "' and IFNULL(\"U_AMan\",'')='Y'  Order By \"LineId\" Asc ";
                    else
                        strQuery = "Select Top 1 \"U_AUser\",\"U_AUser1\" From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" Where  \"DocEntry\" = '" + objEN.HeadDocEntry + "' And \"LineId\" > '" + intLineID.ToString() + "' and isnull(\"U_AMan\",'')='Y'  Order By \"LineId\" Asc ";
                    oRecordSet.DoQuery(strQuery);
                    if (!oRecordSet.EoF)
                    {
                        strMessageUser = oRecordSet.Fields.Item(0).Value;
                        strMessageUser1 = oRecordSet.Fields.Item(1).Value;

                        string strMessage = "";
                        switch (objEN.HistoryType)
                        {
                            case "LveReq":
                                {
                                    if (objCom.ISHANA == "HANA")
                                        strQuery = "Select * from  \"" + objCom.DBName + "\".\"@TTP_ESSLT\" where \"Code\"='" + objEN.DocEntry + "'";
                                    else
                                        strQuery = "Select * from  \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" where \"Code\"='" + objEN.DocEntry + "'";
                                    oTemp.DoQuery(strQuery);
                                    strMessage = "Approval Required on Leave Request " + objEN.DocEntry + " Requested by  " + oTemp.Fields.Item("U_EMPNAME").Value + " Leave Name is " + oTemp.Fields.Item("U_LeaveName").Value;
                                    break;
                                }
                        }

                        switch (objEN.HistoryType)
                        {
                            case "LveReq" // Leave Request"
                           :
                                {
                                    if (objCom.ISHANA == "HANA")
                                        strQuery = "Update \"" + objCom.DBName + "\".\"@TTP_ESSLT\" set \"U_CurApprover\"='" + CurApprover.Trim() + "',\"U_NxtApprover\"='" + strMessageUser + "',\"U_CurApprover1\"='" + CurApprover1.Trim() + "',\"U_NxtApprover1\"='" + strMessageUser1 + "' where \"Code\"='" + objEN.DocEntry + "'";
                                    else
                                        strQuery = "Update \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" set \"U_CurApprover\"='" + CurApprover.Trim() + "',\"U_NxtApprover\"='" + strMessageUser + "',\"U_CurApprover1\"='" + CurApprover1.Trim() + "',\"U_NxtApprover1\"='" + strMessageUser1 + "' where \"Code\"='" + objEN.DocEntry + "'";
                                    break;
                                }
                        }
                        oTemp.DoQuery(strQuery);

                        // Dim IntDoc As Integer = Integer.Parse(objEN.DocEntry)
                        // objEN.DocEntry = IntDoc.ToString()

                        SAPbobsCOM.CompanyService oCmpSrv;
                        SAPbobsCOM.MessagesService oMessageService;
                        SAPbobsCOM.Message oMessage;
                        SAPbobsCOM.MessageDataColumns pMessageDataColumns;
                        SAPbobsCOM.MessageDataColumn pMessageDataColumn;
                        SAPbobsCOM.MessageDataLines oLines;
                        SAPbobsCOM.MessageDataLine oLine;
                        SAPbobsCOM.RecipientCollection oRecipientCollection;
                        // If objDA.ConnectSAP() = True Then
                        oCmpSrv = objEN.SapCompany.GetCompanyService();
                        oMessageService = oCmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.MessagesService);
                        oMessage = oMessageService.GetDataInterface(SAPbobsCOM.MessagesServiceDataInterfaces.msdiMessage);
                        string strusermsg = objEN.DocMessage + " " + objEN.DocEntry + strMessage + " is awaiting your approval ";
                        oMessage.Subject = objEN.DocMessage + ":" + " is awaiting your approval ";
                        oMessage.Text = objEN.DocMessage + " " + objEN.DocEntry + strMessage + " is awaiting your approval ";
                        oRecipientCollection = oMessage.RecipientCollection;
                        oRecipientCollection.Add();
                        oRecipientCollection.Item(0).SendInternal = SAPbobsCOM.BoYesNoEnum.tYES;
                        oRecipientCollection.Item(0).UserCode = strMessageUser;
                        pMessageDataColumns = oMessage.MessageDataColumns;
                        pMessageDataColumn = pMessageDataColumns.Add();
                        pMessageDataColumn.ColumnName = "Request No";
                        oLines = pMessageDataColumn.MessageDataLines;
                        oLine = oLines.Add();
                        oLine.Value = objEN.DocEntry;
                        if (objEN.HistoryType == "Time")
                            oMessageService.SendMessage(oMessage);
                        else
                            oMessageService.SendMessage(oMessage);

                        if (strMessageUser1.Trim() != "" | strMessageUser1.Trim() != null)
                        {
                            string subject = objEN.DocMessage + ":" + " is awaiting your approval ";
                            SendAlternateUsermsg(strMessageUser1, objEN.DocEntry, strusermsg, objEN.SapCompany, subject);
                        }

                        strEmailMessage = objEN.DocMessage + "  " + objEN.DocEntry + " " + strMessage + " is awaiting your approval ";
                        mailType = strEmailMessage;
                        if (objEN.HistoryType == "Air" | objEN.HistoryType == "LveReq")
                        {
                            strEmailMessage = strMessage + " is awaiting your approval ";
                            mailType = objEN.DocMessage;
                        }
                        if (objEN.HistoryType == "Time")
                            SendMail_Approval(strEmailMessage, strMessageUser1, strMessageUser, objEN.SapCompany, mailType, objEN.DocEntry, objEN.HistoryType);
                        else
                            SendMail_Approval(strEmailMessage, strMessageUser1, strMessageUser, objEN.SapCompany, mailType, objEN.DocEntry, objEN.HistoryType);
                    }
                }
            }
            // End If
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }
        public void SendAlternateUsermsg(string strMessageUser1, string strExpReqNo, string UserMessage, SAPbobsCOM.Company objMainCompany, string subject)
        {
            SAPbobsCOM.CompanyService oCmpSrv;
            SAPbobsCOM.MessagesService oMessageService;
            SAPbobsCOM.Message oMessage;
            SAPbobsCOM.MessageDataColumns pMessageDataColumns;
            SAPbobsCOM.MessageDataColumn pMessageDataColumn;
            SAPbobsCOM.MessageDataLines oLines;
            SAPbobsCOM.MessageDataLine oLine;
            SAPbobsCOM.RecipientCollection oRecipientCollection;
            oCmpSrv = objMainCompany.GetCompanyService();
            oMessageService = oCmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.MessagesService);
            oMessage = oMessageService.GetDataInterface(SAPbobsCOM.MessagesServiceDataInterfaces.msdiMessage);
            if (strMessageUser1.Trim() != "" | strMessageUser1.Trim() != null)
            {
                oMessage.Subject = subject;
                oMessage.Text = UserMessage;
                oRecipientCollection = oMessage.RecipientCollection;
                oRecipientCollection.Add();
                oRecipientCollection.Item(0).SendInternal = SAPbobsCOM.BoYesNoEnum.tYES;
                oRecipientCollection.Item(0).UserCode = strMessageUser1;
                pMessageDataColumns = oMessage.MessageDataColumns;

                pMessageDataColumn = pMessageDataColumns.Add();
                pMessageDataColumn.ColumnName = "Request No";
                oLines = pMessageDataColumn.MessageDataLines;
                oLine = oLines.Add();
                oLine.Value = strExpReqNo;
                oMessageService.SendMessage(oMessage);
            }
        }
        public void SendMail_Approval(string aMessage, string aUser1, string aUser, SAPbobsCOM.Company aCompany, string aChoce, string Empid = "", string DocEntry = "")
        {
            string aMail = "";
            string aMail1 = "";
            try
            {
                SAPbobsCOM.Recordset oRecordset, oTemp;
                oRecordset = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oTemp = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (aChoce == "LveReq")
                {
                    if (objCom.ISHANA == "HANA")
                        SqlQuery = "Select IFNULL(\"E_Mail\",'0') AS \"Email\" from \"" + objCom.DBName + "\".OUSR where \"USER_CODE\"='" + aUser + "'";
                    else
                        SqlQuery = "Select ISNULL(\"E_Mail\",'0') AS \"Email\" from \"" + objCom.DBName + "\".dbo.OUSR where \"USER_CODE\"='" + aUser + "'";
                    aMail = objCom.ReturnValue(SqlQuery);
                    if (aUser1 != "")
                    {
                        if (objCom.ISHANA == "HANA")
                            SqlQuery = "Select IFNULL(\"E_Mail\",'0') AS \"Email\" from \"" + objCom.DBName + "\".OUSR where \"USER_CODE\"='" + aUser1 + "'";
                        else
                            SqlQuery = "Select ISNULL(\"E_Mail\",'0') AS \"Email\" from \"" + objCom.DBName + "\".dbo.OUSR where \"USER_CODE\"='" + aUser1 + "'";
                        aMail1 = objCom.ReturnValue(SqlQuery);
                    }
                    if (aMail1.Trim() != "")
                    {
                        aMail = aMail + "," + aMail1;
                    }
                }

                if (aMail != "")
                {

                    SendMailforApproval(aMail, aMessage, aCompany, aChoce, DocEntry, aUser);
                }
            }

            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }
        public void SendMailforApproval(string aMail, string Message, SAPbobsCOM.Company aCompany, string Apptype, string DocEntry, string aUser = "")
        {
            SAPbobsCOM.Recordset oRecordset;
            oRecordset = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string mailServer, mailPort, mailId, mailpwd;
            bool mailSSL;
            try
            {
                if (objCom.ISHANA == "HANA")
                    oRecordset.DoQuery("Select \"U_SMTPSERV\",\"U_SMTPPORT\",\"U_SMTPUSER\",\"U_SMTPPWD\",\"U_SSL\" From \"" + objCom.DBName + "\".\"@TTP_EMAIL\"");
                else
                    oRecordset.DoQuery("Select \"U_SMTPSERV\",\"U_SMTPPORT\",\"U_SMTPUSER\",\"U_SMTPPWD\",\"U_SSL\" From \"" + objCom.DBName + "\".dbo.\"@TTP_EMAIL\"");
                if (!oRecordset.EoF)
                {
                    mailServer = oRecordset.Fields.Item("U_SMTPSERV").Value;
                    mailPort = oRecordset.Fields.Item("U_SMTPPORT").Value;
                    mailId = oRecordset.Fields.Item("U_SMTPUSER").Value;
                    mailpwd = oRecordset.Fields.Item("U_SMTPPWD").Value;
                    mailSSL = Convert.ToBoolean(oRecordset.Fields.Item("U_SSL").Value);
                    if (mailServer != "" & mailId != "" & mailpwd != "")
                    {
                        SmtpServer.Credentials = new System.Net.NetworkCredential(mailId, mailpwd);
                        SmtpServer.Port = Convert.ToInt32(mailPort);
                        SmtpServer.EnableSsl = mailSSL;
                        SmtpServer.Host = mailServer;
                        mail = new MailMessage();
                        mail.From = new MailAddress(mailId, "TTP_HRMS");
                        mail.To.Add(aMail);
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.High;
                        mail.Subject = Message;
                        if (Apptype == "LveReq")
                            mail.Body = BuildHtmBodyLeave(DocEntry, aUser, aCompany, Message, aUser);
                        else
                            mail.Body = Message;
                        DataSet Attds = new DataSet();
                        if (Apptype == "LveReq")
                        {
                            try
                            {
                                if (objCom.ISHANA == "HANA")
                                    SqlQuery = "select IFNULL(\"U_S_PAttach\",'') AS \"U_Attachement\"  from \"" + objCom.DBName + "\".\"@TTP_ESSLT\" WHERE \"Code\"=" + DocEntry.Trim();
                                else
                                    SqlQuery = "select ISNULL(\"U_S_PAttach\",'') AS \"U_Attachement\"  from \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" WHERE \"Code\"=" + DocEntry.Trim();
                                Attds = objCom.ReturnDataset(SqlQuery);
                                if (Attds.Tables[0].Rows.Count > 0)
                                {
                                    for (int intRow = 0; intRow <= Attds.Tables[0].Rows.Count - 1; intRow++)
                                    {
                                        if (Attds.Tables[0].Rows[intRow]["U_Attachement"].ToString() != "")
                                        {
                                            Attachment attachment;
                                            attachment = new Attachment(Attds.Tables[0].Rows[intRow]["U_Attachement"].ToString());
                                            mail.Attachments.Add(attachment);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                CommonFunctions.WriteError(ex.Message);
                            }
                        }
                        SmtpServer.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
            finally
            {
                mail.Dispose();
            }
        }
        public string BuildHtmBodyLeave(string DocEntry, string Name, SAPbobsCOM.Company aCompany, string strMessage = "", string aEmpId = "")
        {
            string oHTML = string.Empty, strPath;
            SAPbobsCOM.Recordset oRecordSet, oPDA;
            string empName = string.Empty;
            oRecordSet = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oPDA = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            if (objCom.ISHANA == "HANA")
            {
                SqlQuery = "SELECT \"Code\", \"U_EMPID\", \"U_EMPNAME\", \"U_TransType\",\"U_FromTime\",\"U_ToTime\", \"U_TrnsCode\", \"U_LeaveName\",TO_NVARCHAR(TO_DATE(\"U_StartDate\"), 'DD/MM/YYYY')  AS \"U_StartDate\",";
                SqlQuery += " TO_NVARCHAR(TO_DATE(\"U_EndDate\"), 'DD/MM/YYYY')  AS \"U_EndDate\", \"U_NoofDays\", \"U_Notes\", \"U_LevBal\",CASE \"U_Status\" WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' else 'Pending' END AS \"U_Status\", \"U_S_PEmAdd\", \"U_S_PEConNo\",";
                SqlQuery += " \"U_S_PAttach\", \"U_RequestBy\", \"U_RequestByName\",\"U_RplEmpName\" FROM \"" + objCom.DBName + "\".\"@TTP_ESSLT\"";
                SqlQuery += " where \"Code\"='" + DocEntry.Trim() + "'";
            }
            else
            {
                SqlQuery = "SELECT \"Code\", \"U_EMPID\", \"U_EMPNAME\", \"U_TransType\", \"U_TrnsCode\",\"U_FromTime\",\"U_ToTime\", \"U_LeaveName\",Convert(Varchar(10),\"U_StartDate\",103) AS \"U_StartDate\",";
                SqlQuery += " Convert(Varchar(10),\"U_EndDate\",103) AS \"U_EndDate\", \"U_NoofDays\", \"U_Notes\", \"U_LevBal\",CASE \"U_Status\" WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' else 'Pending' END AS \"U_Status\", \"U_S_PEmAdd\", \"U_S_PEConNo\",";
                SqlQuery += " \"U_S_PAttach\", \"U_RequestBy\", \"U_RequestByName\",\"U_RplEmpName\" FROM \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\"";
                SqlQuery += " where \"Code\"='" + DocEntry.Trim() + "'";
            }
            oRecordSet.DoQuery(SqlQuery);
            if (oRecordSet.Fields.Item("U_TransType").Value == "L")
            {
                strPath = System.Web.HttpContext.Current.Server.MapPath("~/Email/Leave.html");
                oHTML = GetFileContents(strPath);
                if (oRecordSet.Fields.Item("U_EMPNAME").Value != null)
                    oHTML = oHTML.Replace("$$Messages$$", oRecordSet.Fields.Item("U_EMPNAME").Value);
                else
                    oHTML = oHTML.Replace("$$Messages$$", "");
                if (aEmpId == "Rpl")
                    oHTML = oHTML.Replace("$$Approval$$", "has assigned you as a replacement.");
                else
                    oHTML = oHTML.Replace("$$Approval$$", "Kindly log in approval.");
            }
            else if (oRecordSet.Fields.Item("U_TransType").Value == "P")
            {
                strPath = System.Web.HttpContext.Current.Server.MapPath("Permission.html");
                oHTML = GetFileContents(strPath);

                if (strMessage != string.Empty)
                    oHTML = oHTML.Replace("$$Messages$$", strMessage);
                else
                    oHTML = oHTML.Replace("$$Messages$$", "");
            }
            if (oRecordSet.Fields.Item("Code").Value != null)
                oHTML = oHTML.Replace("$$DocNo$$", oRecordSet.Fields.Item("Code").Value);
            else
                oHTML = oHTML.Replace("$$DocNo$$", "");

            if (oRecordSet.Fields.Item("U_EMPID").Value != null)
                oHTML = oHTML.Replace("$$EmpCode$$", oRecordSet.Fields.Item("U_EMPID").Value);
            else
                oHTML = oHTML.Replace("$$EmpCode$$", "");

            if (oRecordSet.Fields.Item("U_EMPNAME").Value != null)
                oHTML = oHTML.Replace("$$ReqEmpName1$$", oRecordSet.Fields.Item("U_EMPNAME").Value);
            else
                oHTML = oHTML.Replace("$$ReqEmpName1$$", "");

            if (oRecordSet.Fields.Item("U_LeaveName").Value != null)
                oHTML = oHTML.Replace("$$LeaveType$$", oRecordSet.Fields.Item("U_LeaveName").Value);
            else
                oHTML = oHTML.Replace("$$LeaveType$$", "");
            if (oRecordSet.Fields.Item("U_TransType").Value == "L")
            {
                if (oRecordSet.Fields.Item("U_LeaveName").Value != null)
                    oHTML = oHTML.Replace("$$LeaveType1$$", oRecordSet.Fields.Item("U_LeaveName").Value);
                else
                    oHTML = oHTML.Replace("$$LeaveType1$$", "");

                if (oRecordSet.Fields.Item("U_LevBal").Value != null)
                    oHTML = oHTML.Replace("$$LveBalance$$", oRecordSet.Fields.Item("U_LevBal").Value.ToString());
                else
                    oHTML = oHTML.Replace("$$LveBalance$$", "");
                if (oRecordSet.Fields.Item("U_EndDate").Value != null)
                {
                    oHTML = oHTML.Replace("$$Todate$$", oRecordSet.Fields.Item("U_EndDate").Value.ToString());
                    oHTML = oHTML.Replace("$$todate$$", oRecordSet.Fields.Item("U_EndDate").Value.ToString());
                }
                else
                {
                    oHTML = oHTML.Replace("$$Todate$$", "");
                    oHTML = oHTML.Replace("$$todate$$", "");
                }
                if (oRecordSet.Fields.Item("U_NoofDays").Value != null)
                {
                    oHTML = oHTML.Replace("$$Noofdays$$", oRecordSet.Fields.Item("U_NoofDays").Value.ToString());
                    oHTML = oHTML.Replace("$$days$$", oRecordSet.Fields.Item("U_NoofDays").Value.ToString());
                }
                else
                {
                    oHTML = oHTML.Replace("$$Noofdays$$", "");
                    oHTML = oHTML.Replace("$$days$$", "");
                }
                if (oRecordSet.Fields.Item("U_S_PEmAdd").Value != null)
                    oHTML = oHTML.Replace("$$ContAddress$$", oRecordSet.Fields.Item("U_S_PEmAdd").Value);
                else
                    oHTML = oHTML.Replace("$$ContAddress$$", "");
                if (oRecordSet.Fields.Item("U_S_PEConNo").Value != null)
                    oHTML = oHTML.Replace("$$PhoneNo$$", oRecordSet.Fields.Item("U_S_PEConNo").Value);
                else
                    oHTML = oHTML.Replace("$$PhoneNo$$", "");
                if (oRecordSet.Fields.Item("U_StartDate").Value != null)
                    oHTML = oHTML.Replace("$$fromdate$$", oRecordSet.Fields.Item("U_StartDate").Value);
                else
                    oHTML = oHTML.Replace("$$fromdate$$", "");
            }
            else if (oRecordSet.Fields.Item("U_TransType").Value == "P")
            {
                if (oRecordSet.Fields.Item("U_FromTime").Value != null)
                    oHTML = oHTML.Replace("$$FromTime$$", oRecordSet.Fields.Item("U_FromTime").Value);
                else
                    oHTML = oHTML.Replace("$$FromTime$$", "");
                if (oRecordSet.Fields.Item("U_ToTime").Value != null)
                    oHTML = oHTML.Replace("$$ToTime$$", oRecordSet.Fields.Item("U_ToTime").Value);
                else
                    oHTML = oHTML.Replace("$$ToTime$$", "");
                if (oRecordSet.Fields.Item("U_LevBal").Value != null)
                    oHTML = oHTML.Replace("$$TotalTime$$", oRecordSet.Fields.Item("U_LevBal").Value);
                else
                    oHTML = oHTML.Replace("$$TotalTime$$", "");
            }

            if (oRecordSet.Fields.Item("U_StartDate").Value != null)
            {
                oHTML = oHTML.Replace("$$Fromdt$$", oRecordSet.Fields.Item("U_StartDate").Value.ToString());
                oHTML = oHTML.Replace("$$fromdate$$", oRecordSet.Fields.Item("U_StartDate").Value.ToString());
            }
            else
            {
                oHTML = oHTML.Replace("$$Fromdt$$", "");
                oHTML = oHTML.Replace("$$fromdate$$", "");
            }
            if (oRecordSet.Fields.Item("U_Notes").Value != null)
                oHTML = oHTML.Replace("$$Reason$$", oRecordSet.Fields.Item("U_Notes").Value);
            else
                oHTML = oHTML.Replace("$$Reason$$", "");

            if (oRecordSet.Fields.Item("U_Status").Value != null)
                oHTML = oHTML.Replace("$$status$$", oRecordSet.Fields.Item("U_Status").Value);
            else
                oHTML = oHTML.Replace("$$status$$", "");
            if (aEmpId != "Rpl")
            {
                if (aEmpId == "")
                {
                    if (empName != string.Empty)
                        oHTML = oHTML.Replace("$$ReqEmpName$$", empName);
                    else
                        oHTML = oHTML.Replace("$$ReqEmpName$$", "");
                }
                else if (aEmpId != string.Empty)
                    oHTML = oHTML.Replace("$$ReqEmpName$$", aEmpId);
                else
                    oHTML = oHTML.Replace("$$ReqEmpName$$", "");
            }
            else if (empName != string.Empty)
                oHTML = oHTML.Replace("$$ReqEmpName$$", empName);
            else
                oHTML = oHTML.Replace("$$ReqEmpName$$", "");


            if (aEmpId == "" | aEmpId == "Rpl")
            {
                if (objCom.ISHANA == "HANA")
                    SqlQuery = " select ifnull(\"firstName\",'') ||' '|| ifnull(\"lastName\",'') AS \"EmpName\" from \"" + objCom.DBName + "\".OHEM T0 JOIN \"" + objCom.DBName + "\".OUSR T1 On T0.\"userId\"=T1.\"USERID\" where T1.\"USER_CODE\"='" + Name + "'";
                else
                    SqlQuery = " select isnull(firstName,'') +' '+ isnull(lastName,'') AS EmpName from \"" + objCom.DBName + "\".dbo.OHEM T0 JOIN \"" + objCom.DBName + "\".dbo.OUSR T1 On T0.userId=T1.USERID where T1.USER_CODE='" + Name + "'";
                oPDA.DoQuery(SqlQuery);
                if (!oPDA.EoF)
                    Name = oPDA.Fields.Item("EmpName").Value;
            }
            else
                Name = Name;
            if (aEmpId == "Rpl")
            {
                if (oRecordSet.Fields.Item("U_RplEmpName").Value != null)
                    oHTML = oHTML.Replace("$$EmpName$$", oRecordSet.Fields.Item("U_RplEmpName").Value);
                else
                    oHTML = oHTML.Replace("$$EmpName$$", "");
            }
            else if (Name != string.Empty)
                oHTML = oHTML.Replace("$$EmpName$$", Name);
            else
                oHTML = oHTML.Replace("$$EmpName$$", "");
            return oHTML;
        }
        public string GetFileContents(string FullPath)
        {
            string strContents = "";
            StreamReader objReader;
            try
            {
                objReader = new StreamReader(FullPath);
                strContents = objReader.ReadToEnd();
                objReader.Close();
            }
            catch (Exception Ex)
            {
                CommonFunctions.WriteError(Ex.Message);
            }
            return strContents;
        }
        public void SendMail_RequestApproval(string aMessage, string Empid, SAPbobsCOM.Company aCompany, string aChoice, string DocEntry)
        {
            string aMail;
            SAPbobsCOM.Recordset oRecordset, oTest;
            oRecordset = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            if (objCom.ISHANA == "HANA")
                oRecordset.DoQuery("Select \"email\" from \"" + objCom.DBName + "\".OHEM where \"empID\"='" + Empid + "'");
            else
                oRecordset.DoQuery("Select \"email\" from \"" + objCom.DBName + "\".dbo.OHEM where \"empID\"='" + Empid + "'");
            aMail = oRecordset.Fields.Item("email").Value;
            if (aMail != "")
            {
                oTest = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (objCom.ISHANA == "HANA")
                    oTest.DoQuery("Select * from \"" + objCom.DBName + "\".\"@TTP_ESSWL\"");
                else
                    oTest.DoQuery("Select * from \"" + objCom.DBName + "\".dbo.[@TTP_ESSWL]");
                string strESSLink = "";
                if (oTest.RecordCount > 0)
                    strESSLink = oTest.Fields.Item("U_WebPath").Value;
                SendMailforApproval(aMail, aMessage, aCompany, aChoice, DocEntry);
            }
        }
        public void ApprovalHistory(string RefCode, string DocType, GridView Gv)
        {            
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = " SELECT \"DocEntry\", \"U_DocEntry\", \"U_DocType\", \"U_EmpId\", \"U_EmpName\", \"U_ApproveBy\", CAST(\"CreateDate\" AS varchar(10)) AS \"CreateDate\", LEFT(CAST(\"CreateTime\" AS varchar(5)), 2) || ':' ||";
                    SqlQuery += "  RIGHT(CAST(\"CreateTime\" AS varchar(30)), 2) AS \"CreateTime\", CAST(\"UpdateDate\" AS varchar(10)) AS \"UpdateDate\",LEFT(CAST(\"UpdateTime\" AS varchar(5)), 2) || ':' || RIGHT(CAST(\"UpdateTime\" AS varchar(30)), 2) AS \"UpdateTime\",CASE \"U_AppStatus\" WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' ELSE 'Pending' END AS \"U_AppStatus\", \"U_Remarks\",\"U_AltApproveBy\" FROM \"" + objCom.DBName + "\".\"@TTP_APHIS\" ";
                    SqlQuery += " Where \"U_DocType\" = '" + DocType.Trim() + "'";
                    SqlQuery += " And \"U_DocEntry\" = '" + RefCode.Trim() + "'";                   
                }
                else
                {
                    SqlQuery = " Select \"DocEntry\",\"U_DocEntry\",\"U_DocType\",\"U_EmpId\",\"U_EmpName\",\"U_ApproveBy\",Convert(Varchar(10),\"CreateDate\",103) AS \"CreateDate\",";
                    SqlQuery += " LEFT(CONVERT(VARCHAR(5), \"CreateTime\", 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), \"CreateTime\", 9),2) AS \"CreateTime\",Convert(Varchar(10),\"UpdateDate\",103) AS \"UpdateDate\",LEFT(CONVERT(VARCHAR(5), \"UpdateTime\", 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), \"UpdateTime\", 9),2) AS \"UpdateTime\",Case \"U_AppStatus\" when 'A' then 'Approved' when 'R' then 'Rejected' else 'Pending' end as \"U_AppStatus\",\"U_Remarks\",\"U_AltApproveBy\" From \"" + objCom.DBName + "\".dbo.\"@TTP_APHIS\" ";
                    SqlQuery += " Where \"U_DocType\" = '" + DocType.Trim() + "'";
                    SqlQuery += " And \"U_DocEntry\" = '" + RefCode.Trim() + "'";                   
                }
                objCom.BindGrid(SqlQuery, Gv);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);              
            }           
        }
        //Leave Cancel Start
        public void CancelLeave(string EmpId, string ReqCode, SAPbobsCOM.Company company, string UserName)
        {
            string intTempID;
            DataSet ds = new DataSet();
            try
            {
                if (objCom.ISHANA == "HANA")
                    SqlQuery = "Select * from \"" + objCom.DBName + "\".\"@TTP_ESSLT\" where \"Code\"='" + ReqCode + "' and  \"U_EMPID\"='" + EmpId + "'";
                else
                    SqlQuery = "Select * from \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" where \"Code\"='" + ReqCode + "' and  \"U_EMPID\"='" + EmpId + "'";
                ds = objCom.ReturnDataset(SqlQuery);

                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "Delete from \"" + objCom.DBName + "\".\"@TTP_EMOLT\" where \"U_S_ESS_RefNo\"='" + ReqCode + "' and  \"U_S_PEMPID\"='" + EmpId + "'";
                    objCom.ExecuteNonQuery(SqlQuery);
                }
                else
                {
                    SqlQuery = "Delete from \"" + objCom.DBName + "\".dbo.\"@TTP_EMOLT\" where \"U_S_ESS_RefNo\"='" + ReqCode + "' and  \"U_S_PEMPID\"='" + EmpId + "'";
                    objCom.ExecuteNonQuery(SqlQuery);
                }

                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "Delete from \"" + objCom.DBName + "\".\"@TTP_EMOLT\" where \"U_S_ESS_Ref\"='" + ReqCode + "' and  \"U_S_PEMPID\"='" + EmpId + "'";
                    objCom.ExecuteNonQuery(SqlQuery);
                }
                else
                {
                    SqlQuery = "Delete from \"" + objCom.DBName + "\".dbo.\"@TTP_EMOLT\" where \"U_S_ESS_Ref\"='" + ReqCode + "' and  \"U_S_PEMPID\"='" + EmpId + "'";
                    objCom.ExecuteNonQuery(SqlQuery);
                }
                if (ds.Tables[0].Rows.Count > 0)
                    UpdateLeaveBalance_Transaction(company, EmpId, ds.Tables[0].Rows[0]["U_TrnsCode"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[0]["U_Year"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["U_Month"].ToString()));

                intTempID = GetTemplateID("LveReq", EmpId, ds.Tables[0].Rows[0]["U_TrnsCode"].ToString());
                if (intTempID != "0")
                    SendCancelNotification("Leave Request Cancellation Notification", ReqCode, intTempID, company, UserName);
                if (objCom.ISHANA == "HANA")
                    SqlQuery = "Update \"" + objCom.DBName + "\".\"@TTP_ESSLT\" Set \"U_Canelled\" = 'Y' Where \"Code\" = '" + ReqCode + "'";
                else
                    SqlQuery = "Update \"" + objCom.DBName + "\".dbo.\"@TTP_ESSLT\" Set  \"U_Canelled\" = 'Y' Where \"Code\" = '" + ReqCode + "'";
                objCom.ExecuteNonQuery(SqlQuery);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }
        private void SendCancelNotification(string strReqType, string strReqNo, string strTemplateNo, SAPbobsCOM.Company objMainCompany, string UserName)
        {
            try
            {
                string strEmailMessage;
                string strMessageUser, strMessageUser1, strExpReqNo1;
                SAPbobsCOM.Recordset oRecordSet, oTemp;
                SAPbobsCOM.CompanyService oCmpSrv;
                SAPbobsCOM.MessagesService oMessageService;
                SAPbobsCOM.Message oMessage;
                SAPbobsCOM.MessageDataColumns pMessageDataColumns;
                SAPbobsCOM.MessageDataColumn pMessageDataColumn;
                SAPbobsCOM.MessageDataLines oLines;
                SAPbobsCOM.MessageDataLine oLine;
                SAPbobsCOM.RecipientCollection oRecipientCollection;
                oCmpSrv = objMainCompany.GetCompanyService();
                oMessageService = oCmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.MessagesService);
                oMessage = oMessageService.GetDataInterface(SAPbobsCOM.MessagesServiceDataInterfaces.msdiMessage);
                oRecordSet = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oTemp = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                if (objCom.ISHANA == "HANA")
                    SqlQuery = "Select Top 1 \"U_AUser\",\"U_AUser1\" From \"" + objCom.DBName + "\".\"@TTP_APTM2\" Where \"DocEntry\" = '" + strTemplateNo + "'  and IFNULL(\"U_AMan\",'')='Y' Order By \"LineId\" Asc ";
                else
                    SqlQuery = "Select Top 1 \"U_AUser\",\"U_AUser1\" From \"" + objCom.DBName + "\".dbo.\"@TTP_APTM2\" Where \"DocEntry\" = '" + strTemplateNo + "'  and isnull(\"U_AMan\",'')='Y' Order By \"LineId\" Asc ";
                oRecordSet.DoQuery(SqlQuery);
                if (!oRecordSet.EoF)
                {
                    strMessageUser = oRecordSet.Fields.Item(0).Value;
                    strMessageUser1 = oRecordSet.Fields.Item(1).Value;
                    oMessage.Subject = strReqType;
                    oMessage.Text = strReqType + " for the Request " + strReqNo + " Cancelled by " + UserName + "";
                    string strusermsg = strReqType + " for the Request " + strReqNo + " Cancelled by " + UserName + "";
                    oRecipientCollection = oMessage.RecipientCollection;
                    oRecipientCollection.Add();
                    oRecipientCollection.Item(0).SendInternal = SAPbobsCOM.BoYesNoEnum.tYES;
                    oRecipientCollection.Item(0).UserCode = strMessageUser;
                    pMessageDataColumns = oMessage.MessageDataColumns;

                    pMessageDataColumn = pMessageDataColumns.Add();
                    pMessageDataColumn.ColumnName = "Request No";
                    oLines = pMessageDataColumn.MessageDataLines;
                    oLine = oLines.Add();
                    oLine.Value = strReqNo;
                    oMessageService.SendMessage(oMessage);
                    if (strMessageUser1.Trim() != "" | strMessageUser1.Trim() != null)
                    {
                        string subject = strReqType;
                        SendAlternateUsermsg(strMessageUser1, strReqNo, strusermsg, objMainCompany, subject);
                    }
                    strEmailMessage = "Leave Request cancelled of the request " + strReqNo + " Cancelled by " + UserName + "";
                    SendMail_Approval(strEmailMessage, strMessageUser1, strMessageUser, objMainCompany, "Leave Request Cancelled Notification", "LveReq");
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }
        //Leave Cancel End
    }
}