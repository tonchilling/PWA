using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Dto.Smart1662
{
    public class UserInfoDto : BaseDto
    {
        public string AccountId { get; set; }
        public string AccountAvatar { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string AccountFirstName { get; set; }
        public string AccountLastName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountRemark { get; set; }
        public string FlagStatus { get; set; }
        public string FlagSystem { get; set; }
        public string FlagAdminCalc { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string MvcController { get; set; }
        public string MvcAction { get; set; }
        public string NonEmp { get; set; }

        public string Position { get; set; }
        public string Level { get; set; }
        public string CostCenter { get; set; }
        public string Ba { get; set; }
        public string Area { get; set; }

        public string AreaBaCode { get; set; }
        public string JobName { get; set; }
        public string DivName { get; set; }
        public string DepName { get; set; }
        public string OrgName { get; set; }
        public string Mobile { get; set; }
        public string WW_CODE { get; set; }
        public string check { get; set; }
        public string TypeCode { get; set; }
    }
}
