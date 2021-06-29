using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;
using Pwa.FrameWork.Dto.PWA;
using Pwa.FrameWork.Dto.Utils;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.Postgres
{
   public class MeterRespository: IMeterRespository
    {
        protected Smart1662PostgresADO db;
        List<NpgsqlParameter> paramList = null;
        NpgsqlDataReader reader = null;
        NpgsqlCommand sqlCommand = null;
        NpgsqlTransaction trasaction = null;
        public MeterRespository()
        {

            db = new Smart1662PostgresADO();

        }

        public List<PwaRepaireWork_EffectCustomerDto> GeCustomersEffect(string latitude,string longtitude)
        {
            string sql = string.Format("select * from GetCustomerByPoint('{0}','{1}')", latitude, longtitude);
            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>();
            List<PwaRepaireWork_EffectCustomerDto> list = new List<PwaRepaireWork_EffectCustomerDto>();


            NpgsqlDataReader reader = null;
            NpgsqlCommand sqlCommand = null;


            try
            {
             
                db.OpenConnection();
                sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = sql;
             

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectCustomerDto>(reader);

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
        public List<PwaRepaireWork_EffectCustomerDto> GeCustomersEffectByShapeText(string Shape,string AreaCode,string pwacode)
        {
            string sql = "";// string.Format("select * from getcustomerbyshape('{0}')", Shape);

            sql = string.Format("select distinct  ST_AsText(wkb_geometry) ShapeText,CAST(CustCode as text) CustCode from "
           + " oracle.r{0}_meter WHERE CAST(pwa_code as text) = '{2}' and ST_Intersects(wkb_geometry, ST_Buffer(ST_GeomFromText('SRID=4326;' || '{1}'), 0)) ", AreaCode, Shape, pwacode);

            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>();
            List<PwaRepaireWork_EffectCustomerDto> list = new List<PwaRepaireWork_EffectCustomerDto>();


            NpgsqlDataReader reader = null;
            NpgsqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = db.Connection;

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectCustomerDto>(reader);

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

        public List<PwaRepaireWork_EffectCustomerDto> GeCustomersEffectByShapeGeoJsonText(string ShapeGeoJsonText, string AreaCode, string pwacode)
        {
            string sql = "";// string.Format("select * from getcustomerbyshape('{0}')", Shape);
            ShapeGeoJsonText = ShapeGeoJsonText.Remove(ShapeGeoJsonText.Length - 1, 1) + ",\"crs\":{ \"type\":\"name\",\"properties\":{ \"name\":\"EPSG:4326\"}}}";
            sql = string.Format("select distinct  ST_AsText(wkb_geometry) ShapeText,CAST(CustCode as text) CustCode from "
           + " oracle.r{0}_meter WHERE CAST(pwa_code as text) = '{2}' and ST_Intersects(wkb_geometry, ST_Buffer(ST_GeomFromGeoJSON('{1}'), 0)) ", AreaCode, ShapeGeoJsonText.Trim(), pwacode);

            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>();
            List<PwaRepaireWork_EffectCustomerDto> list = new List<PwaRepaireWork_EffectCustomerDto>();


            NpgsqlDataReader reader = null;
            NpgsqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = db.Connection;

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectCustomerDto>(reader);

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
        public List<PwaRepaireWork_EffectCustomerDto> GeCustomersEffectByPipeIds(string AreaCode,List<string> pipeids, string pwacode)
        {
            string sql = "";// string.Format("select * from getcustomerbyshape('{0}')", Shape);
            string pipes = String.Join(", ", pipeids);
            sql = string.Format("select distinct  ST_AsText(wkb_geometry) ShapeText,CAST(CustCode as text) CustCode from "
           + " oracle.r{0}_meter where pipe_id in({1}) and CAST(pwa_code as text) = '{2}' ", AreaCode, pipes, pwacode);

            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>();
            List<PwaRepaireWork_EffectCustomerDto> list = new List<PwaRepaireWork_EffectCustomerDto>();


            NpgsqlDataReader reader = null;
            NpgsqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = db.Connection;

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectCustomerDto>(reader);

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

        //ToolType  1=Buffer,2=Draw Shape Polygon, 3 = Intersect Pipeline
        public List<PwaRepaireWork_EffectPipeDto> GePipesEffectByShapeText(string Shape, string AreaCode, ToolType toolType)
        {

            List<NpgsqlParameter> paramList;
            List<PwaRepaireWork_EffectPipeDto> list =null;
            List<PwaRepaireWork_EffectPipeDto> list2 = null;
            NpgsqlDataReader reader = null;
            NpgsqlCommand sqlCommand = null;

            StringBuilder sql = new StringBuilder();



            sql.AppendFormat("select distinct CAST(ShapeSnapeText as text) ShapeSnapeText,CAST(PipeShapeText as text)  PipeShapeText,PipeShape PipeGeoJson,CAST(Distance as text)    Distance,CAST(pipe_id as text) PipelineId, CAST(project_no as text) ProjectNo,CAST(pipe_size as text)  pipesize,CAST(pipe_type as text)  pipetype,CAST(locate as text)  locate,CAST(pwa_code as text) PwaCode ,CAST(long as text) longs,CAST(depth as text) depth ,CAST(yearinstall as text) yearinstall ,CAST(remark as text) remark,CAST('1' as text) pipemain  from");
            sql.AppendFormat("( select ST_AsText(ST_ClosestPoint(wkb_geometry, ST_GeomFromText('SRID=4326;' || '{0}')))   ShapeSnapeText, ", Shape);
            sql.AppendFormat(" ST_AsText(wkb_geometry) PipeShapeText,ST_AsGeoJSON(wkb_geometry) PipeShape,ST_Distance( ST_GeomFromText('SRID=4326;' || '{0}'),wkb_geometry ) Distance,", Shape);
            sql.AppendFormat(" pipe_id, project_no, pipe_size,pipe_type, locate, pwa_code,long,depth,yearinstall,remark from  oracle.r{0}_pipe ", AreaCode);
            sql.AppendFormat(" WHERE ST_Intersects(wkb_geometry, ST_Buffer(ST_GeographyFromText('SRID=4326;' || '{0}'), 200)) )  a  order by Distance asc  limit 1", Shape);  

         


            try
            {
                paramList = new List<NpgsqlParameter>();
                list = new List<PwaRepaireWork_EffectPipeDto>();
                db.OpenConnection();
                sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = sql.ToString();
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = db.Connection;

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectPipeDto>(reader);
                reader.Close();
                if (list != null && (toolType== ToolType.Pipeline  || toolType== ToolType.Buffer))
                {
        
                    foreach (PwaRepaireWork_EffectPipeDto pipeItem in list)
                    {

                        sql = new StringBuilder();
                        sql.AppendFormat("select CAST(PipeShapeText as text)  PipeShapeText");
                        sql.AppendFormat(",PipeShape PipeGeoJson");
                        sql.AppendFormat(",CAST(pipe_id as text) PipelineId");
                        sql.AppendFormat(" , CAST(project_no as text) ProjectNo");
                        sql.AppendFormat(", CAST(pipe_size as text)  pipesize");
                        sql.AppendFormat(", CAST(pipe_type as text)  pipetype");
                        sql.AppendFormat(", CAST(locate as text)  locate");
                        sql.AppendFormat(", CAST(pwa_code as text) PwaCode");
                        sql.AppendFormat(", CAST(long as text) longs");
                        sql.AppendFormat(", CAST(depth as text) depth");
                        sql.AppendFormat(", CAST(yearinstall as text) yearinstall");
                        sql.AppendFormat(", CAST(remark as text) remark");
                        sql.AppendFormat(", CAST('0' as text) pipemain");
                        sql.AppendFormat(" from ( select");
                        sql.AppendFormat(" ST_AsText(wkb_geometry) PipeShapeText");
                        sql.AppendFormat(" , ST_AsGeoJSON(wkb_geometry) PipeShape");
                        
                        sql.AppendFormat(", pipe_id");
                        sql.AppendFormat(", project_no ");
                        sql.AppendFormat(", pipe_size, pipe_type");
                        sql.AppendFormat(", locate");
                        sql.AppendFormat(", pwa_code");
                        sql.AppendFormat(", long");
                        sql.AppendFormat(", depth");
                        sql.AppendFormat(", yearinstall");
                        sql.AppendFormat(", remark");
                        sql.AppendFormat(" from  oracle.r{0}_pipe", AreaCode);
                        sql.AppendFormat(" WHERE ");
                        sql.AppendFormat(" ST_Intersects(wkb_geometry, ST_Buffer(ST_GeographyFromText('SRID=4326;' || '{0}'), 1))", pipeItem.PipeShapeText);
                        sql.AppendFormat(" and CAST(pipe_size as integer) >=50");
                        sql.AppendFormat(" and pipe_id<>'{0}' order by pipe_size asc)  ",pipeItem.PipelineId);
                        sql.AppendFormat("    a ");
                        paramList = new List<NpgsqlParameter>();
                      
                        sqlCommand = new NpgsqlCommand();
                        sqlCommand.CommandText = sql.ToString();
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                       sqlCommand.Connection = db.Connection;

                        reader = sqlCommand.ExecuteReader();

                        list2 = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectPipeDto>(reader);
                       
                    }
                    reader.Close();

                }



                // 
                if (list != null)
                {

                    foreach (PwaRepaireWork_EffectPipeDto pipeItem in list)
                    {

                        sql = new StringBuilder();

                        sql.AppendFormat("select ST_AsGeoJSON(ST_AsEWKT(ST_Union(wkb_geometry))) PipeAllGeoJson");

         sql.AppendFormat(" from(");
                        sql.AppendFormat(" select wkb_geometry from(");
                        sql.AppendFormat(" select  wkb_geometry");
                        sql.AppendFormat(" from(select wkb_geometry");
                        sql.AppendFormat(" , ST_Distance(ST_GeomFromText('SRID=4326;' || '{0}'), wkb_geometry) Distance", Shape);
                        sql.AppendFormat(" from oracle.r{0}_pipe",AreaCode);
                        sql.AppendFormat(" WHERE ST_Intersects(wkb_geometry, ST_Buffer(ST_GeographyFromText('SRID=4326;' || '{0}'), 200)) )  a", Shape);
                        sql.AppendFormat(" order by Distance asc  limit 1) pipenear");
                        sql.AppendFormat(" union all");
                        sql.AppendFormat(" select  wkb_geometry");
                        sql.AppendFormat(" from(select wkb_geometry from oracle.r{0}_pipe", AreaCode);
                        sql.AppendFormat(" WHERE  ST_Intersects(wkb_geometry, ST_Buffer(ST_GeographyFromText('SRID=4326;' || '{0}'), 1)) and CAST(pipe_size as integer) >=50  ) pipechild  )  b", pipeItem.PipeShapeText);

                        /*  select ST_AsGeoJSON(ST_AsEWKT(ST_Union(wkb_geometry))) PipeAllGeoJson 
         from( 
        select wkb_geometry from (
        select  wkb_geometry  
        from( select wkb_geometry
        ,ST_Distance( ST_GeomFromText('SRID=4326;' || 'Point (100.30032415036091 13.749952595872706)'),wkb_geometry ) Distance
        from  oracle.r3_pipe  
        WHERE ST_Intersects(wkb_geometry, ST_Buffer(ST_GeographyFromText('SRID=4326;' || 'Point (100.30032415036091 13.749952595872706)'), 200)) )  a  
        order by Distance asc  limit 1 ) pipenear
        union all
        select  wkb_geometry 
        from ( select wkb_geometry from  oracle.r3_pipe 
        WHERE  ST_Intersects(wkb_geometry, ST_Buffer(ST_GeographyFromText('SRID=4326;' || 'LINESTRING(100.300324136482 13.7453709855783,100.300370993038 13.7460545736955,100.30037848179 13.7461679644367,100.300386130544 13.7462827100723,100.300434617149 13.7470128365654,100.300504316044 13.7480629864063,100.300598636046 13.7494841172694,100.300602214874 13.7495384183465,100.30060397639 13.7495674218759,100.300607325458 13.7496215433991,100.300694489047 13.7509753766624,100.300713089958 13.7512643221013,100.300719849553 13.7513698533197,100.300729649512 13.7514873893816,100.300754708773 13.7518272765098,100.300834657032 13.7529097285156,100.300836635929 13.7529366520845,100.300839107804 13.7529699902157,100.300843146848 13.7530247407502,100.300845534982 13.753056723584,100.30089010419 13.753767076261)'), 1)) and CAST(pipe_size as integer) <= 100   
        ) pipechild  
         
        )  b 
                         */
                        paramList = new List<NpgsqlParameter>();

                        sqlCommand = new NpgsqlCommand();
                        sqlCommand.CommandText = sql.ToString();
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.Connection = db.Connection;

                        reader = sqlCommand.ExecuteReader();
                        string pipelineGeoJson = "";
                        if (reader.Read())
                        {
                            pipeItem.PipeAllGeoJson = reader["PipeAllGeoJson"].ToString();
                        }


                        //  list2 = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectPipeDto>(reader);
                        reader.Close();
                    }

                }

                list.AddRange(list2);

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

        public List<PwaRepaireWork_EffectPipeDto> GePipesEffectByShapeText2(string Shape)
        {
            string sql = string.Format("select * from getpipebyshape2('{0}')", Shape);
            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>();
            List<PwaRepaireWork_EffectPipeDto> list = new List<PwaRepaireWork_EffectPipeDto>();


            NpgsqlDataReader reader = null;
            NpgsqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = db.Connection;

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectPipeDto>(reader);

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

        public PWAMeterDto GeMeterByCustomerCode(string custcode,string areacode)
        {

            string sql = string.Format("select ST_AsText(wkb_geometry) ShapeText,pipe_id,CAST(custcode as text) custcode ,CAST(contracno as text) contracno"
+", cast(pin as text) pin, cast(custname as text) custname, cast(meterno as text) meterno, cast(mtrmkcode as text) mtrmkcode"
+", cast(metersize as text) metersize, cast(custaddr as text)custaddr, cast(pwa_code as text)  pwa_code"
 +" FROM oracle.r{0}_meter WHERE custcode = '{1}' ", areacode, custcode);
            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>();
           PWAMeterDto list = new PWAMeterDto();


            NpgsqlDataReader reader = null;
            NpgsqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = db.Connection;

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<PWAMeterDto>(reader).FirstOrDefault();
                if (list != null)
                {
                    list.Shape = Converting.ToShape(list.ShapeText);
                }

            }
            catch (Exception ex)
            {
                Pwa.FrameWork.Bal.LogBal.Log("GeMeterByCustomerCode", "", ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }



            return list;
        }


        public List<PWALeakPointHistoryDto> GetRepaireHistoryByPipe(string pipeCoordinate,string areacode)
        {

            StringBuilder sql = new StringBuilder();

            sql.Append("select to_char(repairdate,'dd/mm/yyyy HH24:mm') repairdate ");
            sql.Append(", repairtime");
            sql.Append(", locate ");
            sql.Append(", leakcause ");
            sql.Append(", leakdetail ");
            sql.Append(", leakShapeText ");
            sql.Append(", leakShapeGeoJson ");
            sql.AppendFormat(", ST_AsGeoJSON((select wkb_geometry from oracle.r{0}_pipe where ST_Intersects(wkb_geometry, ST_Buffer(LeakShape, 0.00001)) limit 1)) pipeShapeGeoJson", areacode);
            sql.Append(" from( ");
            sql.Append(" SELECT to_timestamp(repairdate || ' ' || repairtime, 'yymmddHH24MI') repairdate ");
            sql.Append(" , cast(repairtime as text) repairtime ");
            sql.Append(", cast(locate as text) locate ");
            sql.Append(", cast(leakcause as text) leakcause ");
            sql.Append(" , cast(leakdetail as text) leakdetail ");
            sql.Append(" , ST_AsText(wkb_geometry) leakShapeText ");
            sql.Append(", ST_AsGeoJSON(wkb_geometry) leakShapeGeoJson ");
            sql.Append(" ,wkb_geometry LeakShape ");

            sql.AppendFormat(" FROM oracle.r{0}_leakpoint ", areacode);
            sql.AppendFormat("where  ST_Intersects(wkb_geometry, ST_Buffer(ST_GeographyFromText('SRID=4326;' || '{0}'), 1))   order by repairdate desc  ) a where repairtime <> ''  ", pipeCoordinate);
            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>();
            List<PWALeakPointHistoryDto> list = new List<PWALeakPointHistoryDto>();


            NpgsqlDataReader reader = null;
            NpgsqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = sql.ToString();
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = db.Connection;

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<PWALeakPointHistoryDto>(reader).ToList();
              

            }
            catch (Exception ex)
            {
                Pwa.FrameWork.Bal.LogBal.Log("GeMeterByCustomerCode", "", ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }



            return list;
        }

        /*
         select to_char(repairdate,'dd/mm/yyyy HH24:mm') repairdate
,locate
,leakcause,leakdetail,PipeShapeText
from (
SELECT to_timestamp(repairdate ||' '|| repairtime,'yymmddHH24MI') repairdate
           , repairtime
          , cast(locate as text) locate
          , cast(leakcause  as text) leakcause
          , cast(leakdetail as text) leakdetail
        , ST_AsText(wkb_geometry) PipeShapeText
       , ST_AsGeoJSON(wkb_geometry) PipeShape
  FROM oracle.r3_leakpoint 
where  ST_Intersects(wkb_geometry, ST_Buffer(ST_GeographyFromText('SRID=4326;' || 'ฐ,'), 1))
  limit 1
  ) a

          */

        public PWAZoneDTO  GetZoneByWWCode(string wwcode)
        {
            PWAZoneDTO pwaZoneDto = new PWAZoneDTO();

            string sql = string.Format(" select  cast(zone as text) as zone, cast(region as text) as region, cast(pwacode as text) as  zonepwacode"
+ ", ST_AsText(wkb_geometry) shapetext "
 + ", ST_AsGeoJSON(ST_Transform(wkb_geometry, 4326), 15, 0)::json As shapegeojson "
 + "from pwa_office.zone WHERE pwacode = '{0}' ", wwcode);
            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>();

            NpgsqlDataReader reader = null;
            NpgsqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = db.Connection;

                reader = sqlCommand.ExecuteReader();

                pwaZoneDto = Dto.Utils.Converting.ConvertDataReaderToObjList<PWAZoneDTO>(reader).FirstOrDefault();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }



            return pwaZoneDto;

        }
        public PWABranchDTO GetBranchByBA(string bacode)
        {
            PWABranchDTO pwaBranchDto = new PWABranchDTO();
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select ");
            sqlStr.Append("cast(ba as text) as ba ");
            sqlStr.Append(", cast(pwa_id as text) as pwaid ");
            sqlStr.Append(", cast(pwa_code as text) as pwacode ");
            sqlStr.Append(", cast(name as text) as pwaname ");
            sqlStr.Append(", cast(pwa_addres as text) as pwaaddress ");
            sqlStr.Append(", cast(tel as text) as pwatel ");
            sqlStr.Append(", cast(zone as text) as zone ");
            sqlStr.Append(", cast(region as text) as region ");
            sqlStr.Append(", cast(latitude as text) as lat ");
            sqlStr.Append(", cast(longitude as text) as lng ");
            sqlStr.Append(", ST_AsText(wkb_geometry) shapetext ");
            sqlStr.Append(", ST_AsGeoJSON(ST_Transform(wkb_geometry, 4326), 15, 0)::json As shapegeojson ");
            sqlStr.AppendFormat("from pwa_office.pwa_office234 branch WHERE branch.ba = '{0}' ", bacode);

            List<NpgsqlParameter> paramList = new List<NpgsqlParameter>();

            NpgsqlDataReader reader = null;
            NpgsqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandText = sqlStr.ToString();
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = db.Connection;

                reader = sqlCommand.ExecuteReader();

                pwaBranchDto = Dto.Utils.Converting.ConvertDataReaderToObjList<PWABranchDTO>(reader).FirstOrDefault();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }



            return pwaBranchDto;
        }
    }
}
