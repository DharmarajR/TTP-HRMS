using System;

namespace TTP_HRMS.Entities
{
    public class ApprovalEntities
    {
        public string PerEdit { get; set; }        
        public string SrchEmp { get; set; }
        public string SrchLeave { get; set; }
        public string DocType { get; set; }
        public string DocLineId { get; set; }
        public string GLAccount { get; set; }
        public string LoanName { get; set; }
        public string EMIAmt { get; set; }
        public string NoInst { get; set; }
        public DateTime InsDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DisDate { get; set; }
        public string LoanAmt { get; set; }
        public string LoanCode { get; set; }
        public string CreateBy { get; set; }
        public string ApproveId { get; set; }
        public string PostingType { get; set; }
        public string LeaveCode { get; set; }
        public string IntReqNo { get; set; }
        public string InternalEmpInd { get; set; }
        public SAPbobsCOM.Company SapCompany { get; set; }
        public string HeadDocEntry { get; set; }
        public string HeadLineId { get; set; }
        public string DocMessage { get; set; }
        public int EmpUserId { get; set; }
        public string UserCode { get; set; }
        public string HeaderType { get; set; }
        public string HistoryType { get; set; }
        public string DocEntry { get; set; }
        public string EmpId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string AppStatus { get; set; }
        public string Remarks { get; set; }
    }
}
