using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pwa.FrameWork.Bal.PwaSystem;
using Pwa.FrameWork.Extension;
using Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer;
using Pwa.FrameWork.Dao.EF.Smart1662;
using System.Data;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Bal.Smart1662
{
    public class ReportManager : BaseBal
    {
        ReportRespository _responsitory = null;
        DataSet ds = null;

        public DataSet GetGUI001(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI001(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI001", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }

        public DataSet GetGUI002(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI002(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI001", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }

        public DataSet GetGUI004(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI004(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI004", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }
        public DataSet GetGUI006(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI006(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI006", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }
        public DataSet GetGUI007(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI007(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI007", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }
        public DataSet GetGUI008(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI008(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI008", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }
        public DataSet GetGUI009(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI009(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI009", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }
        public DataSet GetGUI010(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI010(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI010", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }
        public DataSet GetGUI011(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI011(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI011", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }

        public DataSet GetGUI012(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI012(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI012", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }

        public DataSet GetGUI013(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI013(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI013", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }

        public DataSet GetGUI015(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI015(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI015", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }
        public DataSet GetGUI016(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI016(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI016", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }
        public DataSet GetGUI018(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI018(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI018", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }
        public DataSet GetGUI019(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI019(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI019", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }
        public DataSet GetGUI020(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI020(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI020", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }

        public DataSet GetGUI021(CriterialDto criterialDto)
        {

            _responsitory = new ReportRespository();
            try
            {

                ds = _responsitory.GetGUI021(criterialDto);
            }
            catch (Exception ex)
            {
                Log("GetGUI020", criterialDto.CreatedBy, ex.Message);
            }

            return ds;
        }

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

        public override bool Update(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
