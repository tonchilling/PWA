using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using Pwa.FrameWork.Dto;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Pwa.FrameWork.Bal.Smart1662;
using Pwa.Web.Models.Api;
namespace Pwa.Web.Controllers.Api
{
    [RoutePrefix("api/DropDownList")]
    public class DropDownListController : ApiController
    {
        HttpResponseMessage response = null;

        UtilManager bal = null;
        List<DropDownlistDto> resultList = null;
        DropDownlistDto result = null;

        [HttpPost]
        [Route("GetAll")]
        public async Task<IHttpActionResult> ViewAll(SearchDTO dto)
        {

            try
            {
                bal = new UtilManager();
                var data =  bal.GetDropDownList(dto.tableName);

                return await Task.FromResult(Ok(data));

            }
            catch (Exception ex)
            {
                //log exception to App log system and throw message to clinet


                return await Task.FromResult(Ok(new IncidentRespModel
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                }));
            }
        }







    }
}
