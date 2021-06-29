using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pwa.FrameWork.Bal.Data;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using Pwa.FrameWork.Dao.ADO;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Bal.Smart1662
{
    public class UtilManager:BaseBal
    {
        private DropDownlistDao ddlDao = null;

        public override bool Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public override List<object> FinByAll()
        {
            throw new NotImplementedException();
        }

        public override object FindByPK(object obj)
        {
            throw new NotImplementedException();
        }

        public List<DropDownlistDto> GetDropDownList(string tableName)
        {
            List<DropDownlistDto> respList = null;

            try
            {

                ddlDao = new DropDownlistDao();
                respList = ddlDao.FindByDropDownList(tableName);
            }
            catch (Exception ex)
            {
                Log("UtilManager.GetDropDownList", "", "error", ex);
                throw new Exception("Cannot Login :" + ex.Message);
            }
            return respList;
        }
        public List<DropDownlistDto> GetDropDownListItem(string tableName, string BranchID)
        {
            List<DropDownlistDto> respList = null;

            try
            {

                ddlDao = new DropDownlistDao();
                respList = ddlDao.FindByDropDownListItem(tableName, BranchID);
            }
            catch (Exception ex)
            {
                Log("UtilManager.GetDropDownListItem", "", "error", ex);
                throw new Exception("Cannot Login :" + ex.Message);
            }
            return respList;
        }
        public List<DropDownlistDto> GetDropDownBranchList(string baCode)
        {
            List<DropDownlistDto> respList = null;

            try
            {

                ddlDao = new DropDownlistDao();
                respList = ddlDao.FindBranchByDropDownList(baCode);
            }
            catch (Exception ex)
            {
                Log("UtilManager.GetDropDownList", "", "error", ex);
                throw new Exception("Cannot Login :" + ex.Message);
            }
            return respList;
        }

        public override bool Insert(object obj)
        {
            throw new NotImplementedException();
        }

        public override bool Update(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
