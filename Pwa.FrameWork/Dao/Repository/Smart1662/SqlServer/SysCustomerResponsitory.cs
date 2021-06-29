using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dto;
using Pwa.FrameWork.Dto.Smart1662;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Pwa.FrameWork.Dao.Repository.Smart1662.Postgres;
namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysCustomerResponsitory : ISysCustomerResponsitory
    {
        protected Smart1662ADO db;
        List<SqlParameter> paramList = null;
        SqlDataReader reader = null;
        SqlCommand sqlCommand = null;
        SqlTransaction trasaction = null;
        string sql = "";
        private IMeterRespository _MeterRepose = RespositoryFactory.GetMeterResponsitory();

        public SysCustomerResponsitory()
        {
          
            db = new Smart1662ADO();

        }

        public PWACustomerDto GetById(SearchDTO searchDto)
        {
            PWACustomerDto data = null;
            string sql = "sp_PWACustomerInfo_GetById";
            List<SqlParameter> paramList = new List<SqlParameter>();
          


            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;

                paramList.Add(new SqlParameter("@CustId", searchDto.CustomerId));
                paramList.Add(new SqlParameter("@CustCode", searchDto.CustomerCode));
                paramList.Add(new SqlParameter("@Telephone", searchDto.Telephone));

                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                data = Dto.Utils.Converting.ConvertDataReaderToObjList<PWACustomerDto>(reader).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }

            if(data!=null && data.CustCode != null)
            {
                data.MeterInfo = _MeterRepose.GeMeterByCustomerCode(data.CustCode, searchDto.AreaCode);
            }
                



            return data;
        }

        public PWACustomerDto GetCustomerEffectById(SearchDTO searchDto)
        {
            PWACustomerDto data = null;
            string sql = "sp_Customer_Effect_Find";
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;

                paramList.Add(new SqlParameter("@CustomerCode", searchDto.CustomerCode));

                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                data = Dto.Utils.Converting.ConvertDataReaderToObjList<PWACustomerDto>(reader).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }



            return data;
        }

        public List<PWACustomerDto> GetByNameAndCode(SearchDTO searchDto)
        {
            List<PWACustomerDto> data = null;

            if (searchDto.SearchType == "N" || searchDto.SearchType == "C")
            {
                string sql = "sp_PWACustomerInfo_GetByName";
                List<SqlParameter> paramList = new List<SqlParameter>();

                SqlDataReader reader = null;
                SqlCommand sqlCommand = null;

                try
                {

                    db.OpenConnection();
                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = db.Connection;

                    paramList.Add(new SqlParameter("@CustomerName", searchDto.CustomerName));
                    paramList.Add(new SqlParameter("@CustCode", searchDto.CustomerCode));
                    paramList.Add(new SqlParameter("@Telephone", searchDto.Telephone));
                    paramList.Add(new SqlParameter("@SearchType", searchDto.SearchType));


                    sqlCommand.Parameters.AddRange(paramList.ToArray());

                    reader = sqlCommand.ExecuteReader();

                    data = Dto.Utils.Converting.ConvertDataReaderToObjList<PWACustomerDto>(reader).ToList();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    db.CloseConnection();
                }
            }
            else
            {
                using (Smart1662Entities conn = new Smart1662Entities())
                {
                    data = (from c in conn.CIS_TB_TR_CUSTOMER_INF
                            join a in conn.CIS_TB_TR_CUSADDR_INF on c.ID equals a.CUS_ID
                            join o in conn.CIS_TB_LT_ORGANIZATION on c.ORG_OWNER_ID equals o.ID
                            where a.ADDR_TYPE == "1"
                            && (searchDto.BACode == "1001" || searchDto.BACode == o.BA_CODE)
                            && ((searchDto.SearchType == "A" && a.ADDRESS_NO.StartsWith(searchDto.AreaCode))
                            || (searchDto.SearchType == "T" && (a.TEL == searchDto.Telephone || a.MOBILE == searchDto.Telephone))
                            || (searchDto.SearchType == "C" && c.CUS_CODE.StartsWith(searchDto.CustomerCode)))
                            select new PWACustomerDto()
                            {
                                CustId = c.ID.ToString(),
                                CustCode = c.CUS_CODE,
                                CustTypeId = (c.CUS_TYPE_ID.HasValue ? c.CUS_TYPE_ID.Value.ToString() : ""),
                                Title = "",
                                CustomerName = (c.INSTALL_CUS_NAME == null ? "" : c.INSTALL_CUS_NAME) + " " + (c.INSTALL_CUS_SURNAME == null ? "" : c.INSTALL_CUS_SURNAME),
                                FName = c.INSTALL_CUS_NAME,
                                LName = c.INSTALL_CUS_SURNAME,
                                InstallType = "",
                                ProjectId = "",
                                MeterRouteId = "",
                                MeterRouteSeq = "",
                                address_no = a.ADDRESS_NO,
                                building = a.BUILDING,
                                floor = a.FLOOR,
                                village_no = a.VILLAGE_NO,
                                village = a.VILLAGE,
                                soi = a.SOI,
                                road = a.ROAD,
                                //TumbolCode = a.DISTRICT,
                                //AmphurCode = a.AMPHUR,
                                //ProvinceCode = a.PROVINCE,
                                zipcode = a.ZIPCODE,
                                tel = (a.MOBILE == null ? a.TEL : a.MOBILE),
                                Active = c.CUST_STATUS,
                                Text = (searchDto.SearchType == "C" ? c.CUS_CODE : (searchDto.SearchType == "T" ? (a.MOBILE == null || a.MOBILE == "" ? a.TEL : a.MOBILE) + " | "  : "") + ((a.ADDRESS_NO == null || a.ADDRESS_NO == "" ? "" : a.ADDRESS_NO) + (a.BUILDING == null || a.BUILDING == "" ? "" : " " + a.BUILDING) + (a.VILLAGE_NO == null || a.VILLAGE_NO == "" ? "" : " หมู่ที่ " + a.VILLAGE_NO) + (a.SOI == null || a.SOI == "" ? "" : " ซอย" + a.SOI) + (a.ROAD == null || a.ROAD == "" ? "" : " ถนน" + a.ROAD))),
                                Value = c.CUS_CODE  //(searchDto.SearchType == "C" ? c.CUS_CODE : (searchDto.SearchType == "A" ? a.ADDRESS_NO : (a.MOBILE == null || a.MOBILE == "" ? a.TEL : a.MOBILE)))
                            }).Take(10).ToList();
                };
            }


            return data;
        }

        public PWACustomerDto GetByAddress(SearchAddressDTO searchDto)
        {
            PWACustomerDto data = null;
            string sql = "sp_PWACustomerInfo_GetByAddress";
            List<SqlParameter> paramList = new List<SqlParameter>();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {

                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;

                paramList.Add(new SqlParameter("@AddressNo", searchDto.AddressNo));
                paramList.Add(new SqlParameter("@Building", searchDto.Building));
                paramList.Add(new SqlParameter("@floor", searchDto.Floor));
                paramList.Add(new SqlParameter("@VillageNo", searchDto.VillageNo));
                paramList.Add(new SqlParameter("@Soi", searchDto.Soi));
                paramList.Add(new SqlParameter("@Road", searchDto.Road));
                paramList.Add(new SqlParameter("@Zipcode", searchDto.Zipcode));

                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                data = Dto.Utils.Converting.ConvertDataReaderToObjList<PWACustomerDto>(reader).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }



            return data;
        }
    }
}
