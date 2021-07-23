using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TTP_HRMS.Models
{
    public class Profile
    {
        public string SqlQuery;
        CommonFunctions objCom = new CommonFunctions();
        public DataSet BindPersonelDetails(string Empid)
        {
            DataSet ds = new DataSet();
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "SELECT IFNULL(\"U_S_PEmpID\",'') as \"TAEmpID\",\"U_S_ThirdName\",\"picture\", \"bankCode\",\"bankAcount\",";
                    SqlQuery += " \"bankBranch\",\"empID\", \"firstName\", \"lastName\", \"middleName\", \"govID\",T0.\"position\",";
                    SqlQuery += " T1.\"descriptio\" AS \"Positionname\", \"dept\", T2.\"Remarks\" AS \"Deptname\",  T3.\"Name\" AS \"BranchName\", \"officeExt\",";
                    SqlQuery += " \"U_Rel_Name\", \"U_Rel_Type\", \"U_Rel_Phone\", \"officeTel\", \"mobile\", \"email\", \"fax\", \"homeTel\", \"pager\",";
                    SqlQuery += " \"sex\", TO_NVARCHAR(TO_DATE(\"birthDate\"), 'YYYY-MM-DD') AS \"birthDate\", \"brthCountr\", \"martStatus\", \"nChildren\", \"govID\",";
                    SqlQuery += " \"citizenshp\", TO_NVARCHAR(TO_DATE(\"passportEx\"), 'YYYY-MM-DD') AS \"passportEx\", \"passportNo\", \"workBlock\", \"workCity\",";
                    SqlQuery += " \"workCountr\", \"workState\", \"workCounty\", \"workStreet\", \"workZip\", \"homeBlock\", \"homeCity\", \"homeCountr\",";
                    SqlQuery += " \"homeCounty\", \"homeState\", \"homeStreet\", \"homeZip\", \"U_S_OrgstCode\", \"U_S_OrgstName\", \"WorkBuild\", \"HomeBuild\",";
                    SqlQuery += " \"U_LvlCode\", \"U_LvlName\", \"U_LocCode\", \"U_LocName\",  \"U_S_ApplId\", IFNULL(\"manager\", 0) AS \"Manager\",IFNULL(T0.\"U_S_PWORK\",'') AS \"U_S_CompName\"";
                    SqlQuery += " FROM \"" + objCom.DBName + "\".OHEM T0 LEFT OUTER JOIN \"" + objCom.DBName + "\".OHPS T1 ON T0.\"position\" = T1.\"posID\"";
                    SqlQuery += " LEFT OUTER JOIN \"" + objCom.DBName + "\".OUDP T2 ON T0.\"dept\" = T2.\"Code\" LEFT OUTER JOIN \"" + objCom.DBName + "\".OUBR T3";
                    SqlQuery += " ON T0.\"branch\" = T3.\"Code\" where \"empID\"='" + Empid + "'";

                }
                else
                {
                    SqlQuery = "SELECT ISNULL(U_S_PEmpID,'') as 'TAEmpID',\"U_S_ThirdName\",\"picture\", \"bankCode\",\"bankAcount\",\"bankBranch\",\"empID\",";
                    SqlQuery += " \"firstName\", \"lastName\", \"middleName\", \"govID\",T0.\"position\", T1.\"descriptio\" AS \"Positionname\", \"dept\",";
                    SqlQuery += " T2.\"Remarks\" AS \"Deptname\",  T3.\"Name\" AS \"BranchName\", \"officeExt\", \"U_Rel_Name\", \"U_Rel_Type\", \"U_Rel_Phone\",";
                    SqlQuery += " \"officeTel\", \"mobile\", \"email\", \"fax\", \"homeTel\", \"pager\",\"sex\", convert(varchar(10),\"birthDate\",120) AS \"birthDate\",";
                    SqlQuery += " \"brthCountr\", \"martStatus\", \"nChildren\", \"govID\", \"citizenshp\", convert(varchar(10),\"passportEx\",120) AS \"passportEx\",";
                    SqlQuery += " \"passportNo\", \"workBlock\", \"workCity\", \"workCountr\", \"workState\", \"workCounty\", \"workStreet\", \"workZip\",";
                    SqlQuery += " \"homeBlock\", \"homeCity\", \"homeCountr\", \"homeCounty\", \"homeState\", \"homeStreet\", \"homeZip\", \"U_S_OrgstCode\",";
                    SqlQuery += " \"U_S_OrgstName\", \"WorkBuild\", \"HomeBuild\", \"U_LvlCode\", \"U_LvlName\", \"U_LocCode\", \"U_LocName\", \"U_S_ApplId\",";
                    SqlQuery += " ISNULL(\"manager\", '0') AS \"Manager\",ISNULL(T0.\"U_S_PWORK\",'') AS \"U_S_CompName\" FROM \"" + objCom.DBName + "\".dbo.OHEM T0";
                    SqlQuery += " LEFT OUTER JOIN \"" + objCom.DBName + "\".dbo.OHPS T1 ON T0.\"position\" = T1.\"posID\" LEFT OUTER JOIN \"" + objCom.DBName + "\".dbo.OUDP T2";
                    SqlQuery += " ON T0.\"dept\" = T2.\"Code\" LEFT OUTER JOIN \"" + objCom.DBName + "\".dbo.OUBR T3 ON T0.\"branch\" = T3.\"Code\"";
                    SqlQuery += " where \"empID\"='" + Empid + "'";
                }
                ds = objCom.ReturnDataset(SqlQuery);                
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);                
            }
            return ds;
        }
        public void BindEduDetails(string EmpId, GridView grd)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "SELECT  CAST(\"fromDate\" AS Varchar) AS \"fromDate\",  CAST(\"toDate\" AS Varchar) AS \"toDate\",\"type\" AS \"name\", \"institute\", \"major\", \"diploma\",\"empID\",\"line\" FROM \"" + objCom.DBName + "\".HEM2   where \"empID\"='" + EmpId + "'";
                }
                else
                {
                    SqlQuery = "SELECT  convert(varchar(10),\"fromDate\",103) AS \"fromDate\",  convert(varchar(10),\"toDate\",103) AS \"toDate\",\"type\" AS \"name\", \"institute\", \"major\", \"diploma\",\"empID\",\"line\" FROM \"" + objCom.DBName + "\".dbo.HEM2   where \"empID\"='" + EmpId + "'";
                }
                objCom.BindGrid(SqlQuery, grd);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }

        public void State(string strCountry, DropDownList ddl)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "select \"Code\",\"Name\"  from \"" + objCom.DBName + "\".OCST where \"Country\"='" + strCountry + "' order by \"Code\"";
                }
                else
                {
                    SqlQuery = "select \"Code\",\"Name\"  from \"" + objCom.DBName + "\".dbo.OCST where \"Country\"='" + strCountry + "' order by \"Code\"";
                }
                objCom.BindDropdown(SqlQuery, "Code", "Name", ddl);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);               
            }
        }
        public void BindCountry(DropDownList ddl)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "select \"Code\",\"Name\"  from \"" + objCom.DBName + "\".OCRY order by \"Code\"";
                }
                else
                {
                    SqlQuery = "select \"Code\",\"Name\"  from \"" + objCom.DBName + "\".dbo.OCRY order by \"Code\"";
                }
                objCom.BindDropdown(SqlQuery, "Code", "Name", ddl);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }
        public void BindEdutype(DropDownList ddl)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "select \"edType\",\"descriptio\"  from \"" + objCom.DBName + "\".OHED";
                }
                else
                {
                    SqlQuery = "select \"edType\",\"descriptio\"  from \"" + objCom.DBName + "\".dbo.OHED";
                }
                objCom.BindDropdown(SqlQuery, "edType", "descriptio", ddl);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }
        public void BankName(string strCountry, DropDownList ddl)
        {
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "select \"BankCode\",\"BankName\"  from \"" + objCom.DBName + "\".ODSC where \"CountryCod\"='" + strCountry + "' order by \"Code\"";
                }
                else
                {
                    SqlQuery = "select \"BankCode\",\"BankName\"  from \"" + objCom.DBName + "\".dbo.ODSC where \"CountryCod\"='" + strCountry + "' order by \"Code\"";
                }
                objCom.BindDropdown(SqlQuery, "BankCode", "BankName", ddl);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
        }
        public string GetPicturePath()
        {
            string BitmapPath = "";
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "SELECT IFNULL(\"BitmapPath\",'')  AS \"BitmapPath\" FROM \"" + objCom.DBName + "\".OADP ";
                }
                else
                {
                    SqlQuery = "SELECT isnull(\"BitmapPath\",'')  AS \"BitmapPath\" FROM \"" + objCom.DBName + "\".dbo.OADP";
                }
                BitmapPath = objCom.ReturnValue(SqlQuery);                
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);
            }
            return BitmapPath;
        }
        public DataSet Manager(string ManagerId)
        {
            DataSet ds = new DataSet();
            try
            {
                if (objCom.ISHANA == "HANA")
                {
                    SqlQuery = "SELECT IFNULL(\"firstName\",'') ||' '|| IFNULL(\"middleName\",'') ||' ' || IFNULL(\"lastName\",'') AS \"ManName\" FROM \"" + objCom.DBName + "\".OHEM where \"empID\"='" + ManagerId + "'";
                }
                else
                {
                    SqlQuery = "SELECT \"firstName\" +' '+ ISNULL(\"middleName\",'') +' '+ \"lastName\" AS \"ManName\" FROM \"" + objCom.DBName + "\".dbo.OHEM where \"empID\"='" + ManagerId + "'";
                }
                ds = objCom.ReturnDataset(SqlQuery);
            }
            catch (Exception ex)
            {
                CommonFunctions.WriteError(ex.Message);                
            }
            return ds;
        }


    }
}