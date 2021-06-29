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
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Utilities;

namespace Pwa.FrameWork.Bal.Smart1662
{
    public class PWACustomerManager
    {
        private ISysCustomerResponsitory _sysResp = RespositoryFactory.GetSysCustomerResponsitory();
        private Smart1662Entities context = new Smart1662Entities();

        public PWACustomerDto GetById(Dto.SearchDTO dto)
        {

            PWACustomerDto ef = new PWACustomerDto();

            ef  = _sysResp.GetById(dto);

            return ef;
        }
        public List<PWACustomerDto> GetByNameAndCode(Dto.SearchDTO dto)
        {

           List<PWACustomerDto> ef = new List<PWACustomerDto>();

            ef = _sysResp.GetByNameAndCode(dto);



            return ef;
        }

        public PWACustomerDto GetByAddress(Dto.SearchAddressDTO dto)
        {
            PWACustomerDto ef = new PWACustomerDto();
            ef = _sysResp.GetByAddress(dto);
            return ef;
        }

        public void GetCustomerFromWpaApi()
        {
            //string url = "https://ws-app.pwa.co.th/customer/address";
            //string token = "PWA:9D4i6eg1tyD100yt8opHay03h58t";

            


            //string data = JsonHelper.Serialize(model);
            //var postString = Post(url, data);
            //Debug.WriteLine("address = " + postString);
            //return postString;
        }
        public PWACustomerDto GetCustomerEffectById(Dto.SearchDTO dto)
        {
            PWACustomerDto ef = new PWACustomerDto();

            ef = _sysResp.GetCustomerEffectById(dto);

            return ef;
        }
    }
}
