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
    
    public partial class PwaRepaireWork_Effect
    {
        public int EffectId { get; set; }
        public Nullable<int> RWId { get; set; }
        public string Buffer { get; set; }
        public System.Data.Entity.Spatial.DbGeometry Shape { get; set; }
        public string ShapeText { get; set; }
        public string ShapeGeoJson { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string ToolType { get; set; }
    
        public virtual PwaRepaireWorkHeader PwaRepaireWorkHeader { get; set; }
    }
}
