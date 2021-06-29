using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Smart1662.Incident;
namespace Pwa.FrameWork.Dto.Smart1662.RepaireWork
{

    public enum ToolType {
        None=0,
        Buffer=1,
        Polygon =2,
        Pipeline=3
    }
    public class PwaRepaireWorkHeaderDto:BaseDto
    {
        public string RWId { get; set; }
        public string RWCode { get; set; }
        public string WorkingDate { get; set; }
        public string WorkingTime { get; set; }
        public string RequestType { get; set; }
        public string RequestTypeName { get; set; }
        public string RequestCategory { get; set; }
        public string RequestCategoryName { get; set; }
        public string RequestCategorySubject { get; set; }
        public string RequestCategorySubjectName { get; set; }
        public string NotificationDate { get; set; }
        public string NotificationTime { get; set; }
        public string AccountId { get; set; }
        public string SLA { get; set; }
        
        public string Reason { get; set; }
        public string RowState { get; set; }
        public string BranchId { get; set; }
        public string AreaCode { get; set; }
        public string WW_CODE { get; set; }
        public string ShapeText { get; set; }
        public List<FileDto> DeleteProcessFiles { get; set; }
        public List<FileDto> DeleteCloseFiles { get; set; }
        public List<IncidentDto> Incidents { get; set; }
        public PwaRepaireWork_SurveyDto Survey { get; set; }
        public PwaRepaireWork_EffectDto Effect { get; set; }
        public PwaRepaireWork_OpenCaseDto OpenCase { get; set; }
        public PwaRepaireWork_ProcessDto Process { get; set; }
        public PwaRepaireWork_CloseJobDto CloseJob { get; set; }

        public List<PwaRepaireWork_ItemDto> Items { get; set; }
    }


    public class PwaRepaireWork_ItemDto : BaseDto
    {
        public string RWId { get; set; }
        public string No { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string Qty { get; set; }
        public string Price { get; set; }
        public string CurrentPrice { get; set; }
        public string Total { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string ItemCode { get; set; }

    }
    public class PwaRepaireWork_IncidentDto:BaseDto {
        public string IncidentId { get; set; }
        public string RWId { get; set; }
        public string PwaIncidentID { get; set; }
        public string IncidentDate { get; set; }
        public string IncidentTime { get; set; }
        public string IncidentSubject { get; set; }
        public string IncidentDetail { get; set; }

        public string IncidentPlace { get; set; }
        public string IncidentLocation { get; set; }

    }

    public class PwaRepaireWork_SurveyDto : BaseDto {
        public string SurveyId { get; set; }
        public string RWId { get; set; }
        public string SurveyDate { get; set; }
        public string SurveyTime { get; set; }
        public string IsBroken { get; set; }
        public string SurveyAccountId { get; set; }
        public string BrokenAppearance { get; set; }
        public string PiplineType { get; set; }
        public string PiplineSize { get; set; }
        public string SurfaceAppearance { get; set; }
        public string Reason { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
        public GeojsonFeatureDto Shape { get; set; }
        public string ShapeText { get; set; }
        public string ShapeGeoJson { get; set; }
        public string Location { get; set; }

        //public string Shape { get; set; }
    }


    public class PwaRepaireWork_EffectDto : BaseDto
    {
        public string EffectId { get; set; }
        public string RWId { get; set; }
        public string ToolType { get; set; }
        public string Buffer { get; set; }
        public GeojsonFeatureDto Shape { get; set; }
        public string ShapeText { get; set; }
        public string ShapeGeoJson { get; set; }
        public string MapImageEffectBase64 { get; set; }
        public string MapImageEffect { get; set; }
        public List<PwaRepaireWork_EffectCustomerDto> CustomerEffects { get; set; }
        public List<PwaRepaireWork_EffectCustomerAddrDto> CustomerAddrEffects { get; set; }
        public List<PwaRepaireWork_EffectPipeDto> PipeEffects { get; set; }
    }
    public class PwaRepaireWork_EffectCustomerDto : BaseDto
    {
        public string EffectId { get; set; }
        public string RWId { get; set; }
        public string CustCode { get; set; }
        public string CustId { get; set; }
        public string CustName { get; set; }
        public string MeterRouteId { get; set; }
        public string ShapeText { get; set; }
        public string ShapeGeoJson { get; set; }
        public GeojsonFeatureDto2 Shape { get; set; }
    }
    public class PwaRepaireWork_EffectCustomerAddrDto : BaseDto
    {
        public string EffectId { get; set; }
        public string RWId { get; set; }
        public string Province { get; set; }
        public string Amphur { get; set; }
        public string Tumbol { get; set; }

        public string VillageNo { get; set; }
        public string ProvinceName { get; set; }
        public string AmphurName { get; set; }
        public string TumbolName { get; set; }
    }
    public class PwaRepaireWork_EffectPipeDto : BaseDto
    {
        public string EffectId { get; set; }
        public string RWId { get; set; }
        public string PipelineId { get; set; }
        public string ProjectNo { get; set; }
        public string PipeSize { get; set; }
        public string PipeType { get; set; }
        public string Locate { get; set; }
        public string PwaCode { get; set; }
        public string longs { get; set; }
        public string depth { get; set; }
        public string yearinstall { get; set; }
        public string remark { get; set; }
        public string pipemain { get; set; }
        
        public string PipeShapeText { get; set; }
        public string PipeGeoJson { get; set; }
        public string ShapeSnapeText { get; set; }
        public string ShapeSnapeLa { get; set; }
        public string ShapeSnapeLng { get; set; }
        public string ShapeSnapGeoJson { get; set; }
        public GeojsonFeatureDto2 PipeShape { get; set; }
        public GeojsonFeatureDto2 ShapeSnape { get; set; }
        public string PipeAllGeoJson { get; set; }
    }

    public class PwaRepaireWork_OpenCaseDto : BaseDto
    {
        public string OpenCaseId { get; set; }
        public string RWId { get; set; }
        public string SysItemSetID { get; set; }
        public string OpenDate { get; set; }
        public string OpenTime { get; set; }
        public string IsUrgent { get; set; }
        public string IsOutSource { get; set; }

        public string OpenAccountId { get; set; }
        public string ResponAccountId { get; set; }
        public string SuperAccountId { get; set; }
        public string IsBroken { get; set; }
        public string SurveyDate { get; set; }
        public string SurveyTime { get; set; }
        public string SurveyAccountId { get; set; }
        public string BrokenAppearance { get; set; }
        public string PiplineType { get; set; }
        public string PiplineSize { get; set; }
        public string SurfaceAppearance { get; set; }
        public string Location { get; set; }
        public string Comment { get; set; }

        public string TeamId { get; set; }


    }



    public class PwaRepaireWork_ProcessDto : BaseDto
    {
        public string ProcessId { get; set; }
        public string RWId { get; set; }
        public string IsSuccess { get; set; }
        public string FromProcessDate { get; set; }
        public string FromProcessTime { get; set; }
        public string ToProcessDate { get; set; }
        public string ToProcessTime { get; set; }
        public string SurfaceFixedDate { get; set; }
        public string SurfaceFixedTime { get; set; }
        public string SuperAccountId { get; set; }
        public string BrokenAppearance { get; set; }
        public string SurfaceAppearance { get; set; }
        public string ToolType { get; set; }
        public string HoleWidth { get; set; }
        public string HoleLength { get; set; }
        public string HoleDepth { get; set; }
        public string IsOutSource { get; set; }
        public string Comment { get; set; }
        public string PiplineType { get; set; }
        public string PiplineSize { get; set; }
        public string IsNotGIS { get; set; }
         public string RepaireAccountId { get; set; }
        public string TeamId { get; set; }
        public List<FileDto> Files { get; set; }
    }
    public class PwaRepaireWork_CloseJobDto : BaseDto
    {
        public string CloseJobId { get; set; }
        public string  RWId { get; set; }
        public string CloseJobNumber { get; set; }
        public string CloseDate { get; set; }
        public string CloseTime { get; set; }
        public string TemplateId { get; set; }
        public string AccountId { get; set; }
        public string Comment { get; set; }
        public string DocumentId { get; set; }

        public List<FileDto> Files { get; set; }

    }



    }
