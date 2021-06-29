using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pwa.FrameWork.Bal.Data;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto.Utils;



namespace Pwa.FrameWork.Bal.Smart1662
{
    public class SysItemManager
    {
        private ISysItemRespository _sysResp = RespositoryFactory.GetSysItemRespository();
        private Smart1662Entities context = new Smart1662Entities();

        public List<ItemDto> Search(string ItemId, string ItemCode, string ItemName, string Status, string BaCode)
        {
            List<ItemDto> dto = new List<ItemDto>();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(ItemId + ItemCode + ItemName + Status + BaCode);
                if (!inj) throw new Exception("Character not allowed.");
                dto = _sysResp.Search(ItemId, ItemCode, ItemName,Status, BaCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dto;
        }
        public bool Add(ItemDto data)
        {

            bool inj = false;
            try
            {
                if (data != null)
                {
                    if (data.Action != "D")
                    {
                        if (data.ItemCode == "" || data.ItemName == null)
                        {
                            throw new Exception("Information must not be blank.");
                        }
                    }
                }

                inj = Injection.SQLInjection(data.ItemCode+data.ItemName);
                if (!inj) throw new Exception("Character not allowed.");
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _sysResp.Add(data);
        }
        public ItemDto GetItemByIdAndUnit(ItemDto dto)
        {

            ItemDto itemDto = null;

            itemDto = _sysResp.GetItemByIdAndUnit(dto);



            return itemDto;
        }

        public List<SysItem_SetDto> GetSysItemSetAll(SysItem_SetDto dto)
        {

            List<SysItem_SetDto>  itemSetList = null;

            itemSetList = _sysResp.GetSysItemSetAll(dto);



            return itemSetList;
        }

        public List<SysItem_SetItemDto> GetSysItemSetItemBySetID(SysItem_SetDto dto)
        {

            List<SysItem_SetItemDto> itemSetList = null;

            itemSetList = _sysResp.GetSysItemSetItemBySetID(dto);



            return itemSetList;
        }
        public TemplateHeader GetTemplate(string id)
        {

            TemplateHeader itemSetList = null;

            itemSetList = _sysResp.GetTemplate(id);



            return itemSetList;
        }

        public bool InsertTemplate(TemplateHeader obj)
        {
            bool value = false;
            bool value2 = false;
            bool inj = false;
            try
            {
                if (obj.SetName == null)
                {
                    throw new Exception("Information must not be blank.");
                }
                inj = Injection.SQLInjection(obj.SetName.Trim());
                if (!inj) throw new Exception("Character not allowed.");
                value = _sysResp.InsertTemplate(obj);
                foreach (TemplateDetail it in obj.Items)
                {
                    value2 = _sysResp.InsertTemplateItem(it);
                }
                
            }
            catch (Exception ex)
            {
                //Log("PwaRepaireWorkManager.Insert", "", "error", ex);
                throw new Exception("Cannot Insert :" + ex.Message);
            }
            return value;
        }
        public bool UpdateTemplate(TemplateHeader obj)
        {
            bool value = false;
            bool value2 = false;
            bool inj = false;
            try
            {
                if (obj.SetName == null)
                {
                    throw new Exception("Information must not be blank.");
                }
                inj = Injection.SQLInjection(obj.SetName.Trim());
                if (!inj) throw new Exception("Character not allowed.");
                value = _sysResp.UpdateTemplate(obj);
                foreach (TemplateDetail it in obj.Items)
                {
                    value2 = _sysResp.InsertTemplateItem(it);
                }
            }
            catch (Exception ex)
            {
                //Log("PwaRepaireWorkManager.Insert", "", "error", ex);
                throw new Exception("Cannot Update :" + ex.Message);
            }
            return value;
        }
        public bool DeleteTemplate(TemplateHeader obj)
        {
            bool value = false;
            try
            {
                value = _sysResp.DeleteTemplate(obj);

            }
            catch (Exception ex)
            {
                //Log("PwaRepaireWorkManager.Insert", "", "error", ex);
                throw new Exception("Cannot Delete :" + ex.Message);
            }
            return value;
        }
    }
}
