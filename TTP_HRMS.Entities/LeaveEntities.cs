using System;

namespace TTP_HRMS.Entities
{
    public class LeaveEntities
    {
        public string RplEmpId { get; set; }        
        public string RplEmpName { get; set; }
        public string DocStatus { get; set; }
        public string OwnStatus { get; set; }
        public string OnBehalfId { get; set; }
        public string OnBehalfName { get; set; }        
        public string ContactAdd { get; set; }       
        public string PhoneNo { get; set; }        
        public string Attachment { get; set; }
        public string Status { get; set; }
        public string RejoinDate { get; set; }
        public DateTime RejoinDt { get; set; }
        public SAPbobsCOM.Company SapCompany { get; set; }
        public double TotalLeave { get; set; }
        public string LeaveName { get; set; }
        public string LeaveBalance { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string strCode { get; set; }
        public string Empid { get; set; }
        public string EmpName { get; set; }
        public string TransType { get; set; }        
        public string LeaveCode { get; set; }        
        public string StartDate { get; set; }
        public string EndDate { get; set; }        
        public string NoofDays { get; set; }
        public string Notes { get; set; }
        public int Month { get; set; }       
        public int Year { get; set; }
        
    }
}
