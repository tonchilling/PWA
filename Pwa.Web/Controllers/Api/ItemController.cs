using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pwa.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Pwa.FrameWork.Dto.Smart1662;
using System.Threading.Tasks;
using Pwa.FrameWork.Bal.Smart1662;
namespace Pwa.Web.Controllers.Api
{
    public class ItemController : ApiController
    {

        [HttpPost]
        public IHttpActionResult GetItemByIdAndUnit(ItemDto dto)
        {
            SysItemManager bal = null;
            try
            {
                dto.BranchId = "59";
                bal = new SysItemManager();
                var data = bal.GetItemByIdAndUnit(dto);

                return Ok(data);

            }
            catch (Exception ex)
            {
                //log exception to App log system and throw message to clinet


                return  Ok(new IncidentRespModel
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                });
            }
        }


        [HttpPost]
        public IHttpActionResult GetTemplateBySetID(SysItem_SetDto dto)
        {
            SysItemManager bal = null;
            try
            {
              
                bal = new SysItemManager();
                var data = bal.GetSysItemSetItemBySetID(dto);

                return Ok(data);

            }
            catch (Exception ex)
            {
                //log exception to App log system and throw message to clinet


                return Ok(new IncidentRespModel
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                });
            }
        }
    }
}
