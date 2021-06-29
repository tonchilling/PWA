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
using Pwa.FrameWork.Dto;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;
 using Pwa.FrameWork.Dto.PWA;
namespace Pwa.Web.Controllers.Api
{
    public class GISController : ApiController
    {

        [HttpPost]
        public IHttpActionResult GetPipeNearByPoint(SearchDTO dto)
        {
            GISManager bal = null;
            SysBranchManager branch = null;
            List<PwaRepaireWork_EffectPipeDto> data = null ;
            try
            {
                branch = new SysBranchManager();
           BranchDto branchDto=     branch.GetByCode(dto.BACode);
                    bal = new GISManager();
                if(branchDto!=null)
                 data = bal.GetPipeNearByPoint(dto.ShapeText,branchDto.AreaCode, dto.ToolType);

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
        public IHttpActionResult GetRepaireHistoryByPipe(SearchDTO dto)
        {
            GISManager bal = null;
            SysBranchManager branch = null;
            List<PWALeakPointHistoryDto> data = null;
            try
            {
                branch = new SysBranchManager();
                BranchDto branchDto = branch.GetByCode(dto.BACode);
                bal = new GISManager();
                if (branchDto != null)
                    data = bal.GetRepaireHistoryByPipe(dto.ShapeText, branchDto.AreaCode);

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

        [HttpPost]
        public IHttpActionResult GetZoneByWWCode(SearchDTO dto)
        {
            GISManager bal = null;
            SysBranchManager branch = null;
            PWABranchDTO branchDto = null;
            PWAZoneDTO data = null;
            try
            {
                branch = new SysBranchManager();
                branchDto = branch.GetPWAByCode(dto.BACode);
                bal = new GISManager();
                if (branchDto != null)
                    data = bal.GetZoneArea(branchDto.wwzonecode);
                if (data != null)
                {
                    data.pwacode = branchDto.pwacode;

                }
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


        [HttpPost]
        public IHttpActionResult GetBranchArea(SearchDTO dto)
        {
            GISManager bal = null;
            SysBranchManager branch = null;
            PWABranchDTO branchDto = null;
            PWABranchDTO data = null;
            try
            {
            //    branch = new SysBranchManager();
            //    branchDto = branch.GetPWAByCode(dto.BACode);
                bal = new GISManager();
                if (dto.BACode != null && dto.BACode!="")
                    data = bal.GetBranchArea(dto.BACode);
            
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
