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
    
    public partial class SysRole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysRole()
        {
            this.SysRolePermission = new HashSet<SysRolePermission>();
        }
    
        public int RoleId { get; set; }
        public int SiteId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool FlagSystem { get; set; }
        public bool FlagActive { get; set; }
        public long CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string MvcController { get; set; }
        public string MvcAction { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysRolePermission> SysRolePermission { get; set; }
    }
}