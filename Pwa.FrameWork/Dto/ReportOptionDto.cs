using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Pwa.FrameWork.Dto
{
    public  enum ExportType {
        PDF=1,
        Excel =2
    }

    public class CriterialDto:BaseDto {
        public string AreaId { get; set; }
        public string Areaname { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }

        public string FromDateTH { get; set; }
        public string ToDateTH { get; set; }


        public string FromMonthYearTH { get; set; }
        public string ToMonthYearTH { get; set; }

        public string Year { get; set; }
        public string DisplayData { get; set; }

        public string IsDay { get; set; }
        public string IsMonth { get; set; }
        public string IsQuarter { get; set; }
        public string PeriodType { get; set; }  // IsDay=1,IsMonth=2,IsQuarter=3


        public string Quarter { get; set; }
        public string RequestType { get; set; }
        public string RequestCategory { get; set; }
        public string RequestCategorySubject { get; set; }
        public string ChannelType { get; set; }
    }
    public class ReportOptionDto
    {
        public ReportOptionDto() {
            Parameters = new List<Parameters>();
        }
        public string ReportName { get; set; }
        public string ReportFile { get; set; }
        public ExportType ExportType { get; set; }
        public string AreaId { get; set; }
        public string Areaname { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string FromMonthYearTH { get; set; }
        public string ToMonthYearTH { get; set; }
        public DataSet DataSource { get; set; }

        public List<Parameters> Parameters { get; set; }
    }

    public class Parameters
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
