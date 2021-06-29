using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto.PWA;
using System.Data.SqlClient;
using System.Data;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysBranchResponsitory : ISysBranchRespository
    {
        protected Smart1662ADO db;
        public SysBranchResponsitory()
        {

            db = new Smart1662ADO();

        }
        public bool Add(BranchDto item)
        {
            bool result = false;
            SqlTransaction trasaction = null;
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            int res = 0;
            try
            {
                db.OpenConnection();
                string sql = "sp_SysBranch_Add";
                trasaction = db.Connection.BeginTransaction();

                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@Id", item.Id));
                paramList.Add(new SqlParameter("@Code", item.Code));
                paramList.Add(new SqlParameter("@Name", item.Name));
                paramList.Add(new SqlParameter("@AreaCode", item.AreaCode));
                paramList.Add(new SqlParameter("@CCTR_CODE", item.CCTR_CODE));
                paramList.Add(new SqlParameter("@WW_CODE", item.WW_CODE));
                paramList.Add(new SqlParameter("@TypeCode", item.TypeCode));
                paramList.Add(new SqlParameter("@Parent", item.Parent));
                paramList.Add(new SqlParameter("@Status", item.Status));
                paramList.Add(new SqlParameter("@CreatedBy", item.CreatedBy));
                paramList.Add(new SqlParameter("@UpdatedBy", item.UpdatedBy));

                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());
                sqlCommand.Transaction = trasaction;
                res = sqlCommand.ExecuteNonQuery();
                trasaction.Commit();

                result = res > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }
            return result;
        }

        public Task Update(SysBranch item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SysBranch item)
        {
            throw new NotImplementedException();
        }

        public SysBranch GetById(int Id)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysBranch.FirstOrDefault(c=>c.Id==Id);
                return target;
            }
        }
        public PWABranchDTO GetPWABranchByCode(string BranchCode)
        {
            PWABranchDTO target = null;
            using (Smart1662Entities context = new Smart1662Entities())
            {
                //  var target = context.SysBranch.FirstOrDefault(c => c.Code == Code);
                target = (from b in context.SysBranch
                          join a in context.SysArea on b.AreaCode equals a.Code
                          where b.Code == BranchCode
                          select new PWABranchDTO
                          {
                              ba = b.Code,
                              pwaname = b.Name,
                              pwacode = b.WW_CODE,
                              zone = b.AreaCode,
                              wwzonecode = a.WW_CODE

                          }).FirstOrDefault();


            }
            return target;

        }
        public SysBranch GetByCode(string Code)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysBranch.FirstOrDefault(c => c.Code == Code);
                return target;
            }
        }
        public List<BranchDto> Search(string Id, string Code, string Name, string CCTR_CODE, string WW_CODE, string TypeCode, string Parent, string Status)
        {
            string sql = "sp_SysBranch_Search";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<BranchDto> list = new List<BranchDto>();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                paramList = new List<SqlParameter>();

                paramList.Add(new SqlParameter("@Id", Id));
                paramList.Add(new SqlParameter("@Code", Code));
                paramList.Add(new SqlParameter("@Name", Name));
                paramList.Add(new SqlParameter("@CCTR_CODE", CCTR_CODE));
                paramList.Add(new SqlParameter("@WW_CODE", WW_CODE));
                paramList.Add(new SqlParameter("@TypeCode", TypeCode));
                paramList.Add(new SqlParameter("@Parent", Parent));
                paramList.Add(new SqlParameter("@Status", Status));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<BranchDto>(reader);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }
            return list;
        }
        public List<SysBranch> GetAll()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysBranch.ToList();
                return target;
            }
        }
    }
}
