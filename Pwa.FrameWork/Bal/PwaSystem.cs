using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Bal
{
    public class PwaSystem
    {
        public enum ProcessStage
        {
            UNKNOW = 0,
            CALLCENTER_RECEIVE_CASE = 1,
            BRANCH_RECEIVE_CASE_BY_SELF = 2,
            BRANCH_RECEIVE_CASE = 3,
            BRANCH_REJECT = 4,
            CHECKING = 5,
            OPEN_REPAIR_CASE = 6,
            REPAIR_COMPLETED = 7,
            WAITING_FOR_APPROVAL = 8,
            COMPLETED_CASE = 9,
            CANCEL_CASE = 10,
            CLOSE_CASE_FOR_INVALID_SOURCE_FROM_OTHER_SYS = 11


            /*

            1	Call center รับเรื่อง	 
            2	กปภ. สาขา รับเรื่อง กรณีเปิดเรื่องเอง
            3	กปภ. สาขา รับเรื่อง	 
            4	ส่งกลับส่วนกลาง	 
            5	ตรวจสอบ	 
            6	เปิดงานซ่อม	 
            7	ซ่อมเสร็จแล้ว	 
            8	รอหัวหน้าอนุมัติ	 
            9	ดำเนินการแล้วเสร็จ	 
            10	ยกเลิก	 

             */



        }

        public enum CustomerProcessStage
        {
            UNKNOW = 0,
            PROCESSING = 1,
            COMPLETED_CASE = 2,
            CANCEL_CASE = 3
        }

    }

   
    public class SystemCode
    {
        public static string Smart1662 { get { return "SM1662"; } }
        public static string Callcenter { get { return "IC360"; } }
    }
}
