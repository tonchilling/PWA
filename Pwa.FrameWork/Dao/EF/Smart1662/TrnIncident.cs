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
    
    public partial class TrnIncident
    {
        public string incident_no { get; set; }
        public string incident_type { get; set; }
        public string channel { get; set; }
        public string topic { get; set; }
        public Nullable<bool> sla_flag { get; set; }
        public string sla_reason { get; set; }
        public string branch_code { get; set; }
        public string status { get; set; }
        public string problem_location { get; set; }
        public string description { get; set; }
        public string solution { get; set; }
        public string solve { get; set; }
        public string create_by { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public string update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public string complete_by { get; set; }
        public Nullable<System.DateTime> complete_date { get; set; }
        public Nullable<int> process_days { get; set; }
    }
}
