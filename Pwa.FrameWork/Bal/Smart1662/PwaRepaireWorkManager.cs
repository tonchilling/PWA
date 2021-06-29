using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pwa.FrameWork.Bal.Data;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;

using System.Threading.Tasks;
using Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;
using Pwa.FrameWork.Dto;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto.Utils;
 using System.Web.Script.Serialization;
namespace Pwa.FrameWork.Bal.Smart1662
{
    public class PwaRepaireWorkManager: BaseBal
    {
        private PwaRepaireWorkResponsitory _Resp = RespositoryFactory.GetPwaRepaireWorkResponsitory(true);
        private IMeterRespository _RespMeter = RespositoryFactory.GetMeterResponsitory();
        private ISysBranchRespository  _RespBranch = RespositoryFactory.GetSysBranchRespository();

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



        public override bool Insert(object obj)
        {
            throw new NotImplementedException();
        }

        public PwaRepaireWorkHeaderDto Insert(PwaRepaireWorkHeaderDto obj)
        {
            PwaRepaireWorkHeaderDto value = null;
            SysBranch branch = null;
            try
            {
                branch = _RespBranch.GetByCode(obj.BranchId);
                obj.AreaCode = branch!=null ? branch.AreaCode :"";
                obj.WW_CODE = branch!=null ? branch.WW_CODE: "";
                obj.WorkingDate = Converting.DateOfyyyyMMdd(obj.WorkingDate);
                obj.NotificationDate = Converting.DateOfyyyyMMdd(obj.NotificationDate);
               // obj.Status = "1";
                if (obj.Survey != null)
                {
                    obj.Status = "6";
                    obj.Survey.SurveyDate = Converting.DateOfyyyyMMdd(obj.Survey.SurveyDate);

                 
                }


                try
                {
                    if (obj.Effect != null)
                    {

                        //Pipeline intersec
                        if (obj.Effect.ToolType == "3")
                        {
                            List<string> pipeIds = obj.Effect.PipeEffects.Select(
                                o => o.PipelineId
                                ).ToList();

                            obj.Effect.CustomerEffects = _RespMeter.GeCustomersEffectByPipeIds(obj.AreaCode, pipeIds, obj.WW_CODE);
                        }
                        else if (obj.Effect.ShapeText != null)
                        {
                            //    obj.Effect.CustomerEffects = _RespMeter.GeCustomersEffectByShapeText(obj.Effect.ShapeText, obj.AreaCode, obj.WW_CODE);
                            obj.Effect.CustomerEffects = _RespMeter.GeCustomersEffectByShapeGeoJsonText(obj.Effect.ShapeGeoJson, obj.AreaCode, obj.WW_CODE);
                        }
                        else if (obj.Effect.ToolType == "4")
                        {
                            obj.Effect.PipeEffects = null;
                        }



                                /*   if (obj.Effect.PipeEffects != null && obj.Effect.PipeEffects.Count > 0)
                                   {
                                       string[] LngLa = obj.Effect.PipeEffects[0].ShapeSnapeText.Trim().ToLower().Replace("point", "").Replace("(", "").Replace(")", "").Split(' ');
                                       obj.Survey.ShapeText = obj.Effect.PipeEffects[0].ShapeSnapeText;
                                       obj.Survey.Latitude = LngLa[1];
                                       obj.Survey.Longtitude = LngLa[0];
                                   }*/
                            }
                }
                catch (Exception ex)
                {
                    Log("PwaRepaireWorkManager.Insert.GIS", "", "error", ex);
                    throw new Exception("Cannot Insert :" + ex.Message);
                }
                finally { }

      

                    if (obj.OpenCase != null && obj.OpenCase.OpenDate!=null)
                {
                   obj.Status = "7";

                    obj.OpenCase.OpenDate = Converting.DateOfyyyyMMdd(obj.OpenCase.OpenDate);
                    obj.OpenCase.SurveyDate = Converting.DateOfyyyyMMdd(obj.OpenCase.SurveyDate);

                }

                if (obj.Process != null && obj.Process.FromProcessDate!=null)
                {
                    obj.Status = "8";
                    obj.Process.FromProcessDate= Converting.DateOfyyyyMMdd(obj.Process.FromProcessDate);
                    obj.Process.ToProcessDate = Converting.DateOfyyyyMMdd(obj.Process.ToProcessDate);
                    obj.Process.SurfaceFixedDate = Converting.DateOfyyyyMMdd(obj.Process.SurfaceFixedDate);

                }

                if (obj.CloseJob != null && obj.CloseJob.CloseDate!=null)
                {
                   obj.Status = "9";
                    obj.CloseJob.CloseDate = Converting.DateOfyyyyMMdd(obj.CloseJob.CloseDate);
                  

                }
                value = _Resp.Add(obj);
                if (obj.CloseJob != null && obj.CreatorEmail!="")
                {
                    var notifyer = new Pwa.FrameWork.Bal.Smart1662.IncidentNotify(Converting.ToInt(obj.RWId));
                    notifyer.SendNotifyEmail(obj.CreatorEmail);
                }
            }
            catch (Exception ex)
            {
                Log("PwaRepaireWorkManager.Insert", "", "error", ex);
                throw new Exception("Cannot Insert :" + ex.Message);
            }
            return value;
        }

        public  List<PwaRepaireWorkHeaderDto> GetAll()
        {
            List<PwaRepaireWorkHeaderDto> list = null; 

            try
            {

                list = _Resp.GetAll();

            }
            catch (Exception ex)
            {
                Log("PwaRepaireWorkManager.GetAll", "", "error", ex);
                throw new Exception("Cannot GetAll :" + ex.Message);
            }
            return list;
        }


        public PwaRepaireWorkHeaderDto GetById(string RwId)
        {
            PwaRepaireWorkHeaderDto  list = null;

            try
            {

                list = _Resp.GetById(RwId);
                if (list != null && list.Effect != null && list.Effect.CustomerEffects != null)
                {

                    foreach (PwaRepaireWork_EffectCustomerDto custEff in list.Effect.CustomerEffects)
                    {
                        custEff.Shape = Converting.ToShape(custEff.ShapeText);
                        custEff.ShapeGeoJson = new JavaScriptSerializer().Serialize(custEff.Shape);
                     
                    }
                

                    foreach (PwaRepaireWork_EffectPipeDto custEff in list.Effect.PipeEffects)
                    {
                        custEff.ShapeSnape= Converting.ToShape(custEff.ShapeSnapeText);
                        custEff.PipeShape = Converting.ToShape(custEff.PipeShapeText);
                        custEff.ShapeSnapGeoJson = new JavaScriptSerializer().Serialize(custEff.ShapeSnape);
                        custEff.PipeGeoJson = new JavaScriptSerializer().Serialize(custEff.PipeShape);
                    }
                }

            }
            catch (Exception ex)
            {
                Log("PwaRepaireWorkManager.GetById", "", "error", ex);
                throw new Exception("Cannot GetById :" + ex.Message);
            }
            return list;
        }

        public override bool Update(object obj)
        {
            throw new NotImplementedException();
        }


    }
}
