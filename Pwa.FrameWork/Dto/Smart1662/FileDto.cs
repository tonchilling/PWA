using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dto.Smart1662
{
    public class FileDto : BaseDto
    {
        public string FileID { get; set; }

        public string RWId { get; set; }
        public string PwaIncidentID { get; set; }
        public string No { get; set; }
        public string UploadDate { get; set; }
        public string UploadType { get; set; }
        public string FileName { get; set; }
        public string Profile { get; set; }
        public string FileSize { get; set; }
        // public HttpPostedFile PostFile { get; set; }
        public string HtmlFile { get; set; }
        public string FilePath { get; set; }
        public string FullPath { get; set; }
       public string Comment { get; set; }
        public string Base64File { get; set; }
        public string Tab { get; set; }
    }
}
