using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;

namespace Pwa.FrameWork.Dto.Smart1662
{
    public class OpenRepairDto
    {
        public string RWId { get; set; }
        public string RWCode { get; set; }
        public string PwaIncidentID { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
        public string BrokenAppearance { get; set; }
        public string PiplineType { get; set; }
        public string PiplineSize { get; set; }
        public string Reason { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustCode { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string SubDistrictID { get; set; }
        public string DistrictID { get; set; }
        public string ProvinceID { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
    }
    public class RequisitionDto
    {
        public string RWId { get; set; }
        public string RWCode { get; set; }
        public string BaCode { get; set; }
        public string BaName { get; set; }
        public string TypeCode { get; set; }
        public string CCTR_CODE { get; set; }
        public string WW_CODE { get; set; }

        public string PwaIncidentID { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
        public string BrokenAppearance { get; set; }
        public string PiplineType { get; set; }
        public string PiplineSize { get; set; }
        public string Reason { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustCode { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string SubDistrictID { get; set; }
        public string DistrictID { get; set; }
        public string ProvinceID { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public List<PwaRepaireWork_ItemDto> PwaRepaireWork_ItemDto { get; set; }
    }
    public class CloseRepairDto
    {
        public string RWId { get; set; }
        public string RWCode { get; set; }
        public string CloseJobNumber { get; set; }
        public string CloseDate { get; set; }
        public string CloseTime { get; set; }
        public string SurveyAccountId { get; set; }
        public string SurveyDate { get; set; }
        public string SurveyTime { get; set; }
        public string Surveyer { get; set; }
        public string IncidentLocation { get; set; }
        public string BrokenAppearance_Survey { get; set; }
        public string SurfaceAppearance_Survey { get; set; }
        public string PipelineType_Survey { get; set; }
        public string PipelineSize_Survey { get; set; }
        public string BrokenAppearance_Process { get; set; }
        public string SurfaceAppearance_Process { get; set; }
        public string ToolType { get; set; }
        public string HoleWidth { get; set; }
        public string HoleLength { get; set; }
        public string HoleDepth { get; set; }
        public string FromProcessDate { get; set; }
        public string FromProcessTime { get; set; }
        public string ToProcessDate { get; set; }
        public string ToProcessTime { get; set; }
        public string SurfaceFixedDate { get; set; }
        public string SurfaceFixedTime { get; set; }
        public string Remark { get; set; }
        public string BaCode { get; set; }
        public string BaName { get; set; }
        public string TypeCode { get; set; }
        public List<PwaRepaireWork_ItemDto> PwaRepaireWork_ItemDto { get; set; }
        public List<FileCloseDto> Files { get; set; }

    }
    public class FileCloseDto
    {
        public string Id { get; set; }
        public string RWId { get; set; }
        public string No { get; set; }
        public string UploadDate { get; set; }
        public string FilePath { get; set; }
        public string FullPath { get; set; }
        public string HtmlFile { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
    
}

