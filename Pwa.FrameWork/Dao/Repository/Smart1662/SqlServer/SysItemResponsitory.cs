using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dto.Smart1662;
using System.Data.SqlClient;
using System.Data;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysItemResponsitory : ISysItemRespository
    {
        protected Smart1662ADO db;

        public SysItemResponsitory()
        {

            db = new Smart1662ADO();

        }
        public bool Add(ItemDto dto)
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
                string sql = "sp_SysItem_Update";
                trasaction = db.Connection.BeginTransaction();

                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@BranchID", dto.BranchId));
                paramList.Add(new SqlParameter("@ItemId", dto.ItemId));
                paramList.Add(new SqlParameter("@ItemCode", dto.ItemCode));
                paramList.Add(new SqlParameter("@ItemName", dto.ItemName));
                paramList.Add(new SqlParameter("@Status", dto.Status));
                paramList.Add(new SqlParameter("@Price", dto.Price));
                paramList.Add(new SqlParameter("@UnitId", dto.UnitId));
                paramList.Add(new SqlParameter("@CreatedBy", dto.CreatedBy));
                paramList.Add(new SqlParameter("@UpdatedBy", dto.UpdatedBy));
                paramList.Add(new SqlParameter("@Action", dto.Action));
                paramList.Add(new SqlParameter("@BaCode", dto.BaCode));

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

        public Task Update(ItemDto item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(ItemDto item)
        {
            throw new NotImplementedException();
        }

        public ItemDto GetById(string id)
        {
            string sql = "sp_SysItem_GetById";
            List<SqlParameter> paramList = new List<SqlParameter>();
            ItemDto list = new ItemDto();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                paramList = new List<SqlParameter>();

                paramList.Add(new SqlParameter("@Id", id));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<ItemDto>(reader).FirstOrDefault();

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
        public ItemDto GetByCode(string Code)
        {
            throw new NotImplementedException();
        }
        public List<ItemDto> Search(string ItemId, string ItemCode, string ItemName, string Status, string BaCode)
        {
            string sql = "sp_SysItem_Search";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<ItemDto> list = new List<ItemDto>();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                paramList = new List<SqlParameter>();

                paramList.Add(new SqlParameter("@ItemId", ItemId));
                paramList.Add(new SqlParameter("@ItemCode", ItemCode));
                paramList.Add(new SqlParameter("@ItemName", ItemName));
                paramList.Add(new SqlParameter("@Status", Status));
                paramList.Add(new SqlParameter("@Bacode", BaCode));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<ItemDto>(reader);

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


        public ItemDto GetItemByIdAndUnit(ItemDto itemDto)
        {
            string sql = "sp_SysItem_GetItemByIdAndUnit";
            List<SqlParameter> paramList = new List<SqlParameter>();
            ItemDto list = new ItemDto();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                paramList = new List<SqlParameter>();

                paramList.Add(new SqlParameter("@BranchId", itemDto.BranchId));
                paramList.Add(new SqlParameter("@ItemId", itemDto.ItemId));
                paramList.Add(new SqlParameter("@UnitId", itemDto.UnitId));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<ItemDto>(reader).FirstOrDefault();

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


        public List<SysItem_SetDto> GetSysItemSetAll(SysItem_SetDto dto)
        {
            string sql = "sp_SysItem_SetGetAll";
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            List<SysItem_SetDto> itemList = null;
            try
            {
                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@SysBranchID", dto.SysBranchID));
                paramList.Add(new SqlParameter("@BaCode", dto.BACode));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                itemList = Dto.Utils.Converting.ConvertDataReaderToObjList<SysItem_SetDto>(reader).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }
            return itemList;
        }

        public List<SysItem_SetItemDto> GetSysItemSetItemBySetID(SysItem_SetDto dto)
        {
            string sql = "sp_SysItem_SetItem_SetGetSetID";
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            List<SysItem_SetItemDto> itemList = null;
            try
            {
                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@SetID", dto.SetID));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                itemList = Dto.Utils.Converting.ConvertDataReaderToObjList<SysItem_SetItemDto>(reader).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }
            return itemList;
        }
        public TemplateHeader GetTemplate(string id)
        {
            string sql = "sp_SysItem_SetItem_HeaderDetail";
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            TemplateHeader result = null;
            try
            {
                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@SetID", id));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                result = Dto.Utils.Converting.ConvertDataReaderToObjList<TemplateHeader>(reader).FirstOrDefault();
                reader.NextResult();

                if (result != null)
                    result.Items = Dto.Utils.Converting.ConvertDataReaderToObjList<TemplateDetail>(reader);


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
        public bool InsertTemplate(TemplateHeader dto)
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
                string sql = "sp_SetItem_InsertSet";
                trasaction = db.Connection.BeginTransaction();

                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@SysBranchID", dto.SysBranchID));
                paramList.Add(new SqlParameter("@SetName", dto.SetName));
                paramList.Add(new SqlParameter("@Status", dto.Status));
                paramList.Add(new SqlParameter("@CreatedBy", dto.CreatedBy));

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
        public bool UpdateTemplate(TemplateHeader dto)
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
                string sql = "sp_SetItem_UpdateSet";
                trasaction = db.Connection.BeginTransaction();

                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@SetID", dto.SetID));
                paramList.Add(new SqlParameter("@SysBranchID", dto.SysBranchID));
                paramList.Add(new SqlParameter("@SetName", dto.SetName));
                paramList.Add(new SqlParameter("@Status", dto.Status));
                paramList.Add(new SqlParameter("@UpdatedBy", dto.UpdatedBy));

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
        public bool DeleteTemplate(TemplateHeader dto)
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
                string sql = "sp_SetItem_DeleteSet";
                trasaction = db.Connection.BeginTransaction();

                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@SetID", dto.SetID));
                

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
        public bool InsertTemplateItem(TemplateDetail dto)
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
                string sql = "sp_SetItem_InsertSetItem";
                trasaction = db.Connection.BeginTransaction();

                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@SetID", dto.SetID));
                paramList.Add(new SqlParameter("@ItemId", dto.ItemId));
                paramList.Add(new SqlParameter("@UnitId", dto.UnitId));
                paramList.Add(new SqlParameter("@Price", dto.Price));
                paramList.Add(new SqlParameter("@CreatedBy", dto.CreatedBy));

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
    }
}
