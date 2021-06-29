using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pwa.FrameWork.Dto.Utils
{
    public class LogDTO:BaseDto
    {
        public string ID { get; set; }
        public string IssueDate { get; set; }
        public string Project { get; set; }
        public string Page { get; set; }
        public string ValueField { get; set; }
        public string Exception { get; set; }
       
    }
}
