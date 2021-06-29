using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dto.Smart1662
{
   public class SysRolePermissionDto:BaseDto
    {
        public int RoleId { get; set; }
        public int SiteId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool FlagSystem { get; set; }
        public bool FlagActive { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int PermissionId { get; set; }
        public bool PermissionFlag { get; set; }
        public string ParentId { get; set; }

        public List<SysRolePermissionDto> SubMenus { get; set; }


    }
}
