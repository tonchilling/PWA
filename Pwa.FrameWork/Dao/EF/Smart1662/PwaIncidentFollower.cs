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
    
    public partial class PwaIncidentFollower
    {
        public int PwaFollowerId { get; set; }
        public Nullable<int> PwaIncidentId { get; set; }
        public string PwaIncidentNo { get; set; }
        public Nullable<int> No { get; set; }
        public Nullable<int> ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string Information { get; set; }
        public Nullable<System.DateTime> FollowerDate { get; set; }
        public Nullable<int> InformerID { get; set; }
        public string InformFirstName { get; set; }
        public string InformLastName { get; set; }
        public string UpdatedBy { get; set; }
        public string SysOwnerCode { get; set; }
        public string IncidentNoRefer { get; set; }
    }
}
