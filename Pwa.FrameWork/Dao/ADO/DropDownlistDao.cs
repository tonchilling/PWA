using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Dao.ADO
{
    public class DropDownlistDao : Smart1662DBBase
    {
        SearchDTO searchDto = null;
        DropDownlistDto requestDto = null;
        UserInfoDto userInfReqDto = null;
        DropDownlistDto result = null;
        UserInfoDto userInfResDto = null;
        List<DropDownlistDto> resultList = null;
        public DropDownlistDao()
        { }
        public override object FindByPK(object obj)
        {
            throw new NotImplementedException();
        }
        public override bool Insert(object obj)
        {
            throw new NotImplementedException();
        }
        public override bool Update(object obj)
        {
            throw new NotImplementedException();
        }
        public override bool Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public override List<object> FinByAll()
        {
            throw new NotImplementedException();
        }

        public List<DropDownlistDto> FindByDropDownList(string tablename)
        {
            string sql = string.Format("sp_{0}_DropDownlist", tablename);
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<DropDownlistDto> result = new List<DropDownlistDto>();


            try
            {
                result = this.FindByColumn<DropDownlistDto>(sql, paramList).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("DropDownListDAO.FindByDropDownList:: table - " + tablename + "--" + ex.ToString());
            }
            finally
            {

            }

            return result;
        }
        public List<DropDownlistDto> FindByDropDownListItem(string tablename, string BranchID)
        {
            string sql = string.Format("sp_{0}_DropDownlist", tablename);
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<DropDownlistDto> result = new List<DropDownlistDto>();


            try
            {
                paramList.Add(new SqlParameter("@BranchID", BranchID));
                result = this.FindByColumn<DropDownlistDto>(sql, paramList).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("DropDownListDAO.FindByDropDownList:: table - " + tablename + "--" + ex.ToString());
            }
            finally
            {

            }

            return result;
        }
        public List<DropDownlistDto> FindBranchByDropDownList(string BaCode)
        {
            string sql = string.Format("sp_SysBranch_FilterDropDownList");
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<DropDownlistDto> result = new List<DropDownlistDto>();


            try
            {
                paramList.Add( new SqlParameter("@BACode", BaCode));
                result = this.FindByColumn<DropDownlistDto>(sql, paramList).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("DropDownListDAO.FindBranchByDropDownList:: "+ ex.ToString());
            }
            finally
            {

            }

            return result;
        }



    }

}
