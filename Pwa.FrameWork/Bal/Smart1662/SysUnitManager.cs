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
    public class SysUnitManager
    {
        private ISysUnitRespository _sysResp = RespositoryFactory.GetSysUnitRespository();
        private Smart1662Entities context = new Smart1662Entities();

        public List<UnitDto> Search(string UnitId, string UnitName, string Status)
        {
            List<UnitDto> dto = new List<UnitDto>();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(UnitId + UnitName + Status);
                if (!inj) throw new Exception("Character not allowed.");
                dto = _sysResp.Search(UnitId, UnitName, Status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dto;
        }
        public bool Add(UnitDto data)
        {

            bool inj = false;
            try
            {
                if (data != null)
                {
                    switch (data.Action.ToUpper())
                    {
                        case "I": 
                            if(data.UnitName == null) throw new Exception("Information must not be blank.");
                            break;
                        case "U":
                            if(data.UnitId == "" || data.UnitName == null) throw new Exception("Information must not be blank.");
                            break;
                        case "D":
                            break;
                        default: throw new Exception("Action parameter is invalid.");
                    }
                }

                inj = Injection.SQLInjection(data.UnitName);
                if (!inj) throw new Exception("Character not allowed.");


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _sysResp.Add(data);
        }

    }
}
