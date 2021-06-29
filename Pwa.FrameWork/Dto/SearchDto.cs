using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;
namespace Pwa.FrameWork.Dto
{
    public class SearchDTO
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeId { get; set; }
        public string AccountCode { get; set; }
        public string AccountId { get; set; }
        public string tableName { get; set; }
        public string Telephone { get; set; }
        public string CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Id { get; set; }
        public string SearchType { get; set; }
        public string AreaCode { get; set; }
        public string ShapeText { get; set; }
        public string BACode { get; set; }
        public ToolType ToolType { get; set; }

    }

    public class SearchAddressDTO
    {
        public string AddressNo { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string VillageNo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public string Zipcode { get; set; }
    }


    public class SearchIncidentDto
    {
        public string UserLogin { get; set; }
        public string BranchNo { get; set; }
        public string CustomerID { get; set; }
        public string Telephone { get; set; }
        public string CustomerCode { get; set; }
        public string ProvinceID { get; set; }
        public string Detail { get; set; }
        public string Area { get; set; }
        public string StartDate { get; set; } 
        public string EndDate { get; set; }
        public string Status { get; set; }

        public DateTime dtStartDate { get; set; }
        public DateTime dtEndDate { get; set; }

    }

    
}
