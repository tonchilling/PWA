using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto.Smart1662.Incident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface ISysMessagesIncidentRespository
    {
        bool Add(SysMessagesDto message);
        Task Update(SysMessagesDto message);
        Task Delete(SysMessagesDto message);
        SysMessagesDto GetById(string id);
        List<SysMessagesDto> GetMessagesIncident(string BranchNo);
        void UpdateMessagesIncident(string PwaIncidentID, int isRead, string BranchNo);
    }


    /*string sql = "sp_GetMessagesIncident_BranchNo";
    List<SqlParameter> paramList = new List<SqlParameter>();
    result = new SysMessagesDto();
    requestDto = (IncidentDto) obj;

    paramList.Add(new SqlParameter("@BranchNo", requestDto.BranchNo));
            paramList.Add(new SqlParameter("@PwaIncidentID", requestDto.PwaIncidentID));
         
            try
            {
                result = this.FindByPK<SysMessagesDto>(sql, paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("SysMessagesDto.FindByPK::" + ex.ToString());
}
            finally
            {

            }

            return result;*/
}
