using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Dto.Smart1662
{
    public class ItemDto : BaseDto
    {
        public string ItemId { get; set; }
        public string BranchId { get; set; }
        public string BaCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Price { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string CreatedByLogin { get; set; }
        public string UpdatedByLogin { get; set; }
        public string Action { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }

    }
    

    public class SysItem_SetDto : BaseDto
    {
      public string SetID { get; set; }
        public string SysBranchID { get; set; }
        public string BACode { get; set; }
        public string SetName { get; set; }

    }

    public class SysItem_SetItemDto : BaseDto
    {
        public string Id { get; set; }
        public string SetID { get; set; }
      
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string Price { get; set; }
        public string Total { get; set; }

    }
    public class TemplateHeader
    {
        public string SetID { get; set; }
        public string SysBranchID { get; set; }
        public string SetName { get; set; }
        public string Status { get; set; }

        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedName { get; set; }
        public List<TemplateDetail> Items { get; set; }
        public string FlagType { get; set; }

    }

    public class TemplateDetail
    {
        public string Seq { get; set; }
        public string SetID { get; set; }
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string Price { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedBy { get; set; }

    }
}
