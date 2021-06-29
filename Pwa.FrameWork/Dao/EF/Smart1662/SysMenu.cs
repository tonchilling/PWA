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
    
    public partial class SysMenu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysMenu()
        {
            this.SysMenu1 = new HashSet<SysMenu>();
            this.SysMenuActive = new HashSet<SysMenuActive>();
            this.SysRolePermission = new HashSet<SysRolePermission>();
        }
    
        public int MenuId { get; set; }
        public int SiteId { get; set; }
        public int MenuLevel { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public int MenuOrder { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string MvcArea { get; set; }
        public string MvcController { get; set; }
        public string MvcAction { get; set; }
        public bool FlagActive { get; set; }
        public long UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysMenu> SysMenu1 { get; set; }
        public virtual SysMenu SysMenu2 { get; set; }
        public virtual SysSite SysSite { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysMenuActive> SysMenuActive { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysRolePermission> SysRolePermission { get; set; }
    }
}