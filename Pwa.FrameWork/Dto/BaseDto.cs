using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dto
{
    public class BaseDto
    {

        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public string Status { get; set; }
        public string StatusDesc { get; set; }
        public string Active { get; set; }

        public string View { get; set; }
        public string Edit { get; set; }
        public string Delete { get; set; }
        public string CreatorEmail { get; set; }
    }
}
