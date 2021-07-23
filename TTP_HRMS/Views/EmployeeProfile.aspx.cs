using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TTP_HRMS.Models;

namespace TTP_HRMS.Views
{
    public partial class EmployeeProfile : System.Web.UI.Page
    {
        Profile objProfile = new Profile();
        public enum MessageType { Success, Error, Info, Warning };
        string strBankCode, WorkCountry, HomeCountry, WrkState, OffState, Manager, PictureName;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            if (btnSave.Text == "EDIT")
            {
                EnableDisable("EDIT");
                btnSave.Text = "SAVE";
            }
            else if(btnSave.Text=="SAVE")
            {
                AddUDO();               
            }                
        }

        protected void ddlWcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlWcountry.SelectedValue != "")
            {
                objProfile.State(ddlWcountry.SelectedValue, ddlWstate);
            }           
        }
        protected void ddlhcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlhcountry.SelectedValue != "")
            {
                objProfile.State(ddlhcountry.SelectedValue, ddlhstate);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserCode"] == null)
                    Response.Redirect("~/Views/Login.aspx?sessionExpired=true", true);
                else
                {
                    objProfile.BindCountry(ddlhcountry);
                    objProfile.BindCountry(ddlWcountry);
                    objProfile.BindCountry(ddlcountryofbirth);
                    objProfile.BindCountry(ddlcitizen);
                    objProfile.BindEdutype(ddledutype);
                    Populatedetails();
                    objProfile.BindEduDetails(Session["UserCode"].ToString(), grdEducation);
                }
            }
        }
        private void EnableDisable(string aChoice)
        {
            if (aChoice == "EDIT")
            {
                txtMiddleName.Enabled = true;
                txtOfficephone.Enabled = true;
                txtmobile.Enabled = true;
                txthomePhone.Enabled = true;
                txtEmail.Enabled = true;
                txtfax.Enabled = true;
                txtWstreet.Enabled = true;
                txtHstreet.Enabled = true;
                txtWblock.Enabled = true;
                txtHblock.Enabled = true;
                txtWfloor.Enabled = true;
                txtHfloor.Enabled = true;
                txtWzipcode.Enabled = true;
                txtHzipcode.Enabled = true;
                txtWcity.Enabled = true;
                txtHcity.Enabled = true;
                txtWcounty.Enabled = true;
                txtHcounty.Enabled = true;
                ddlWstate.Enabled = true;
                ddlhstate.Enabled = true;
                ddlWcountry.Enabled = true;
                ddlhcountry.Enabled = true;
                ddlcitizen.Enabled = true;
                txtdob.Enabled = true;
                txtpassportno.Enabled = true;
                ddlcountryofbirth.Enabled = true;
                txtpaasportexpiry.Enabled = true;
                ddlmarital.Enabled = true;
                txtNoofchild.Enabled = true;
                txtrelName.Enabled = true;
                txtidno.Enabled = true;
                txtrelType.Enabled = true;
                txtContactno.Enabled = true;
                ddlbankname.Enabled = true;
                txtaccno.Enabled = true;
                txtbranchName.Enabled = true;
            }
            else
            {              
                txtMiddleName.Enabled = false;
                txtOfficephone.Enabled = false;
                txtmobile.Enabled = false;
                txthomePhone.Enabled = false;
                txtEmail.Enabled = false;
                txtfax.Enabled = false;
                txtWstreet.Enabled = false;
                txtHstreet.Enabled = false;
                txtWblock.Enabled = false;
                txtHblock.Enabled = false;
                txtWfloor.Enabled = false;
                txtHfloor.Enabled = false;
                txtWzipcode.Enabled = false;
                txtHzipcode.Enabled = false;
                txtWcity.Enabled = false;
                txtHcity.Enabled = false;
                txtWcounty.Enabled = false;
                txtHcounty.Enabled = false;
                ddlWstate.Enabled = false;
                ddlhstate.Enabled = false;
                ddlWcountry.Enabled = false;
                ddlhcountry.Enabled = false;
                ddlcitizen.Enabled = false;
                txtdob.Enabled = false;
                txtpassportno.Enabled = false;
                ddlcountryofbirth.Enabled = false;
                txtpaasportexpiry.Enabled = false;
                ddlmarital.Enabled = false;
                txtNoofchild.Enabled = false;
                txtrelName.Enabled = false;
                txtidno.Enabled = false;
                txtrelType.Enabled = false;
                txtContactno.Enabled = false;
                ddlbankname.Enabled = false;
                txtaccno.Enabled = false;
                txtbranchName.Enabled = false;
            }
        }
        private void AddUDO()
        {
            if (Session["UserCode"] == null || Session["SAPCompany"] == null)
                Response.Redirect("Login.aspx?sessionExpired=true", true);           
            else 
            {
                try
                {
                    SAPbobsCOM.Company company;
                    company = (SAPbobsCOM.Company)Session["SAPCompany"];
                    SAPbobsCOM.EmployeesInfo oEmployee;
                    oEmployee = company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oEmployeesInfo);
                    if (oEmployee.GetByKey(System.Convert.ToInt32(txtEmpNo.Text.Trim())))
                    {
                        oEmployee.OfficePhone = txtOfficephone.Text.Trim();
                        oEmployee.MiddleName = txtMiddleName.Text.Trim();
                        oEmployee.LastName = txtlastName.Text.Trim();
                        oEmployee.WorkStreet = txtWstreet.Text.Trim();
                        oEmployee.WorkBlock = txtWblock.Text.Trim();
                        oEmployee.WorkBuildingFloorRoom = txtWfloor.Text.Trim();
                        oEmployee.WorkCity = txtWcity.Text.Trim();
                        if (txtdob.Text != "")
                            oEmployee.DateOfBirth = Convert.ToDateTime(txtdob.Text.Trim());// Date.ParseExact(txtdob.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) ' txtdob.Text.Trim()
                        if (ddlWcountry.SelectedIndex != 0)
                            oEmployee.WorkCountryCode = ddlWcountry.SelectedValue;
                        if (ddlWstate.SelectedIndex != 0)
                            oEmployee.WorkStateCode = ddlWstate.SelectedValue;
                        oEmployee.WorkZipCode = txtWzipcode.Text.Trim();
                        oEmployee.HomeStreet = txtHstreet.Text.Trim();
                        oEmployee.HomeBlock = txtHblock.Text.Trim();
                        oEmployee.HomeBuildingFloorRoom = txtHfloor.Text.Trim();
                        oEmployee.HomeCity = txtHcity.Text.Trim();
                        if (ddlhcountry.SelectedIndex != 0)
                            oEmployee.HomeCountry = ddlhcountry.SelectedValue;
                        if (ddlhstate.SelectedIndex != 0)
                            oEmployee.HomeState = ddlhstate.SelectedValue;
                        oEmployee.HomeZipCode = txtHzipcode.Text.Trim();
                        if (txtNoofchild.Text != "")
                            oEmployee.NumOfChildren = Convert.ToInt32(txtNoofchild.Text.Trim());

                        if (ddlcitizen.SelectedIndex != 0)
                            oEmployee.CitizenshipCountryCode = ddlcitizen.SelectedValue;
                        oEmployee.PassportNumber = txtpassportno.Text.Trim();
                        if (txtpaasportexpiry.Text != "")
                            oEmployee.PassportExpirationDate =Convert.ToDateTime(txtpaasportexpiry.Text.Trim());// Date.ParseExact(txtexpdate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture) 'txtexpdate.Text.Trim()
                        oEmployee.eMail = txtEmail.Text.Trim();
                        oEmployee.MobilePhone = txtmobile.Text.Trim();
                        oEmployee.Fax = txtfax.Text.Trim();
                        oEmployee.HomePhone = txthomePhone.Text.Trim();
                        oEmployee.HomeCounty = txtHcounty.Text.Trim();
                        oEmployee.WorkCounty = txtWcounty.Text.Trim();

                        if (ddlgender.SelectedValue == "M")
                            oEmployee.Gender = SAPbobsCOM.BoGenderTypes.gt_Male;
                        else if (ddlgender.SelectedValue == "F")
                            oEmployee.Gender = SAPbobsCOM.BoGenderTypes.gt_Female;
                        else
                            oEmployee.Gender = SAPbobsCOM.BoGenderTypes.gt_Undefined;

                        if (ddlmarital.SelectedValue == "S")
                            oEmployee.MartialStatus = SAPbobsCOM.BoMeritalStatuses.mts_Single;
                        else if (ddlmarital.SelectedValue == "M")
                            oEmployee.MartialStatus = SAPbobsCOM.BoMeritalStatuses.mts_Married;
                        else if (ddlmarital.SelectedValue == "D")
                            oEmployee.MartialStatus = SAPbobsCOM.BoMeritalStatuses.mts_Divorced;
                        else
                            oEmployee.MartialStatus = SAPbobsCOM.BoMeritalStatuses.mts_Widowed;
                        if (ddlcountryofbirth.SelectedIndex != 0)
                            oEmployee.CountryOfBirth = ddlcountryofbirth.SelectedValue;

                        oEmployee.UserFields.Fields.Item("U_Rel_Name").Value = txtrelName.Text.Trim();
                        oEmployee.UserFields.Fields.Item("U_Rel_Type").Value = txtrelType.Text.Trim();
                        oEmployee.UserFields.Fields.Item("U_Rel_Phone").Value = txtContactno.Text.Trim();
                        // oEmployee.UserFields.Fields.Item("U_IBAN").Value = lbliban.Text.Trim()
                        oEmployee.IdNumber = txtidno.Text.Trim();
                        if (ddlbankname.SelectedIndex != 0)
                            oEmployee.BankCode = ddlbankname.SelectedValue;
                        oEmployee.BankAccount = txtaccno.Text.Trim();
                        oEmployee.BankBranch = txtbranchName.Text.Trim();
                        //if (DbCon.IsHANA == "HANA")
                        //    SqlQuery = "Select IFNULL(Max(\"line\"),0) AS \"LineNum\" From \"" + DBName + "\".HEM2 WHERE \"empID\"='" + txtempno.Text.Trim() + "'";
                        //else
                        //    SqlQuery = "Select ISNULL(Max(\"line\"),0) AS \"LineNum\" From \"" + DBName + "\".dbo.HEM2 WHERE \"empID\"='" + txtempno.Text.Trim() + "'";
                        //int count = System.Convert.ToInt32(objCom.ReturnValue(SqlQuery));
                        //foreach (GridViewRow dr_add in grdedutype.Rows)
                        //{
                        //    if ((TextBox)dr_add.FindControl("txtdtfrom").Text != "" & (TextBox)dr_add.FindControl("txtdtTo").Text != "" & (DropDownList)dr_add.FindControl("ddledutype").SelectedValue != "")
                        //    {
                        //        if ((Label)dr_add.FindControl("lblempId").Text == "" & (Label)dr_add.FindControl("lblline").Text == "")
                        //        {
                        //            oEmployee.EducationInfo.Add();
                        //            count = count + 1;
                        //            oEmployee.EducationInfo.SetCurrentLine(count - 1);
                        //        }
                        //        else
                        //        {
                        //            count = System.Convert.ToInt32((Label)dr_add.FindControl("lblline").Text);
                        //            oEmployee.EducationInfo.SetCurrentLine(count - 1);
                        //        }
                        //        oEmployee.EducationInfo.EmployeeNo = txtempno.Text.Trim();
                        //        if ((TextBox)dr_add.FindControl("txtdtfrom").Text != "")
                        //            oEmployee.EducationInfo.FromDate = DbCon.GetDate((TextBox)dr_add.FindControl("txtdtfrom").Text);
                        //        if ((TextBox)dr_add.FindControl("txtdtTo").Text != "")
                        //            oEmployee.EducationInfo.ToDate = DbCon.GetDate((TextBox)dr_add.FindControl("txtdtTo").Text);

                        //        if ((DropDownList)dr_add.FindControl("ddledutype").SelectedIndex != 0)
                        //        {
                        //            int IntType = System.Convert.ToInt32((DropDownList)dr_add.FindControl("ddledutype").SelectedValue);
                        //            oEmployee.EducationInfo.EducationType = IntType;
                        //        }
                        //        else
                        //            oEmployee.EducationInfo.EducationType = 0;
                        //        oEmployee.EducationInfo.Institute = (TextBox)dr_add.FindControl("txtinsti").Text;
                        //        oEmployee.EducationInfo.Major = (TextBox)dr_add.FindControl("txtmajor").Text;
                        //        oEmployee.EducationInfo.Diploma = (TextBox)dr_add.FindControl("txtDiploma").Text;
                        //    }
                        //}

                        if (oEmployee.Update() != 0)
                        {                            
                            ShowMessage(company.GetLastErrorDescription(), MessageType.Error);                            
                        }
                        else
                        {                            
                            ShowMessage("Employee profile updated successfully...", MessageType.Success);
                            Populatedetails();
                            EnableDisable("SAVE");
                            btnSave.Text = "EDIT";
                            
                        }
                    }
                }
                catch (Exception ex)
                {                    
                    ShowMessage(ex.Message, MessageType.Error);
                }
            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        private void Populatedetails()
        {
            DataSet ds = new DataSet();
            ds = objProfile.BindPersonelDetails(Session["UserCode"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtEmpNo.Text = ds.Tables[0].Rows[0]["empID"].ToString();
                txtWorkCompany.Text = ds.Tables[0].Rows[0]["U_S_CompName"].ToString();
                PictureName = ds.Tables[0].Rows[0]["picture"].ToString();
                txtaccno.Text = ds.Tables[0].Rows[0]["bankAcount"].ToString();
                txtbranchName.Text = ds.Tables[0].Rows[0]["bankBranch"].ToString();
                txtrelName.Text = ds.Tables[0].Rows[0]["U_Rel_Name"].ToString();
                txtrelType.Text = ds.Tables[0].Rows[0]["U_Rel_Type"].ToString();
                txtContactno.Text = ds.Tables[0].Rows[0]["U_Rel_Phone"].ToString();
                txtWblock.Text = ds.Tables[0].Rows[0]["workBlock"].ToString();
                txtWfloor.Text = ds.Tables[0].Rows[0]["WorkBuild"].ToString();
                txtWcity.Text = ds.Tables[0].Rows[0]["workCity"].ToString();
                txtWcounty.Text = ds.Tables[0].Rows[0]["workCounty"].ToString();
                txtWstreet.Text = ds.Tables[0].Rows[0]["workStreet"].ToString();
                txtWzipcode.Text = ds.Tables[0].Rows[0]["workZip"].ToString();
                txtHblock.Text = ds.Tables[0].Rows[0]["homeBlock"].ToString();
                txtHcity.Text = ds.Tables[0].Rows[0]["homeCity"].ToString();
                txtHcounty.Text = ds.Tables[0].Rows[0]["homeCounty"].ToString();
                txtHstreet.Text = ds.Tables[0].Rows[0]["homeStreet"].ToString();
                txtHzipcode.Text = ds.Tables[0].Rows[0]["homeZip"].ToString();
                txtHfloor.Text = ds.Tables[0].Rows[0]["HomeBuild"].ToString();
                ddlmarital.SelectedValue = ds.Tables[0].Rows[0]["martStatus"].ToString();
                ddlgender.SelectedValue = ds.Tables[0].Rows[0]["sex"].ToString();
                txtdob.Text = ds.Tables[0].Rows[0]["birthDate"].ToString();
                txtNoofchild.Text = ds.Tables[0].Rows[0]["nChildren"].ToString();
                txtidno.Text = ds.Tables[0].Rows[0]["govID"].ToString();
                txtpaasportexpiry.Text = ds.Tables[0].Rows[0]["passportEx"].ToString();
                txtpassportno.Text = ds.Tables[0].Rows[0]["passportNo"].ToString();
                txtfirstName.Text = ds.Tables[0].Rows[0]["firstName"].ToString();
                txtlastName.Text = ds.Tables[0].Rows[0]["lastName"].ToString();
                txtMiddleName.Text = ds.Tables[0].Rows[0]["middleName"].ToString();
                txtthirdName.Text = ds.Tables[0].Rows[0]["U_S_ThirdName"].ToString();
                if (ds.Tables[0].Rows[0]["position"].ToString() != "")
                    txtPosition.Text = ds.Tables[0].Rows[0]["Positionname"].ToString();
                if (ds.Tables[0].Rows[0]["dept"].ToString() != "")
                    txtdept.Text = ds.Tables[0].Rows[0]["Deptname"].ToString();
                txtmobile.Text = ds.Tables[0].Rows[0]["officeTel"].ToString();
                txtmobile.Text = ds.Tables[0].Rows[0]["mobile"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                txtfax.Text = ds.Tables[0].Rows[0]["fax"].ToString();
                txthomePhone.Text = ds.Tables[0].Rows[0]["homeTel"].ToString();
                Manager = ds.Tables[0].Rows[0]["Manager"].ToString();

                if (ds.Tables[0].Rows[0]["brthCountr"].ToString() != "")
                    ddlcountryofbirth.SelectedValue = ds.Tables[0].Rows[0]["brthCountr"].ToString();
                if (ds.Tables[0].Rows[0]["citizenshp"].ToString() != "")
                    ddlcitizen.SelectedValue = ds.Tables[0].Rows[0]["citizenshp"].ToString();
                if (ds.Tables[0].Rows[0]["workCountr"].ToString() != "")
                    ddlWcountry.SelectedValue = ds.Tables[0].Rows[0]["workCountr"].ToString();
                if (ds.Tables[0].Rows[0]["homeCountr"].ToString() != "")
                    ddlhcountry.SelectedValue = ds.Tables[0].Rows[0]["homeCountr"].ToString();
                if (ds.Tables[0].Rows[0]["bankCode"].ToString() != "")
                    strBankCode = ds.Tables[0].Rows[0]["bankCode"].ToString();
                WorkCountry = ds.Tables[0].Rows[0]["workCountr"].ToString();
                HomeCountry = ds.Tables[0].Rows[0]["homeCountr"].ToString();
                WrkState = ds.Tables[0].Rows[0]["workState"].ToString();
                OffState = ds.Tables[0].Rows[0]["homeState"].ToString();
                if (WorkCountry != "")
                {
                    objProfile.State(WorkCountry, ddlWstate);
                    if (WrkState != "")
                        ddlWstate.SelectedValue = WrkState;
                }
                if (HomeCountry != "")
                {
                    objProfile.State(HomeCountry, ddlhstate);
                    objProfile.BankName(HomeCountry, ddlbankname);
                    if (OffState != "")
                        ddlhstate.SelectedValue = OffState;
                    if (strBankCode != "")
                        ddlbankname.SelectedValue = strBankCode;
                }

                if (Manager != "0")
                {
                    ds = objProfile.Manager(Manager);
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        txtManager.Text = ds.Tables[0].Rows[0]["ManName"].ToString();
                    }
                }

                string Targetpath = objProfile.GetPicturePath();
                string strpath = Server.MapPath(@"~\People\");
                try
                {
                    System.IO.File.Copy(Targetpath + PictureName, strpath + PictureName.Replace("'", "").Replace(",", ""));
                }
                catch 
                {
                }
                Image2.ImageUrl = @"~\People\" + PictureName;
            }
        }
        protected void btnEdusave_Click(object sender, EventArgs e)
        {

        }
    }
}