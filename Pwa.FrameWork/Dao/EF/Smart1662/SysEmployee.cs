//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pwa.FrameWork.Dao.EF.Smart1662
{
    using System;
    using System.Collections.Generic;
    
    public partial class SysEmployee
    {
        public int EmpId { get; set; }
        public string EmpCode { get; set; }
        public Nullable<long> AccountId { get; set; }
        public Nullable<int> PrefixId { get; set; }
        public Nullable<int> EmpTypeId { get; set; }
        public string EmpNameTH { get; set; }
        public string EmpNameEN { get; set; }
        public string EmpPID { get; set; }
        public string EmpMobile { get; set; }
        public string EmpEmail { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> PositionId { get; set; }
        public Nullable<int> LeadId { get; set; }
        public Nullable<System.DateTime> EmpApplyDate { get; set; }
        public Nullable<System.DateTime> EmpPromoteDate { get; set; }
        public string EmpAddress { get; set; }
        public string EmpPostcode { get; set; }
        public string EmpSignature { get; set; }
        public string EmpRemark { get; set; }
        public int StatusId { get; set; }
        public long CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}