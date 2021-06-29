using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto;
using Pwa.FrameWork.Dto.Smart1662;
namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface ISysCustomerResponsitory
    {

        PWACustomerDto GetById(SearchDTO searchDto);

        PWACustomerDto GetCustomerEffectById(SearchDTO searchDto);

        PWACustomerDto GetByAddress(SearchAddressDTO searchDto);

        List<PWACustomerDto> GetByNameAndCode(SearchDTO searchDto);
    }

}
