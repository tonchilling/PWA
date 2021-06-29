using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;
 using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;
using Pwa.FrameWork.Dto.Smart1662.Incident;
namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class PwaRepaireWorkResponsitory : IPwaRepaireWorkRepository
    {
        private readonly Smart1662Entities dbEntity;
        protected Smart1662ADO db;
        List<SqlParameter> paramList = null;
        SqlDataReader reader = null;
        SqlCommand sqlCommand = null;
        SqlTransaction trasaction = null;
        string sql = "";
        public PwaRepaireWorkResponsitory()
        {
            dbEntity = new Smart1662Entities();
            db = new Smart1662ADO();

        }
        public PwaRepaireWorkHeaderDto Add(PwaRepaireWorkHeaderDto item)
        {

            try
            {
                sql = "sp_PwaRepaireWorkHeader_Add";

                #region  "Header sp_PwaRepaireWorkHeader_Add"
                paramList = new List<SqlParameter>();
                SqlParameter outPutVal = new SqlParameter("@RWId", SqlDbType.NVarChar);
                outPutVal.Direction = ParameterDirection.InputOutput;
                outPutVal.Size = 50;
                outPutVal.Value = item.RWId;
                paramList.Add(outPutVal);
                paramList.Add(new SqlParameter("@RWCode", item.RWCode));
                paramList.Add(new SqlParameter("@WorkingDate", item.WorkingDate));
                paramList.Add(new SqlParameter("@WorkingTime", item.WorkingTime));
                paramList.Add(new SqlParameter("@BranchId", item.BranchId));
                paramList.Add(new SqlParameter("@RequestType", item.RequestType));
                paramList.Add(new SqlParameter("@RequestCategory", item.RequestCategory));
                paramList.Add(new SqlParameter("@RequestCategorySubject", item.RequestCategorySubject));
                paramList.Add(new SqlParameter("@NotificationDate", item.NotificationDate));
                paramList.Add(new SqlParameter("@NotificationTime", item.NotificationTime));
                paramList.Add(new SqlParameter("@AccountId", item.AccountId));
                paramList.Add(new SqlParameter("@SLA", item.SLA));
                paramList.Add(new SqlParameter("@Reason", item.Reason));
                paramList.Add(new SqlParameter("@Status", item.Status));
             
                paramList.Add(new SqlParameter("@CreatedBy", item.CreatedBy));
                paramList.Add(new SqlParameter("@UpdatedBy", item.UpdatedBy));

                db.OpenConnection();
                 trasaction = db.Connection.BeginTransaction();

                //connect.Open();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());
                sqlCommand.Transaction = trasaction;
                sqlCommand.ExecuteNonQuery();
                item.RWId = outPutVal.Value.ToString();
                #endregion

                sql = "sp_PwaRepaireWork_Incident_Add";
                foreach (IncidentDto incident in item.Incidents)
                {
                    paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@RWId", item.RWId));
                    paramList.Add(new SqlParameter("@PwaIncidentID", incident.PwaIncidentID));
                    paramList.Add(new SqlParameter("@Status", item.Status));
                    paramList.Add(new SqlParameter("@CreatedBy", incident.CreatedBy));
                    paramList.Add(new SqlParameter("@UpdatedBy", incident.CreatedBy));
             
                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = db.Connection;
                    sqlCommand.Parameters.AddRange(paramList.ToArray());
                    sqlCommand.Transaction = trasaction;
                    sqlCommand.ExecuteNonQuery();
                }

                sql = "sp_PwaRepaireWork_Survey_Add";
                #region  "Tab1 - sp_PwaRepaireWork_Survey_Add"

                if (item.Survey != null)
                {
                    paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@RWId", item.RWId));
                    paramList.Add(new SqlParameter("@SurveyDate", item.Survey.SurveyDate));
                    paramList.Add(new SqlParameter("@SurveyTime", item.Survey.SurveyTime));
                    paramList.Add(new SqlParameter("@IsBroken", item.Survey.IsBroken));
                    paramList.Add(new SqlParameter("@SurveyAccountId", item.Survey.SurveyAccountId));
                    paramList.Add(new SqlParameter("@BrokenAppearance", item.Survey.BrokenAppearance));
                    paramList.Add(new SqlParameter("@PiplineType", item.Survey.PiplineType));
                    paramList.Add(new SqlParameter("@SurfaceAppearance", item.Survey.SurfaceAppearance));
                    paramList.Add(new SqlParameter("@PiplineSize", item.Survey.PiplineSize));
                    paramList.Add(new SqlParameter("@Reason", item.Survey.Reason));
                    paramList.Add(new SqlParameter("@Latitude", item.Survey.Latitude));
                    paramList.Add(new SqlParameter("@Longtitude", item.Survey.Longtitude));
                    paramList.Add(new SqlParameter("@ShapeText", item.Survey.ShapeText));
                    paramList.Add(new SqlParameter("@ShapeGeoJson", item.Survey.ShapeGeoJson));
                    paramList.Add(new SqlParameter("@Status", item.Survey.Status));
                    paramList.Add(new SqlParameter("@Location", item.Survey.Location));
                    paramList.Add(new SqlParameter("@CreatedBy", item.Survey.CreatedBy));
                    paramList.Add(new SqlParameter("@UpdatedBy", item.Survey.UpdatedBy));
                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = db.Connection;
                    sqlCommand.Parameters.AddRange(paramList.ToArray());
                    sqlCommand.Transaction = trasaction;
                    sqlCommand.ExecuteNonQuery();
                }
                #endregion


                sql = "sp_PwaRepaireWork_Effect_Add";
                #region  "Tab2 - sp_PwaRepaireWork_Effect_Add"

                if (item.Effect != null)
                {
                    paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@RWId", item.RWId));
                    paramList.Add(new SqlParameter("@ToolType", item.Effect.ToolType));
                    paramList.Add(new SqlParameter("@Buffer", item.Effect.Buffer));
                    paramList.Add(new SqlParameter("@Shape", null));
                    paramList.Add(new SqlParameter("@ShapeText", item.Effect.ShapeText));
                    paramList.Add(new SqlParameter("@ShapeGeoJson", item.Effect.ShapeGeoJson));
                    paramList.Add(new SqlParameter("@Status", item.Effect.Status));
                    paramList.Add(new SqlParameter("@CreatedBy", item.Effect.CreatedBy));
                    paramList.Add(new SqlParameter("@UpdatedBy", item.Effect.UpdatedBy));
                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = db.Connection;
                    sqlCommand.Parameters.AddRange(paramList.ToArray());
                    sqlCommand.Transaction = trasaction;
                    sqlCommand.ExecuteNonQuery();


                    if (item.Effect.CustomerEffects != null)
                    {
                        sql = "sp_PwaRepaireWork_Effect_Customers_Add";
                        foreach (PwaRepaireWork_EffectCustomerDto custItem in item.Effect.CustomerEffects)
                        {
                            paramList = new List<SqlParameter>();
                            paramList.Add(new SqlParameter("@EffectId", item.Effect.EffectId));
                            paramList.Add(new SqlParameter("@RWId", item.RWId));
                            paramList.Add(new SqlParameter("@CustomerCode", custItem.CustCode));
                            paramList.Add(new SqlParameter("@ShapeText", custItem.ShapeText));
                            paramList.Add(new SqlParameter("@Status", custItem.Status));
                            paramList.Add(new SqlParameter("@CreatedBy", custItem.CreatedBy));
                            paramList.Add(new SqlParameter("@UpdatedBy", custItem.UpdatedBy));
                            /*
                             * 
                             *  @EffectId nvarchar(20)
             ,@CustomerCode nvarchar(20)
             ,@Latitude nvarchar(20)
             ,@Longtitude nvarchar(20)
             ,@ShapeText nvarchar(20)
             ,@Status nvarchar(20)
             ,@CreatedBy nvarchar(20)
             ,@UpdatedBy nvarchar(20)
                             * */
                            sqlCommand = new SqlCommand();
                            sqlCommand.CommandText = sql;
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.Connection = db.Connection;
                            sqlCommand.Parameters.AddRange(paramList.ToArray());
                            sqlCommand.Transaction = trasaction;
                            sqlCommand.ExecuteNonQuery();
                        }
                    }

                    if (item.Effect.PipeEffects != null)
                    {
                        sql = "sp_PwaRepaireWork_Effect_Pipelines_Add";
                        foreach (PwaRepaireWork_EffectPipeDto pipeItem in item.Effect.PipeEffects)
                        {
                            paramList = new List<SqlParameter>();
                            paramList.Add(new SqlParameter("@EffectId", item.Effect.EffectId));
                            paramList.Add(new SqlParameter("@RWId", item.RWId));
                            paramList.Add(new SqlParameter("@PipelineId", pipeItem.PipelineId));
                            paramList.Add(new SqlParameter("@PipeShapeText", pipeItem.PipeShapeText));
                            paramList.Add(new SqlParameter("@PipeGeoJson", pipeItem.PipeGeoJson));
                            paramList.Add(new SqlParameter("@ProjectNo", pipeItem.ProjectNo));
                            paramList.Add(new SqlParameter("@PipeType", pipeItem.PipeType));
                            paramList.Add(new SqlParameter("@PipeSize", pipeItem.PipeSize));
                            paramList.Add(new SqlParameter("@Locate", pipeItem.Locate));
                            paramList.Add(new SqlParameter("@PwaCode", pipeItem.PwaCode));
                            paramList.Add(new SqlParameter("@longs", pipeItem.longs));
                            paramList.Add(new SqlParameter("@depth", pipeItem.depth));
                            paramList.Add(new SqlParameter("@yearinstall", pipeItem.yearinstall));
                            paramList.Add(new SqlParameter("@remark", pipeItem.remark));
                            paramList.Add(new SqlParameter("@pipemain", pipeItem.pipemain));
                            paramList.Add(new SqlParameter("@Status", pipeItem.Status));
                            paramList.Add(new SqlParameter("@CreatedBy", pipeItem.CreatedBy));
                            paramList.Add(new SqlParameter("@UpdatedBy", pipeItem.UpdatedBy));
                            /*
                             * 
                             * @EffectId nvarchar(50)
           ,@RWId nvarchar(50)
           ,@PipelineId nvarchar(50)
           ,@Latitude nvarchar(50)
           ,@Longtitude nvarchar(50)
           ,@ProjectNo nvarchar(50)
           ,@PipeSize nvarchar(50)
           ,@Locate nvarchar(600)
           ,@PwaCode nvarchar(50)
           ,@ShapeText nvarchar(200)
           ,@Status nvarchar(50)
           ,@CreatedBy nvarchar(50)
           ,@UpdatedBy nvarchar(50)
                             * */
                            sqlCommand = new SqlCommand();
                            sqlCommand.CommandText = sql;
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.Connection = db.Connection;
                            sqlCommand.Parameters.AddRange(paramList.ToArray());
                            sqlCommand.Transaction = trasaction;
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                else if(item.RWId!=null) {
                    sql = "sp_PwaRepaireWork_Effect_Clear";
                    paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@RWId", item.RWId));
                 
                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = db.Connection;
                    sqlCommand.Parameters.AddRange(paramList.ToArray());
                    sqlCommand.Transaction = trasaction;
                    sqlCommand.ExecuteNonQuery();
                }
                #endregion

                sql = "sp_PwaRepaireWork_OpenCase_Add";
                #region  "Tab3 - sp_PwaRepaireWork_OpenCase_Add"
                if (item.OpenCase != null)
                {
                    paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@RWId", item.RWId));
                    paramList.Add(new SqlParameter("@OpenDate", item.OpenCase.OpenDate));
                    paramList.Add(new SqlParameter("@OpenTime", item.OpenCase.OpenTime));
                    paramList.Add(new SqlParameter("@IsUrgent", item.OpenCase.IsUrgent));
                    paramList.Add(new SqlParameter("@IsOutSource", item.OpenCase.IsOutSource));
                    paramList.Add(new SqlParameter("@OpenAccountId", item.OpenCase.OpenAccountId));
                    paramList.Add(new SqlParameter("@ResponAccountId", item.OpenCase.ResponAccountId));
                    paramList.Add(new SqlParameter("@SuperAccountId", item.OpenCase.SuperAccountId));
                    paramList.Add(new SqlParameter("@IsBroken", item.OpenCase.IsBroken));
                    paramList.Add(new SqlParameter("@SurveyDate", item.OpenCase.SurveyDate));
                    paramList.Add(new SqlParameter("@SurveyTime", item.OpenCase.SurveyTime));
                    paramList.Add(new SqlParameter("@SurveyAccountId", item.OpenCase.SurveyAccountId));
                    paramList.Add(new SqlParameter("@BrokenAppearance", item.OpenCase.BrokenAppearance));
                    paramList.Add(new SqlParameter("@PiplineType", item.OpenCase.PiplineType));
                    paramList.Add(new SqlParameter("@PiplineSize", item.OpenCase.PiplineSize));
                    paramList.Add(new SqlParameter("@SurfaceAppearance", item.OpenCase.SurfaceAppearance));
                    paramList.Add(new SqlParameter("@Location", item.OpenCase.Location));
                    paramList.Add(new SqlParameter("@Comment", item.OpenCase.Comment));
                    paramList.Add(new SqlParameter("@TeamId", item.OpenCase.TeamId));
                    paramList.Add(new SqlParameter("@Status", item.OpenCase.Status));
                    paramList.Add(new SqlParameter("@CreatedBy", item.OpenCase.CreatedBy));
                    paramList.Add(new SqlParameter("@UpdatedBy", item.OpenCase.UpdatedBy));

                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = db.Connection;
                    sqlCommand.Parameters.AddRange(paramList.ToArray());
                    sqlCommand.Transaction = trasaction;
                    sqlCommand.ExecuteNonQuery();
                }
                #endregion


                #region  "Tab3 - sp_PwaRepaireWork_Items_Delete"
                sql = "sp_PwaRepaireWork_Items_Delete";
                if (item.Items != null && item.Items != null)
                {
                    paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@RWId", item.RWId));

                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = db.Connection;
                    sqlCommand.Parameters.AddRange(paramList.ToArray());
                    sqlCommand.Transaction = trasaction;
                    sqlCommand.ExecuteNonQuery();


                    sql = "sp_PwaRepaireWork_Items_Add";
                    foreach (PwaRepaireWork_ItemDto itemClose in item.Items)
                    {
                        paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@RWId", item.RWId));
                        paramList.Add(new SqlParameter("@No", itemClose.No));
                        paramList.Add(new SqlParameter("@ItemId", itemClose.ItemId));
                        paramList.Add(new SqlParameter("@itemName", itemClose.ItemName));
                        paramList.Add(new SqlParameter("@UnitId", itemClose.UnitId));
                        paramList.Add(new SqlParameter("@UnitName", itemClose.UnitName));
                        paramList.Add(new SqlParameter("@Qty", itemClose.Qty));
                        paramList.Add(new SqlParameter("@Price", itemClose.Price));
                        paramList.Add(new SqlParameter("@CurrentPrice", itemClose.CurrentPrice));
                        paramList.Add(new SqlParameter("@Total", itemClose.Total));
                        paramList.Add(new SqlParameter("@CreatedBy", itemClose.CreatedBy));
                        paramList.Add(new SqlParameter("@UpdatedBy", itemClose.UpdateBy));
                        paramList.Add(new SqlParameter("@Status", itemClose.Status));
                        sqlCommand = new SqlCommand();
                        sqlCommand.CommandText = sql;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Connection = db.Connection;
                        sqlCommand.Parameters.AddRange(paramList.ToArray());
                        sqlCommand.Transaction = trasaction;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                #endregion

                sql = "sp_PwaRepaireWork_Process_Add";
                #region  "Tab4 - sp_PwaRepaireWork_Process_Add"

                if (item.Process != null)
                {
                    paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@RWId", item.RWId));
                    paramList.Add(new SqlParameter("@FromProcessDate", item.Process.FromProcessDate));
                    paramList.Add(new SqlParameter("@FromProcessTime", item.Process.FromProcessTime));
                    paramList.Add(new SqlParameter("@ToProcessDate", item.Process.ToProcessDate));
                    paramList.Add(new SqlParameter("@ToProcessTime", item.Process.ToProcessTime));
                    paramList.Add(new SqlParameter("@SurfaceFixedDate", item.Process.SurfaceFixedDate));
                    paramList.Add(new SqlParameter("@SurfaceFixedTime", item.Process.SurfaceFixedTime));
                    paramList.Add(new SqlParameter("@SuperAccountId", item.Process.SuperAccountId));
                    paramList.Add(new SqlParameter("@BrokenAppearance", item.Process.BrokenAppearance));
                    paramList.Add(new SqlParameter("@SurfaceAppearance", item.Process.SurfaceAppearance));
                    paramList.Add(new SqlParameter("@ToolType", item.Process.ToolType));
                    paramList.Add(new SqlParameter("@HoleWidth", item.Process.HoleWidth));
                    paramList.Add(new SqlParameter("@HoleLength", item.Process.HoleLength));
                    paramList.Add(new SqlParameter("@HoleDepth", item.Process.HoleDepth));
                    paramList.Add(new SqlParameter("@IsOutSource", item.Process.IsOutSource));
                    paramList.Add(new SqlParameter("@IsSuccess", item.Process.IsSuccess));
                    paramList.Add(new SqlParameter("@Comment", item.Process.Comment));
                    paramList.Add(new SqlParameter("@PiplineSize", item.Process.PiplineSize));
                    paramList.Add(new SqlParameter("@PiplineType", item.Process.PiplineType));
                    paramList.Add(new SqlParameter("@IsNotGIS", item.Process.IsNotGIS));
                    paramList.Add(new SqlParameter("@RepaireAccountId", item.Process.RepaireAccountId));
                    paramList.Add(new SqlParameter("@TeamId", item.Process.TeamId));
                    paramList.Add(new SqlParameter("@Status", item.Process.Status));
                    paramList.Add(new SqlParameter("@CreatedBy", item.Process.CreatedBy));
                    paramList.Add(new SqlParameter("@UpdatedBy", item.Process.UpdatedBy));

                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = db.Connection;
                    sqlCommand.Parameters.AddRange(paramList.ToArray());
                    sqlCommand.Transaction = trasaction;
                    sqlCommand.ExecuteNonQuery();
                }


                #region  "Tab4 - sp_PwaRepaireWork_Process_Add"
                sql = "sp_PwaRepaireWork_Process_File_Delete";
                if (item.Process != null && item.DeleteProcessFiles != null)
                {
                    foreach (FileDto filedto in item.DeleteProcessFiles)
                    {
                        paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@FileID", filedto.FileID));
                        paramList.Add(new SqlParameter("@RWId", item.RWId));
                   
                        sqlCommand = new SqlCommand();
                        sqlCommand.CommandText = sql;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Connection = db.Connection;
                        sqlCommand.Parameters.AddRange(paramList.ToArray());
                        sqlCommand.Transaction = trasaction;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                        sql = "sp_PwaRepaireWork_Process_File_Add";

                if (item.Process!=null && item.Process.Files != null && item.Process.Files.Count > 0)
                {
                    foreach (FileDto filedto in item.Process.Files)
                    {
                        paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@FileID", filedto.FileID));
                        paramList.Add(new SqlParameter("@RWId", item.RWId));
                        paramList.Add(new SqlParameter("@No", filedto.No));
                        paramList.Add(new SqlParameter("@UploadDate", filedto.UploadDate));
                        paramList.Add(new SqlParameter("@FilePath", filedto.FilePath));
                        paramList.Add(new SqlParameter("@FullPath", filedto.FullPath));
                        paramList.Add(new SqlParameter("@HtmlFile", filedto.HtmlFile));
                        paramList.Add(new SqlParameter("@FileName", filedto.FileName));
                        paramList.Add(new SqlParameter("@FileSize", filedto.FileSize));
                        paramList.Add(new SqlParameter("@Comment", filedto.Comment));
                        paramList.Add(new SqlParameter("@Status", filedto.Status));
                        paramList.Add(new SqlParameter("@CreatedBy", filedto.CreatedBy));
                        sqlCommand = new SqlCommand();
                        sqlCommand.CommandText = sql;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Connection = db.Connection;
                        sqlCommand.Parameters.AddRange(paramList.ToArray());
                        sqlCommand.Transaction = trasaction;
                        sqlCommand.ExecuteNonQuery();
                    }
             

                    /*
       @RWId  nvarchar(10)
           ,@No nvarchar(10)
           ,@UploadDate nvarchar(10)
           ,@FilePath nvarchar(200)
           ,@FullPath nvarchar(200)
           ,@FileName nvarchar(200)
           ,@FileSize nvarchar(10)
           ,@Comment nvarchar(200)
           ,@Status nvarchar(10)
		   ,@CreatedBy  nvarchar(50)
                      */
                }
                #endregion
                #endregion

                sql = "sp_PwaRepaireWork_CloseJob_Add";
                #region  "Tab5 - sp_PwaRepaireWork_CloseJob_Add"

                if (item.CloseJob != null)
                {
                  
                    paramList = new List<SqlParameter>();

                     outPutVal = new SqlParameter("@CloseJobId", SqlDbType.NVarChar);
                    outPutVal.Direction = ParameterDirection.InputOutput;
                    outPutVal.Size = 50;
                    outPutVal.Value = item.CloseJob.CloseJobId;
                    paramList.Add(outPutVal);
                    paramList.Add(new SqlParameter("@RWId", item.RWId));
                    paramList.Add(new SqlParameter("@CloseJobNumber", item.CloseJob.CloseJobNumber));
                    paramList.Add(new SqlParameter("@CloseDate", item.CloseJob.CloseDate));
                    paramList.Add(new SqlParameter("@CloseTime", item.CloseJob.CloseTime));
                    paramList.Add(new SqlParameter("@TemplateId", item.CloseJob.TemplateId));
                    paramList.Add(new SqlParameter("@AccountId", item.CloseJob.AccountId));
                    paramList.Add(new SqlParameter("@DocumentId", item.CloseJob.DocumentId));
                    paramList.Add(new SqlParameter("@Comment", item.CloseJob.Comment));
                    paramList.Add(new SqlParameter("@Status", item.Process.Status));
                    paramList.Add(new SqlParameter("@CreatedBy", item.Process.CreatedBy));
                    paramList.Add(new SqlParameter("@UpdatedBy", item.Process.UpdatedBy));

                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = db.Connection;
                    sqlCommand.Parameters.AddRange(paramList.ToArray());
                    sqlCommand.Transaction = trasaction;
                    sqlCommand.ExecuteNonQuery();
                    item.CloseJob.CloseJobId = outPutVal.Value.ToString();
                }
                #region  "Tab4 - sp_PwaRepaireWork_CloseJob_DeleteFile"
                sql = "sp_PwaRepaireWork_CloseJob_File_Delete";
                if (item.Process != null && item.DeleteCloseFiles != null)
                {
                    foreach (FileDto filedto in item.DeleteCloseFiles)
                    {
                        paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@FileID", filedto.FileID));
                        paramList.Add(new SqlParameter("@RWId", item.RWId));

                        sqlCommand = new SqlCommand();
                        sqlCommand.CommandText = sql;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Connection = db.Connection;
                        sqlCommand.Parameters.AddRange(paramList.ToArray());
                        sqlCommand.Transaction = trasaction;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                sql = "sp_PwaRepaireWork_CloseJob_File_Add";

                if (item.CloseJob != null && item.CloseJob.Files != null && item.CloseJob.Files.Count > 0)
                {
                    foreach (FileDto filedto in item.CloseJob.Files)
                    {
                        paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@FileID", filedto.FileID));
                        paramList.Add(new SqlParameter("@RWId", item.RWId));
                        paramList.Add(new SqlParameter("@No", filedto.No));
                        paramList.Add(new SqlParameter("@UploadDate", filedto.UploadDate));
                        paramList.Add(new SqlParameter("@FilePath", filedto.FilePath));
                        paramList.Add(new SqlParameter("@FullPath", filedto.FullPath));
                        paramList.Add(new SqlParameter("@HtmlFile", filedto.HtmlFile));
                        paramList.Add(new SqlParameter("@FileName", filedto.FileName));
                        paramList.Add(new SqlParameter("@FileSize", filedto.FileSize));
                        paramList.Add(new SqlParameter("@Comment", filedto.Comment));
                        paramList.Add(new SqlParameter("@Status", filedto.Status));
                        paramList.Add(new SqlParameter("@CreatedBy", filedto.CreatedBy));
                        sqlCommand = new SqlCommand();
                        sqlCommand.CommandText = sql;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Connection = db.Connection;
                        sqlCommand.Parameters.AddRange(paramList.ToArray());
                        sqlCommand.Transaction = trasaction;
                        sqlCommand.ExecuteNonQuery();
                    }


                }
                #endregion


                #endregion


                sql = "sp_PwaRepaireWorkHeader_UpdateIncidents";
                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@RWId", item.RWId));
                paramList.Add(new SqlParameter("@Status", item.Status));
                paramList.Add(new SqlParameter("@CreatedBy", item.CreatedBy));

                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());
                sqlCommand.Transaction = trasaction;
                sqlCommand.ExecuteNonQuery();
                
                trasaction.Commit();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }



            return item;
        }

        public Task Update(PwaRepaireWorkHeaderDto data)
        {
            throw new NotImplementedException();
        }

        public Task Delete(PwaRepaireWorkHeaderDto data)
        {
            throw new NotImplementedException();
        }

        public PwaRepaireWorkHeaderDto GetById(int id)
        {
            throw new NotImplementedException();
            /* using (Smart1662Entities context = new Smart1662Entities())
             {
                 var target = context.SysChannel.FirstOrDefault(c=>c.ChannelId==id);
                 return target;
             }*/
        }
      
        public List<PwaRepaireWorkHeaderDto> GetAll()
        {
            string sql = "sp_PwaRepaireWorkHeader_GetAll";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<PwaRepaireWorkHeaderDto> list = new List<PwaRepaireWorkHeaderDto>();
  

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWorkHeaderDto>(reader);

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
        public PwaRepaireWorkHeaderDto GetById(string RwId)
        {
            string sql = "sp_PwaRepaireWorkHeader_Gets";
            List<SqlParameter> paramList = new List<SqlParameter>();
            PwaRepaireWorkHeaderDto result = null;


             SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {
                paramList = new List<SqlParameter>();


                paramList.Add(new SqlParameter("@RWId", RwId));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                result = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWorkHeaderDto>(reader).FirstOrDefault();
                reader.NextResult();

                result.Incidents = Dto.Utils.Converting.ConvertDataReaderToObjList<IncidentDto>(reader);
                reader.NextResult();
                result.Survey = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_SurveyDto>(reader).FirstOrDefault();
                reader.NextResult();

                result.Effect = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectDto>(reader).FirstOrDefault();
                reader.NextResult();

                if (result.Effect != null)
                {
                    result.Effect.CustomerEffects = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectCustomerDto>(reader);
                    reader.NextResult();

                    result.Effect.CustomerAddrEffects = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectCustomerAddrDto>(reader);
                    reader.NextResult();


                    result.Effect.PipeEffects = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_EffectPipeDto>(reader);
                    reader.NextResult();
                }
                else {
                    reader.NextResult();
                    reader.NextResult();
                }
                result.OpenCase = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_OpenCaseDto>(reader).FirstOrDefault();
                reader.NextResult();
                result.Process = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_ProcessDto>(reader).FirstOrDefault();
                reader.NextResult();

                if (result.Process!=null)
                result.Process.Files = Dto.Utils.Converting.ConvertDataReaderToObjList<FileDto>(reader).ToList();

                reader.NextResult();
                result.CloseJob = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_CloseJobDto>(reader).FirstOrDefault();

                reader.NextResult();
          
                    result.Items = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_ItemDto>(reader).ToList();

             
                    reader.NextResult();
                if (result.CloseJob != null)
                    result.CloseJob.Files = Dto.Utils.Converting.ConvertDataReaderToObjList<FileDto>(reader).ToList();


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
