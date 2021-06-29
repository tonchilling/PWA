using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pwa.Web.Models.Api
{
    public class IncidentRespModel : BaseModel
    {
        public List<IncidentModel> Incidents { get; set; }
    }

    public class IncidentListModel : BaseModel
    {
        public List<VwIncidentList> Incidents { get; set; }
    }
}