using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface ISysItemRespository
    {
        bool Add(ItemDto item);
        Task Update(ItemDto item);
        Task Delete(ItemDto item);
        ItemDto GetById(string id);
        ItemDto GetByCode(string code);
        ItemDto GetItemByIdAndUnit(ItemDto itemDto);
        List<SysItem_SetDto> GetSysItemSetAll(SysItem_SetDto dto);
        List<SysItem_SetItemDto> GetSysItemSetItemBySetID(SysItem_SetDto dto);
        TemplateHeader GetTemplate(string id);
        List<ItemDto> Search(string ItemId, string ItemCode, string ItemName, string Status, string BaCode);
        bool InsertTemplate(TemplateHeader item);
        bool InsertTemplateItem(TemplateDetail item);
        bool UpdateTemplate(TemplateHeader item);
        bool DeleteTemplate(TemplateHeader item);


    }
}
