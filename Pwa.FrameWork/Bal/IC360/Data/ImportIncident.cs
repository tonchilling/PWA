using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Bal.IC360.Data
{
    public class ImportIncident : Pwa.FrameWork.Dao.EF.Smart1662.PwaIncident
    {
        public string OpenerTitle { get; set; }
        public string OpenerFirstName { get; set; }
        public string OpenerLastName { get; set; }

        public string ClosedTitle { get; set; }
        public string ClosedFirstName { get; set; }
        public string ClosedLastName { get; set; }


    }
}
