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
    
    public partial class PwaRepaireWork_Incident
    {
        public int IncidentId { get; set; }
        public Nullable<int> RWId { get; set; }
        public Nullable<int> PwaIncidentID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string Status { get; set; }
    
        public virtual PwaRepaireWorkHeader PwaRepaireWorkHeader { get; set; }
    }
}
