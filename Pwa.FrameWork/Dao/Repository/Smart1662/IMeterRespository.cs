using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;
using Pwa.FrameWork.Dto.PWA;
namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface IMeterRespository
    {

        List<PwaRepaireWork_EffectCustomerDto> GeCustomersEffect(string latitude,string longtitude);
        List<PwaRepaireWork_EffectCustomerDto> GeCustomersEffectByShapeText(string ShapeText,string areacode,string pwacode);
        List<PwaRepaireWork_EffectCustomerDto> GeCustomersEffectByShapeGeoJsonText(string ShapeGeoJsonText, string areacode, string pwacode);
        List<PWALeakPointHistoryDto> GetRepaireHistoryByPipe(string areacode, string pipeCoordinate);
        List<PwaRepaireWork_EffectCustomerDto> GeCustomersEffectByPipeIds(string areacode, List<string> pipeidList,string pwacode);

        
        List<PwaRepaireWork_EffectPipeDto> GePipesEffectByShapeText(string ShapeText, string areacode,ToolType toolType);
        PWAMeterDto GeMeterByCustomerCode(string custcode,string areacode);
        PWAZoneDTO GetZoneByWWCode( string wwcode);
        PWABranchDTO GetBranchByBA(string bacode);




    }
}
