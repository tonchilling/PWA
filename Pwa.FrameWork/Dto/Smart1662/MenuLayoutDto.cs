using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Smart1662;
 using Pwa.FrameWork.Dao.EF.Smart1662;
namespace Pwa.FrameWork.Dto.Smart1662
{
   public  class MenuLayoutDto
    {

        public List<int> ActiveMenuId { get; set; }
        public List<MenuDto> Menus { get; set; }
    }
}
