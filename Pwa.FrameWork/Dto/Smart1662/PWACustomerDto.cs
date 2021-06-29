using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.PWA;
namespace Pwa.FrameWork.Dto.Smart1662
{
    public class PWACustomerDto
    {
        public string CustId { get; set; }
        public string CustCode { get; set; }
        public string CustTypeId { get; set; }
        public string Title { get; set; }
        public string CustomerName { get; set; }
        public string FName { get; set; }
       public string LName { get; set; }
       public string InstallType { get; set; }
       public string ProjectId { get; set; }
       public string MeterRouteId { get; set; }
       public string MeterRouteSeq { get; set; }
	   public string address_no { get; set; }
       public string building { get; set; }
       public string floor { get; set; }
       public string village_no { get; set; }
       public string village { get; set; }
       public string soi { get; set; }
       public string road { get; set; }
       public string TumbolCode { get; set; }
       public string AmphurCode { get; set; }
       public string ProvinceCode { get; set; }
        public string TumbolName { get; set; }
        public string AmphurName{ get; set; }
        public string ProvinceName { get; set; }
        public string zipcode { get; set; }
       public string tel { get; set; }
       public string fax { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string Active { get; set; }
        public PWAMeterDto MeterInfo { get; set; }

}
}
