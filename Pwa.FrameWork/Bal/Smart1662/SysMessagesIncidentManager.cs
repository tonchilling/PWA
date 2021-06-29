using System.Collections.Generic;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using Pwa.FrameWork.Dto.Smart1662;

namespace Pwa.FrameWork.Bal.Smart1662
{
    public class SysMessagesIncidentManager
    {
        private ISysMessagesIncidentRespository _sysResp = RespositoryFactory.GetMessagesIncidentRespository();
        //private Smart1662Entities context = new Smart1662Entities();

        public List<SysMessagesDto> GetMessagesIncident(string BranchNo)
        {
            List<SysMessagesDto> list = null;
            list = _sysResp.GetMessagesIncident(BranchNo);
            return list;
        }

        public void UpdateMessagesIncident(string PwaIncidentNo, int isRead, string BranchNo)
        {
            _sysResp.UpdateMessagesIncident(PwaIncidentNo, isRead, BranchNo);
        }

        /*  public List<ItemDto> Search(string ItemId, string ItemCode, string ItemName, string Status)
          {
              List<ItemDto> dto = new List<ItemDto>();
              bool inj = false;
              try
              {
                  inj = Injection.SQLInjection(ItemId + ItemCode + ItemName + Status);
                  if (!inj) throw new Exception("Character not allowed.");
                  dto = _sysResp.Search(ItemId, ItemCode, ItemName,Status);
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
          }*/
    }
}
