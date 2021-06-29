using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer;
using Pwa.FrameWork.Dao.Repository.Smart1662.Postgres;
namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public class RespositoryFactory
    {
        public static IIncidentRepository GetIncidentRepository()
        {
            return new Incident.IncidentRepository();
        }

        public static ISysChannelRespository GetSysChannelRespository()
        {
            return new SqlServer.SysChannelResponsitory();
        }

        public static IFormRespository GetFormRespository()
        {
            return new SqlServer.FormResponsitory();
        }
        public static ISysRequestTypeRespository GetSysRequestTypeRespository()
        {
            return new SqlServer.SysRequestTypeResponsitory();
        }
        public static ISysRequestCategoryResponsitory GetSysRequestCategoryResponsitory()
        {
            return new SqlServer.SysRequestCategoryResponsitory();
        }
        public static ISysRequestCategorySubjectResponsitory GetSysRequestCategorySubjectResponsitory()
        {
            return new SqlServer.SysRequestCategorySubjectResponsitory();
        }
        public static ISysAreaRespository GetSysAreaRespository()
        {
            return new SqlServer.SysAreaResponsitory();
        }
        public static SysMenuResponsitory GetSysMenuRepository(bool sqlserver)
        {
            return new SqlServer.SysMenuResponsitory();
        }

        public static SysRolPermissionReponsitory GetSysRolPermissionRepository(bool sqlserver)
        {
            return new SqlServer.SysRolPermissionReponsitory();
        }


        public static SysAccountReponsitory GetSysAccountReponsitory(bool sqlserver)
        {
            return new SqlServer.SysAccountReponsitory();
        }

        
             public static PwaRepaireWorkResponsitory GetPwaRepaireWorkResponsitory(bool sqlserver)
        {
            return new SqlServer.PwaRepaireWorkResponsitory();
        }

        /*
        public static MeterRespository GetMeterResponsitory(bool sqlserver)
        {
            return new Postgres.MeterRespository(true);
        }*/

        public static IPWACustomerTypeRespository GetCustomerTypeRespository()
        {
            return new SqlServer.PWACustomerTypeRespository();
        }

        public static IPwaIncidentGroupRespository GetIncidentGroupRespository()
        {
            return new SqlServer.PwaIncidentGroupResponsitory();
        }

        public static IPwaInformChannelRespository GetInformChannelRespository()
        {
            return new SqlServer.PwaInformChannelRespository();
        }

        public static ISysBranchRespository GetSysBranchRespository()
        {
            return new SqlServer.SysBranchResponsitory();
        }


        public static IPwaIncidentRespository GetPwaIncidentResponsitory()
        {
            return new SqlServer.PwaIncidentResponsitory();
        }

        public static ISysCustomerResponsitory GetSysCustomerResponsitory()
        {
            return new SqlServer.SysCustomerResponsitory();
        }
        public static ISysItemRespository GetSysItemRespository()
        {
            return new SqlServer.SysItemResponsitory();
        }

        public static IMeterRespository GetMeterResponsitory()
        {
            return new Postgres.MeterRespository();
        }


        public static ISysStatusResponsitory GetSysStatusResponsitory()
        {
            return new SqlServer.SysStatusResponsitory();
        }


        public static ISysUnitRespository GetSysUnitRespository()
        {
            return new SqlServer.SysUnitResponsitory();
        }
        public static ITrackingRespository GetTrackingRespository()
        {
            return new SqlServer.TrackingResponsitory();
        }
		public static IIC360MappingResponsitory GetIC360MappingResponsitory()
        {
            return new SqlServer.IC360MappingResponsitory();
        }
		public static ISysMessagesIncidentRespository GetMessagesIncidentRespository()
        {
            return new SqlServer.SysMessagesIncidentSqlRepository();
        }

        public static ISysHolidayResponsitory GetSysHolidayResponsitory()
        {
            return new SqlServer.SysHolidayResponsitory();
        }
    }
}
