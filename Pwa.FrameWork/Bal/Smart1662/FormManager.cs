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
    public class FormManager
    {
        private IFormRespository _sysResp = RespositoryFactory.GetFormRespository();
        private Smart1662Entities context = new Smart1662Entities();

        public OpenRepairDto getOpenRepair(string id)
        {
            OpenRepairDto dto = new OpenRepairDto();
            bool inj = false;
            try
            {
                //string enc1 = DEncrypt.encrypt("S2020072700001", "1662" + "1239");
                //string RandomNo1 = enc1.Replace('/', '6').Replace('=', 'x').Substring(0, 6);

                //string enc2 = DEncrypt.encrypt("S2020072700002", "1662" + "1240");
                //string RandomNo2 = enc2.Replace('/', '6').Replace('=', 'x').Substring(0, 6);

                //string enc3 = DEncrypt.encrypt("S2020072800001", "1662" + "1241");
                //string RandomNo3 = enc3.Replace('/', '6').Replace('=', 'x').Substring(0, 6);

                //string enc4 = DEncrypt.encrypt("S2020072800002", "1662" + "1242");
                //string RandomNo4 = enc4.Replace('/', '6').Replace('=', 'x').Substring(0, 6);

                dto = _sysResp.GetOpenRepair(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dto;
        }
        public RequisitionDto getRequisition(string id)
        {
            RequisitionDto dto = new RequisitionDto();
            bool inj = false;
            try
            {
                dto = _sysResp.GetRequisition(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dto;
        }
        public CloseRepairDto getCloseRepair(string id)
        {
            CloseRepairDto dto = new CloseRepairDto();
            bool inj = false;
            try
            {
                dto = _sysResp.GetCloseRepair(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dto;
        }


    }
}
