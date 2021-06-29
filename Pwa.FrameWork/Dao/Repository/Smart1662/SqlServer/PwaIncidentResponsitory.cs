using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using Pwa.FrameWork.Extension;
using Pwa.FrameWork.Dto.Smart1662.Incident;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Bal.Data;
using Pwa.FrameWork.Dto;

using System.Diagnostics;


namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class PwaIncidentResponsitory : IPwaIncidentRespository
    {
        protected Smart1662ADO db;

        public PwaIncidentResponsitory()
        {
         
            db = new Smart1662ADO();

        }

        public bool IsCallcenter { get; set; }

        public int Add(PwaIncident incident,List<PwaInformer> informs)
        {
            SqlTransaction trasaction = null;
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            int PwaIncidentID = 0;
            Smart1662Entities context;
            try
            {
            
                using (var scope = new TransactionScope())
            {
                context = new Smart1662Entities();
                context.PwaIncident.Add(incident);
                context.SaveChanges();

                    PwaIncidentID = incident.PwaIncidentID;

                    /*     informs.ForEach(i => i.PwaIncidentID = id);

                         context.PwaInformer.AddRange(informs);
                         context.SaveChanges();*/

                    if(incident.ProcessStage == 12)
                    {
                        context.PwaIncidentWorker.Add(new PwaIncidentWorker()
                        {
                            PwaIncidentID = PwaIncidentID,
                            WorkerID = int.Parse(incident.CreatedBy),
                            ProcessStep = (IsCallcenter)?1:2,
                            CreatedDate = DateTime.Now,
                            CreatedBy = 1

                        });
                    }

                    context.PwaIncidentWorker.Add(new PwaIncidentWorker() {
                        PwaIncidentID = PwaIncidentID,
                        WorkerID = int.Parse( incident.CreatedBy),
                        ProcessStep = incident.ProcessStage,
                        CreatedDate = DateTime.Now,
                        CreatedBy = 1

                    });
                    context.SaveChanges();



                scope.Complete();
                scope.Dispose();

            }

       

                db.OpenConnection();


                string sql = "sp_PwaInformer_Add";

                if (informs != null && informs.Count > 0)
                {
                    trasaction = db.Connection.BeginTransaction();
                    foreach (PwaInformer item in informs)
                    {
                       
                        paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@PwaIncidentID", PwaIncidentID));
                        paramList.Add(new SqlParameter("@CustomerID", item.CustomerID));
                        paramList.Add(new SqlParameter("@CustCode", item.CustCode));
                        paramList.Add(new SqlParameter("@Title", item.Title));
                        paramList.Add(new SqlParameter("@FirstName", item.FirstName));
                        paramList.Add(new SqlParameter("@LastName", item.LastName));
                        paramList.Add(new SqlParameter("@MeterNo", item.MeterNo));
                        paramList.Add(new SqlParameter("@Telephone", item.Telephone));
                        paramList.Add(new SqlParameter("@InformChannelID", item.InformChannelID));
                        paramList.Add(new SqlParameter("@InformReference", item.InformReference));
                        paramList.Add(new SqlParameter("@ProvinceID", item.ProvinceID));
                        paramList.Add(new SqlParameter("@DistrictID", item.DistrictID));
                        paramList.Add(new SqlParameter("@SubDistrictID", item.SubDistrictID));
                        paramList.Add(new SqlParameter("@Address", item.Address));
                        paramList.Add(new SqlParameter("@CreatedBy", item.CreatedBy));
                        paramList.Add(new SqlParameter("@Email", item.Email));
                        paramList.Add(new SqlParameter("@FaceBook", item.FaceBook));
                        paramList.Add(new SqlParameter("@ICustomerType", item.ICustomerType));
                        paramList.Add(new SqlParameter("@Address_no", item.Address_no));
                        paramList.Add(new SqlParameter("@Village_no", item.Village_no));
                        paramList.Add(new SqlParameter("@Building", item.Building));
                        paramList.Add(new SqlParameter("@Soi", item.Soi));
                        paramList.Add(new SqlParameter("@Road", item.Road));
                        sqlCommand = new SqlCommand();
                        sqlCommand.CommandText = sql;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Connection = db.Connection;
                        sqlCommand.Parameters.AddRange(paramList.ToArray());
                        sqlCommand.Transaction = trasaction;
                        sqlCommand.ExecuteNonQuery();
                    }

                    trasaction.Commit();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }

            return PwaIncidentID;
        }

        public void Update(PwaIncident incident, List<PwaInformer> informs)
        {
            bool statusChange = false;
            using (var scope = new TransactionScope())
            {
                Smart1662Entities context = new Smart1662Entities();
                var targetIncident = context.PwaIncident.Where(inc => inc.PwaIncidentID == incident.PwaIncidentID).First();
                
                statusChange = targetIncident.ProcessStage == incident.ProcessStage;


                targetIncident.PwaIncidentTypeID = incident.PwaIncidentTypeID;
                targetIncident.PwaIncidentGroupID = incident.PwaIncidentGroupID;
                targetIncident.CaseTitle = incident.CaseTitle;
                targetIncident.CaseTitleDetail = incident.CaseTitleDetail;
                targetIncident.CaseDetail = incident.CaseDetail;
                targetIncident.ResolvedDetail = incident.ResolvedDetail;
                targetIncident.Sla = incident.Sla;
                targetIncident.SlaDetail = incident.SlaDetail;

                targetIncident.CaseLatitude = incident.CaseLatitude;
                targetIncident.CaseLongtitude = incident.CaseLongtitude;
                targetIncident.ProcessStage = incident.ProcessStage;
                targetIncident.CustomerProcessStage = incident.ProcessStage;
                targetIncident.PwaParentIncidentID = "0";
                targetIncident.PwaInformReceiver = incident.PwaInformReceiver;


                targetIncident.ReceivedCaseDate = incident.ReceivedCaseDate;
                targetIncident.CompletedCaseDate = incident.CompletedCaseDate;
                targetIncident.PwsIncidentAddress = incident.PwsIncidentAddress;
                targetIncident.BracnchNo = incident.BracnchNo;
                targetIncident.Recorder = incident.Recorder;
                targetIncident.RecordDate = incident.RecordDate;
                targetIncident.UpdatedBA = incident.UpdatedBA;
                targetIncident.UpdatedBy = incident.UpdatedBy;
                targetIncident.UpdatedDate = incident.UpdatedDate;

                context.SaveChanges();

                informs.ForEach(inf =>
                {
                    var targetInform = context.PwaInformer.Where(i => i.InformerID == inf.InformerID).First();
                    targetInform.CustomerID = inf.CustomerID;
                    targetInform.CustCode = inf.CustCode;
                    targetInform.Title = inf.Title.ToString();
                    targetInform.FirstName = inf.FirstName;
                    targetInform.LastName = inf.LastName;
                    targetInform.MeterNo = inf.MeterNo;
                    targetInform.Telephone = inf.Telephone;
                    targetInform.InformChannelID = inf.InformChannelID;
                    targetInform.InformReference = inf.InformReference;

                    targetInform.ProvinceID = inf.ProvinceID;
                    targetInform.DistrictID = inf.DistrictID;
                    targetInform.SubDistrictID = inf.SubDistrictID;
                    targetInform.Address = inf.Address;

                    targetInform.Address_no = inf.Address_no;
                    targetInform.Village_no = inf.Village_no;
                    targetInform.Building = inf.Building;
                    targetInform.Soi = inf.Soi;
                    targetInform.Road = inf.Road;

                    context.SaveChanges();

                });

                if (statusChange)
                {
                    context.PwaIncidentWorker.Add(new PwaIncidentWorker()
                    {
                        PwaIncidentID = targetIncident.PwaIncidentID,
                        ProcessStep = targetIncident.ProcessStage,
                        WorkerID = int.Parse( targetIncident.Recorder),
                        CreatedBy = int.Parse( targetIncident.Recorder),
                        CreatedDate = DateTime.Now
                    });
                    context.SaveChanges();
                }



                scope.Complete();

            }
        }

        public void AddFiles(List<FileDto> Files)
        {
            SqlTransaction trasaction = null;
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {

                db.OpenConnection();


                string sql = "sp_PwaIncident_File_Add";

                if (Files != null && Files.Count > 0)
                {
                    trasaction = db.Connection.BeginTransaction();
                    foreach (FileDto filedto in Files)
                    {
                        paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@FileID", filedto.FileID));
                        paramList.Add(new SqlParameter("@PwaIncidentID", filedto.PwaIncidentID));
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

                    trasaction.Commit();
                }
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
        public void Update(PwaIncident incident, List<PwaInformer> informs, List<FileDto> files)
        {
            SqlTransaction trasaction = null;
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            bool statusChange = false;

            using (var scope = new TransactionScope())
            {
                Smart1662Entities context = new Smart1662Entities();
                var targetIncident = context.PwaIncident.Where(inc => inc.PwaIncidentID == incident.PwaIncidentID).First();

                statusChange = targetIncident.ProcessStage == incident.ProcessStage;


                targetIncident.PwaIncidentTypeID = incident.PwaIncidentTypeID;
             
                    targetIncident.PwaIncidentGroupID = incident.PwaIncidentGroupID;
                targetIncident.CaseTitle = incident.CaseTitle;
                targetIncident.CaseTitleDetail = incident.CaseTitleDetail;
                targetIncident.CaseDetail = incident.CaseDetail;
                targetIncident.ResolvedDetail = incident.ResolvedDetail;
                targetIncident.Sla = incident.Sla;
                targetIncident.SlaDetail = incident.SlaDetail;

                targetIncident.CaseLatitude = incident.CaseLatitude;
                targetIncident.CaseLongtitude = incident.CaseLongtitude;
                targetIncident.ProcessStage = incident.ProcessStage;
                targetIncident.CustomerProcessStage = incident.ProcessStage;
                targetIncident.PwaParentIncidentID = "0";
                targetIncident.PwaInformReceiver = incident.PwaInformReceiver;


                targetIncident.ReceivedCaseDate = incident.ReceivedCaseDate;
                targetIncident.CompletedCaseDate = incident.CompletedCaseDate;
                targetIncident.PwsIncidentAddress = incident.PwsIncidentAddress;
                targetIncident.BracnchNo = incident.BracnchNo;
                targetIncident.Recorder = incident.Recorder;
                targetIncident.RecordDate = incident.RecordDate;
                targetIncident.UpdatedBA = incident.UpdatedBA;
                targetIncident.UpdatedDate = incident.UpdatedDate;
                targetIncident.UpdatedBy = incident.UpdatedBy;

                targetIncident.AddressNo = incident.AddressNo;
                targetIncident.VillageNo = incident.VillageNo;
                targetIncident.VillageBuilding = incident.VillageBuilding;
                targetIncident.Soi = incident.Soi;
                targetIncident.Road = incident.Road;

                targetIncident.ProvinceID = incident.ProvinceID;
                targetIncident.DistrictID = incident.DistrictID;
                targetIncident.SubDistrictID = incident.SubDistrictID;

                targetIncident.NearLocate = incident.NearLocate;

                targetIncident.OverSlaId = incident.OverSlaId;
                targetIncident.OverSlaOther = incident.OverSlaOther;

                context.SaveChanges();

                /*    informs.ForEach(inf =>
                    {
                        var targetInform = context.PwaInformer.Where(i => i.InformerID == inf.InformerID).First();
                        targetInform.CustomerID = inf.CustomerID;
                        targetInform.CustCode = inf.CustCode;
                        targetInform.Title = inf.Title.ToString();
                        targetInform.FirstName = inf.FirstName;
                        targetInform.LastName = inf.LastName;
                        targetInform.MeterNo = inf.MeterNo;
                        targetInform.Telephone = inf.Telephone;
                        targetInform.InformChannelID = inf.InformChannelID;
                        targetInform.InformReference = inf.InformReference;

                        targetInform.ProvinceID = inf.ProvinceID;
                        targetInform.DistrictID = inf.DistrictID;
                        targetInform.SubDistrictID = inf.SubDistrictID;
                        targetInform.Address = inf.Address;

                        context.SaveChanges();

                    });

        */
                context.PwaIncidentWorker.Add(new PwaIncidentWorker()
                {
                    PwaIncidentID = targetIncident.PwaIncidentID,
                    ProcessStep = targetIncident.ProcessStage,
                    WorkerID = int.Parse(targetIncident.Recorder),
                    CreatedDate = DateTime.Now,
                    CreatedBy = int.Parse(targetIncident.Recorder)

                });

                context.SaveChanges();

                scope.Complete();




              
            }
            try
            {

                db.OpenConnection();
                trasaction = db.Connection.BeginTransaction();
                string sql = "sp_PwaInformer_Add";

                if (informs != null && informs.Count > 0)
                {
                   
                    foreach (PwaInformer item in informs)
                    {

                        paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@PwaIncidentID", incident.PwaIncidentID));
                        paramList.Add(new SqlParameter("@CustomerID", item.CustomerID));
                        paramList.Add(new SqlParameter("@CustCode", item.CustCode));
                        paramList.Add(new SqlParameter("@Title", item.Title));
                        paramList.Add(new SqlParameter("@FirstName", item.FirstName));
                        paramList.Add(new SqlParameter("@LastName", item.LastName));
                        paramList.Add(new SqlParameter("@MeterNo", item.MeterNo));
                        paramList.Add(new SqlParameter("@Telephone", item.Telephone));
                        paramList.Add(new SqlParameter("@InformChannelID", item.InformChannelID));
                        paramList.Add(new SqlParameter("@InformReference", item.InformReference));
                        paramList.Add(new SqlParameter("@ProvinceID", item.ProvinceID));
                        paramList.Add(new SqlParameter("@DistrictID", item.DistrictID));
                        paramList.Add(new SqlParameter("@SubDistrictID", item.SubDistrictID));
                        paramList.Add(new SqlParameter("@Address", item.Address));
                        paramList.Add(new SqlParameter("@CreatedBy", item.CreatedBy));
                        paramList.Add(new SqlParameter("@Email", item.Email));
                        paramList.Add(new SqlParameter("@FaceBook", item.FaceBook));
                        paramList.Add(new SqlParameter("@ICustomerType", item.ICustomerType));
                        paramList.Add(new SqlParameter("@Address_no", item.Address_no));
                        paramList.Add(new SqlParameter("@Village_no", item.Village_no));
                        paramList.Add(new SqlParameter("@Building", item.Building));
                        paramList.Add(new SqlParameter("@Soi", item.Soi));
                        paramList.Add(new SqlParameter("@Road", item.Road));
                        sqlCommand = new SqlCommand();
                        sqlCommand.CommandText = sql;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Connection = db.Connection;
                        sqlCommand.Parameters.AddRange(paramList.ToArray());
                        sqlCommand.Transaction = trasaction;
                        sqlCommand.ExecuteNonQuery();
                    }

                
                }

                 sql = "sp_PwaIncident_File_Add";

                if (files != null && files.Count > 0)
                {
               
                    foreach (FileDto filedto in files)
                    {
                        paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@FileID", filedto.FileID));
                        paramList.Add(new SqlParameter("@PwaIncidentID", incident.PwaIncidentID));
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

        }
        public List<VwIncidentList> GetIncidents()
        {
            using (var context = new Smart1662Entities())
            {
                return context.VwIncidentList.Where(i => i.Status == 1).ToList();
            }
        }

        public VwIncidentListV2 GetIncidentWithInfo(int incidentID)
        {
            using (var context = new Smart1662Entities())
            {
                return context.VwIncidentListV2.Where(i => i.PwaIncidentID == incidentID).FirstOrDefault();
            }
        }


        public List<VwIncidentListV2> GetIncidentsV2()
        {
            using (var context = new Smart1662Entities())
            {
                return context.VwIncidentListV2.Where(i => i.Status == 1).ToList();
            }
        }


        public List<IncidentDto> GetIncidents(string [] id,string status)
        {
            string sql = "sp_PwaIncident_GetByIds";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<IncidentDto> list = new List<IncidentDto>();


            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                paramList.Add(new SqlParameter("@PwaIncidentID", string.Join(",", id)));
                paramList.Add(new SqlParameter("@Status", status));
              

                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<IncidentDto>(reader);

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


        public Pwa.FrameWork.Bal.Data.IncidentEditInfo GetIncident(int incidentID)
        {
            Pwa.FrameWork.Bal.Data.IncidentEditInfo result = new Bal.Data.IncidentEditInfo();

           
            PwaInformer informer = null;
            List<FileDto> files = null;
            
            using (var context = new Smart1662Entities())
            {
                result.Incident = context.PwaIncident.Where(i => i.PwaIncidentID == incidentID).FirstOrDefault();
                informer = context.PwaInformer.Where(i => i.PwaIncidentID == incidentID).FirstOrDefault();
              
                files = context.PwaIncident_Files.Where(i => i.PwaIncidentID == incidentID).Select(file=>new FileDto() {
                FileID= file.Id.ToString(),
                PwaIncidentID= file.PwaIncidentID.ToString(),
                No= file.No.ToString(),
                UploadDate = file.UploadDate,
                FileName =file.FileName,
                FileSize= file.FileSize,
                HtmlFile = file.HtmlFile,
                FilePath =file.FilePath,
                FullPath=file.FullPath,
                Comment =file.Comment

                }).ToList();
                foreach (FileDto file in files)
                {
                    file.HtmlFile = string.Format("{0}?{1}", file.HtmlFile, DateTime.Now.ToString("yyMMddHHmmss"));
                }
            }

           

            result.Informers = new List<PwaInformer>();
            result.Informers.Add(informer);
            result.Files = files;


            return result;
        }

        public string GetNextIncidentNo()
        {
            System.Data.Entity.Core.Objects.ObjectParameter output = new System.Data.Entity.Core.Objects.ObjectParameter("NextIndex", typeof(string));
           
            int result;
            using (var context = new Smart1662Entities())
            {
                string sequenceCode = $"S{DateTime.Now.ToString("yyyyMMdd",new System.Globalization.CultureInfo("en-US"))}";
                result = context.NextSequence(sequenceCode, output);
            }

            return output.Value.ToString();
        }


        public PwaIncident GetLastIncident()
        {
            PwaIncident incident = null;
            using (var context = new Smart1662Entities())
            {
                string sequenceCode = $"S{DateTime.Now.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"))}";
                incident = context.PwaIncident.OrderByDescending(o => o.PwaIncidentID).FirstOrDefault();
            }

            return incident;

        }


        public List<IncidentDto> GetIncidents(IncidentDto item)
        {
            //string sql = "sp_GetIncident_GetByColumn";
            //string sql = "sp_GetIncident_SearchCallCenter";
            string sql = "sp_GetIncident_Filter";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<IncidentDto> list = new List<IncidentDto>();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                paramList.Add(new SqlParameter("@PwaIncidentNo", item.PwaIncidentNo));
                paramList.Add(new SqlParameter("@CustomerName", item.CustomerName));
                paramList.Add(new SqlParameter("@CustomerId", item.CustomerId));
                paramList.Add(new SqlParameter("@ChannelId", item.ChannelId));
                paramList.Add(new SqlParameter("@Status", item.Status));
                paramList.Add(new SqlParameter("@CaseDetail", item.CaseDetail));
                paramList.Add(new SqlParameter("@BranchNo", item.BranchNo));
                //paramList.Add(new SqlParameter("@ProcessStatus", item.StatusText));
                paramList.Add(new SqlParameter("@ProcessStatus", item.ProcessStage));
                paramList.Add(new SqlParameter("@PwsIncidentAddress", item.PwsIncidentAddress));
                paramList.Add(new SqlParameter("@RequestCategorySubject", item.RequestCategorySubject));
                   paramList.Add(new SqlParameter("@CaseLatitude", item.CaseLatitude));
                paramList.Add(new SqlParameter("@CaseLongtitude", item.CaseLongtitude));
                /*Debug.WriteLine("ReceivedCaseDate  " + item.ReceivedCaseDate);
                var compareDt = DateTime.Parse("1/1/0544 0:00:00");
                if (item.ReceivedCaseDate != compareDt)
                {
                //    paramList.Add(new SqlParameter("@ReceivedCaseDate", item.ReceivedCaseDate));
                    Debug.WriteLine("ReceivedCaseDate add");
                }

                if (item.CompletedCaseDate != compareDt)
                {
                   // paramList.Add(new SqlParameter("@CompletedCaseDate", item.CompletedCaseDate));
                }*/

                paramList.Add(new SqlParameter("@Telephone", item.Telephone));
                paramList.Add(new SqlParameter("@Email", item.Email));

                Debug.WriteLine("paramList Count " + paramList);
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<IncidentDto>(reader);
                Debug.WriteLine("IncidentDto Count " + list.Count);
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


        public List<IncidentDto> GetIncidentsByBranch(IncidentDto item)
        {
            //string sql = "sp_GetIncident_GetByColumn";
            //string sql = "sp_GetIncident_SearchCallCenter";
            string sql = "[sp_GetIncidentByBranch_Filter]";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<IncidentDto> list = new List<IncidentDto>();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                paramList.Add(new SqlParameter("@PwaIncidentNo", item.PwaIncidentNo));
                paramList.Add(new SqlParameter("@CustomerName", item.CustomerName));
                paramList.Add(new SqlParameter("@CustomerId", item.CustomerId));
                paramList.Add(new SqlParameter("@ChannelId", item.ChannelId));
                paramList.Add(new SqlParameter("@Status", item.Status));
                paramList.Add(new SqlParameter("@CaseDetail", item.CaseDetail));
                paramList.Add(new SqlParameter("@BranchNo", item.BranchNo));
                paramList.Add(new SqlParameter("@ProcessStatus", item.Status));
                paramList.Add(new SqlParameter("@PwsIncidentAddress", item.PwsIncidentAddress));
                paramList.Add(new SqlParameter("@RequestCategorySubject", item.RequestCategorySubject));
                paramList.Add(new SqlParameter("@FromReceivedCaseDate", item.ReceivedCaseDateText));
                paramList.Add(new SqlParameter("@ToReceivedCaseDate", item.CompletedCaseDateText));
                paramList.Add(new SqlParameter("@Telephone", item.Telephone));
                paramList.Add(new SqlParameter("@Email", item.Email));
          
                /*Debug.WriteLine("ReceivedCaseDate  " + item.ReceivedCaseDate);
                var compareDt = DateTime.Parse("1/1/0544 0:00:00");
                if (item.ReceivedCaseDate != compareDt)
                {
                //    paramList.Add(new SqlParameter("@ReceivedCaseDate", item.ReceivedCaseDate));
                    Debug.WriteLine("ReceivedCaseDate add");
                }

                if (item.CompletedCaseDate != compareDt)
                {
                   // paramList.Add(new SqlParameter("@CompletedCaseDate", item.CompletedCaseDate));
                }*/




                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<IncidentDto>(reader);
                Debug.WriteLine("IncidentDto Count " + list.Count);
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

        public List<CISProvince> GetProvince()
        {
            using (var context = new Smart1662Entities())
            {
                return context.CISProvince.Where(p=>p.ACTIVE=="T").ToList();
            }
        }

        public List<CISAmphure> GetDistricts(string provinceID)
        {
            using (var context = new Smart1662Entities())
            {
                return context.CISAmphure.Where(a => a.ProvinceId == provinceID && a.ACTIVE=="T").ToList();
            }
        }


        public List<CISAmphure> GetDistricts()
        {
            using (var context = new Smart1662Entities())
            {
                return context.CISAmphure.Where(a => a.ACTIVE == "T").ToList();
            }
        }


        public List<CISDistrinct> GetSubDistricts(string districtID)
        {
            using (var context = new Smart1662Entities())
            {
                return context.CISDistrinct.Where(d => d.AmphureId== districtID && d.ACTIVE=="T").ToList();
            }
        }

        public List<LocationDto> SearchSubDistricts(string name)
        {
            using (var context = new Smart1662Entities())
            {
                return (from dis in context.CIS_TB_MS_DISTRICT.Where(d => d.ACTIVE == "T")
                        join am in context.CIS_TB_MS_AMPHUR on dis.AMPHUR_CODE equals am.CODE
                        join pro in context.CIS_TB_MS_PROVINCE on am.PROVINCE_CODE equals pro.CODE
                        select new LocationDto
                        {
                            ProvinceID = pro.CODE,
                            DistrictID = am.CODE,
                            SubDistrictID = dis.CODE,
                            SubDistrictName = dis.NAME,
                            Value = dis.CODE,
                            Text = (dis.NAME + " > " + am.NAME + " > " + pro.NAME)
                        }).Where(o=>o.SubDistrictName.Contains(name)).OrderBy(x => x.SubDistrictName).ToList();
            }
        }


        public VwLocation GetSubDistrictInfo(string id)
        {
            using (var context = new Smart1662Entities())
            {
                return context.VwLocation.Where(d => d.DistrinctId == id).FirstOrDefault();
            }
        }

        public List<PWACustomerInfo> GetCustomers()
        {
            using (var context = new Smart1662Entities())
            {
                return context.PWACustomerInfo.Where(c => c.Active =="T" && c.CreateDate!=null).OrderBy(o=>o.CreateDate).Take(1000).ToList();
            }
        }
   


        public PWACustomerInfo GetCustomers(string customerCode)
        {
            using (var context = new Smart1662Entities())
            {
                return context.PWACustomerInfo.Where(c => c.CustCode == customerCode).FirstOrDefault();
            }
        }

        public List<LocationDto> GetSubDistricts()
        {
            using (var context = new Smart1662Entities())
            {
                /*
                 (from ep in dbContext.tbl_EntryPoint
                 join e in dbContext.tbl_Entry on ep.EID equals e.EID
                 join t in dbContext.tbl_Title on e.TID equals t.TID
                 where e.OwnerID == user.UID
                 select new {
                     UID = e.OwnerID,
                     TID = e.TID,
                     Title = t.Title,
                     EID = e.EID
                 }).Take(10);
                 */
                return (from dis in context.CIS_TB_MS_DISTRICT.Where(d => d.ACTIVE == "T")
                        join am in context.CIS_TB_MS_AMPHUR on dis.AMPHUR_CODE equals am.CODE
                        join pro in context.CIS_TB_MS_PROVINCE on am.PROVINCE_CODE equals pro.CODE
                        select new LocationDto
                        {
                            ProvinceID = pro.CODE,
                            DistrictID = am.CODE,
                            SubDistrictID = dis.CODE,
                            SubDistrictName = dis.NAME,
                            Value = dis.CODE,
                            Text = (dis.NAME + " > " + am.NAME + " > " + pro.NAME)
                        }).OrderBy(x=>x.SubDistrictName).ToList();

        }
        }

        public SubdistrictDetail GetSubDistrictDetail(string subDistcitID)
        {
            using (var context = new Smart1662Entities())
            {
                var subDis = context.CISDistrinct.Where(d => d.DistrinctId == subDistcitID &&  d.ACTIVE == "T" ).First();
                var district = context.CISAmphure.Where(a => a.AmphureId == subDis.AmphureId).First();
                var province = context.CISProvince.Where(p => p.ProvinceId == district.ProvinceId).First();

                return new SubdistrictDetail()
                {
                    SubDistrict = context.CISDistrinct.Where(d => d.DistrinctId == subDistcitID && d.ACTIVE == "T").First(),
                    District = context.CISAmphure.Where(a => a.AmphureId == subDis.AmphureId).First(),
                    Province = context.CISProvince.Where(p => p.ProvinceId == district.ProvinceId).First()
                };
            }
        }


        public CISAmphure GetDistrict(string subDistcitID)
        {
            using (var context = new Smart1662Entities())
            {
                var targetSubDistrict = context.CISDistrinct.Where(d => d.DistrinctId == subDistcitID).First();
                return context.CISAmphure.Where(a => a.AmphureId == targetSubDistrict.AmphureId && a.ACTIVE == "T").First();
            }
        }

        public CISProvince GetProvice(string distcitID)
        {
            using (var context = new Smart1662Entities())
            {
                var targetDistrict = context.CISAmphure.Where(d => d.AmphureId == distcitID).First();
                return context.CISProvince.Where(a => a.ProvinceId == distcitID && a.ACTIVE == "T").First();
            }
        }

        public List<SysTitle> GetTitles()
        {
            using (var context = new Smart1662Entities())
            {
                return context.SysTitle.ToList();
            }
        }

        public List<IncidentDto> SearchIncidents(string incidentNo,string subject,string informer ,string receiver ,DateTime? startDate,DateTime? endDate,string status )
        {

            List<IncidentDto>   incdents = null;
            using (var context = new Smart1662Entities())
            {
                IQueryable<VwSearchIncident> qeuery = context.VwSearchIncident;
                qeuery = qeuery.Where(q => q.Status == 1);
                if (!string.IsNullOrEmpty(incidentNo))
                {
                    qeuery = qeuery.Where(v => v.PwaIncidentNo == incidentNo);
                }

                var a = qeuery.ToList();

                if (!string.IsNullOrEmpty(subject))
                {
                    qeuery = qeuery.Where(v => v.RequestCategorySubject == subject);
                }

                if (!string.IsNullOrEmpty(informer))
                {
                    qeuery = qeuery.Where(v => (v.FirstName != null && informer.Contains(v.LastName)) || (v.FirstName != null && informer.Contains(v.LastName)));
                }

                //if (!string.IsNullOrEmpty(receiver))
                //{
                //    qeuery = context.VwSearchIncident.Where(v => v.PwaInformReceiver!=null &&  (v.FirstName != null && informer.Contains(v.LastName)) || (v.FirstName != null && informer.Contains(v.LastName)));
                //}

                if (startDate != null && startDate.HasValue && endDate != null && endDate.HasValue)
                {
                    qeuery = qeuery.Where(v => v.ReceivedCaseDate >= startDate.Value && v.ReceivedCaseDate <= endDate);
                }

             

                incdents = qeuery.Select( q => new IncidentDto {
                   PwaIncidentID=q.PwaIncidentID.ToString(),
                               id =q.PwaIncidentID.ToString(),
                    PwaIncidentNo=q.PwaIncidentNo,
                       PwaIncidentTypeID =q.PwaIncidentTypeID,
                       PwaIncidentGroupID =q.PwaIncidentGroupID,
                        CaseTitle =q.CaseTitle,
                       CaseDetail =q.CaseDetail,
                  Sla =q.Sla,
                    SlaDetail=q.SlaDetail,
                       ReceivedCaseDate=q.ReceivedCaseDate,
                        CompletedCaseDate =q.CompletedCaseDate,
                      CaseLatitude =q.CaseLatitude,
                       CaseLongtitude =q.CaseLongtitude,
                      ProcessStage =q.ProcessStage.ToString(),
                       PwsIncidentAddress =q.PwsIncidentAddress,
                       CustomerProcessStage =q.CustomerProcessStage.ToString(),
                      PwaParentIncidentID =q.PwaParentIncidentID,
                       Status =q.Status.ToString(),
                       StatusText=q.StatusText,
                       CreatedDate=q.CreatedDate,
                     CreatedBy=q.CreatedBy,
                    RequestType=q.RequestType,
                        RequestCategory =q.RequestCategory,
                         RequestCategorySubject =q.RequestCategorySubject,
                     CustomerName =q.FirstName +" "+q.LastName,
                       CustomerId =q.CustomerID.ToString(),
                       ChannelId=q.InformChannelID.ToString(),
                        BranchNo=q.BracnchNo,
                     Telephone=q.Telephone,
                   Email=q.Email
                    }).ToList();
                return incdents;
            }
        }

        public List<VwSearchIncident> SearchIncidentByCriteria(SearchIncidentDto dto)
        {
            using (var context = new Smart1662Entities())
            {
                context.Database.Log = generatedSQL =>
                 {
                     Console.WriteLine("==>> " + generatedSQL);
                 };
                DateTime end = dto.dtEndDate.AddDays(1).AddSeconds(-1);
                var result = context.VwSearchIncident.Where(i => i.ReceivedCaseDate >= dto.dtStartDate && i.ReceivedCaseDate <= end);


                if (dto.BranchNo != null && dto.BranchNo != "")
                {
                    result = result.Where(r => r.BracnchNo != null && r.BracnchNo == dto.BranchNo);
                }

                if (dto.ProvinceID != null && dto.ProvinceID != "" && dto.ProvinceID != "0")
                {
                    result = result.Where(r => r.ProvinceID != null && r.ProvinceID == dto.ProvinceID);
                }

                if (dto.Detail != null && dto.Detail != "")
                {
                    result = result.Where(r => r.CaseDetail != null && r.CaseDetail != "" && r.CaseDetail.Contains(dto.Detail));
                }

                if (dto.Area != null && dto.Area != "")
                {
                    result = result.Where(r => r.PwsIncidentAddress != null && r.PwsIncidentAddress != "" && r.PwsIncidentAddress.Contains(dto.Area));
                }



                if (dto.BranchNo != null && dto.BranchNo != "")
                {
                    result = result.Where(r => r.BracnchNo != null && r.BracnchNo == dto.BranchNo);
                }

                if (dto.Status != null && dto.Status != "0") 
                {
                    string[] tmp = dto.Status.Split(',');
                    int[] status = new int[tmp.Length];

                    for (int i = 0; i < tmp.Length; i++)
                    { status[i] = Convert.ToInt32(tmp[0]); }

                    result = result.Where(r => status.Contains(r.ProcessStage));
                }

                return result.ToList();

            }

            
        }


        public List<VwSearchIncident> SearchIncidentByBranch(string branchNo, string provinceID, string detail, string area, DateTime startDate, DateTime endDate)
        {
            using (var context = new Smart1662Entities())
            {
                var result = context.VwSearchIncident.Where(i => i.ReceivedCaseDate >= startDate && i.ReceivedCaseDate <= endDate);
                if (branchNo != null && branchNo != "")
                {
                    result = result.Where(r => r.BracnchNo != null && r.BracnchNo == branchNo);
                }

                if (provinceID != null && provinceID != "")
                {
                    result = result.Where(r => r.ProvinceID != null && r.ProvinceID == provinceID);
                }

                if (detail != null && detail != "")
                {
                    result = result.Where(r => r.CaseDetail != null && r.CaseDetail != "" && r.CaseDetail.Contains(detail));
                }

                if (area != null && area != "")
                {
                    result = result.Where(r => r.PwsIncidentAddress != null && r.PwsIncidentAddress != "" && r.PwsIncidentAddress.Contains(area));
                }



                if (branchNo != null)
                {
                    result = result.Where(r => r.BracnchNo != null && r.BracnchNo == branchNo);
                }

                return result.ToList();

            }


        }


        public List<VwSearchIncident> GetIncidentByBranch( )
        {
            using (var context = new Smart1662Entities())
            {
                return context.VwSearchIncident.Where(s=>s.CaseLatitude!=null && s.CaseLatitude!="" && s.CaseLongtitude!=null && s.CaseLongtitude!="").OrderByDescending(o => o.CreatedDate).Take(20).ToList();
            }
        }



        public List<FollowerDto> getFollowerByIncidentNo(string IncidentNo)
        {
            string sql = "sp_PwaIncidentFollower_GetByIncidentNo";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<FollowerDto> list = new List<FollowerDto>();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                paramList = new List<SqlParameter>();


                paramList.Add(new SqlParameter("@PwaIncidentNo", IncidentNo));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<FollowerDto>(reader);

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
        public List<FollowerDto> getFollowerById(string id)
        {
            string sql = "sp_PwaIncidentFollower_GetById";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<FollowerDto> list = new List<FollowerDto>();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                paramList = new List<SqlParameter>();


                paramList.Add(new SqlParameter("@PwaIncidentId", id));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<FollowerDto>(reader);

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
        public bool AddFollower(FollowerDto dto)
        {
            bool reusult = false;
            SqlTransaction trasaction = null;
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                string sql = "sp_PwaIncidentFollower_Add";
                trasaction = db.Connection.BeginTransaction();
                    
                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@PwaIncidentId", dto.PwaIncidentId));
                paramList.Add(new SqlParameter("@PwaIncidentNo", dto.PwaIncidentNo));
                paramList.Add(new SqlParameter("@ChannelId", dto.ChannelId));
                paramList.Add(new SqlParameter("@ChannelName", dto.ChannelName));
                paramList.Add(new SqlParameter("@Information", dto.Information));
                paramList.Add(new SqlParameter("@UpdatedBy", dto.UpdatedBy));
                
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
            return reusult;
        }

        public List<PwaIncidentFollower> GetFollowByIncidentID(int incidentID)
        {
            using (var context = new Smart1662Entities())
            {
                return context.PwaIncidentFollower.Where(i => i.PwaIncidentId == incidentID).ToList();
            }

        }

        public void UpdateProcessStatus(List<PwaIncident> incidents)
        {
            using (var context = new Smart1662Entities())
            {
                incidents.ForEach(updInc =>
                {
                    var target = context.PwaIncident.Where(inc => inc.PwaIncidentID == updInc.PwaIncidentID).FirstOrDefault();
                    target.ProcessStage = updInc.ProcessStage;
                    target.CustomerProcessStage = updInc.CustomerProcessStage;

                });
                context.SaveChanges();

                
            }
        }


        public void UpdateStatus(PwaIncident incident,PwaIncidentWorker workerStatus,PwaBranchReject reject)
        {
            using (var trans = new TransactionScope())
            {
                var context = new Smart1662Entities();

                var target = context.PwaIncident.Where(inc => inc.PwaIncidentID == incident.PwaIncidentID).FirstOrDefault();
                target.ProcessStage = incident.ProcessStage;
                target.CustomerProcessStage = incident.CustomerProcessStage;
                context.SaveChanges();

                context.PwaIncidentWorker.Add(workerStatus);
                context.SaveChanges();

                if (reject != null)
                {
                    context.PwaBranchReject.Add(reject);
                    context.SaveChanges();
                }
                trans.Complete();

            }


            
        }


        public List<VwPwaBranchReject> GetRejectHistory(int incidentNo)
        {
            using (var context = new Smart1662Entities())
            {
                return context.VwPwaBranchReject.Where(r => r.PwaIncidentID == incidentNo).ToList();

            }



        }

        public List<sp_GetEffectCustomer_Result> GetEffectCustomer(string customerCode)
        {
            using (var context = new Smart1662Entities())
            {
                return context.sp_GetEffectCustomer(customerCode).ToList();

            }



        }

        public PWAEmployee GetEmployee(string firstName,string lastName)
        {
            using (var context = new Smart1662Entities())
            {
                return context.PWAEmployee.Where(p => p.FNameTH != null && p.FNameTH == firstName &&
                                                    p.LNameTH != null && p.LNameTH == lastName
                ).FirstOrDefault();

            }
        }

        /*
        public PWAEmployee AddEmployee(string title,string firstName, string lastName)
        {
            using (var context = new Smart1662Entities())
            {
                return context.PWAEmployee.Add(new PWAEmployee()
                {

                });
            }
        }*/




    }
}
