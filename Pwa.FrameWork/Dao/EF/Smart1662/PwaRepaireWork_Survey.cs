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
    
    public partial class PwaRepaireWork_Survey
    {
        public int SurveyId { get; set; }
        public Nullable<int> RWId { get; set; }
        public string SurveyDate { get; set; }
        public string SurveyTime { get; set; }
        public string IsBroken { get; set; }
        public string SurveyAccountId { get; set; }
        public string BrokenAppearance { get; set; }
        public string PiplineType { get; set; }
        public string SurfaceAppearance { get; set; }
        public string PiplineSize { get; set; }
        public string Reason { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
        public System.Data.Entity.Spatial.DbGeometry Shape { get; set; }
        public string ShapeText { get; set; }
        public string ShapeGeoJson { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Location { get; set; }
    
        public virtual PwaRepaireWorkHeader PwaRepaireWorkHeader { get; set; }
    }
}
