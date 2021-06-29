using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;
using System.Data;
using System.Data.SqlClient;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{

    public class SysMenuResponsitory : ISysMenuRepository
    {
        private readonly Smart1662Entities dbEntity;
        protected  Smart1662ADO db;

        public SysMenuResponsitory()
        {
            dbEntity = new Smart1662Entities();
            db = new Smart1662ADO();

        }
        public Task Add(SysMenu comlain)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SysMenu comlain)
        {
            throw new NotImplementedException();
        }

        public async Task<SysMenu> GetById(int id)
        {
           
                var target = await dbEntity.SysMenu.FirstOrDefaultAsync(c=>c.MenuId==id);
                return target;
       
        }

        public Task<IEnumerable<SysMenu>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(SysMenu comlain)
        {
            throw new NotImplementedException();
        }


        public List<SysMenu> GetSysmMenus()
        {

            var target =  dbEntity.SysMenu.ToList();
            return target;

        }

        public MenuLayoutDto GetMenuByRole(int roleId)
        {
            string sql = "GetMenuActive";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<MenuDto> list = new List<MenuDto>();
            MenuDto dto = null;
            MenuLayoutDto result = null;
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {
                result = new MenuLayoutDto();
                db.OpenConnection();
                paramList.Add(new SqlParameter("@RoleId", roleId));

                //connect.Open();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();
                list = Dto.Utils.Converting.ConvertDataReaderToObjList<MenuDto>(reader);
            /*   while (reader.Read())
                {
                    dto = new MenuDto();
                    dto.MenuId = Converting.ToInt(reader["MenuId"].ToString());
                    dto.SiteId = Converting.ToInt(reader["SiteId"].ToString());
                    dto.MenuName = reader["MenuName"].ToString();
                    dto.MenuIcon = reader["MenuIcon"].ToString();
                    dto.MenuOrder = Converting.ToInt(reader["MenuOrder"].ToString());
                    dto.ParentId = reader["ParentId"].ToString();
                    dto.MvcArea = reader["MvcArea"].ToString();
                    dto.MvcController = reader["MvcController"].ToString();
                    dto.MvcAction = reader["MvcAction"].ToString();
                    dto.FlagActive = Converting.ToBoolean(reader["FlagActive"].ToString());
                   dto.View = reader["View"].ToString();
                    dto.Edit = reader["Edit"].ToString();
                    dto.Delete = reader["Delete"].ToString();
                    list.Add(dto);

                }*/
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }

            result.Menus = list;

            return result;
        }

        public List<MenuDto> GetMenuByRoles(int roleId)
        {
            string sql = "GetMenuActive";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<MenuDto> list = new List<MenuDto>();
            MenuDto dto = null;
           
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {
            
                db.OpenConnection();
                paramList.Add(new SqlParameter("@RoleId", roleId));

                //connect.Open();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<MenuDto>(reader);

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

    }
}
