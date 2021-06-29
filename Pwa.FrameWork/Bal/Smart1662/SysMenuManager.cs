using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pwa.FrameWork.Bal.Data;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;

using System.Threading.Tasks;
using Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Bal.Smart1662
{
    public class SysMenuManager
    {
        private SysMenuResponsitory _sysMenuResp = RespositoryFactory.GetSysMenuRepository(true);

        public List<MenuDto> GetAllMenu(int roleId)
        {
            return  _sysMenuResp.GetMenuByRoles(roleId);
        }

        public List<SysMenu> GetSysMenus()
        {
            return  _sysMenuResp.GetSysmMenus();
        }



    }
}
