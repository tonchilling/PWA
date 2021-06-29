using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dto
{
   public class MenuDto
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public int MenuOrder { get; set; }
        public string MvcArea { get; set; }
        public string MvcController { get; set; }
        public string MvcAction { get; set; }
        public string ParentId { get; set; }
        public List<MenuDto> SubMenus { get; set; }
    }
}
