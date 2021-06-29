using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pwa.FrameWork.Bal.PwaSystem;
using Pwa.FrameWork.Extension;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using Pwa.FrameWork.Dao.EF.Smart1662;
using  Pwa.FrameWork.Dto.Smart1662.RepaireWork;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dto.PWA;
namespace Pwa.FrameWork.Bal.Smart1662
{
    public class GISManager
    {
        IMeterRespository  _Resp = RespositoryFactory.GetMeterResponsitory();
        public List<PwaRepaireWork_EffectPipeDto> GetPipeNearByPoint(string shapeText,string areaCode,ToolType toolType)
        {
            List<PwaRepaireWork_EffectPipeDto> pipeEffect = null;
            pipeEffect= _Resp.GePipesEffectByShapeText(shapeText, areaCode, toolType);

            if (pipeEffect != null)
            {
                foreach (PwaRepaireWork_EffectPipeDto dto in pipeEffect)
                {
                    //  dto.ShapeSnape = Converting.ToShape(dto.ShapeSnapeText);
                   // dto.PipeShape = Converting.ToShape(dto.PipeShapeText);

                    if (dto.ShapeSnapeText != null && dto.ShapeSnapeText != "")
                    {
                        string[] LngLa = dto.ShapeSnapeText.Trim().ToLower().Replace("point", "").Replace("(", "").Replace(")", "").Split(' ');

                        dto.ShapeSnapeLa = LngLa[1];
                        dto.ShapeSnapeLng = LngLa[0];
                    }

                }
            }


            return pipeEffect;
        }

        public List<PWALeakPointHistoryDto> GetRepaireHistoryByPipe(string areaCode, string shapeText)
        {
            List<PWALeakPointHistoryDto> pipeEffect = null;
            pipeEffect = _Resp.GetRepaireHistoryByPipe(areaCode, shapeText);

         


            return pipeEffect;
        }
        public PWAZoneDTO GetZoneArea( string wwcode)
        {
            PWAZoneDTO  zoneDto = null;
            zoneDto = _Resp.GetZoneByWWCode(wwcode);




            return zoneDto;
        }

        public PWABranchDTO GetBranchArea(string bacode)
        {
            PWABranchDTO zoneDto = null;
            zoneDto = _Resp.GetBranchByBA(bacode);




            return zoneDto;
        }
    }
}
